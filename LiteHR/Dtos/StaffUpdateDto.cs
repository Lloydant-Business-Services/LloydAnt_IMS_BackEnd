using LiteHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    //public class StaffSalaryRefDto
    //{
    //    public long SalaryCategoryId { get; set; }
    //    public long SalaryLevelId { get; set; }
    //    public long SalaryStepId { get; set; }

    //}

    public class StaffUpdateDto
    {
        public Staff Staff { get; set; }
        public long PfaNameId { get; set; }
        public long PfaStatusId { get; set; }
        public long AreaOfSpecializationId { get; set; }
        public string RSANumber { get; set; }

        
        public List<PersonEducationDto> PersonEducations { get; set; }
        //public long SalaryCategoryId { get; set; }
        //public long SalaryLevelId { get; set; }
        //public long SalaryStepId { get; set; }
        //public List<StaffSalaryRefDto> StaffSalaryRefs { get; set; }
    }
    public class PersonEducationDto
    {
        public long Id { get; set; }
        public long PersonId { get; set; }
        public long QualificationId { get; set; }
        
        public string InstitutionName { get; set; }
        public string CourseOfStudy { get; set; }
        public int Year { get; set; }
    }





  
}
