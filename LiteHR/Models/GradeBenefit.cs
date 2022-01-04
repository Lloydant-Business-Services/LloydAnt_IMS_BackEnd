using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class GradeBenefit: BaseModel
    {
        public long SalaryGradeId { get; set; }

        public SalaryGrade SalaryGrade { get; set; }

        public long SalaryTypeId { get; set; }

        public SalaryType SalaryType { get; set; }

        public bool Active { get; set; }
    }
}
