using LiteHR.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Interface
{
    public interface ILeaveRequestManagementService
    {
        Task<long> MakeLeaveRequest(AddStaffLeaveRequestDto addStaffLeaveRequest);
        Task<long> AddLeaveResponseChain(LeaveResponseChainDto leaveResponseChainDto);
        //Task<IEnumerable<AddStaffLeaveRequestDto>> GetStaffLeaveRequest(AddStaffLeaveRequestDto addStaffLeaveRequestDto);
        Task<IEnumerable<AddStaffLeaveRequestDto>> GetStaffLeaveRequest();
        //Task<IEnumerable<AddStaffLeaveRequestDto>> GetStaffLeaveRequestByHOD(long departmentId);
        Task<IEnumerable<LeaveResponseStructure>> GetLeaveResponse(long departmentId);
        //Task<LeaveResponseStructure> HeadOfDepartmentToAct(LeaveResponseStructure leaveResponseStructure);
        Task<IEnumerable<LeaveResponseStructure>> GetLeaveResponseByResponseId(long leaveResponseId);
        //Task<LeaveActionDto> HeadOfDepartmentToAct(LeaveActionDto leaveActionDto);
        //Task<LeaveActionDto> DeanToAct(LeaveActionDto leaveActionDto);
        //Task<LeaveActionDto> ViceChancellorToAct(LeaveActionDto leaveActionDto);
        //Task<IEnumerable<AddStaffLeaveRequestDto>> GetStaffLeaveRequestByDean(long departmentId);
        //Task<IEnumerable<AddStaffLeaveRequestDto>> GetStaffLeaveRequestByVC();
        Task<IEnumerable<StaffLeaveRecordDto>> StaffLeaveRequestHistory(long staffId);
        Task<IEnumerable<AddStaffLeaveRequestDto>> GetStaffLeaveRequestByRole(long departmentId, long roleId);
        Task<AppReturnObject> LeaveAction(LeaveActionDto leaveActionDto, long roleId);
        Task<AppReturnObject> DeclineLeaveRequest(LeaveActionDto leaveActionDto, long roleId);
        Task<IEnumerable<LeaveResponseChainDto>> GetLeaveResponseChain();
       Task<long> LeaveRequestCounter();
        Task<IEnumerable<ChainCommentsDto>> GetLeaveActionComments(long leaveRequestId);
        Task<long> LeaveDayCount(DateTime start, DateTime end, long staffId);
        Task<IEnumerable<AddStaffLeaveRequestDto>> GetStaffLeaveRecordHistoryByRole(long departmentId, long roleId);
        Task<AddStaffLeaveRequestDto> GetLeaveRequestByLeaveId(long leaveRequestId);
        Task<long> UpdateResponseChain(long chainId, LeaveResponseChainDto leaveChainDto);

        //long LeaveDayCount(DateTime start, DateTime end);

    }
}

