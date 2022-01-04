using LiteHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class StaffGradeBenefitDto
    {
        public Staff Staff { get; set; }

        public InstitutionDepartment Department { get; set; }
        public SalaryLevel SalaryLevel { get; set; }
        public SalaryStep SalaryStep { get; set; }
        public SalaryGradeCategory SalaryGradeCategory { get; set; }


        public List<GradeBenefit> GradeBenefits { get; set; }
        public List<SalaryExtraEarning> SalaryExtraEarnings { get; set; }
    }
}
