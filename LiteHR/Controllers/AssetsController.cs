using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LiteHR.Models;
using Microsoft.AspNetCore.Authorization;

namespace LiteHR.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly HRContext _context;

        public AssetsController(HRContext context)
        {
            _context = context;
        }

        // GET: api/Assets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asset>>> GetASSET()
        {
            return await _context.ASSET.Include(a => a.AssetType).ToListAsync();
        }

        // GET: api/Assets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Asset>> GetAsset(long id)
        {
            var asset = await _context.ASSET.Include(a => a.AssetType).Where(a => a.Id ==  id).FirstOrDefaultAsync();

            if (asset == null)
            {
                return NotFound();
            }

            return asset;
        }

        // PUT: api/Assets/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> PutAsset(long id, Asset asset)
        {
            if (id != asset.Id)
            {
                return BadRequest();
            }

            _context.ASSET.Update(asset);
            await _context.SaveChangesAsync();
            return StatusCodes.Status200OK;
        }

        // POST: api/Assets
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<int>> PostAsset(Asset asset)
        {
            _context.ASSET.Add(asset);
            await _context.SaveChangesAsync();

            return StatusCodes.Status200OK;
        }

        // DELETE: api/Assets/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteAsset(long id)
        {
            var asset = await _context.ASSET.FindAsync(id);
            if (asset == null)
            {
                return NotFound();
            }

            _context.ASSET.Remove(asset);
            await _context.SaveChangesAsync();

            return StatusCodes.Status200OK;
        }

        private bool AssetExists(long id)
        {
            return _context.ASSET.Any(e => e.Id == id);
        }
    }
}
