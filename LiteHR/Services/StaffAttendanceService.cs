//using LiteHR.Interface;
//using LiteHR.Models;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace LiteHR.Services
//{
//    public class StaffAttendanceService : IAttendanceService
//    {

//        private readonly HRContext _context;
//        private readonly IUserService _userService;
//        public StaffAttendanceService(HRContext context, IUserService userService)
//        {
//            _context = context;
//            _userService = userService;
//        }

//        public async Task<Object> GetMonthlyAttendanceData()
//        {
//            //Removed Checkout temp
//            var AttendanceReport = (from a in _context.STAFF_ATTENDANCE.Where(x => x.CheckIn != null) select a)
//             .GroupBy(x => x.CheckIn.Value.Date).Select(g => new { Date = (g.Key).Date.ToString(), Count = g.Count() });

//            return AttendanceReport;
//        }

//        public async Task<Object> GetMonthlyAttendanceDataByStaff(Staff staff)
//        { 
//            //Removed Checkout temp
//            var AttendanceReport = (from a in _context.STAFF_ATTENDANCE
//                                    .Where(x => x.CheckIn != null && x.StaffId == staff.Id)
//                                    select a)
//             .GroupBy(x => x.CheckIn.Value.Date).Select(g => new { Date = (g.Key).Date.ToString(), Count = g.Count() });
//            return AttendanceReport;

//        }

//        public async Task ProcessAttendanceData(IEnumerable<AttendanceExcel> attendanceExcelData)
//        {

//            //Loop through records
//            foreach (var record in attendanceExcelData)
//            {
//                if (!string.IsNullOrEmpty(record.EmployeeNumber))
//                {
//                    var staff = await _context.STAFF.Where(s => s.StaffNumber == record.EmployeeNumber).SingleOrDefaultAsync();
//                    if (staff != null)
//                    {
//                        await saveAttendance(record, staff);
//                    }
//                    else
//                    {
//                        staff = new Staff();
//                        staff.StaffNumber = record.EmployeeNumber;
//                        var department = await _context.INSTITUTION_DEPARTMENT.Where(d => d.Name.ToLower() == record.Department.ToLower()).SingleOrDefaultAsync();
//                        if (department != null)
//                        {
//                            staff.DepartmentId = department.Id;
//                        }
//                        else
//                        {
//                            staff.Department = new InstitutionDepartment() { Name = record.Department.ToLower() };
//                        }
//                        var names = record.Name.Split(" ");
//                        if (names != null && names.Length > 0)
//                        {

//                            staff.Person = new Person()
//                            {
//                                ReligionId = 1,
//                                Address = "--",
//                                PhoneNumber = "0",
//                                Surname = names[0],
//                                Firstname = names[names.Length-1],
//                            };

//                            User user = new User()
//                            {
//                                Username = record.EmployeeNumber,
//                                Password = "1234567",
//                                RoleId = 1,
//                                Staff = staff
//                            };

//                            await _userService.Create(user, "1234567");
//                            if (staff.Id > 0)
//                            {
//                                await saveAttendance(record, staff);
//                            }

//                        }
//                    }

//                    await _context.SaveChangesAsync();
//                }

//            }

//        }

//        private async Task saveAttendance(AttendanceExcel record, Staff staff)
//        {
//            System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");
//            var checkIn = DateTime.Parse(record.Date + " " + record.ClockIn, cultureinfo);
//            var checkOut = DateTime.Parse(record.Date + " " + record.ClockOut, cultureinfo);
//            StaffAttendance attendance = new StaffAttendance();
//            if (!string.IsNullOrEmpty(record.ClockIn) && !string.IsNullOrEmpty(record.ClockOut))
//            {
//                attendance = await _context.STAFF_ATTENDANCE.Where(a =>a.CheckOut.Value.Date == checkOut.Date && a.CheckIn.Value.Date == checkIn.Date && a.StaffId == staff.Id).FirstOrDefaultAsync();
//            }
//            else if(!string.IsNullOrEmpty(record.ClockOut) && string.IsNullOrEmpty(record.ClockIn))
//            {
//                attendance = await _context.STAFF_ATTENDANCE.Where(a => a.CheckOut.Value.Date == checkOut.Date  && a.StaffId == staff.Id).FirstOrDefaultAsync();
//            }
//            else if(string.IsNullOrEmpty(record.ClockOut) && !string.IsNullOrEmpty(record.ClockIn))
//            {
//                attendance = await _context.STAFF_ATTENDANCE.Where(a => a.CheckIn.Value.Date == checkIn.Date && a.StaffId == staff.Id).FirstOrDefaultAsync();
//            }
            
            
//            if (attendance == null)
//            {

//                attendance = new StaffAttendance();
//                attendance.StaffId = staff.Id;
//                if (!string.IsNullOrEmpty(record.ClockIn))
//                {
//                    attendance.CheckIn = checkIn;
//                }
//                if (!string.IsNullOrEmpty(record.ClockOut))
//                {
//                    attendance.CheckOut = checkOut;
//                }
//                _context.Add(attendance);
//            }
//        }

//        public async Task<Reporting> GetStaffAttendanceMonthly(string yearMonth, string MonthString,int validWorkDays, int noOfHolidays, int departmentId)
//        {
//            Reporting reporting = new Reporting();
//            reporting.AttendanceReportings = new List<AttendanceReporting>();
//            try
//            {
//                var staffAttendance=await _context.STAFF_ATTENDANCE.Where(m => m.Staff.Department.Id == departmentId && (m.CheckIn.Value.ToString().StartsWith(yearMonth)) || (m.CheckOut.Value.ToString().StartsWith(yearMonth)))
//                    .Include(M=>M.Staff)
//                    .ThenInclude(m=>m.Department)
//                    .Include(m=>m.Staff.Person)
//                    .ToListAsync();
//                if (staffAttendance?.Count > 0)
//                {
                    
//                    var groupData=staffAttendance.GroupBy(m => m.Staff).ToList();
//                    foreach (var item in groupData)
//                    {
//                        reporting.Department = item.Key.Department.Name;
//                        reporting.Period = MonthString;
//                        reporting.HolidayCount = noOfHolidays;
//                        reporting.ValidWorkingDays = validWorkDays - noOfHolidays;
//                        TimeSpan earlyTime = TimeSpan.Parse("08:30");
//                        AttendanceReporting attendanceReporting = new AttendanceReporting();
//                        var va=staffAttendance.FirstOrDefault().CheckIn.Value;
//                        attendanceReporting.AbsentCount = staffAttendance.Where(m => m.StaffId == item.Key.Id && (m.CheckIn == null || m.CheckOut == null)).ToList().Count();
//                        attendanceReporting.PresentCount = staffAttendance.Where(m => m.StaffId == item.Key.Id && (m.CheckIn != null && m.CheckOut != null)).ToList().Count();
//                        //attendanceReporting.EarlyCount = staffAttendance.Where(m => m.StaffId == item.Key.Id && m.CheckIn.Value.TimeOfDay <= earlyTime).ToList().Count();
//                        attendanceReporting.FullName = item.Key.Person?.Surname + " " + item.Key.Person?.Firstname + " " + item.Key.Person?.Othername;
//                        attendanceReporting.StaffNo = item.Key.StaffNumber;
//                        decimal pecentagePresent = ((attendanceReporting.PresentCount *100) / (reporting.ValidWorkingDays));
//                        attendanceReporting.AttendancePercentage = pecentagePresent + "%";
//                        reporting.AttendanceReportings.Add(attendanceReporting);
//                        var sortedList=reporting.AttendanceReportings.OrderBy(m => m.EarlyCount).ToList();
//                        if (sortedList.FirstOrDefault()?.EarlyCount > 0)
//                        {
//                            reporting.MostPunctual = sortedList.FirstOrDefault()?.FullName;
//                        }
                        
//                        reporting.LeastPunctual = sortedList.LastOrDefault()?.FullName;
//                    }
//                }
//            }
//            catch (Exception ex)
//            {

//                throw ex;
//            }
//            return reporting;
//        }

//    }
//}
