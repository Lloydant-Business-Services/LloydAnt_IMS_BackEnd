using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class JobVacancy: BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public long JobTypeId { get; set; }
        public JobType JobType { get; set; }
        public DateTime DateCreated { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public List<ApplicationSectionWeight> ApplicationSectionWeight { get; set; }
    }
}
