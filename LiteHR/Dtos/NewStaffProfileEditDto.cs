using LiteHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class NewStaffProfileEditDto
    {
        //public Staff Staff { get; set; }

        //Person

        public string Surname { get; set; }
        public string Firstname { get; set; }
        public string Othername { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public long StateId { get; set; }
        public long? LGAId { get; set; }
        public long MaritalStatusId { get; set; }
        public long ReligionId { get; set; }
        public long GenderId { get; set; }

        //Staff
       
        public long PersonId { get; set; }
        public string StaffNumber { get; set; }
        public long StaffId { get; set; }
        public long RankId { get; set; }
        public long DepartmentId { get; set; }
        public long AppointmentId { get; set; }
        public long InstitutionId { get; set; }
        public long StaffTypeId { get; set; }
        public long? CategoryId { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public DateTime? DateOfConfirmation { get; set; }
        public DateTime? DateOfRetirement { get; set; }
        public long AppointmentTypeId { get; set; }
        public long PfaNameId { get; set; }
        public long PfaStatusId { get; set; }
        public long AreaOfSpecializationId { get; set; }
        public string RSANumber { get; set; }
        public string BiometricNumber { get; set; }


        public List<PersonEducationsDto> PersonEducations { get; set; }


    }

    public class PersonEducationsDto
    {
        public long Id { get; set; }
        public long PersonId { get; set; }
        public long QualificationId { get; set; }
        public string InstitutionName { get; set; }
        public string CourseOfStudy { get; set; }
        public int Year { get; set; }
    }

}
