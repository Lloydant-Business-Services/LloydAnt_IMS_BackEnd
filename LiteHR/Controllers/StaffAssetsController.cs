using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LiteHR.Models;
using Microsoft.AspNetCore.Authorization;
using LiteHR.Dtos;

namespace LiteHR.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StaffAssetsController : ControllerBase
    {
        private readonly HRContext _context;

        public StaffAssetsController(HRContext context)
        {
            _context = context;
        }

        // GET: api/StaffAssets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StaffAssets>>> GetStaffAssets()
        {
            return await _context.STAFF_ASSET.Include(a => a.Staff).ThenInclude(x => x.Person)
                .Include(a => a.Asset).ThenInclude(x => x.AssetType).ToListAsync();
        }

        // GET: api/StaffAssets/5
        [HttpGet("{id}")]
        public async Task<IEnumerable<StaffAssetDto>> GetStaffAssets(long id)
        {
            var staffAssets = await _context.STAFF_ASSET.Where(x => x.StaffId == id)
                .Include(d => d.Asset)
                .Include(t => t.Asset.AssetType)
                .Select(a => new StaffAssetDto
                {
                    AssetName = a.Asset.Name,
                    AssetTypeName = a.Asset.AssetType.Name,
                    SerialNumber = a.SerialNumber
                }).ToListAsync();

            if (staffAssets == null)
            {
                return null;
            }

            return staffAssets;
        }

        // PUT: api/StaffAssets/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStaffAssets(long id, StaffAssets staffAssets)
        {
            if (id != staffAssets.Id)
            {
                return BadRequest();
            }

            _context.Entry(staffAssets).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffAssetsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/StaffAssets
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<int>> PostStaffAssets(StaffAssetDto staffAssetDto)
        {
            var verifyStaffId = await _context.STAFF.Where(s => s.Id == staffAssetDto.StaffId).FirstOrDefaultAsync();
            if (verifyStaffId == null)
                throw new NullReferenceException();
            StaffAssets staffAssets = new StaffAssets()
            {
                StaffId = staffAssetDto.StaffId,
                AssetId = staffAssetDto.AssetId,
                SerialNumber = staffAssetDto.SerialNumber,
                AssetNumber = Guid.NewGuid().ToString()
        };
            _context.STAFF_ASSET.Add(staffAssets);
            await _context.SaveChangesAsync();

            return StatusCodes.Status200OK;
        }

        // DELETE: api/StaffAssets/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<StaffAssets>> DeleteStaffAssets(long id)
        {
            var staffAssets = await _context.STAFF_ASSET.Include(a => a.Staff).ThenInclude(x => x.Person)
                .Include(a => a.Asset).ThenInclude(x => x.AssetType).Where(x => x.Id == id).FirstOrDefaultAsync();
            if (staffAssets == null)
            {
                return NotFound();
            }

            _context.STAFF_ASSET.Remove(staffAssets);
            await _context.SaveChangesAsync();

            return staffAssets;
        }

        private bool StaffAssetsExists(long id)
        {
            return _context.STAFF_ASSET.Any(e => e.Id == id);
        }
    }
}
