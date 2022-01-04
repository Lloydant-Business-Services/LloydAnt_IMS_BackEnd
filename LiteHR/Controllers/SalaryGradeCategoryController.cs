using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteHR.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LiteHR.Services;
using Microsoft.AspNetCore.Http;

namespace LiteHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryGradeCategoryController : ControllerBase
    {
        private readonly HRContext _context;

        public SalaryGradeCategoryController(HRContext context)
        {
            _context = context;

        }

        // GET: api/SalaryGradeCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalaryGradeCategory>>> GetSALARY_CATEGORIES()
        {
            return await _context.SALARY_GRADE_CATEGORY.ToListAsync();
        }

        // GET: api/SalaryGradeCategory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SalaryGradeCategory>> GetSalaryCategory(long id)
        {
            var salaryCategory = await _context.SALARY_GRADE_CATEGORY.FindAsync(id);

            if (salaryCategory == null)
            {
                return NotFound();
            }

            return salaryCategory;
        }


        // POST: api/SalaryGradeCategory
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<int>> PostSalaryGradeCategory(SalaryGradeCategory salaryGradeCategory)
        {
            _context.SALARY_GRADE_CATEGORY.Add(salaryGradeCategory);
            await _context.SaveChangesAsync();

            return StatusCodes.Status200OK;
        }

        // DELETE: api/SalaryGradeCategory/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteSalaryGradeCategory(long id)
        {
            var salaryGradeCategory = await _context.SALARY_GRADE_CATEGORY.FindAsync(id);
            if (salaryGradeCategory == null)
            {
                return NotFound();
            }

            _context.SALARY_GRADE_CATEGORY.Remove(salaryGradeCategory);
            await _context.SaveChangesAsync();

            return StatusCodes.Status200OK;
        }

        // PUT: api/SalaryGradeCategory/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> PutSalaryGradeCategory(long id, SalaryGradeCategory salaryGradeCategory)
        {
            if (id != salaryGradeCategory.Id)
            {
                return BadRequest();
            }

            _context.SALARY_GRADE_CATEGORY.Update(salaryGradeCategory);
            await _context.SaveChangesAsync();
            return StatusCodes.Status200OK;
        }

        private bool SalaryGradeCategoryExists(long id)
        {
            return _context.SALARY_GRADE_CATEGORY.Any(e => e.Id == id);
        }

    }
}