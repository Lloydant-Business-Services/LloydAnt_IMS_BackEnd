using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class FailedStaffUploads : BaseModel
    {
        public string Surname { get; set; }
        public string Firstname { get; set; }
        public string Othername { get; set; }
        public string StaffNumber { get; set; }
        public string DOB { get; set; }
        public string DateOfConfirmation { get; set; }
        public string PhoneNumber { get; set; }
        public long InstitutionDepartmentId { get; set; }
        public string DateOfLastPromotion { get; set; }
        public string Gender { get; set; }
        public string AppointmentType { get; set; }
        public string SalaryGradeCategory { get; set; }
        public long SalaryLevelId { get; set; }
        public long SalaryStepId { get; set; }
        public long InstitutionRankId { get; set; }
        public string DOE { get; set; }
        public bool Active { get; set; }
        
    }
}
