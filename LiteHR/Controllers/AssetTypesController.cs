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
    public class AssetTypesController : ControllerBase
    {
        private readonly HRContext _context;

        public AssetTypesController(HRContext context)
        {
            _context = context;
        }

        // GET: api/AssetTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssetType>>> GetASSET_TYPE()
        {
            return await _context.ASSET_TYPE.ToListAsync();
        }

        // GET: api/AssetTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AssetType>> GetAssetType(long id)
        {
            var assetType = await _context.ASSET_TYPE.FindAsync(id);

            if (assetType == null)
            {
                return NotFound();
            }

            return assetType;
        }

        // PUT: api/AssetTypes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> PutAssetType(long id, AssetType assetType)
        {
            if (id != assetType.Id)
            {
                return BadRequest();
            }

            _context.ASSET_TYPE.Update(assetType);
            await _context.SaveChangesAsync();
            return StatusCodes.Status200OK;
        }

        // POST: api/AssetTypes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<int>> PostAssetType(AssetType assetType)
        {
            _context.ASSET_TYPE.Add(assetType);
            await _context.SaveChangesAsync();

            return StatusCodes.Status200OK;
        }

        // DELETE: api/AssetTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteAssetType(long id)
        {
            var assetType = await _context.ASSET_TYPE.FindAsync(id);
            if (assetType == null)
            {
                return NotFound();
            }

            _context.ASSET_TYPE.Remove(assetType);
            await _context.SaveChangesAsync();

            return StatusCodes.Status200OK;
        }

        private bool AssetTypeExists(long id)
        {
            return _context.ASSET_TYPE.Any(e => e.Id == id);
        }
    }
}
