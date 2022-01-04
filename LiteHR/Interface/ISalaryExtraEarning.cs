using LiteHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Interface
{
    public interface ISalaryExtraEarning
    {
        Task<SalaryExtraEarning> AddSalaryExtraEarning(SalaryExtraEarning salaryExtraEarning);

        Task<List<SalaryExtraEarning>> GetSalaryExtraEarnings();

        Task<SalaryExtraEarning> GetSalaryExtraEarningById(long Id);

        Task<List<SalaryExtraEarning>> GetSalaryExtraEarningByStaffId(long staffId);

        Task<SalaryExtraEarning> UpdateSalaryExtraEarning(SalaryExtraEarning salaryExtraEarning);

        Task<SalaryExtraEarning> DeleteSalaryExtraEarning(int Id);

        Task<List<SalaryExtraEarning>> GetSalaryExtraEarningByStaffIdMonthAndYear(long staffId, int month, int year);
    }
}
