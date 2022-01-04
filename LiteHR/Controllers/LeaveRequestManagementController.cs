//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;
//using LiteHR.Dtos;
//using LiteHR.Interface;
//using LiteHR.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using OfficeOpenXml;

//namespace LiteHR.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class LeaveRequestManagementController : ControllerBase
//    {
//        public readonly ILeaveRequestManagementService _service;
//        private readonly HRContext _context;


//        public LeaveRequestManagementController(ILeaveRequestManagementService service, HRContext context)
//        {
//            _service = service;
//            _context = context;
//        }



//        [HttpPost("MakeLeaveRequests")]
//        public async Task<long> CreateLeaveRequest(AddStaffLeaveRequestDto addStaffLeaveRequest) => await _service.MakeLeaveRequest(addStaffLeaveRequest);

//        [HttpPost("AddLeaveResponseChain")]
//        public async Task<long> CreateLeaveResponseChain(LeaveResponseChainDto leaveResponseChainDto) => await _service.AddLeaveResponseChain(leaveResponseChainDto);

//        //[HttpGet("AllLeaveRequests")]

//        //public async Task<IEnumerable<AddStaffLeaveRequestDto>> GetAllLeaveRequests()
//        //{
//        //    return await _service.GetStaffLeaveRequest();
//        //}

//        //[HttpGet("LeaveRequestsByHOD")]

//        //public async Task<IEnumerable<AddStaffLeaveRequestDto>> LeaveRequestByDepartment(long departmentId) => await _service.GetStaffLeaveRequestByHOD(departmentId);

//        //[HttpGet("GetLeaveResponse")]
//        //public async Task<IEnumerable<LeaveResponseStructure>> GetAllLeaveResponse(long departmentId)
//        //{
//        //    return await _service.GetLeaveResponse(departmentId);
//        //}

//        //[HttpPut("DepartmentHeadAction")]
//        //public async Task<LeaveActionDto> AddHodAction(LeaveActionDto leaveActionDto) => await _service.HeadOfDepartmentToAct(leaveActionDto);

//        //[HttpPut("DeanLeaveAction")]
//        //public async Task<LeaveActionDto> DeanAction(LeaveActionDto leaveActionDto) => await _service.DeanToAct(leaveActionDto);

//        //[HttpPut("ViceChancellorLeaveAction")]
//        //public async Task<LeaveActionDto> VCAction(LeaveActionDto leaveActionDto) => await _service.ViceChancellorToAct(leaveActionDto);

//        [HttpGet("GetLeaveRequestByResponseId")]
//        public async Task<IEnumerable<LeaveResponseStructure>> FetchLeaveResponseByLeaveRequestId(long leaveResponseId)
//        {
//            return await _service.GetLeaveResponseByResponseId(leaveResponseId);
//        }


//        //[HttpGet("LeaveRequestsByDean")]

//        //public async Task<IEnumerable<AddStaffLeaveRequestDto>> LeaveRequestByDeanDepartment(long departmentId) => await _service.GetStaffLeaveRequestByDean(departmentId);

//        //[HttpGet("LeaveRequestsVC")]

//        //public async Task<IEnumerable<AddStaffLeaveRequestDto>> LeaveRequestByVC() => await _service.GetStaffLeaveRequestByVC();

//        [HttpGet("GetLeaveRequestByStaffId")]

//        public async Task<IEnumerable<StaffLeaveRecordDto>> StaffLeaveRequestHistory(long staffId) => await _service.StaffLeaveRequestHistory(staffId);
        
//        [HttpGet("GetLeaveRequestsByRole")]
//        public async Task<IEnumerable<AddStaffLeaveRequestDto>> FetchLeaveRequestByHodAndDean(long departmentId, long roleId)
//        {
//            return await _service.GetStaffLeaveRequestByRole(departmentId, roleId);
//        }

//        [HttpPut("LeaveAction")]
//        public async Task<AppReturnObject> LeaveAction(LeaveActionDto leaveActionDto, long roleId) => await _service.LeaveAction(leaveActionDto, roleId);

//        [HttpPut("DeclineRequest")]
//        public async Task<AppReturnObject> DeclineLeaveRequest(LeaveActionDto leaveActionDto, long roleId) => await _service.DeclineLeaveRequest(leaveActionDto, roleId);

//        [HttpGet("GetLeaveResponseChain")]
//        public async Task<IEnumerable<LeaveResponseChainDto>> GetLeaveResponseChain() => await _service.GetLeaveResponseChain();
//        [HttpGet("GetActionComments")]
//        public async Task<IEnumerable<ChainCommentsDto>> GetLeaveActionComments(long leaveRequestId) => await _service.GetLeaveActionComments(leaveRequestId
//            );


//        [HttpGet("RequestCount")]
//        public async Task<long> LeaveRequestCounter() => await _service.LeaveRequestCounter();
//        [HttpPost("UploadExcelLeaveTypeRank")]
//        public async Task<IActionResult> UploadLeaveTypeRank(IFormFile file)
//        {
//            try
//            {
//                long size = file.Length;
//                if(size > 0)
//                {
//                    var filepath = Path.GetTempFileName();
//                    using(var stream = System.IO.File.Create(filepath))
//                    {
//                        await file.CopyToAsync(stream);

//                        ExcelPackage excelPackage = new ExcelPackage(stream);
//                        ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();

//                        if(excelWorksheet != null)
//                        {
//                            int totalRows = excelWorksheet.Dimension.Rows;
//                            for (int i = 2; i <= totalRows; i++){
//                                //LeaveRankExcelModel leaveRankExcelModel = new LeaveRankExcelModel();

//                                int Id = Convert.ToInt32(excelWorksheet.Cells[i, 1].Value);
//                                int rankId = Convert.ToInt32(excelWorksheet.Cells[i, 2].Value);
//                                int leaveTypeId = Convert.ToInt32(excelWorksheet.Cells[i, 3].Value);
//                                int duration = Convert.ToInt32(excelWorksheet.Cells[i, 4].Value);


//                                var doesExist = await _context.LEAVE_TYPE_RANK.Where(d => d.RankId == rankId && d.LeaveTypeId == leaveTypeId && d.Duration == duration).FirstOrDefaultAsync();

//                                if(doesExist == null)
//                                {
//                                    var addLeaveTypeRank = new LeaveTypeRank()
//                                    {
//                                        RankId = rankId,
//                                        LeaveTypeId = leaveTypeId,
//                                        Duration = duration,
//                                        Active = true
//                                    };
//                                    _context.Add(addLeaveTypeRank);
//                                    await _context.SaveChangesAsync();
//                                }
//                            }
//                        }
//                        else
//                        {
//                            return BadRequest(new { Message = "Excel Sheet is empty and Invalid" });
//                        }
//                    }
//                    return Ok(new { Message = "Excel Sheet was backed up successfully" });

//                }
//                return NoContent();

//            }
//            catch (Exception ex) { throw ex; }

//        }
//        //[HttpGet("[action]")]
//        //public long LeaveDayCount(DateTime start, DateTime end) => _service.LeaveDayCount(start, end);

//        [HttpGet("[action]")]
//        public async Task<long> LeaveDayCount(DateTime start, DateTime end, long staffId) => await _service.LeaveDayCount(start, end, staffId);

//        [HttpGet("GetLeaveRecordsHistory")]
//        public async Task<IEnumerable<AddStaffLeaveRequestDto>> GetStaffLeaveRecordHistoryByRole(long departmentId, long roleId) => await _service.GetStaffLeaveRecordHistoryByRole(departmentId, roleId);

//        [HttpGet("GetLeaveByLeaveRequestId")]
//        public async Task<AddStaffLeaveRequestDto> GetLeaveRequestByLeaveId(long leaveRequestId) => await _service.GetLeaveRequestByLeaveId(leaveRequestId);
//        [HttpPut("UpdateResponseChain")]
//        public async Task<long> UpdateResponseChain(long chainId, LeaveResponseChainDto leaveChainDto) => await _service.UpdateResponseChain(chainId, leaveChainDto);

//    }
//}
