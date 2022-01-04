using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LiteHR.Dtos;
using LiteHR.Interface;
using LiteHR.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace LiteHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicomController : ControllerBase
    {
        private readonly HRContext _context;
        private readonly IServicomService _service;

        public ServicomController(HRContext context, IServicomService service)
        {
            _context = context;
            _service = service;
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
                    List<AttendanceExcelModel> attendanceList = new List<AttendanceExcelModel>();

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
                                AttendanceExcelModel attendanceModel = new AttendanceExcelModel();

                                attendanceModel.Date = worksheet.Cells[i, 1].Value != null ? worksheet.Cells[i, 1].Value.ToString() : " ";
                                attendanceModel.BiometricNo = worksheet.Cells[i, 2].Value != null ? worksheet.Cells[i, 2].Value.ToString() : " ";
                                attendanceModel.StaffName = worksheet.Cells[i, 3].Value != null ? worksheet.Cells[i, 3].Value.ToString() : " ";
                                //attendanceModel.DepartmentId = Convert.ToInt64(worksheet.Cells[i, 4].Value);
                                attendanceModel.ClockIn = worksheet.Cells[i, 5].Value != null ? worksheet.Cells[i, 5].Value.ToString() : " ";
                                attendanceModel.ClockOut = worksheet.Cells[i, 6].Value != null ? worksheet.Cells[i, 6].Value.ToString() : " ";
                                attendanceModel.Absent = worksheet.Cells[i, 7].Value != null ? worksheet.Cells[i, 7].Value.ToString() : " ";

                                attendanceList.Add(attendanceModel);
                            }

                            if (attendanceList?.Count() > 0)
                            {

                                excelUploadRecordCountDto = await _service.ProcessStaffAttendanceFromExcel(attendanceList);
                            }
                        }
                        else
                        {
                            return BadRequest(new { Message = "Excel Sheet is empty and Invalid" });
                        }
                    }

                    return Ok(new
                    {
                        Message = "Excel Sheet was Uploaded successfully." + excelUploadRecordCountDto.SuccessfullUpload + " : Success and" +excelUploadRecordCountDto.FailedUpload + " : Failed.",
                        Success = excelUploadRecordCountDto.SuccessfullUpload,
                        Failed = excelUploadRecordCountDto.FailedUpload,
                        Updated = excelUploadRecordCountDto.UpdatedRecords,
                        excelUploadRecordCountDto.FailedAttendanceUpload
                        
                    });
                }

                return BadRequest(new { Message = "Oops...something went wrong" });
            }
            catch (Exception ex) { throw ex; }
        }



        [HttpPost("[action]")]
        public async Task<IActionResult> ProcessStaffBiometricInfoFromExcel(IFormFile file)
        {
            ExcelUploadRecordCountDto excelUploadRecordCountDto = new ExcelUploadRecordCountDto();
            try
            {
                long size = file.Length;
                if (size > 0)
                {
                    List<BiometricExcelModel> biometricsList = new List<BiometricExcelModel>();

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
                                BiometricExcelModel biometricsModel = new BiometricExcelModel();

                                biometricsModel.BiometricNumber = worksheet.Cells[i, 1].Value != null ? worksheet.Cells[i, 1].Value.ToString() : " ";
                                biometricsModel.Username = worksheet.Cells[i, 2].Value != null ? worksheet.Cells[i, 2].Value.ToString() : " ";
                                biometricsModel.Surname = worksheet.Cells[i, 4].Value != null ? worksheet.Cells[i, 4].Value.ToString() : " ";
                                biometricsModel.Firstname = worksheet.Cells[i, 3].Value != null ? worksheet.Cells[i, 3].Value.ToString() : " ";
                                biometricsModel.Othername = worksheet.Cells[i, 5].Value != null ? worksheet.Cells[i, 5].Value.ToString() : " ";


                                biometricsList.Add(biometricsModel);
                            }

                            if (biometricsList?.Count() > 0)
                            {

                                excelUploadRecordCountDto = await _service.ProcessStaffBiometricInfoFromExcel(biometricsList);
                            }
                        }
                        else
                        {
                            return BadRequest(new { Message = "Excel Sheet is empty and Invalid" });
                        }
                    }

                    return Ok(new
                    {
                     
                        Failed = excelUploadRecordCountDto.FailedUpload,
                        Updated = excelUploadRecordCountDto.UpdatedRecords,
                        excelUploadRecordCountDto.FailedBiometricsUpload

                    });
                }

                return BadRequest(new { Message = "Oops...something went wrong" });
            }
            catch (Exception ex) { throw ex; }
        }

        [HttpGet("[action]")]
        public async Task<CustomOkObject> GetAttendanceReportByDepartmentAndDate(DateTime From, DateTime To, long DepartmentId) => await _service.GetAttendanceReportByDepartmentAndDate(From, To, DepartmentId);

    }
}



