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
using System.Globalization;

namespace LiteHR.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InstitutionRanksController : ControllerBase
    {
        private readonly HRContext _context;

        public InstitutionRanksController(HRContext context)
        {
            _context = context;
        }

        // GET: api/InstitutionRanks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddInstittutionRankDto>>> GetINSTITUTION_RANK()
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            return await _context.INSTITUTION_RANK
                .Where(a => a.Active)
                .OrderBy(r => r.Name.Trim())
                .Select(f => new AddInstittutionRankDto
                {
                    Name = f.Name,
                    InstistutionUnitName = textInfo.ToTitleCase(f.InstitutionUnit.Name),
                    Id = f.Id,
                    InstitutionUnitId = f.InstitutionUnitId
                    

                })
                
                .ToListAsync();
        }

        // GET: api/InstitutionRanks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InstitutionRank>> GetInstitutionRank(long id)
        {
            var institutionRank = await _context.INSTITUTION_RANK.FindAsync(id);

            if (institutionRank == null)
            {
                return NotFound();
            }

            return institutionRank;
        }
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<InstitutionRank>>> GetInstitutionRankBy(long unitId)
        {
            var institutionRank = await _context.INSTITUTION_RANK.Where(f=>f.Active && f.InstitutionUnitId==unitId)
                .OrderBy(f=> f.Name)
                .ToListAsync();

            if (institutionRank == null)
            {
                return NotFound();
            }

            return institutionRank;
        }

        // PUT: api/InstitutionRanks/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<ActionResult<InstitutionRank>> PutInstitutionRank(long id, InstitutionRank institutionRank)
        {
            if (id != institutionRank.Id)
            {
                return BadRequest();
            }

            _context.INSTITUTION_RANK.Update(institutionRank);
            await _context.SaveChangesAsync();
            return institutionRank;
        }

        // POST: api/InstitutionRanks
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<InstitutionRank>> PostInstitutionRank(AddInstittutionRankDto addInstittutionRankDto)
        {
            InstitutionRank institutionRank = new InstitutionRank()
            {
                InstitutionUnitId = addInstittutionRankDto.InstitutionUnitId,
                Active = true,
                GradeLevel = addInstittutionRankDto.GradeLevel,
                Name = addInstittutionRankDto.Name,
            };

            _context.INSTITUTION_RANK.Add(institutionRank);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInstitutionRank", new { id = institutionRank.Id }, institutionRank);
        }

        // DELETE: api/InstitutionRanks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteInstitutionRank(long id)
        {
            var institutionRank = await _context.INSTITUTION_RANK.FindAsync(id);
            if (institutionRank == null)
            {
                return NotFound();
            }

            _context.INSTITUTION_RANK.Remove(institutionRank);
            await _context.SaveChangesAsync();

            return StatusCodes.Status200OK;
        }

        private bool InstitutionRankExists(long id)
        {
            return _context.INSTITUTION_RANK.Any(e => e.Id == id);
        }
    }
}
