//using LiteHR.Interface;
//using LiteHR.Models;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace LiteHR.Services
//{
//    public class LeaveRequestService : ILeaveRequestService
//    {
//        public LeaveRequestService(HRContext context)
//        {
//            _context = context;
//        }

//        public HRContext _context { get; }

//        public async Task ApproveAsHR(StaffLeaveRequest leaveRequest)
//        {
//            var request = await GetLeaveRequestById(leaveRequest.Id);
//            if (request != null)
//            {
//                request.Approved = true;
//                request.ApprovedDate = DateTime.Now;
//                request.ApprovedById = leaveRequest.Staff.Id;
//                await _context.SaveChangesAsync();
//            }
//        }

//        public async Task ApproveAsSupervisor(StaffLeaveRequest leaveRequest)
//        {
//            var request = await GetLeaveRequestById(leaveRequest.Id);
//            if (request != null)
//            {
//                request.Approved = true;
//                request.ApprovedDate = DateTime.Now;
//                request.ApprovedById = leaveRequest.Staff.Id;
//                await _context.SaveChangesAsync();
//            }
//        }

//        public async Task<bool> DeleteLeaveRequest(long Id)
//        {
//            int status = 0;
//            var request = await GetLeaveRequestById(Id);
//            if (request != null && !request.Approved)
//            {
//                _context.Remove(request);
//                status = await _context.SaveChangesAsync();
//            }

//            return (status > 0) ? true : false;
//        }

//        public async Task<StaffLeaveRequest> GetLeaveRequestById(long Id)
//        {
//            return await _context.STAFF_LEAVE_REQUEST.Include(x => x.Leave).Include(x => x.Staff).ThenInclude(x => x.Person)
//                .Include(x => x.Staff.Rank).Include(x => x.Staff.Department).SingleOrDefaultAsync(o => o.Id == Id);
//        }

//        public async Task<StaffLeaveRequest> GetLeaveRequestByStaff(Staff staff)
//        {
//            return await _context.STAFF_LEAVE_REQUEST.Include(x => x.Leave).Include(x => x.Staff).ThenInclude(x => x.Person)
//                .Include(x => x.Staff.Rank).Include(x => x.Staff.Department).SingleOrDefaultAsync(o => o.Staff.Id == staff.Id);
//        }

//        public async Task<List<StaffLeaveRequest>> GetLeaveRequests()
//        {
//            return await _context.STAFF_LEAVE_REQUEST.Include(x => x.Leave).Include(x => x.Staff).ThenInclude(x => x.Person)
//                .Include(x => x.Staff.Rank).Include(x => x.Staff.Department).ToListAsync();

//        }

//        public async Task<List<StaffLeaveRequest>> GetLeaveRequests(Staff staff)
//        {
//            return await _context.STAFF_LEAVE_REQUEST.Include(x => x.Leave).Include(x => x.Staff).ThenInclude(x => x.Person)
//                .Include(x => x.Staff.Rank).Include(x => x.Staff.Department).Where(o => o.Staff.Id == staff.Id).ToListAsync();

//        }

//        public async Task<StaffLeaveRequest> MakeRequest(StaffLeaveRequest leaveRequest)
//        {
//            _context.Add(leaveRequest);
//            await _context.SaveChangesAsync();
//            var newRequest = await _context.STAFF_LEAVE_REQUEST.Include( x=> x.Leave).Include(x => x.Staff).ThenInclude(x => x.Person)
//                .Include(x => x.Staff.Rank).Include(x => x.Staff.Department).SingleOrDefaultAsync(o => o.Id == leaveRequest.Id);

//            return newRequest;
//        }

//        public async Task<bool> UpdateRequest(long Id, StaffLeaveRequest leaveRequest)
//        {
//            int status = 0;
//            var request = await GetLeaveRequestById(Id);
//            if (request != null)
//            {
//                if (!request.Approved && leaveRequest.Approved)
//                {
//                    request.ApprovedDate = DateTime.Now;
//                }
//                request.Approved = leaveRequest.Approved;
//                request.ApprovedById = leaveRequest.ApprovedById;
//                request.StartDate = leaveRequest.StartDate;
//                request.EndDate = leaveRequest.EndDate;
//                request.AttachmentUrl = leaveRequest.AttachmentUrl;
//                request.Reason = leaveRequest.Reason;
//                request.Remarks = leaveRequest.Remarks;
//                request.LeaveId = leaveRequest.LeaveId;
//                status = await _context.SaveChangesAsync();
//            }

//            return (status > 0) ? true : false;
//        }
        
//    }
//}
