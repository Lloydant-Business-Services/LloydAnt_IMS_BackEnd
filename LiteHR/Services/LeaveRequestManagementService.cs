//using LiteHR.Dtos;
//using LiteHR.Helpers;
//using LiteHR.Infrastructure;
//using LiteHR.Interface;
////using LiteHR.Migrations;
//using LiteHR.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace LiteHR.Services
//{
//    public class LeaveRequestManagementService: ILeaveRequestManagementService
//    {
//        public readonly HRContext _context;
//        private readonly IFileUpload _fileUpload;



//        AppReturnObject appReturnObject = new AppReturnObject()
//        {
//            Status = (long)StatusCode.Status,

//        };
//        public LeaveRequestManagementService(HRContext context, IFileUpload fileUpload)
//        {
//            _context = context;
//            _fileUpload = fileUpload;

//        }


//        public async Task<long> MakeLeaveRequest(AddStaffLeaveRequestDto addStaffLeaveRequest)
//        {
//            long getRemainingLeaveDays = 0;
//            long daysLeft = 0;
//            if (addStaffLeaveRequest == null)
//            {
//                throw new AppException("Provide Required Information");
//            }



//            var pickLeaveTypeRank = await _context.LEAVE_TYPE_RANK
//               .Where(a => a.Id == addStaffLeaveRequest.LeaveTypeRankId).FirstOrDefaultAsync();

//            //Checks, flags and restricts the staff from applying for the same leave type when its not up to a year from previous request         
//            //LeaveRequest checkLeaveRequestRepeat = await _context.LEAVE_REQUEST.Where(a => a.StaffId == addStaffLeaveRequest.StaffId && a.LeaveTypeRankId == pickLeaveTypeRank.Id).OrderByDescending(f=>f.Id).FirstOrDefaultAsync();

//            var requests  = await _context.LEAVE_REQUEST.Where(a => a.StaffId == addStaffLeaveRequest.StaffId && a.LeaveTypeRankId == pickLeaveTypeRank.Id).ToListAsync();
//            LeaveRequest checkLeaveRequestRepeat= requests.Where(f => f.Id == requests.Max(f => f.Id)).FirstOrDefault();

//            Staff staffAssumptionOfDutyCheck = await _context.STAFF
//                .Where(s => s.Id == addStaffLeaveRequest.StaffId).FirstOrDefaultAsync();

//            StaffSalaryCategoryReference staffSalaryInfoCheck = await _context.STAFF_SALARY_CATEGORY_REFERENCE.Where(sc => sc.StaffId == addStaffLeaveRequest.StaffId).FirstOrDefaultAsync();

//            //Check if a request is still active and awaiting action
//            LeaveRequest isProcessing = await _context.LEAVE_REQUEST.Where(a => a.StaffId == addStaffLeaveRequest.StaffId && a.LeaveTypeRankId == addStaffLeaveRequest.LeaveTypeRankId && a.Staus == (long)ProgressTrack.Processing)
//                .Include(s => s.Staff)
//                .FirstOrDefaultAsync();


//            if (staffAssumptionOfDutyCheck.DateOfConfirmation == null || staffSalaryInfoCheck == null || staffAssumptionOfDutyCheck.DepartmentId == null)
//                return (long)StatusCode.RequiredDataNull;
//            if (isProcessing != null)
//                return (long)StatusCode.NotClosed;
//            if (checkLeaveRequestRepeat != null && (DateTime.Now - checkLeaveRequestRepeat.End).Days < 365 && checkLeaveRequestRepeat.IsCompleted)
//                return (long)StatusCode.Ineligible;

//            if (addStaffLeaveRequest.Duration > pickLeaveTypeRank.Duration)
//            {
//                return (long)StatusCode.DurationDisparity;
//            }
//            else if (addStaffLeaveRequest.Duration <= pickLeaveTypeRank.Duration)
//            {
//                getRemainingLeaveDays = pickLeaveTypeRank.Duration- addStaffLeaveRequest.Duration;
//            }        

//            var responseChain = await _context.LEAVE_RESPONSE_CHAIN.Where(f => f.LeaveTypeId == pickLeaveTypeRank.LeaveTypeId).OrderBy(f => f.Order).ToListAsync();

//            if (responseChain?.Count == 0)

//                return (long)StatusCode.NoChainSet;
    
//            LeaveRequest isLeaveCompleted = await _context.LEAVE_REQUEST.Where(a => a.StaffId == addStaffLeaveRequest.StaffId && a.LeaveTypeRankId == pickLeaveTypeRank.Id && !a.IsCompleted).OrderByDescending(f=>f.Id).FirstOrDefaultAsync();
//            if (isLeaveCompleted != null && isLeaveCompleted.RemainingLeaveDays > 0)
//            {
//                getRemainingLeaveDays = isLeaveCompleted.RemainingLeaveDays - addStaffLeaveRequest.Duration;
//                //If a request exists and still has unused leave days, check to make sure the applied duration does not exceed number of unused days
//                if(addStaffLeaveRequest.Duration > isLeaveCompleted.RemainingLeaveDays)
//                {
//                    return (long)StatusCode.DurationDisparity;
//                }
//            }



//            LeaveRequest leaveRequest = new LeaveRequest()
//            {
//                Active = true,
//                Remarks = addStaffLeaveRequest.Comment,
//                Start = addStaffLeaveRequest.Start,
//                End = addStaffLeaveRequest.End,
//                EnteredDate = DateTime.Now,
//                StaffId = addStaffLeaveRequest.StaffId,
//                LeaveTypeRankId = addStaffLeaveRequest.LeaveTypeRankId,
//                SupportDocumentUrl = addStaffLeaveRequest.SupportDocument,
//                RemainingLeaveDays = getRemainingLeaveDays,
//                LeaveDaysApplied = addStaffLeaveRequest.Duration,
//                IsCompleted = false

//            };

//            _context.Add(leaveRequest);
//            await _context.SaveChangesAsync();

//            foreach (var item in responseChain)
//            {
//                LeaveResponse leaveResponse = new LeaveResponse()
//                {
//                    LeaveRequestId = leaveRequest.Id,
//                    LeaveResponseChainId = item.Id,
//                    ActiveDesk = item.Order == 1 ? true : false

//                };
//                _context.Add(leaveResponse);

//            }

//            var created = await _context.SaveChangesAsync();

//            if (created > 0)
//            {
//                return (long)StatusCode.Status;
//            }
//            return 0;


//        }


//        //public async Task<long> StaffLeaveCriteriaCheck(AddStaffLeaveRequestDto addStaffLeaveRequest)
//        //{


//        //    return 0;
//        //}

//        public async Task<IEnumerable<AddStaffLeaveRequestDto>> GetStaffLeaveRequest()
//        {

//            return await _context.LEAVE_REQUEST

//                .Include(p => p.Staff)

//                .Select(f => new AddStaffLeaveRequestDto
//                {
//                    StaffId = f.StaffId,
//                    Comment = f.Remarks,
//                    Start = f.Start,
//                    End = f.End,
//                    LeaveTypeId = f.LeaveTypeRank.LeaveTypeId,
//                    StaffNumber = f.Staff.StaffNumber,
//                    StaffName = f.Staff.Person.Firstname + " " + f.Staff.Person.Surname

//                }).ToListAsync();

//        }
     

//        public async Task<long> AddLeaveResponseChain(LeaveResponseChainDto leaveResponseChainDto)
//        {
//            if (leaveResponseChainDto == null)
//            {
//                throw new AppException("Fields are not be left empty");
//            }

//            LeaveResponseChain leaveResponseChain = new LeaveResponseChain()
//            {
//                LeaveTypeId = leaveResponseChainDto.LeaveTypeId,
//                RoleId = leaveResponseChainDto.RoleId,
//                Active = true,
//                Order = leaveResponseChainDto.Order

//            };
//            _context.Add(leaveResponseChain);
//            var added = await _context.SaveChangesAsync();
//            if (added > 0)
//            {
//                return leaveResponseChain.Id;
//            }
//            return 0;

//        }
//        public async Task<long> UpdateResponseChain(long chainId, LeaveResponseChainDto leaveChainDto)
//        {
//            var getChain = await _context.LEAVE_RESPONSE_CHAIN.Where(r => r.Id == chainId).FirstOrDefaultAsync();
//            if(getChain != null)
//            {
//                getChain.LeaveTypeId = leaveChainDto.LeaveTypeId;
//                getChain.Order = leaveChainDto.Order;
//                getChain.RoleId = leaveChainDto.RoleId;

//                _context.Update(getChain);
//                await _context.SaveChangesAsync();
//                return StatusCodes.Status200OK;
//            }
//            return 0;           
//        }

//        public async Task<IEnumerable<LeaveResponseChainDto>> GetLeaveResponseChain()
//        {
//            return await _context.LEAVE_RESPONSE_CHAIN
//                .Select(e => new LeaveResponseChainDto
//                {
//                    RoleName = e.Role.Name,
//                    LeaveName = e.LeaveType.Name,
//                    Order = e.Order,
//                    LeaveTypeId = e.LeaveTypeId,
//                    RoleId = e.RoleId,
//                    Id = e.Id
//                }).ToListAsync();
//        }



//        public async Task<IEnumerable<LeaveResponseStructure>> GetLeaveResponse(long departmentId)
//        {

//            return await _context.LEAVE_RESPONSE
//                .Where(r => r.LeaveRequest.Staff.DepartmentId == departmentId)

//                .Select(e => new LeaveResponseStructure
//                {
//                    LeaveRequestId = e.LeaveRequestId,
//                    StaffName = e.LeaveRequest.Staff.Person.Firstname + " " + e.LeaveRequest.Staff.Person.Surname,
//                    StaffNumber = e.LeaveRequest.Staff.GeneratedStaffNumber,
//                    Start = e.LeaveRequest.Start,
//                    End = e.LeaveRequest.End,
//                    EnteredDate = e.LeaveRequest.EnteredDate,
//                    LeaveTypeId = e.LeaveRequest.LeaveTypeRank.LeaveTypeId,
//                    DepartmentId = (long)e.LeaveRequest.Staff.DepartmentId


//                }).ToListAsync();


//        }

//        public async Task<IEnumerable<LeaveResponseStructure>> GetLeaveResponseByResponseId(long leaveResponseId)
//        {

//            return await _context.LEAVE_RESPONSE
//                .Where(r => r.Id == leaveResponseId)

//                .Select(e => new LeaveResponseStructure
//                {
//                    LeaveResponseId = e.Id,
//                    StaffName = e.LeaveRequest.Staff.Person.Firstname + " " + e.LeaveRequest.Staff.Person.Surname,
//                    StaffNumber = e.LeaveRequest.Staff.GeneratedStaffNumber,
//                    Start = e.LeaveRequest.Start,
//                    End = e.LeaveRequest.End,
//                    EnteredDate = e.LeaveRequest.EnteredDate,
//                    LeaveTypeId = e.LeaveRequest.LeaveTypeRank.LeaveTypeId,
//                    DepartmentId = (long)e.LeaveRequest.Staff.DepartmentId


//                }).ToListAsync();


//        }

//        public async Task<IEnumerable<StaffLeaveRecordDto>> StaffLeaveRequestHistory(long staffId)

//        {
//            var leaveResponseInfo = await _context.LEAVE_RESPONSE.Where(x => x.LeaveRequest.StaffId == staffId && x.IsClosed == false)                
//                .ToListAsync();
//            var getPercentage = leaveResponseInfo.Count();

//            StaffLeaveRecordDto staffLeaveRecordDto = new StaffLeaveRecordDto();
//            var staffLeave = await _context.LEAVE_REQUEST

//                .Where(f => f.StaffId == staffId)
//                .Include(l => l.LeaveTypeRank)
                

//                  .Select(f => new StaffLeaveRecordDto 
//                  {
                    
//                      Remarks = f.Remarks,
//                      Progress = f.Staus,
//                      DateEntered = f.EnteredDate,
//                      LeaveName = f.LeaveTypeRank.LeaveType.Name,
//                      Start = f.Start,
//                      ProgressInPercentage = f.Staus,
//                      OriginalLeaveDuration = f.LeaveTypeRank.Duration,
//                      AppliedLeaveDuration = f.LeaveDaysApplied,
//                      RemainingLeaveDays = f.RemainingLeaveDays                     
//                 })
//                .ToListAsync();
//            return staffLeave;

//        }

//        public async Task<IEnumerable<AddStaffLeaveRequestDto>> GetStaffLeaveRequestByRole(long departmentId, long roleId)

//        {
//            if(roleId == (long)UserRole.HOD)
//            {
//                var listRes = await _context.LEAVE_RESPONSE
//                    .Where(f => f.LeaveResponseChain.RoleId == roleId && !f.IsActed && f.ActiveDesk == true && f.LeaveRequest.Staff.DepartmentId == departmentId)
//                    .Include(l => l.LeaveRequest)
//                    .ThenInclude(s => s.LeaveTypeRank)
//                      .Select(f => new AddStaffLeaveRequestDto
//                      {
//                          StaffId = f.LeaveRequest.Staff.Id,
//                          Reason = f.LeaveRequest.Remarks,
//                          Start = f.LeaveRequest.Start,
//                          End = f.LeaveRequest.End,
//                          LeaveTypeId = f.LeaveRequest.LeaveTypeRank.LeaveTypeId,
//                          StaffNumber = f.LeaveRequest.Staff.GeneratedStaffNumber,
//                          StaffName = f.LeaveRequest.Staff.Person.Firstname + " " + f.LeaveRequest.Staff.Person.Surname,
//                          DepartmentId = (long)f.LeaveRequest.Staff.DepartmentId,
//                          DepartmentName = f.LeaveRequest.Staff.Department.Name,
//                          LeaveType = f.LeaveRequest.LeaveTypeRank.LeaveType.Name,
//                          isActed = f.IsActed,
//                          isApproved = f.IsApproved,
//                          LeaveResponseId = f.Id,
//                          LeaveRequestId = f.LeaveRequestId,
//                          Status = f.LeaveRequest.Staus,
//                          Duration = f.LeaveRequest.LeaveTypeRank.Duration,
//                          Comment = f.Remarks,
//                          EnteredDate = f.LeaveRequest.EnteredDate,
//                          AppliedLeaveDuration = f.LeaveRequest.LeaveDaysApplied,
//                          RemainingLeaveDays = f.LeaveRequest.RemainingLeaveDays,
//                          ProgressInPercentage = f.LeaveRequest.Staus == (long)ProgressTrack.HOD_Phase ? (long)ProgressPercentage.CaseOne :
//                          f.LeaveRequest.Staus == (long)ProgressTrack.Dean_Phase ? (long)ProgressPercentage.CaseTwo : f.LeaveRequest.Staus == (long)ProgressTrack.VC_Phase ? (long)ProgressPercentage.CaseThree : 0

//                      })
//                    .ToListAsync();

//                return listRes;
//            }
//            else
//            {
//                var listRes = await _context.LEAVE_RESPONSE
//                    .Where(f => f.LeaveResponseChain.RoleId == roleId && !f.IsActed && f.ActiveDesk == true)
//                    .Include(l => l.LeaveRequest)
//                    .ThenInclude(s => s.LeaveTypeRank)
//                      .Select(f => new AddStaffLeaveRequestDto
//                      {
//                          StaffId = f.LeaveRequest.Staff.Id,
//                          Reason = f.LeaveRequest.Remarks,
//                          Start = f.LeaveRequest.Start,
//                          End = f.LeaveRequest.End,
//                          LeaveTypeId = f.LeaveRequest.LeaveTypeRank.LeaveTypeId,
//                          StaffNumber = f.LeaveRequest.Staff.GeneratedStaffNumber,
//                          StaffName = f.LeaveRequest.Staff.Person.Firstname + " " + f.LeaveRequest.Staff.Person.Surname,
//                          DepartmentId = (long)f.LeaveRequest.Staff.DepartmentId,
//                          DepartmentName = f.LeaveRequest.Staff.Department.Name,
//                          LeaveType = f.LeaveRequest.LeaveTypeRank.LeaveType.Name,
//                          isActed = f.IsActed,
//                          isApproved = f.IsApproved,
//                          LeaveResponseId = f.Id,
//                          LeaveRequestId = f.LeaveRequestId,
//                          Status = f.LeaveRequest.Staus,
//                          Duration = f.LeaveRequest.LeaveTypeRank.Duration,
//                          Comment = f.Remarks,
//                          EnteredDate = f.LeaveRequest.EnteredDate,
//                          AppliedLeaveDuration = f.LeaveRequest.LeaveDaysApplied,
//                          RemainingLeaveDays = f.LeaveRequest.RemainingLeaveDays,
//                          ProgressInPercentage = f.LeaveRequest.Staus == (long)ProgressTrack.HOD_Phase ? (long)ProgressPercentage.CaseOne :
//                          f.LeaveRequest.Staus == (long)ProgressTrack.Dean_Phase ? (long)ProgressPercentage.CaseTwo : f.LeaveRequest.Staus == (long)ProgressTrack.VC_Phase ? (long)ProgressPercentage.CaseThree : 0

//                      })
//                    .ToListAsync();

//                return listRes;
//            }
        
           
          
//        }

//        public async Task<IEnumerable<AddStaffLeaveRequestDto>> GetStaffLeaveRecordHistoryByRole(long departmentId, long roleId)
//        {
//            if (roleId == (long)UserRole.HOD)
//            {
//                var listRes = await _context.LEAVE_RESPONSE
//                    .Where(f => f.LeaveResponseChain.RoleId == roleId && f.LeaveRequest.Staff.DepartmentId == departmentId && f.IsActed == true)
//                    .Include(l => l.LeaveRequest)
//                    .ThenInclude(s => s.LeaveTypeRank)
//                      .Select(f => new AddStaffLeaveRequestDto
//                      {
//                          StaffId = f.LeaveRequest.Staff.Id,
//                          Reason = f.LeaveRequest.Remarks,
//                          Start = f.LeaveRequest.Start,
//                          End = f.LeaveRequest.End,
//                          LeaveTypeId = f.LeaveRequest.LeaveTypeRank.LeaveTypeId,
//                          StaffNumber = f.LeaveRequest.Staff.GeneratedStaffNumber,
//                          StaffName = f.LeaveRequest.Staff.Person.Firstname + " " + f.LeaveRequest.Staff.Person.Surname,
//                          DepartmentId = (long)f.LeaveRequest.Staff.DepartmentId,
//                          DepartmentName = f.LeaveRequest.Staff.Department.Name,
//                          LeaveType = f.LeaveRequest.LeaveTypeRank.LeaveType.Name,
//                          isActed = f.IsActed,
//                          isApproved = f.IsApproved,
//                          LeaveResponseId = f.Id,
//                          LeaveRequestId = f.LeaveRequestId,
//                          Status = f.LeaveRequest.Staus,
//                          Duration = f.LeaveRequest.LeaveTypeRank.Duration,
//                          Comment = f.Remarks,
//                          EnteredDate = f.LeaveRequest.EnteredDate,
//                          AppliedLeaveDuration = f.LeaveRequest.LeaveDaysApplied,
//                          RemainingLeaveDays = f.LeaveRequest.RemainingLeaveDays,
//                          ProgressInPercentage = f.LeaveRequest.Staus

//                      })
//                    .ToListAsync();

//                return listRes;
//            }
//            else
//            {
//                var listRes = await _context.LEAVE_RESPONSE
//                    .Where(f => f.LeaveResponseChain.RoleId == roleId && f.IsActed == true)
//                    .Include(l => l.LeaveRequest)
//                    .ThenInclude(s => s.LeaveTypeRank)
//                      .Select(f => new AddStaffLeaveRequestDto
//                      {
//                          StaffId = f.LeaveRequest.Staff.Id,
//                          Reason = f.LeaveRequest.Remarks,
//                          Start = f.LeaveRequest.Start,
//                          End = f.LeaveRequest.End,
//                          LeaveTypeId = f.LeaveRequest.LeaveTypeRank.LeaveTypeId,
//                          StaffNumber = f.LeaveRequest.Staff.GeneratedStaffNumber,
//                          StaffName = f.LeaveRequest.Staff.Person.Firstname + " " + f.LeaveRequest.Staff.Person.Surname,
//                          DepartmentId = (long)f.LeaveRequest.Staff.DepartmentId,
//                          DepartmentName = f.LeaveRequest.Staff.Department.Name,
//                          LeaveType = f.LeaveRequest.LeaveTypeRank.LeaveType.Name,
//                          isActed = f.IsActed,
//                          isApproved = f.IsApproved,
//                          LeaveResponseId = f.Id,
//                          LeaveRequestId = f.LeaveRequestId,
//                          Status = f.LeaveRequest.Staus,
//                          Duration = f.LeaveRequest.LeaveTypeRank.Duration,
//                          Comment = f.Remarks,
//                          EnteredDate = f.LeaveRequest.EnteredDate,
//                          AppliedLeaveDuration = f.LeaveRequest.LeaveDaysApplied,
//                          RemainingLeaveDays = f.LeaveRequest.RemainingLeaveDays,
//                          ProgressInPercentage = f.LeaveRequest.Staus == (long)ProgressTrack.HOD_Phase ? (long)ProgressPercentage.CaseOne :
//                          f.LeaveRequest.Staus == (long)ProgressTrack.Dean_Phase ? (long)ProgressPercentage.CaseTwo : f.LeaveRequest.Staus == (long)ProgressTrack.VC_Phase ? (long)ProgressPercentage.CaseThree : 0

//                      })
//                    .ToListAsync();

//                return listRes;
//            }


//        }

//        public async Task<AppReturnObject> LeaveAction(LeaveActionDto leaveActionDto, long roleId)
//        {
//            var leaveResponseInfo = await _context.LEAVE_RESPONSE.Where(x => x.LeaveRequestId == leaveActionDto.LeaveRequestId && x.IsClosed == false)
//               .ToListAsync();
//            var getPercentage = leaveResponseInfo.Count();
//            var getStatus = 100 / getPercentage;

//            var leaveResponse = await _context.LEAVE_RESPONSE
//                  .Where(e => e.LeaveResponseChain.RoleId == roleId && e.LeaveRequest.Id == leaveActionDto.LeaveRequestId)
//                  .Include(l => l.LeaveResponseChain)
//                  .Include(c => c.LeaveRequest)
//                  .FirstOrDefaultAsync();
//            //var statusUpd = await _context.LEAVE_REQUEST.Where(f => f.Id == leaveActionDto.LeaveRequestId).FirstOrDefaultAsync();
//            long add = leaveResponse.LeaveResponseChain.Order + 1;

//            leaveResponse.IsActed = true;
//            leaveResponse.IsApproved = true;
//            leaveResponse.Remarks = leaveActionDto.Remarks;
//            leaveResponse.DateActed = DateTime.Now;
//            leaveResponse.ActiveDesk = false;
//            leaveResponse.LeaveRequest.Staus = leaveResponse.LeaveRequest.Staus + getStatus;

//            var nextResponse = await _context.LEAVE_RESPONSE
//                 .Where(e => e.LeaveRequest.Id == leaveActionDto.LeaveRequestId && e.LeaveResponseChain.Order == leaveResponse.LeaveResponseChain.Order+1).FirstOrDefaultAsync();

//            if(nextResponse != null)
//            {
//                nextResponse.ActiveDesk = true;
//                _context.Update(nextResponse);


//            }

//            //var checkRemainingDays = leaveResponseInfo.Where(r => r.LeaveResponseChain.Order == leaveResponseInfo.Max(m => m.LeaveResponseChain.Order)).FirstOrDefault();

//            var checkRemainingDays = await _context.LEAVE_RESPONSE.Where(s => s.LeaveRequestId == leaveActionDto.LeaveRequestId).OrderByDescending(d => d.LeaveResponseChain.Order)
//                .Include(d => d.LeaveResponseChain)
//                .Include(l => l.LeaveRequest)
//                .FirstOrDefaultAsync();

//            if(roleId == checkRemainingDays.LeaveResponseChain.RoleId && checkRemainingDays.LeaveRequest.RemainingLeaveDays == 0)
//            {          
//                    leaveResponse.LeaveRequest.IsCompleted = true;
             
//            }


//            _context.Update(leaveResponse);


//            if (leaveResponse == null)
//            {
//                throw new AppException("Disparity with details provided");
//            }

//            await _context.SaveChangesAsync();

      
//            return appReturnObject;

//        }

//        public async Task<AppReturnObject> DeclineLeaveRequest(LeaveActionDto leaveActionDto, long roleId)
//        {
//            var getLeaveRequest = await _context.LEAVE_REQUEST.Where(f => f.Id == leaveActionDto.LeaveRequestId).FirstOrDefaultAsync();

//            if (roleId == (long)UserRole.HOD)
//            {
//                var hodLeaveResponse = await _context.LEAVE_RESPONSE.Where(d => d.LeaveResponseChain.RoleId == (long)UserRole.HOD && d.LeaveRequest.Id == leaveActionDto.LeaveRequestId).FirstOrDefaultAsync();

//                var leaveResponse = await _context.LEAVE_RESPONSE

//                   .Where(e => e.LeaveResponseChain.RoleId == (long)UserRole.HOD && e.Id == hodLeaveResponse.Id)
//                   .Include(l => l.LeaveRequest)
//                   .FirstOrDefaultAsync();
               
//                if (leaveResponse == null)
//                {
//                    throw new AppException("Disparity with details provided");
//                }
                
//                leaveResponse.Id = hodLeaveResponse.Id;
//                leaveResponse.IsActed = true;
//                leaveResponse.IsClosed = true;
//                getLeaveRequest.Staus = (long)ProgressTrack.Declined;
//                getLeaveRequest.RemainingLeaveDays = getLeaveRequest.RemainingLeaveDays + getLeaveRequest.LeaveDaysApplied;
                
               
//                _context.Update(leaveResponse);
//                await _context.SaveChangesAsync();

//                return appReturnObject;

//            }else if(roleId == (long)UserRole.Dean)
//            {
//                var deanLeaveResponse = await _context.LEAVE_RESPONSE.Where(d => d.LeaveResponseChain.RoleId == (long)UserRole.Dean && d.LeaveRequest.Id == leaveActionDto.LeaveRequestId).FirstOrDefaultAsync();

//                var leaveResponse = await _context.LEAVE_RESPONSE

//                   .Where(e => e.LeaveResponseChain.RoleId == (long)UserRole.Dean && e.Id == deanLeaveResponse.Id).FirstOrDefaultAsync();

//                if (leaveResponse == null)
//                {
//                    throw new AppException("Disparity with details provided");
//                }
//                leaveResponse.Id = deanLeaveResponse.Id;
//                leaveResponse.IsActed = true;
//                leaveResponse.IsClosed = true;
//                getLeaveRequest.Staus = (long)ProgressTrack.Declined;
//                getLeaveRequest.RemainingLeaveDays = getLeaveRequest.RemainingLeaveDays + getLeaveRequest.LeaveDaysApplied;


//                _context.Update(leaveResponse);
//                await _context.SaveChangesAsync();

//                return appReturnObject;

//            }else if(roleId == (long)UserRole.VC)
//            {
//                var vcLeaveResponse = await _context.LEAVE_RESPONSE.Where(d => d.LeaveResponseChain.RoleId == (long)UserRole.VC && d.LeaveRequest.Id == leaveActionDto.LeaveRequestId).FirstOrDefaultAsync();

//                var leaveResponse = await _context.LEAVE_RESPONSE

//                   .Where(e => e.LeaveResponseChain.RoleId == (long)UserRole.VC && e.Id == vcLeaveResponse.Id).FirstOrDefaultAsync();

//                if (leaveResponse == null)
//                {
//                    throw new AppException("Disparity with details provided");
//                }
//                leaveResponse.Id = vcLeaveResponse.Id;
//                leaveResponse.IsActed = true;
//                leaveResponse.IsClosed = true;
//                getLeaveRequest.Staus = (long)ProgressTrack.Declined;
//                getLeaveRequest.RemainingLeaveDays = getLeaveRequest.RemainingLeaveDays + getLeaveRequest.LeaveDaysApplied;


//                _context.Update(leaveResponse);
//                await _context.SaveChangesAsync();

//                return appReturnObject;
//            }
//            return null;
//        }

//        public async Task<long> LeaveRequestCounter()
//        {
//            var count = _context.LEAVE_REQUEST;
//            var sum = count.Count();
//            return sum;
//        }

//        public async Task<IEnumerable<ChainCommentsDto>> GetLeaveActionComments(long leaveRequestId)
//        {
//            return await _context.LEAVE_RESPONSE.Where(d => d.LeaveRequestId == leaveRequestId)
//                .Select(f => new ChainCommentsDto
//                {
//                    RoleName = f.LeaveResponseChain.Role.Name,
//                    Comments = f.Remarks,
//                    Action = f.IsApproved
//                }).ToListAsync();
            
            
//        }


//        public async Task<long> LeaveDayCount(DateTime start, DateTime end, long staffId)
//        {
//            var getStaffType = await _context.STAFF.Where(s => s.Id == staffId).FirstOrDefaultAsync();
//            int workDays = 0;
//            int holidayCount = 0;
//            var holiday = await _context.HOLIDAYS.ToListAsync();
           
//            List<DateTime> dateTimeList = new List<DateTime>();

//            if(getStaffType.StaffTypeId == (long)StaffType.NonAcademicStaff)
//            {
//                foreach (var u in holiday)
//                    dateTimeList.Add(u.Date);

//                while (start != end)
//                {
//                    if (start.DayOfWeek != DayOfWeek.Saturday && start.DayOfWeek != DayOfWeek.Sunday)
//                    {
//                        workDays++;

//                    }
//                    foreach (var specialDays in dateTimeList)
//                    {
//                        if (start.Date == specialDays)
//                        {
//                            holidayCount++;
//                        }
//                    }

//                    start = start.AddDays(1);

//                }
//                var finalCount = workDays - holidayCount;
//                return finalCount;

//            }
//            else if(getStaffType.StaffTypeId == (long)StaffType.AcademicStaff)
//            {
//                foreach (var u in holiday)
//                    dateTimeList.Add(u.Date);

//                while (start != end)
//                {
//                        workDays++;

                    
//                    foreach (var specialDays in dateTimeList)
//                    {
//                        if (start.Date == specialDays)
//                        {
//                            holidayCount++;
//                        }
//                    }

//                    start = start.AddDays(1);

//                }
//                var finalCount = workDays - holidayCount;
//                return finalCount;

//            }
//            return 0;

           
//        }

//        public async Task<AddStaffLeaveRequestDto> GetLeaveRequestByLeaveId(long leaveRequestId)
//        {
//            return await _context.LEAVE_RESPONSE
//                    .Where(f => f.LeaveRequestId == leaveRequestId)
//                    .Include(l => l.LeaveRequest)
//                    .ThenInclude(s => s.LeaveTypeRank)
//                      .Select(f => new AddStaffLeaveRequestDto
//                      {
//                          StaffId = f.LeaveRequest.Staff.Id,
//                          Reason = f.LeaveRequest.Remarks,
//                          Start = f.LeaveRequest.Start,
//                          End = f.LeaveRequest.End,
//                          LeaveTypeId = f.LeaveRequest.LeaveTypeRank.LeaveTypeId,
//                          StaffNumber = f.LeaveRequest.Staff.GeneratedStaffNumber,
//                          StaffName = f.LeaveRequest.Staff.Person.Firstname + " " + f.LeaveRequest.Staff.Person.Surname,
//                          DepartmentId = (long)f.LeaveRequest.Staff.DepartmentId,
//                          DepartmentName = f.LeaveRequest.Staff.Department.Name,
//                          LeaveType = f.LeaveRequest.LeaveTypeRank.LeaveType.Name,
//                          isActed = f.IsActed,
//                          isApproved = f.IsApproved,
//                          LeaveResponseId = f.Id,
//                          LeaveRequestId = f.LeaveRequestId,
//                          Status = f.LeaveRequest.Staus,
//                          Duration = f.LeaveRequest.LeaveTypeRank.Duration,
//                          Comment = f.Remarks,
//                          EnteredDate = f.LeaveRequest.EnteredDate,
//                          AppliedLeaveDuration = f.LeaveRequest.LeaveDaysApplied,
//                          RemainingLeaveDays = f.LeaveRequest.RemainingLeaveDays,
//                          ProgressInPercentage = f.LeaveRequest.Staus

//                      })
//                    .FirstOrDefaultAsync();
//        }

//    }
//}
