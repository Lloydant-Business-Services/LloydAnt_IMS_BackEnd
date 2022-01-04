using LiteHR.Interface;
using LiteHR.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Services
{
    public class SalaryExtraEarningService: ISalaryExtraEarning
    {
        private readonly HRContext _context;


        public SalaryExtraEarningService(HRContext context)
        {
            _context = context;
        }

        public async Task<SalaryExtraEarning> AddSalaryExtraEarning(SalaryExtraEarning salaryExtraEarning)
        {
            try
            {
                salaryExtraEarning.Active = true;
                if (salaryExtraEarning.Amount > 0.0m && salaryExtraEarning.SalaryExtraTypeId > 0 && salaryExtraEarning.StaffId > 0)
                {

                        salaryExtraEarning.Staff = await _context.STAFF.Where(d => d.Id == salaryExtraEarning.StaffId).FirstOrDefaultAsync();
                        salaryExtraEarning.SalaryExtraType = await _context.SALARY_EXTRA_TYPE.Where(d => d.Id == salaryExtraEarning.SalaryExtraTypeId).FirstOrDefaultAsync();

                    _context.SALARY_EXTRA_EARNING.Add(salaryExtraEarning);
                    await _context.SaveChangesAsync();


                }
                return salaryExtraEarning;
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<List<SalaryExtraEarning>> GetSalaryExtraEarnings()
        {
            try
            {
                List<SalaryExtraEarning> salaryExtraEarnings = await _context.SALARY_EXTRA_EARNING.Where(s => s.Active).ToListAsync();
                return salaryExtraEarnings;
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<SalaryExtraEarning> GetSalaryExtraEarningById(long Id)
        {
            try
            {
                return await _context.SALARY_EXTRA_EARNING.Where(s => s.Id == Id).FirstOrDefaultAsync();
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<List<SalaryExtraEarning>> GetSalaryExtraEarningByStaffId(long staffId)
        {
            try
            {
                return await _context.SALARY_EXTRA_EARNING.Where(s => s.StaffId == staffId).ToListAsync();
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<SalaryExtraEarning> UpdateSalaryExtraEarning(SalaryExtraEarning salaryExtraEarning)
        {
            try
            {
                if (salaryExtraEarning.Id > 0)
                {
                    _context.Entry(salaryExtraEarning).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }

                return salaryExtraEarning;
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<SalaryExtraEarning> DeleteSalaryExtraEarning(int Id)
        {
            try
            {
                SalaryExtraEarning salaryExtraEarning = await _context.SALARY_EXTRA_EARNING.Where(s => s.Id == Id).FirstOrDefaultAsync();

                if (salaryExtraEarning != null)
                {
                    _context.SALARY_EXTRA_EARNING.Remove(salaryExtraEarning);
                    await _context.SaveChangesAsync();
                }

                return salaryExtraEarning;
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<List<SalaryExtraEarning>> GetSalaryExtraEarningByStaffIdMonthAndYear(long staffId, int month, int year)
        {
            try
            {
                return await _context.SALARY_EXTRA_EARNING.Where(s => s.StaffId == staffId && s.DateCreated.Month == month && s.DateCreated.Year == year).ToListAsync();
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
