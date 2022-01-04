using LiteHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Interface
{
    public interface ILeaveRequestService
    {
        Task<StaffLeaveRequest> GetLeaveRequestById(long Id);
        Task<bool> DeleteLeaveRequest(long Id);
        Task<List<StaffLeaveRequest>> GetLeaveRequests();
        Task<StaffLeaveRequest> GetLeaveRequestByStaff(Staff staff);
        Task<List<StaffLeaveRequest>> GetLeaveRequests(Staff staff);
        Task<StaffLeaveRequest> MakeRequest (StaffLeaveRequest leaveRequest);
        Task<bool> UpdateRequest(long Id, StaffLeaveRequest leaveRequest);
        Task ApproveAsSupervisor(StaffLeaveRequest leaveRequest);
        Task ApproveAsHR(StaffLeaveRequest leaveRequest);
        
    }
}
