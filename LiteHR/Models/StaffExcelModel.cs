using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class StaffExcelModel
    {
        public long DepartmentId { get; set; }

        public string Surname { get; set; }

        public string FirstName { get; set; }

        public string OtherName { get; set; }

        public string DOB { get; set; }
        //public string DateofAssumption { get; set; }

        public string DateOfConfirmation { get; set; }

        //public string Address { get; set; }

        public string PhoneNumber { get; set; }

        //public string StateName { get; set; }

        public string DateOfLastPromotion { get; set; }
        public string DateOfEmployment { get; set; }

        public string Gender { get; set; }

        public string StaffNumber { get; set; }
        public string SalaryCategory { get; set; }
        public long Level { get; set; }
        //public string AppointmentType { get; set; }
        public long Step { get; set; }
        public long Rank { get; set; }
        public long QualificationId { get; set; }
        //public long PFAStatusId { get; set; }
        //public long AreaOfSpecializationId { get; set; }
        public string RSANumber { get; set; }
        public string GeneratedStaffNumber { get; set; }
    }
}