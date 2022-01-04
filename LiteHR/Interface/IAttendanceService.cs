using LiteHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Interface
{
    public interface IAttendanceService
    {
        Task ProcessAttendanceData(IEnumerable<AttendanceExcel> attendanceExcelData);
        Task<Object> GetMonthlyAttendanceData();
        Task<Object> GetMonthlyAttendanceDataByStaff(Staff staff);
        Task<Reporting> GetStaffAttendanceMonthly(string yearMonth,string MonthString, int validWorkingDays, int noHolidays, int departmentId);

    }
}
