using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class StaffSalaryDto
    {
        public long Amount { get; set; }
        public string AmountInWords { get; set; }
        public string StaffSalaryDetails { get; set; }
        //public DateTime PayDate { get; set; }
    }
}
