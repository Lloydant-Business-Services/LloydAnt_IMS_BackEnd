using LiteHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class StaffSalaryReportDto
    {

        public long Total { get; set; }
        public List<SalaryLoad> SalaryReport { get; set; }
        
    }
    public class SalaryLoad
    {
        public string StaffName { get; set; }
        public string Department { get; set; }
        public string SalaryGrade { get; set; }
        public decimal? Amount { get; set; }
        //public string AmountInWords { get; set; }
    }
}
