using LiteHR.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Interface
{
    public interface ILeaveManagement
    {

        Task<long> AddLeaveType(string LeaveName);
        Task<IEnumerable<LeaveTypeDto>> GetLeaveType();
        Task<long> AddLeaveTypeRank(AddLeaveTypeRankDto addLeaveTypeRankDto);
        Task<IEnumerable<LeaveTypeRankDto>> GetLeaveTypeRankBy(long RankId);
        Task<long> MakeLeaveRequest(AddStaffLeaveRequestDto addStaffLeaveRequest);
        Task<long> AddLeaveResponseChain(LeaveResponseChainDto leaveResponseChainDto);
        //Task<IEnumerable<AddStaffLeaveRequestDto>> GetStaffLeaveRequest(AddStaffLeaveRequestDto addStaffLeaveRequestDto);
        Task<IEnumerable<AddStaffLeaveRequestDto>> GetStaffLeaveRequest();
        Task<IEnumerable<AddStaffLeaveRequestDto>> GetStaffLeaveRequestByDepartment(long departmentId);

    }
}
