using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteHR.Dtos;
using LiteHR.Interface;
using LiteHR.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LiteHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffNominalRollController : ControllerBase
    {
        public readonly IStaffNominalRollService _service;
        private readonly HRContext _context;

    
        public StaffNominalRollController(IStaffNominalRollService service, HRContext context)
        {
            _service = service;
            _context = context;

        }

        [HttpPost("PostStaffNominalList")]
        public async Task<int> StaffConfirmationForNominalRoll(IEnumerable<NominalRollDto> nominalRollDtos) => await _service.StaffConfirmationForNominalRoll(nominalRollDtos);

        [HttpGet("GetNomnalListByFacultyOfficer")]
        public async Task<IEnumerable<StaffNominalRollDto>> StaffNominalRollByFaculty(long month, long year) => await _service.StaffNominalRollByFaculty(month, year);

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteNominal(long id)
        {
            var nominalRoll = await _context.STAFF_NOMINAL_ROLL.FindAsync(id);
            if (nominalRoll == null)
            {
                return NotFound();
            }

            _context.STAFF_NOMINAL_ROLL.Remove(nominalRoll);
            await _context.SaveChangesAsync();

            return StatusCodes.Status200OK;
        }

        [HttpGet("GetNominalRollBy")]
        public async Task<IEnumerable<StaffNominalRollDto>> GetNominalRollBy(long facultyId, long departmentId) => await _service.GetNominalRollBy(facultyId, departmentId);


        [HttpGet("[action]")]
        public async Task<IEnumerable<StaffNominalRollDto>> PullStaffForMonthlyNominalRoll(long departmentId, long month, long year) => await _service.PullStaffForMonthlyNominalRoll(departmentId, month, year);

        [HttpPost("[action]")]
        public async Task<long> PostNominalRollComment(long staffId, string comment) => await _service.PostNominalRollComment(staffId, comment);
    }

}
