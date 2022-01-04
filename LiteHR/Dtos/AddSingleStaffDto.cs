using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class AddSingleStaffDto
    {
        public long StateId { get; set; }
        public long LgaId { get; set; }
        public long GenderId { get; set; }
        public long ReligionId { get; set; }
        public long MaritalStatusId { get; set; }
        public long DepartmentId { get; set; }
        public long AppointmentTypeId { get; set; }
        public DateTime DateOfConfirmation { get; set; }
        public DateTime DateOfAppointment { get; set; }
        public DateTime DoB { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string OtherName { get; set; }
        public string EmailAddress { get; set; }
        public string CourseOfStudy { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public string StaffNumber { get; set; }
        public long RankId { get; set; }
        public long CategoryId { get; set; }
        public long AppointmentId { get; set; }
        public long StaffTypeId { get; set; }
        public bool IsConfirmed { get; set; }
        public string InstitutionOfQualification { get; set; }
        public int Year { get; set; }
        public long QualificationId { get; set; }
        public long SalaryCategoryId { get; set; }
        public long SalaryLevelId { get; set; }
        public long SalaryStepId { get; set; }
    }
}
