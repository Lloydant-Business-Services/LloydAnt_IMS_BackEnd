using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class AddInstittutionRankDto
    {
        public string Name { get; set; }
        public bool Active { get; set; }
        public long InstitutionUnitId { get; set; }
        public string GradeLevel { get; set; }
        public string InstistutionUnitName { get; set; }
        public long Id { get; set; }
    }
}
