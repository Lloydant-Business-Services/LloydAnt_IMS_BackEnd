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
    public class LeaveTypeController : ControllerBase
    {
        public readonly ILeaveTypeService _service;
        private readonly HRContext _context;

        public LeaveTypeController(ILeaveTypeService service, HRContext context)
        {
            _service = service;
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<LeaveTypeDto>> GetAllLeaveType()=> await _service.GetLeaveType();
        [HttpGet("LeaveTypeRankByRank")]
        public async Task<IEnumerable<LeaveTypeRankDto>> GetLeaveTypeRankByRank(long RankId) => await _service.GetLeaveTypeRankBy(RankId);
        [HttpPost("CreateLeaveType")]
        public async Task<long> CreateLeaveType(string LeaveTypeName) => await _service.AddLeaveType(LeaveTypeName);
        [HttpPost("CreateLeaveTypeRank")]
        public async Task<long> CreateLeaveTypeRank(AddLeaveTypeRankDto addLeaveTypeRankDto) => await _service.AddLeaveTypeRank(addLeaveTypeRankDto);

        [HttpPut("{id}")]
        public async Task<ActionResult<int>> PutLeave(long id, LeaveType leaveType)
        {
            if (id != leaveType.Id)
            {
                return BadRequest();
            }

            _context.LEAVE_TYPE.Update(leaveType);
            await _context.SaveChangesAsync();
            return StatusCodes.Status200OK;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteLeave(long id)
        {
            var leaveType = await _context.LEAVE_TYPE.FindAsync(id);
            if (leaveType == null)
            {
                return NotFound();
            }

            _context.LEAVE_TYPE.Remove(leaveType);
            await _context.SaveChangesAsync();

            return StatusCodes.Status200OK;
        }

    }
}