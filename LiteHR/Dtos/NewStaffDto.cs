using LiteHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class NewStaffDto
    {
        public string StaffFullName { get; set; }
        public string FirstName { get; set; }
        public string OtherName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string State { get; set; }
        public string Lga { get; set; }
        public string Phone { get; set; }
        public string StaffIdentityNumber { get; set; }
        public string BiometricNumber { get; set; }
        public DateTime? StaffDOB { get; set; }
        public DateTime? DateOfRetirement { get; set; }
        public DateTime? DateOfEmployment { get; set; }
        public string StaffRank { get; set; }
        public string StaffDepartment { get; set; }
        public string AppointmentType { get; set; }
        public long StaffId { get; set; }
        public string StaffType { get; set; }
        public string StaffCategory { get; set; }
        public string SalaryCategory { get; set; }
        public bool IsDisengaged { get; set; }
        public bool IsRetired { get; set; }
        public long status { get; set; }
        public List<PersonEducationDtoNew> PersonEducations { get; set; }



    }
    public class PersonEducationDtoNew : BaseModel
    {
        public long PersonId { get; set; }
        public string Institution { get; set; }
        public string Course { get; set; }
        public string QualificationName { get; set; }
        public int Year { get; set; }
        public long EducationalQualificationId { get; set; }
    }

}