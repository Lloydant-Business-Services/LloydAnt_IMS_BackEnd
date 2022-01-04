using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteHR.Dtos;
using LiteHR.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LiteHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffPostingController : ControllerBase
    {
        private readonly IStaffPostingService _sevice;
        public StaffPostingController(IStaffPostingService service)
        {
            _sevice = service;
        }
        [HttpGet("NewStaffCount")]
        public async Task<long> GetStaffCountByDepartmentAndRank(long departmentId, long rankId) => await _sevice.GetStaffCountByDepartmentAndRank(departmentId, rankId);

        [HttpGet("GetNewStaff")]
        public async Task<IEnumerable<GenerateStaffNumberListDto>> GetNewlyCreatedStaff(DateTime from, DateTime to) => await _sevice.GetNewlyCreatedStaff(from, to);

        [HttpPut("UpdateNewStaff")]
        public async Task<int> UpdateNewlyCreatedStaff(UpdateNewStaffDto updateNewStaffDto, long staffId) => await _sevice.UpdateNewlyCreatedStaff(updateNewStaffDto, staffId);

        [HttpGet("AllDepartmentRankCount")]
        public async Task<DeptRankCountDto> GetAllDepartmentStaffRankCount(long rankId, long departmentId) => await _sevice.GetAllDepartmentStaffRankCount(rankId, departmentId);

        [HttpGet("Whole")]
        public async Task<IEnumerable<NewStaffCountDto>> GetStaffRankCountWhole(long cadreId) => await _sevice.GetStaffRankCountWhole(cadreId);
    }
}
