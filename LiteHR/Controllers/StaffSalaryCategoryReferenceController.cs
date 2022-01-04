using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteHR.Dtos;
using LiteHR.Interface;
using LiteHR.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LiteHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffSalaryCategoryReferenceController : ControllerBase
    {
        private readonly HRContext _context;


        public StaffSalaryCategoryReferenceController(HRContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StaffSalaryCatRefDto>>> GetSALARY_REF_CATEGORY()
        {

            var salaryRef =  await _context.STAFF_SALARY_CATEGORY_REFERENCE
                .Select(sg => new StaffSalaryCatRefDto
                {
                    StaffId = sg.StaffId,
                    SalaryCategoryId = sg.SalaryGradeCategoryId,
                    SalaryLevelId = sg.SalaryLevelId,
                    SalaryStepId = sg.SalaryStepId
                })
                .ToListAsync();
            return salaryRef;
        }

        [HttpGet("ByStaffId")]
        public async Task<ActionResult<StaffSalaryCatRefDto>> GetSalaryRefCategory(long staffId)
        {
            var salaryRef = await _context.STAFF_SALARY_CATEGORY_REFERENCE.Where(s => s.StaffId == staffId)
                .Include(sg => sg.SalaryGradeCategory)
                .Include(sl => sl.SalaryLevel)
                .Include(ss => ss.SalaryStep)
               .Select(sg => new StaffSalaryCatRefDto
               {
                   StaffId = sg.StaffId,
                   SalaryCategoryId = sg.SalaryGradeCategoryId,
                   SalaryLevelId = sg.SalaryLevelId,
                   SalaryStepId = sg.SalaryStepId,
                   SalaryCategory = sg.SalaryGradeCategory.Name,
                   SalaryLevel = sg.SalaryLevel.Name,
                   SalaryStep = sg.SalaryStep.Name
               })
               .FirstOrDefaultAsync();
            return salaryRef;

        }

        
        [HttpPost]
        public async Task<ActionResult<int>> PostSalaryRefCategory(StaffSalaryCatRefDto staffSalaryCatRefDto, long staffId)
        {
            StaffSalaryCategoryReference staffSalaryReferenceUpd = await _context.STAFF_SALARY_CATEGORY_REFERENCE.Where(s => s.StaffId == staffId).FirstOrDefaultAsync();
            if(staffSalaryReferenceUpd != null)
            {
              
                staffSalaryReferenceUpd.StaffId = staffSalaryCatRefDto.StaffId;
                staffSalaryReferenceUpd.SalaryGradeCategoryId = staffSalaryCatRefDto.SalaryCategoryId;
                staffSalaryReferenceUpd.SalaryLevelId = staffSalaryCatRefDto.SalaryLevelId;
                staffSalaryReferenceUpd.SalaryStepId = staffSalaryCatRefDto.SalaryStepId;
                staffSalaryReferenceUpd.DatePromoted = DateTime.Now;

                _context.Update(staffSalaryReferenceUpd);
                await _context.SaveChangesAsync();

                return StatusCodes.Status200OK;
            }
            else
            {
                StaffSalaryCategoryReference staffSalaryReference = new StaffSalaryCategoryReference();
                staffSalaryReference.StaffId = staffSalaryCatRefDto.StaffId;
                staffSalaryReference.SalaryGradeCategoryId = staffSalaryCatRefDto.SalaryCategoryId;
                staffSalaryReference.SalaryLevelId = staffSalaryCatRefDto.SalaryLevelId;
                staffSalaryReference.SalaryStepId = staffSalaryCatRefDto.SalaryStepId;
                staffSalaryReference.DatePromoted = DateTime.Now;

                _context.Add(staffSalaryReference);
                await _context.SaveChangesAsync();

                return StatusCodes.Status200OK;
            }

       
            

        }

        [HttpPut]
        public async Task<ActionResult<int>> ModifySalaryRefCategory(long staffId)
        {
            StaffSalaryCatRefDto staffSalaryCatRefDto = new StaffSalaryCatRefDto();
            StaffSalaryCategoryReference staffSalaryReference = await _context.STAFF_SALARY_CATEGORY_REFERENCE.Where(s => s.StaffId == staffId).FirstOrDefaultAsync();

            staffSalaryReference.StaffId = staffSalaryCatRefDto.StaffId;
            staffSalaryReference.SalaryGradeCategoryId = staffSalaryCatRefDto.SalaryCategoryId;
            staffSalaryReference.SalaryLevelId = staffSalaryCatRefDto.SalaryLevelId;
            staffSalaryReference.SalaryStepId = staffSalaryCatRefDto.SalaryStepId;
            staffSalaryReference.DatePromoted = DateTime.Now;

            _context.Add(staffSalaryReference);
            await _context.SaveChangesAsync();

            return StatusCodes.Status200OK;
        }
    }
}
