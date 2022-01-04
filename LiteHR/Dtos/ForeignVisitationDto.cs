using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class ForeignVisitationDto
    {
        public string FullName { get; set; }
        public string Surname { get; set; }
        public string Firstname { get; set; }
        public string Othername { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public long? GenderId { get; set; }
        public long Id { get; set; }

        public string AddressSecond { get; set; }

        public string PassportNumber { get; set; }

        public string ProgrammeType { get; set; }

        public string AcademicYear { get; set; }

        //public long? FacultyId { get; set; }
        public long? InstitutionDepartmentId { get; set; }
        public int? DurationOfStay { get; set; }
        public DateTime? DegreeAwardDate { get; set; }

        public string AwardingInstitution { get; set; }

        public long? CurrentInstitutionDepartmentId { get; set; }
        //public long? CurrentInstitutionFacultyId { get; set; }
        public int? CurrentYearOfStudy { get; set; }

        public string CurrentExpectedQualificationYear { get; set; }
        public string Reason { get; set; }

        public string SponsorshipType { get; set; }

        public string SponsorshipOrganization { get; set; }
        public string ApplicationNumber { get; set; }
        public bool? isApproved { get; set; }
        public string NextOfKinFullname { get; set; }
        public string NextOfKinEmail { get; set; }
        public string NextOfKinPhone { get; set; }
        public string NextOfKinAddress { get; set; }
        public string ImageUrl { get; set; }
        public long VisitationTypeId { get; set; }
        public string VisitationType { get; set; }
        public string InstitutionDepartment { get; set; }
        public string CurrentInstitutionDepartment { get; set; }

    }
}
