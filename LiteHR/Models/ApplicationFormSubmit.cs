using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class ApplicationFormSubmit
    {
        public Person Person { get; set; }
        public List<PersonEducation> PersonEducations { get; set; }
        public List<PersonProfessionalBody> PersonProfessionalBodies { get; set; }
        public List<PersonJournal> PersonJournals { get; set; }
        public List<PersonExperience> PersonExperiences { get; set; }
        public List<PersonResearchGrant> PersonResearchGrants { get; set; }
        public List<PersonCertification> PersonCertifications { get; set; }
        public List<PersonReferee> PersonReferees { get; set; }
        public long JobVacancyId { get; set; }
    }
}
