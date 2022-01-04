using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class ForeignVisitation:BaseModel
    {
        public long PersonId { get; set; }
        public Person Person { get; set; }

        [MaxLength(200)]
        public string AddressSecond { get; set; }

        [MaxLength(100)]
        public string PassportNumber { get; set; }

        [MaxLength(100)]
        public string ProgrammeType { get; set; }

        [MaxLength(20)]
        public string AcademicYear { get; set; }

        public long? FacultyId { get; set; }
        public Faculty Faculty { get; set; }
        public long? InstitutionDepartmentId { get; set; }
        public InstitutionDepartment InstitutionDepartment { get; set; }
        public int? DurationOfStay { get; set; }
        public DateTime? DegreeAwardDate { get; set; }

        [MaxLength(100)]
        public string AwardingInstitution { get; set; }

        public long? CurrentInstitutionDepartmentId { get; set; }
        public InstitutionDepartment CurrentInstitutionDepartment { get; set; }
        public long? CurrentInstitutionFacultyId { get; set; }
        public VisitationType VisitationType { get; set; }
        public long VisitationTypeId { get; set; }
        public Faculty CurrentInstitutionFaculty { get; set; }
        public int? CurrentYearOfStudy { get; set; }

        [MaxLength(50)]
        public string CurrentExpectedQualificationYear { get; set; }
        public string Reason { get; set; }

        [MaxLength(50)]
        public string SponsorshipType { get; set; }

        [MaxLength(100)]
        public string SponsorshipOrganization { get; set; }
        public string ApplicationNumber { get; set; }
        public string ImageUrl { get; set; }
        public bool? isApproved { get; set; }
        public bool? Active { get; set; }




    }
}
