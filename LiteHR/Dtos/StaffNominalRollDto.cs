using LiteHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class StaffNominalRollDto
    {
        public string StaffName { get; set; }
        //public string OtherName { get; set; }
        //public string Surname { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string StaffIdentityNumber { get; set; }
        public DateTime? StaffDOB { get; set; }
        public string StaffRank { get; set; }
        public string StaffDepartment { get; set; }
        public long StaffId { get; set; }
        public string StaffType { get; set; }
        public string StaffCategory { get; set; }
        public string StaffSalaryCategory { get; set; }
        public string Rank { get; set; }
        public bool IsCleared { get; set; }
        public long Month { get; set; }
        public string Year { get; set; }
        public string Comments { get; set; }
        public string AppointmentType { get; set; }
        public string Phone { get; set; }
        public string State { get; set; }
        public string LGA { get; set; }
        public DateTime? DateOfEmployment { get; set; }
        public DateTime? DateOfRetirement { get; set; }
        public string PFAName { get; set; }
        public string PFAStatus { get; set; }
        public string AreaOfSpecialization { get; set; }
        public List<PersonEducationDtoNew> PersonEducations { get; set; }



        public class PersonEducationNominal : BaseModel
        {
            public long PersonId { get; set; }
            public string Institution { get; set; }
            public string Course { get; set; }
            public string QualificationName { get; set; }
            public int Year { get; set; }
            public long EducationalQualificationId { get; set; }
        }
        //public bool IsComment { get; set; }


    }
}
