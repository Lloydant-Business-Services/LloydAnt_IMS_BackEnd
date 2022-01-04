using LiteHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class SalaryGradeDto : BaseModel
    {
        public long SalaryGradeCategoryId { get; set; }
        public string SalaryGradeCategoryName { get; set; }
        public string SalaryStepName { get; set; }
        public string SalaryLevelName { get; set; }

        public long SalaryStepId { get; set; }

        public long SalaryLevelId { get; set; }

        public decimal? Amount { get; set; }

        public bool Active { get; set; }
    }
}
