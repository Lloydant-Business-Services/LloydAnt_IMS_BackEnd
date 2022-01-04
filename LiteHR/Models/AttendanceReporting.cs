using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class AttendanceReporting
    {
        public int SN { get; set; }
        public string FullName { get; set; }
        public string StaffNo { get; set; }
        public int EarlyCount { get; set; }
        public int LateCount { get; set; }
        public int PresentCount { get; set; }
        public int AbsentCount { get; set; }
        public string AttendancePercentage { get; set; }
    }
    public class Reporting
    {
        public string Department { get; set; }
        public string Period { get; set; }
        public int HolidayCount { get; set; }
        public int ValidWorkingDays { get; set; }
        public string LeastPunctual { get; set; }
        public string MostPunctual { get; set; }

        public List<AttendanceReporting> AttendanceReportings { get; set; }
    }
}
