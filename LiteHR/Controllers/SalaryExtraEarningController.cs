using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteHR.Interface;
using LiteHR.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LiteHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryExtraEarningController : Controller
    {

        public readonly ISalaryExtraEarning _service;

        public SalaryExtraEarningController(ISalaryExtraEarning service)
        {
            _service = service;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<SalaryExtraEarning>> AddSalaryExtraEarning(SalaryExtraEarning salaryExtraEarning)
        {
            try
            {
                if (salaryExtraEarning == null)
                {
                    return BadRequest(new { Message = "Salary extra cannot be null" });
                }
                if (salaryExtraEarning.SalaryExtraTypeId < 1)
                {
                    return BadRequest(new { Message = "Salary Extra is not defined" });
                }
                if (salaryExtraEarning.Amount < 0.1m)
                {
                    return BadRequest(new { Message = "Salary Amount are not defined" });
                }

                return await _service.AddSalaryExtraEarning(salaryExtraEarning);
            }
            catch (Exception ex) { throw ex; }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<SalaryExtraEarning>>> GetAllSalaryExtraEarnings()
        {
            try
            {
                return await _service.GetSalaryExtraEarnings();
            }
            catch (Exception ex) { throw ex; }
        }

        [HttpGet("[action]/{Id}")]
        public async Task<ActionResult<SalaryExtraEarning>> GetSalaryExtraEarningById(long Id)
        {
            try
            {
                return await _service.GetSalaryExtraEarningById(Id);
            }
            catch (Exception ex) { throw ex; }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<SalaryExtraEarning>>> GetSalaryExtraEarningByStaffId(long staffId)
        {
            try
            {
                return await _service.GetSalaryExtraEarningByStaffId(staffId);
            }
            catch (Exception ex) { throw ex; }
        }

        [HttpDelete("[action]/{Id}")]
        public async Task<ActionResult<SalaryExtraEarning>> DeleteSalaryExtraEarning(int Id)
        {
            try
            {
                return await _service.DeleteSalaryExtraEarning(Id);
            }
            catch (Exception ex) { throw ex; }
        }

        [HttpPut("[action]/{Id}")]
        public async Task<ActionResult<SalaryExtraEarning>> UpdateSalaryExtraEarning(long Id, SalaryExtraEarning salaryExtraEarning)
        {
            try
            {
                return await _service.UpdateSalaryExtraEarning(salaryExtraEarning);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _service.GetSalaryExtraEarningById(Id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex) { throw ex; }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<SalaryExtraEarning>>> GetSalaryExtraEarningByStaffIdMonthAndYear(long staffId, int month, int year)
        {
            try
            {
                return await _service.GetSalaryExtraEarningByStaffIdMonthAndYear(staffId, month, year);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}