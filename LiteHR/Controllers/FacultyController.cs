using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteHR.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LiteHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultyController : ControllerBase
    {
        private readonly HRContext _context;
        public FacultyController(HRContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Faculty>>> GET_FACULTY()
        {
            return await _context.FACULTY.Where(f => f.Id != 1).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Faculty>> GetFaculty(long id)
        {
            var faculty = await _context.FACULTY.FindAsync(id);

            if (faculty == null)
            {
                return NotFound();
            }

            return faculty;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<int>> PutFaculty(long id, Faculty faculty)
        {
            if (id != faculty.Id)
            {
                return BadRequest();
            }

            _context.FACULTY.Update(faculty);
            await _context.SaveChangesAsync();
            return StatusCodes.Status200OK;
        }

        [HttpPost]
        public async Task<ActionResult<int>> PostFaculty(Faculty faculty)
        {
            _context.FACULTY.Add(faculty);
            await _context.SaveChangesAsync();

            return StatusCodes.Status200OK;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteFaculty(long id)
        {
            var faculty = await _context.FACULTY.FindAsync(id);
            if (faculty == null)
            {
                return NotFound();
            }

            _context.FACULTY.Remove(faculty);
            await _context.SaveChangesAsync();

            return StatusCodes.Status200OK;
        }

    }
}
