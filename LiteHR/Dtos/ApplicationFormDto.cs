using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class ApplicationFormDto
    {
        public PersonDto Person { get; set; }
        public List<PersonEducationObj> PersonEducation { get; set; }
        public List<PersonCertificationObj> PersonCertification { get; set; }
        public List<PersonExperienceObj> PersonExperience { get; set; }
        public List<PersonJournalObj> PersonJournal { get; set; }
        public List<PersonProfessionalBodyObj> PersonProfessionalBody { get; set; }
        public List<PersonRefereeObj> PersonReferee { get; set; }
        public List<PersonResearchObj> PersonResearch { get; set; }
        public long JobVacancyId { get; set; }
    }
    public class PersonEducationObj
    {
        public long QualificationId { get; set; }

        public string InstitutionName { get; set; }
        public string CourseOfStudy { get; set; }
        public int Year { get; set; }
    }
    public class PersonCertificationObj
    {
        public string Name { get; set; }
        public string Issuer { get; set; }
        public int Year { get; set; }
    }
    public class PersonExperienceObj
    {
        public string Organisation { get; set; }
        public string Role { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class PersonJournalObj
    {
        public string Name { get; set; }
        public string Publisher { get; set; }
        public int Year { get; set; }
    }
    public class PersonProfessionalBodyObj
    {
        public string Name { get; set; }
        public string Comments { get; set; }
        public int Year { get; set; }
    }
    public class PersonRefereeObj
    {
        public string Name { get; set; }
        public string Organisation { get; set; }
        public string Designation { get; set; }
        public string Email { get; set; }
    }
    public class PersonResearchObj
    {
        public string Name { get; set; }
        public string Topic { get; set; }
        public int Year { get; set; }
    }
}
