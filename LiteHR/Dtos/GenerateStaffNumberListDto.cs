using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class GenerateStaffNumberListDto
    {
        public string StaffName { get; set; }
        public string Department { get; set; }
        public long ?DepartmentId { get; set; }
        public string DefaultDepartmentName { get; set; }
        public string Rank { get; set; }
        public string SalaryCategory { get; set; }
        public string SalaryLevel { get; set; }
        public string SalaryStep { get; set; }
        public string StaffNumber { get; set; }
        public string Comment { get; set; } = "Assumption of Duty as";
        public DateTime? DateOfAssumption { get; set; }
        public long ?StateId { get; set; }
        public long ?LgaId { get; set; }
        public long ?GenderId { get; set; }
        public long StaffId { get; set; }
        public string PhoneNumber { get; set; }
        public string HeadRep { get; set; }
    }
}
