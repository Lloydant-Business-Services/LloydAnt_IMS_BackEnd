using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LiteHR.Services;
using LiteHR.Models;
using Microsoft.AspNetCore.Authorization;
using LiteHR.Interface;
using LiteHR.Dtos;

namespace LiteHR.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryGradingSystemController : ControllerBase
    {
        private readonly ISalaryGradingSystem _salaryGradingSystemService;

        public SalaryGradingSystemController(ISalaryGradingSystem salaryGradingSystemService)
        {
            _salaryGradingSystemService = salaryGradingSystemService;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<IEnumerable<GradeBenefit>>> AddGradeBenefit(SalaryGradeBenefitModel salaryGradeBenefitModel)
        {
            try
            {
                return await _salaryGradingSystemService.AddGradeBenefit(salaryGradeBenefitModel.SalaryTypeModels, salaryGradeBenefitModel.SalaryStepId, salaryGradeBenefitModel.SalaryLevelId, salaryGradeBenefitModel.SalaryCategoryId);
            }
            catch (Exception ex) { throw ex; }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<SalaryGradeModel>>> GetGradeBenefitsBySalaryGradeCategoryId(long salaryGradeCategoryId)
        {
            try
            {
                return await _salaryGradingSystemService.GetGradeBenefitsBySalaryGradeCategoryId(salaryGradeCategoryId);
            }
            catch (Exception ex) { throw ex; }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<GradeBenefit>>> GetGradeBenefitsByStaffId(long staffId)
        {
            try
            {
                return await _salaryGradingSystemService.GetGradeBenefitsByStaffId(staffId);
            }
            catch (Exception ex) { throw ex; }
        }
        [HttpPost("AddStaffGrade")]
        public async Task<ActionResult<IEnumerable<GradeBenefit>>> AddStaffGrade(long staffId, long salaryGradeId)
        {
            try
            {
                return await _salaryGradingSystemService.SetStaffGrade(staffId, salaryGradeId);
            }
            catch (Exception ex) { throw ex; }
        }
        [HttpGet("[action]")]
        public async Task<ActionResult<StaffGradeBenefitDto>> GetStaffCurreentPaycheck(long staffId, int month, int year)
        {
            try
            {
                return await _salaryGradingSystemService.GetStaffCurrentMonthPayCheckId(staffId,month,year);

            }
            catch (Exception ex) { throw ex; }



        }
        [HttpGet("[action]")]
        public async Task<StaffSalaryDto> GetStaffPayCheque(long staffId) => await _salaryGradingSystemService.GetStaffPayCheque(staffId);
        [HttpPost("[action]")]
        public async Task<int> AddSalaryGrade(SalaryGradeDto gradeDto) => await _salaryGradingSystemService.AddSalaryGrade(gradeDto);
        [HttpGet("[action]")]
        public async Task<IEnumerable<SalaryGradeDto>> GetSalaryGrade() => await _salaryGradingSystemService.GetSalaryGrade();
        [HttpPut("[action]")]
        public async Task<int> UpdateSalaryGrade(long gradeId, SalaryGradeDto gradeDto) => await _salaryGradingSystemService.UpdateSalaryGrade(gradeId, gradeDto);

        [HttpGet("[action]")]
        public async Task<StaffSalaryReportDto> GetStaffMonthlySalary(int month, int year) => await _salaryGradingSystemService.GetStaffMonthlySalary(month, year);

    }
}