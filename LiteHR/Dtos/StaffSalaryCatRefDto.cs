using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class StaffSalaryCatRefDto
    {
        public long StaffId { get; set; }
        public long? SalaryCategoryId { get; set; }
        public long? SalaryLevelId { get; set; }
        public long? SalaryStepId { get; set; }
        public string SalaryCategory { get; set; }
        public string SalaryLevel { get; set; }
        public string SalaryStep { get; set; }
    }
}
