using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class SalaryGrade: BaseModel
    {
        public long SalaryGradeCategoryId { get; set; }

        public SalaryGradeCategory SalaryGradeCategory { get; set; }

        public long SalaryStepId { get; set; }

        public SalaryStep SalaryStep { get; set; }

        public long SalaryLevelId { get; set; }

        public SalaryLevel SalaryLevel { get; set; }
        public decimal? Amount { get; set; }

        public bool Active { get; set; }
    }
}
