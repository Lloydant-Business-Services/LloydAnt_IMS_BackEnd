using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class Person:BaseModel
    {
        public string ImageUrl { get; set; }
        public string Surname { get; set; }
        public string Firstname { get; set; }
        public string Othername { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public long? StateId { get; set; }
        public long? LGAId { get; set; }
        public long? MaritalStatusId { get; set; }
        public long? ReligionId { get; set; }
        public long? GenderId { get; set; }
        public State State { get; set; }
        public Lga Lga { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public Religion Religion { get; set; }
        public Gender Gender { get; set; }
        public List<PersonEducation> PersonEducations { get; set; }
        public List<PersonProfessionalBody> PersonProfessionalBodies { get; set; }
        public List<PersonJournal> PersonJournals { get; set; }
        public List<PersonExperience> PersonExperiences { get; set; }
        public List<PersonResearchGrant> PersonResearchGrants { get; set; }
        public List<PersonCertification> PersonCertifications { get; set; }
        public List<PersonReferee> PersonReferees { get; set; }
    }
}
