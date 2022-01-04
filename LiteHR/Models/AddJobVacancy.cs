using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class AddJobVacancy
    {
        
        public string Description { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public long JobTypeId { get; set; }
        public long UserId { get; set; }
        public List<SectionHeaderWeight> SectionHeaderWeight { get; set; }
    }
    public class SectionHeaderWeight
    {
        public long ApplicationSectionHeaderId { get; set; }
        public decimal weight { get; set; }
    }
}
