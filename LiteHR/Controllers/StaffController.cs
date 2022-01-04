using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LiteHR.Dtos;
using LiteHR.Interface;
using LiteHR.Models;
using LiteHR.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using OfficeOpenXml;

namespace LiteHR.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _service;
        private readonly IHostEnvironment _hostingEnvironment;
        private readonly HRContext _context;
        private readonly IConfiguration _configuration;
        private readonly string baseUrl;



        public StaffController(IStaffService staffService, IHostEnvironment hostingEnvironment, HRContext context, IConfiguration configuration)
        {
            _service = staffService;
            _hostingEnvironment = hostingEnvironment;
            _context = context;
            _configuration = configuration;
            baseUrl = _configuration.GetValue<string>("Url:root");

        }



        // GET: api/Staff
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Staff>>> Get()
        {
            return await _service.AllStaff();
        }
      
        [HttpGet("GetTemporalStaff")]
        public async Task<ActionResult<IEnumerable<NewStaffDto>>> GetUnregularizeStaff()
        {
            return await _service.GetTemporalStaff();
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<FailedStaffUploads>>> GetFailedUploads()
        {

            return await _context.FAILED_STAFF_UPLOADS.ToListAsync();
        }
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<StaffGradeBenefitDto>>> GetStaffPayrollByDepartmentMonthAndYear(int departmentId, int month, int year)
        {
            if (departmentId < 1 || month < 1 || year < 1)
            {
                return BadRequest(new { Message = "Required paramters were not supplied" });
            }

            return await _service.GetStaffPayrollByDepartmentMonthAndYear(departmentId, month, year);
        }

        // GET: api/Staff/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<Staff>> Get(long id)
        {
            return await _service.GetStaff(id);
        }

        //GET: api/Staff/5
        [HttpGet("GetStaffUpdate")]
        public async Task<ActionResult<StaffUpdateDto>> GetStaffUpdate(long id)
        {
            return  await _service.GetStaffUpdate(id);          


        }

        [HttpGet("StaffStatus")]
        public async Task<ActionResult<string>> GetStatus(long id)
        {
            return await _service.AutomateStaffStatus(id);
        }
        // POST: api/Staff
        [HttpPost]
        public async Task<ActionResult<Staff>> Post(Staff staff)
        {
            var newStaff = await _service.AddStaff(staff);
            return CreatedAtAction("Get", new { id = newStaff.Id }, newStaff);
        }
        [HttpPost("AddSingleStaff")]
        public async Task<ActionResult<Staff>> PostSingleStaff(CreateStaffDto createStaffDto)
        {
            
            return await _service.CreateStaff(createStaffDto);

        }
        [HttpPost("CreateSingleStaff")]
        public async Task<ActionResult<long>> PostCreateSingleStaff(AddSingleStaffDto addSingleStaffDto)
        {
            return await _service.AddSingleStaff(addSingleStaffDto);
        }

        //New Staff Update
        [HttpPatch("PostStaffUpdate")]
        public async Task<IActionResult> PostStaffUpdate(long id, NewStaffProfileEditDto staffUpdateDto)
        {
            if (id != staffUpdateDto?.StaffId)
            {
                return BadRequest();
            }

            try
            {
                //await _service.UpdateStaff(id,staff);
                await _service.PostStaffUpdate(id, staffUpdateDto);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }




        // PUT: api/Staff/5
        //[HttpPut("{id}")]
        //[HttpPatch("{id}")]
        //public async Task<IActionResult> Update (long id, StaffUpdateDto staffUpdateDto)
        //{
        //    if (id != staffUpdateDto?.Staff?.Id)
        //    {
        //        return BadRequest();
        //    }
          
        //    try
        //    {
        //        await _service.UpdateStaff(id,staff);
        //        await _service.PostStaffUpdate(id, staffUpdateDto);
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!StaffExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}
        [HttpPut("RegularizeStaff")]
        public async Task<long> RegulariseStaff(StaffRegularizationDto staffRegularizationDto)
        {
            return await _service.RegularizeStaff(staffRegularizationDto);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> UploadStaffListFromExcelSheet(IFormFile file)
        {
            ExcelUploadRecordCountDto excelUploadRecordCountDto = new ExcelUploadRecordCountDto();
            try
            {
                long size = file.Length;
                if (size > 0)
                {
                    List<StaffExcelModel> staffDetails = new List<StaffExcelModel>();

                    var filePath = Path.GetTempFileName();
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await file.CopyToAsync(stream);
                        //Initialize the Data from the excel sheet into stream
                        ExcelPackage package = new ExcelPackage(stream);
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();

                        if (worksheet != null)
                        {
                            //Give 2 rows space from the top to allow for Excel Headers
                            int totalRows = worksheet.Dimension.Rows;

                            for (int i = 2; i <= totalRows; i++)
                            {
                                StaffExcelModel staffDetail = new StaffExcelModel();

                                int serialNumber = Convert.ToInt32(worksheet.Cells[i, 1].Value);
                                staffDetail.Surname = worksheet.Cells[i, 2].Value!=null? worksheet.Cells[i, 2].Value.ToString():" ";
                                staffDetail.FirstName = worksheet.Cells[i, 3].Value!=null?worksheet.Cells[i, 3].Value.ToString():" ";
                                staffDetail.OtherName = worksheet.Cells[i, 4].Value!=null? worksheet.Cells[i, 4].Value.ToString():" ";
                                staffDetail.DOB = worksheet.Cells[i, 5].Value!=null?worksheet.Cells[i, 5].Value.ToString():" ";
                                staffDetail.DateOfConfirmation = worksheet.Cells[i, 6].Value!=null?worksheet.Cells[i, 6].Value.ToString():" ";
                                staffDetail.DateOfLastPromotion = worksheet.Cells[i, 7].Value != null ? worksheet.Cells[i, 7].Value.ToString() : " ";
                                staffDetail.DateOfEmployment = worksheet.Cells[i, 8].Value != null ? worksheet.Cells[i, 8].Value.ToString() : " ";
                                staffDetail.PhoneNumber = worksheet.Cells[i, 9].Value!=null?worksheet.Cells[i, 9].Value.ToString():" ";                          
                                staffDetail.StaffNumber = worksheet.Cells[i, 10].Value!=null?worksheet.Cells[i, 10].Value.ToString():" ";
                                staffDetail.DepartmentId = Convert.ToInt64(worksheet.Cells[i, 11].Value);
                                staffDetail.Gender = worksheet.Cells[i, 12].Value!=null?worksheet.Cells[i, 12].Value.ToString():" ";
                                staffDetail.SalaryCategory = worksheet.Cells[i, 13].Value!=null?worksheet.Cells[i, 13].Value.ToString():" ";
                                staffDetail.Level = Convert.ToInt64(worksheet.Cells[i, 14].Value);
                                staffDetail.Step = Convert.ToInt64(worksheet.Cells[i, 15].Value);
                                staffDetail.Rank = Convert.ToInt64(worksheet.Cells[i, 16].Value);
                                //staffDetail.DateofAssumption = worksheet.Cells[i, 16].Value != null ? worksheet.Cells[i, 16].Value.ToString() : " ";
                                //staffDetail.AppointmentType = worksheet.Cells[i, 17].Value != null ? worksheet.Cells[i, 17].Value.ToString() : " ";
                                staffDetail.QualificationId = Convert.ToInt64(worksheet.Cells[i, 17].Value);
                                //staffDetail.PFAStatusId = Convert.ToInt64(worksheet.Cells[i, 19].Value);
                                //staffDetail.AreaOfSpecializationId = Convert.ToInt64(worksheet.Cells[i, 20].Value);
                                staffDetail.RSANumber = worksheet.Cells[i, 18].Value != null ? worksheet.Cells[i, 18].Value.ToString() : " ";


                                staffDetails.Add(staffDetail);
                            }

                            if (staffDetails?.Count() > 0)
                            {

                                excelUploadRecordCountDto=await _service.ProcessStaffDetailsFromExcel(staffDetails);
                            }
                        }
                        else
                        {
                            return BadRequest(new { Message = "Excel Sheet is empty and Invalid" });
                        }
                    }

                    return Ok(new 
                    { 
                        Message = "Excel Sheet was Uploaded successfully." + excelUploadRecordCountDto.SuccessfullUpload + " : Success and" + excelUploadRecordCountDto.FailedUpload + " : Failed.",
                        Success = excelUploadRecordCountDto.SuccessfullUpload,
                        Failed = excelUploadRecordCountDto.FailedUpload,
                        Updated = excelUploadRecordCountDto.UpdatedRecords
                    });
                }

                return BadRequest(new { Message = "Oops...something went wrong" });
            }
            catch (Exception ex) { throw ex; }
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> UploadStaffBiometricInfo(IFormFile file)
        {
            ExcelUploadRecordCountDto excelUploadRecordCountDto = new ExcelUploadRecordCountDto();
            try
            {
                long size = file.Length;
                if (size > 0)
                {
                    List<StaffExcelModel> biometricDetails = new List<StaffExcelModel>();

                    var filePath = Path.GetTempFileName();
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await file.CopyToAsync(stream);
                        //Initialize the Data from the excel sheet into stream
                        ExcelPackage package = new ExcelPackage(stream);
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();

                        if (worksheet != null)
                        {
                            //Give 2 rows space from the top to allow for Excel Headers
                            int totalRows = worksheet.Dimension.Rows;

                            for (int i = 2; i <= totalRows; i++)
                            {
                                StaffExcelModel staffDetail = new StaffExcelModel();
                                int serialNumber = Convert.ToInt32(worksheet.Cells[i, 1].Value);
                                staffDetail.Surname = worksheet.Cells[i, 2].Value != null ? worksheet.Cells[i, 2].Value.ToString() : " ";
                                staffDetail.FirstName = worksheet.Cells[i, 3].Value != null ? worksheet.Cells[i, 3].Value.ToString() : " ";
                                staffDetail.OtherName = worksheet.Cells[i, 4].Value != null ? worksheet.Cells[i, 4].Value.ToString() : " ";

                                biometricDetails.Add(staffDetail);
                            }

                            if (biometricDetails?.Count() > 0)
                            {

                                excelUploadRecordCountDto = await _service.ProcessStaffDetailsFromExcel(biometricDetails);
                            }
                        }
                        else
                        {
                            return BadRequest(new { Message = "Excel Sheet is empty and Invalid" });
                        }
                    }

                    return Ok(new
                    {
                        Message = "Excel Sheet was Uploaded successfully." + excelUploadRecordCountDto.SuccessfullUpload + " : Success and" + excelUploadRecordCountDto.FailedUpload + " : Failed.",
                        Success = excelUploadRecordCountDto.SuccessfullUpload,
                        Failed = excelUploadRecordCountDto.FailedUpload,
                        Updated = excelUploadRecordCountDto.UpdatedRecords
                    });
                }

                return BadRequest(new { Message = "Oops...something went wrong" });
            }
            catch (Exception ex) { throw ex; }
        }
        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}

        [HttpPost("StaffProfileUpdate")]
        public async Task<ActionResult<long>> StaffProfileUpdate([FromForm]StaffUpdateProfileDto staffUpdateProfileDto)
        {
            var directory = Path.Combine("Resources", "Passport");
            var filePath = Path.Combine(_hostingEnvironment.ContentRootPath, directory);
            return await _service.UpdateStaffProfile(staffUpdateProfileDto, filePath, directory);
        }
        [HttpPost("[action]")]
        public async Task<long> UploadStaffPassport(IFormFile file, long staffId)
        {
            var directory = Path.Combine("Resources", "Passport");
            var filePath = Path.Combine(_hostingEnvironment.ContentRootPath, directory);
            return await _service.UploadStaffPassport(file, staffId, filePath, directory);
            
        }
        private bool StaffExists(long id)
        {
            return _service.GetStaff(id).Result != null ? true: false;
        }
        [HttpDelete("[action]")]
        public async Task<ActionResult<bool>> DeleteStaff(long staffId) =>await _service.DeleteStaff(staffId);
        [HttpGet("GetStaffBy")]
        public async Task<ActionResult<IEnumerable<NewStaffDto>>> GetStaffBy(long departmentId, long staffTypeId, long rankId, long appointmentTypeId) => await _service.GetStaffBy(departmentId, staffTypeId, rankId, appointmentTypeId);
        [HttpGet("GetStaffByStaffNumber")]
        public async Task<ActionResult<Staff>> GetStaffByStaffNumber(string generatedStaffNumber) => await _service.GetStaffBy(generatedStaffNumber);

        [HttpPut("AssignRole")]
        public async Task<AppReturnObject> AssignRole(AssignRoleDto assignRoleDto) => await _service.AssignRole(assignRoleDto);


        [HttpGet("StaffCounter")]
        public async Task<long> StaffCount() => await _service.StaffCount();

        [HttpGet("NewStaffDto")]
        public async Task<IEnumerable<NewStaffDto>> FetchStaff() => await _service.FetchStaff();

        [HttpGet("DisengagedRetired")]
        public async Task<IEnumerable<NewStaffDto>> DisengagedRetiredStaff(long serviceStatus) => await _service.DisengagedRetiredStaff(serviceStatus);

        [HttpPut("DisengageAction")]
        public async Task<int> DisengageStaff(long staffId) => await _service.DisengageStaff(staffId);
        [HttpPut("ReinstateStaff")]
        public async Task<int> ReinstateStaff(long staffId) => await _service.ReinstateStaff(staffId);
        [HttpGet("MonthlyNominalRoll")]
        public async Task<IEnumerable<StaffNominalRollDto>> FetchStaffForMonthlyNominalRoll(long departmentId) => await _service.FetchStaffForMonthlyNominalRoll(departmentId);

        [HttpPost("GenerateStaffNumber")]
        public async Task<long> NewStaffNumberGeneration(AddSingleStaffDto addSingleStaffDto) => await _service.NewStaffNumberGeneration(addSingleStaffDto);

        [HttpGet("AssumptionOfDuty")]
        public async Task<IEnumerable<GenerateStaffNumberListDto>> GetGeneratedStaffNumberByDate(DateTime from, DateTime to) => await _service.GetGeneratedStaffNumberByDate(from, to);


        [HttpGet("GetLoginCredentials")]
        public async Task<NewStaffDto> GetStaffLoginInfo(string surname, string staffNumber) => await _service.GetStaffLoginInfo(surname, staffNumber);

        [HttpDelete("NewlyCreatedStaff")]
        public async Task<int> RemoveFromNewlyCreatedStaffTable(long staffId)
        {
            var selectRecord = await _context.GENERATED_STAFFNUMBER_RECORDS.Where(s => s.StaffId == staffId).FirstOrDefaultAsync();
            _context.Remove(selectRecord);
            await _context.SaveChangesAsync();
            return StatusCodes.Status200OK;
        }

     [HttpDelete("ClearFailedStaffUploads")]
     public async Task<bool> ClearFailedUploads()
        {
           var get =  await _context.FAILED_STAFF_UPLOADS.ToListAsync();
            foreach(var item in get)
            {
                var getId = await _context.FAILED_STAFF_UPLOADS.Where(a => a.Id == item.Id).FirstOrDefaultAsync();
                _context.FAILED_STAFF_UPLOADS.Remove(getId);
            }
            await _context.SaveChangesAsync();
            return true;
        }

        [HttpPost("[action]")]
        public async Task<int> ModifyStaffUserName(string staffNumber, string newInput)
        {
            User staff = await _context.USER.Where(s => s.Username == staffNumber.Trim()).FirstOrDefaultAsync();
            if(staff != null)
            {
                staff.Username = newInput;
                _context.Update(staff);
                await _context.SaveChangesAsync();
                return StatusCodes.Status200OK;
            }
            return 0;
        }
        [HttpPost("[action]")]
        public async Task<int> ModifyUserPassword(ChangePassword dto) => await _service.ModifyUserPassword(dto);

    }
}
