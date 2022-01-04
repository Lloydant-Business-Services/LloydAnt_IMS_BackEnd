using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class StaffSalaryCategoryReference:BaseModel
    {
        public long StaffId { get; set; }
        public Staff Staff { get; set; }
        public long? SalaryGradeCategoryId { get; set; }
        public SalaryGradeCategory SalaryGradeCategory { get; set; }
        public long? SalaryLevelId { get; set; }
        public SalaryLevel SalaryLevel { get; set; }
        public long? SalaryStepId { get; set; }
        public SalaryStep SalaryStep { get; set; }
        public DateTime DatePromoted { get; set; }
    }
}
