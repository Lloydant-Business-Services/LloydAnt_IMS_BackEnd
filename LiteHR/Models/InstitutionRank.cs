using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class InstitutionRank:BaseModel
    {
        public string Name { get; set; }
        public bool Active { get; set; }
        public long InstitutionUnitId { get; set; }
        public InstitutionUnit InstitutionUnit { get; set; }
        public string GradeLevel { get; set; }
    }
}
