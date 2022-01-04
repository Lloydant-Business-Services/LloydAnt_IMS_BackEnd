using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class AttendanceExcel
    {
        public string Date { get; set; }
        public string EmployeSerial { get; set; }
        public string EmployeeNumber { get; set; }
        public string AccNumber { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string ClockIn { get; set; }
        public string ClockOut { get; set; }
        public string Absent { get; set; }
    }
}
