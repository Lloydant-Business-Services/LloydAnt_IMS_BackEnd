using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LiteHR.Models;
using LiteHR.Interface;

namespace LiteHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffRetirementController : ControllerBase
    {
        private readonly IStaffRetirement _service;

        public StaffRetirementController(IStaffRetirement service)
        {
            _service = service;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAcademicStaffDueForRetirement()
        {
            try
            {
                await _service.GetAcademicStaffDueForRetirement();
                return Ok();
            }
            catch(Exception ex) { throw ex; }
        } 

        [HttpPatch("[action]")]
        public async Task<ActionResult<StaffRetirement>> RetireStaffDueRetirement(long staffId)
        {
            try
            {
                StaffRetirement staffRetirement = await _service.RetireStaffDueRetirement(staffId);
                if (staffRetirement == null)
                {
                    return NotFound();
                }
                return staffRetirement;
            }
            catch(Exception ex) { throw ex; }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<bool>> StaffRetirementOneYearCheck(long staffId)
        {
            try
            {
                return await _service.StaffRetirementOneYearCheck(staffId);
            }
            catch(Exception ex) { throw ex; }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<bool>> StaffRetirementThreeMonthCheck(long staffId)
        {
            try
            {
                return await _service.StaffRetirementThreeMonthCheck(staffId);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}