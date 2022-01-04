using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class ApplicationSectionWeight:BaseModel
    {
        public long  JobVacancyId { get; set; }
        public bool Active { get; set; }
        public long ApplicationSectionHeaderId { get; set; }
        public ApplicationSectionHeader applicationSectionHeader { get; set; }
        public decimal Weight { get; set; }

    }
}
