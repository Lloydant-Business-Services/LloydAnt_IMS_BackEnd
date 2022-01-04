using LiteHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteHR.Dtos;

namespace LiteHR.Interface
{
    public interface ISalaryGradingSystem
    {
        Task<List<GradeBenefit>> AddGradeBenefit(List<long> salaryTypes, long salaryStepId, long salaryLevelId, long salaryGradeCategoryId);

        Task<List<GradeBenefit>> GetGradeBenfitsBySalaryGradeId(long salaryGradeId);
        Task<int> AddSalaryGrade(SalaryGradeDto gradeDto);

        Task<GradeBenefit> GetGradeBenefitById(long id);

        Task<SalaryType> GetSalaryTypeById(long id);

        Task<SalaryLevel> GetSalaryLevelById(long id);

        Task<SalaryGradeCategory> GetSalaryGradeCategoryId(long id);

        Task<SalaryStep> GetSalaryStepById(long id);

        Task<SalaryGrade> GetSalaryGradeByLevelStepAndCategory(long salaryGradeId, long salaryLevelId, long salaryGradeCategoryId);

        Task<List<GradeBenefit>> GetGradeBenefitsByStaffId(long staffId);

        Task<List<SalaryGradeModel>> GetGradeBenefitsBySalaryGradeCategoryId(long salaryGradeCategoryId);
        Task<List<GradeBenefit>> SetStaffGrade(long staffId, long salaryGradeId);
        Task<StaffGradeBenefitDto> GetStaffCurrentMonthPayCheckId(long staffId, int month, int year);
        Task<StaffSalaryDto> GetStaffPayCheque(long staffId);
        Task<IEnumerable<SalaryGradeDto>> GetSalaryGrade();
        Task<int> UpdateSalaryGrade(long gradeId, SalaryGradeDto gradeDto);
        Task<StaffSalaryReportDto> GetStaffMonthlySalary(int month, int year);
    }
}
