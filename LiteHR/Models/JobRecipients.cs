using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class JobRecipients:BaseModel
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Guid { get; set; }
        public JobVacancy JobVacancy { get; set; }
        public long JobVacancyId { get; set; }
    }
}
