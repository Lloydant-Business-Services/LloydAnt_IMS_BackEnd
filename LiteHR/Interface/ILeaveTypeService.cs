using LiteHR.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Interface
{
    public interface ILeaveTypeService
    {
        Task<long> AddLeaveType(string LeaveName);
        Task<IEnumerable<LeaveTypeDto>> GetLeaveType();
        Task<long> AddLeaveTypeRank(AddLeaveTypeRankDto addLeaveTypeRankDto);
        Task<IEnumerable<LeaveTypeRankDto>> GetLeaveTypeRankBy(long RankId);
    }
}
