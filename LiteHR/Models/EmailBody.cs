using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class EmailBody:BaseModel
    {
        public string Subject { get; set; }
        public string Message { get; set; }
    }
    public class SalaryGradeBenefitModel
    {
        public long SalaryCategoryId { get; set; }
        public long SalaryStepId { get; set; }
        public long SalaryLevelId { get; set; }
        public List<long> SalaryTypeModels { get; set; }
    }
    public class SalaryGradeModel
    {
        public SalaryGrade SalaryGrade { get; set; }
        public List<GradeBenefit> GradeBenefit { get; set; } = new List<GradeBenefit>();
    }


}
