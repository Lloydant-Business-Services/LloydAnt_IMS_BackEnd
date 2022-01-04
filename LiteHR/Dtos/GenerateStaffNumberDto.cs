using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class GenerateStaffNumberDto
    {
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string OtherName { get; set; }
        public long StaffTypeId { get; set; }
        public long StaffCategoryId { get; set; }
        public long StaffRankId { get; set; }
        public long AppointmentId { get; set; }
        public long AppointmentTypeId { get; set; }
        public long DepartmentId { get; set; }
        public long SalaryCategoryId { get; set; }
        public long SalaryLevelId { get; set; }
        public long SalaryStepId { get; set; }
        public DateTime DateOfAssumption { get; set; }
    }
}
