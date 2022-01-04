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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingTypesController : ControllerBase
    {
        private readonly HRContext _context;

        public TrainingTypesController(HRContext context)
        {
            _context = context;
        }

        // GET: api/TrainingTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainingType>>> GetTRAINING_TYPE()
        {
            return await _context.TRAINING_TYPE.ToListAsync();
        }

        // GET: api/TrainingTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrainingType>> GetTrainingType(long id)
        {
            var trainingType = await _context.TRAINING_TYPE.FindAsync(id);

            if (trainingType == null)
            {
                return NotFound();
            }

            return trainingType;
        }

        // PUT: api/TrainingTypes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainingType(long id, TrainingType trainingType)
        {
            if (id != trainingType.Id)
            {
                return BadRequest();
            }

            _context.Entry(trainingType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainingTypeExists(id))
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

        // POST: api/TrainingTypes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<TrainingType>> PostTrainingType(TrainingType trainingType)
        {
            _context.TRAINING_TYPE.Add(trainingType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrainingType", new { id = trainingType.Id }, trainingType);
        }

        // DELETE: api/TrainingTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TrainingType>> DeleteTrainingType(long id)
        {
            var trainingType = await _context.TRAINING_TYPE.FindAsync(id);
            if (trainingType == null)
            {
                return NotFound();
            }

            _context.TRAINING_TYPE.Remove(trainingType);
            await _context.SaveChangesAsync();

            return trainingType;
        }

        private bool TrainingTypeExists(long id)
        {
            return _context.TRAINING_TYPE.Any(e => e.Id == id);
        }
    }
}
