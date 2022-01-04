using LiteHR.Dtos;
using LiteHR.Infrastructure;
using LiteHR.Interface;
using LiteHR.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LiteHR.Services
{
    public class ServicomService : IServicomService
    {
        private readonly HRContext _context;
        //private readonly ControllerBase _base;

        public ServicomService(HRContext context)
        {
            _context = context;
            //_base = c_base;

        }


        public async Task<ExcelUploadRecordCountDto> ProcessStaffAttendanceFromExcel(IEnumerable<AttendanceExcelModel> attendanceModel)
        {
            ExcelUploadRecordCountDto excelUploadRecordCountDto = new ExcelUploadRecordCountDto();
            StaffAdditionalInfo staffAdditionalInfo = new StaffAdditionalInfo();
            excelUploadRecordCountDto.SuccessfullUpload = 0;
            excelUploadRecordCountDto.FailedUpload = 0;
            excelUploadRecordCountDto.UpdatedRecords = 0;
        
            List<FailedAttendanceUpload> failedList = new List<FailedAttendanceUpload>();

            try
            {
                if (attendanceModel?.Count() > 0)
                {
                    foreach (AttendanceExcelModel staff in attendanceModel)
                    {
                        TimeSpan clockIn = new TimeSpan();
                        TimeSpan clockOut = new TimeSpan();
                        DateTime dateEntered = new DateTime();

                        var enteredDate = Regex.Replace(staff.Date, "[ ().-]+", "");
                        var clockInDate = Regex.Replace(staff.ClockIn, "[ ().-]+", "");
                        var clockOutDate = Regex.Replace(staff.ClockOut, "[ ().-]+", "");
                        var absentStatus = Regex.Replace(staff.Absent, @"s", "");
                        var biometricNumber = Regex.Replace(staff.BiometricNo, @"s", "");
                        if (!String.IsNullOrWhiteSpace(clockInDate))
                        {
                           clockIn = TimeSpan.Parse(clockInDate);
                        }
                        if ((!String.IsNullOrWhiteSpace(clockOutDate)))
                        {
                            clockOut = TimeSpan.Parse(clockOutDate);
                        }
                        
                        if (!String.IsNullOrWhiteSpace(enteredDate))
                        {
                            //dateEntered = DateTime.Parse(enteredDate);
                            bool isValid = ValidateDate(enteredDate);
                            if (isValid)
                            {
                                dateEntered = ConvertToDate(enteredDate);

                            }
                        }
                        else
                        {
                            dateEntered = new DateTime();
                        }
                        
                        bool isAbsent;
                        if (staff.Absent == "True" || staff.Absent == "TRUE")
                        {
                            isAbsent = true;
                        }
                        else
                        {
                            isAbsent = false;
                        }

                        var staffSelector = !String.IsNullOrWhiteSpace(biometricNumber) ? await _context.STAFF.Where(s => s.BiometricId == biometricNumber).FirstOrDefaultAsync() : null;
                        var doesExist = staffSelector != null ? await _context.STAFF_ATTENDANCE.Where(s => s.StaffId == staffSelector.Id && s.Date == dateEntered).FirstOrDefaultAsync() : null;

                       
                        if (staffSelector == null)
                        {
                            FailedAttendanceUpload failedAttendanceUpload = new FailedAttendanceUpload();

                            failedAttendanceUpload.BiometricNo = staff.BiometricNo;
                            failedAttendanceUpload.StaffName = staff.StaffName;

                            failedList.Add(failedAttendanceUpload);
                            excelUploadRecordCountDto.FailedUpload += 1;


                        }
                        // UPDATE Record if exists
                        else if (staffSelector != null && doesExist != null)
                        {
                        
                                doesExist.Date = dateEntered;
                                doesExist.ClockIn = clockIn;
                                doesExist.ClockOut = clockOut;
                                doesExist.Absent = isAbsent;
                                _context.Update(doesExist);
                                await _context.SaveChangesAsync();

                                excelUploadRecordCountDto.UpdatedRecords += 1;
                         

                        }

                        else
                        {
                            StaffAttendance staffAttendance = new StaffAttendance();
                            staffAttendance.Date = dateEntered;
                            staffAttendance.ClockIn = clockIn;
                            staffAttendance.ClockOut = clockOut;
                            staffAttendance.Staff = staffSelector;
                            staffAttendance.Absent = isAbsent;
                            _context.Add(staffAttendance);
                            await _context.SaveChangesAsync();

                            excelUploadRecordCountDto.SuccessfullUpload += 1;
                        }
                    }
                }
                excelUploadRecordCountDto.FailedAttendanceUpload = failedList;

            }
            catch (Exception ex) { 
                throw ex; 
            }
            return excelUploadRecordCountDto;
        }






        public async Task<ExcelUploadRecordCountDto> ProcessStaffBiometricInfoFromExcel(IEnumerable<BiometricExcelModel> biometricModel)
        {
            ExcelUploadRecordCountDto excelUploadRecordCountDto = new ExcelUploadRecordCountDto();
            StaffAdditionalInfo staffAdditionalInfo = new StaffAdditionalInfo();
            excelUploadRecordCountDto.SuccessfullUpload = 0;
            excelUploadRecordCountDto.FailedUpload = 0;
            excelUploadRecordCountDto.UpdatedRecords = 0;

            List<FaileStaffBiometricModel> failedList = new List<FaileStaffBiometricModel>();

            try
            {
                if (biometricModel?.Count() > 0)
                {
                    foreach (BiometricExcelModel staff in biometricModel)
                    {
                        
                        var biometricNumber = Regex.Replace(staff.BiometricNumber, @"s", "");
                        var username = Regex.Replace(staff.Username, @"s", "");
                        
                        var staffSelector = !String.IsNullOrWhiteSpace(username) ? await _context.STAFF.Where(s => s.GeneratedStaffNumber == username).FirstOrDefaultAsync() : null;

                        if (staffSelector == null)
                        {

                            FaileStaffBiometricModel failedBiometrics = new FaileStaffBiometricModel();

                            failedBiometrics.Username = staff.Username;
                            failedBiometrics.BiometricNumber = staff.BiometricNumber;
                            failedBiometrics.Firstname = staff.Firstname;
                            failedBiometrics.Surname = staff.Surname;
                            failedBiometrics.Othername = staff.Othername;

                            failedList.Add(failedBiometrics);
                            excelUploadRecordCountDto.FailedUpload += 1;


                        }
                        // UPDATE Record
                        else
                        {

                            staffSelector.BiometricId = biometricNumber;
                            _context.Update(staffSelector);
                            await _context.SaveChangesAsync();

                            excelUploadRecordCountDto.UpdatedRecords += 1;



                        }
                    }
                }
                excelUploadRecordCountDto.FailedBiometricsUpload = failedList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return excelUploadRecordCountDto;
        }

        public async Task<CustomOkObject> GetAttendanceReportByDepartmentAndDate(DateTime From, DateTime To, long DepartmentId)
        {
            StaffAttendance staffAttendance = new StaffAttendance();
            CustomOkObject okObject = new CustomOkObject();
            List<StaffAttendance> attendanceList = new List<StaffAttendance>();
            List<StaffAttendanceDto> dtoList = new List<StaffAttendanceDto>();
            List<long> isResolved = new List<long>();


            TimeSpan isEarly = TimeSpan.Parse("8:30");
            var attendanceSelector = await _context.STAFF_ATTENDANCE.Where(s => (s.Date >= From && s.Date <= To) && s.Staff.DepartmentId == DepartmentId)
                .Include(s => s.Staff)
                .ThenInclude(s => s.Person)
                .ToListAsync();
            var holidayCount = await _context.HOLIDAYS.ToListAsync();

            foreach (var resolvedId in attendanceSelector)
            {
                isResolved.Add(resolvedId.StaffId);
            }
            List<long> newIds = isResolved.Distinct().ToList();
           
            
                foreach(var item in newIds)
                {
                StaffAttendanceDto attendanceDto = new StaffAttendanceDto();

                var staffInfo = await _context.STAFF_ATTENDANCE.Where(s => s.StaffId == item && (s.Date >= From && s.Date <= To))
                     .Include(s => s.Staff)
                     .ThenInclude(s => s.Person)
                     .FirstOrDefaultAsync();
                        attendanceList = await _context.STAFF_ATTENDANCE.Where(s => s.StaffId == item && (s.Date >= From && s.Date <= To))
                    .Include(s => s.Staff)
                    .ThenInclude(s => s.Person)
                    .ToListAsync();
                     
                    var presentCount = await _context.STAFF_ATTENDANCE.Where(s => s.StaffId == item && (s.Date >= From && s.Date <= To) && !s.Absent).ToListAsync();
                        var absentCount = await _context.STAFF_ATTENDANCE.Where(s => s.StaffId == item && (s.Date >= From && s.Date <= To) && s.Absent).ToListAsync();

                        attendanceDto.StaffName = staffInfo?.Staff?.Person?.Surname + " " + staffInfo?.Staff?.Person?.Firstname + " " + staffInfo?.Staff?.Person?.Othername;

                        attendanceDto.Absent = absentCount.Count;
                        attendanceDto.Present = presentCount.Count;
                        foreach (var puntualStatus in attendanceList)
                        {
                            if (!puntualStatus.Absent && puntualStatus.ClockIn <= isEarly)
                            {
                                attendanceDto.Early += 1;
                            }
                            else if (!puntualStatus.Absent && puntualStatus.ClockIn >= isEarly)
                            {
                                attendanceDto.Late += 1;
                            }

                        }
                        dtoList.Add(attendanceDto);
                    }
                

            okObject.Holidays = holidayCount.Count;
            okObject.attendanceList = dtoList;

            return okObject;
            
            

        }


        public DateTime ConvertToDate(string date)
        {
            DateTime newDate = new DateTime();

            try
            {

                string[] dateSplit = date.Split('/');
                int year = Convert.ToInt16(dateSplit[2].Split(' ')[0]);
                newDate = new DateTime(year, Convert.ToInt16(dateSplit[1]), Convert.ToInt32(dateSplit[0]));
                //return newDate;

            }
            catch (Exception ex) { throw ex; }



            return newDate;
        }

        public bool ValidateDate(string date)
        {
            //string dateStrings = ("02/13/2020");
            //DateTime dateValue;

            var seperator = "/";
            int slashCount = date.Split(seperator).Length - 1;
            if ((date.Length == 10 || date.Length == 9 || date.Length == 8) && date.Substring(0, 2) != "00" && slashCount == 2 && !String.IsNullOrEmpty(date))
            {

                return true;



            }
            return false;

        }
    }
}
