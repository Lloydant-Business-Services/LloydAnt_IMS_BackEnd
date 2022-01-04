using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LiteHR.Models;
using Microsoft.AspNetCore.Authorization;
using OfficeOpenXml;
using System.IO;
using LiteHR.Interface;

namespace LiteHR.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _service;

        public AttendanceController(IAttendanceService service)
        {
            _service = service;
        }

        // GET: api/LeaveRequest/5
        [HttpGet("AttendanceByMonth")]
        public async Task<ActionResult<Object>> GetAttendanceByMonth()
        {
            return await _service.GetMonthlyAttendanceData();
        }
        [HttpGet("AttendanceMonthlyReport")]
        public async Task<ActionResult<Object>> GetAttendanceMonthlyReport(int monthFigure, int yearFigure, string monthWord, int holidayCount, int departmentId)
        {
            Reporting reporting = new Reporting();
            if (monthFigure>0 && yearFigure>0 && !string.IsNullOrEmpty(monthWord))
            {
                var monthYear = "";
                if (monthFigure.ToString().Length == 1)
                {
                    monthYear = yearFigure + "-" +"0"+ monthFigure;
                }
                else
                {
                    monthYear = yearFigure + "-" + monthFigure;
                }
                //get no of workingDays in selected month and year
                var yearMonthConversion=DateTime.DaysInMonth(yearFigure, monthFigure);
                var weekenedDays=Enumerable.Range(1, yearMonthConversion)
                    .Select(n => new DateTime(yearFigure, monthFigure, n))
                    .Where(date => (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)).ToList();
                var validWorkingDays = yearMonthConversion - weekenedDays.Count();
                reporting =await _service.GetStaffAttendanceMonthly(monthYear,monthWord, validWorkingDays, holidayCount, departmentId);
            }
            return reporting;
        }
        
        // GET: api/LeaveRequest/5
        [HttpGet("AttendanceByStaff/{id}")]
        public async Task<ActionResult<Object>> GetAttendanceByMonthForStaffId(long id)
        {
            return await _service.GetMonthlyAttendanceDataByStaff(new Staff { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult> ReadExcelFile(IFormFile file)
        {
            List<AttendanceExcel> attendanceList = new List<AttendanceExcel>();

            try
            {
               
                long size = file.Length;
                
                if (file.Length > 0)
                {
                    var filePath = Path.GetTempFileName();
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await file.CopyToAsync(stream);

                        //FileInfo fileInfo = new FileInfo(filePath);
                        ExcelPackage package = new ExcelPackage(stream);
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();

                        if (worksheet != null)
                        {
                            int totalRows = worksheet.Dimension.Rows;

                            for (int i = 2; i <= totalRows; i++)
                            {
                                attendanceList.Add(new AttendanceExcel
                                {
                                    Date = worksheet.Cells[i, 1].Value.ToString(),
                                    EmployeSerial = worksheet.Cells[i, 2].Value.ToString(),
                                    AccNumber = worksheet.Cells[i, 3].Value.ToString(),
                                    EmployeeNumber = worksheet.Cells[i, 4].Value.ToString(),
                                    Name = worksheet.Cells[i, 5].Value.ToString(),
                                    Department = worksheet.Cells[i, 6].Value.ToString(),
                                    ClockIn = worksheet.Cells[i, 7].Value.ToString(),
                                    ClockOut = worksheet.Cells[i, 8].Value.ToString(),
                                    Absent = worksheet.Cells[i, 9].Value.ToString()
                                });
                            }

                        }

                    }

                }
                await  _service.ProcessAttendanceData(attendanceList);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok(attendanceList);
        }
    }
}