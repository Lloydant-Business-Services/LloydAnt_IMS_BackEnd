using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class ApplicationForm:BaseModel
    {
        public long PersonId { get; set; }
        public Person Person { get; set; }
        
        public decimal ApplicationScore { get; set; }
        public long JobVacancyId { get; set; }
        public JobVacancy JobVacancy { get; set; }
        public DateTime DateSubmitted { get; set; }
        public bool Active { get; set; }
        public bool InterviewEmailSent { get; set; }
    }
}