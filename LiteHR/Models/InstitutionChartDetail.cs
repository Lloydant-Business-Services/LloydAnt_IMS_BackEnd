using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class InstitutionChartDetail:BaseModel
    {
        public InstitutionChart Unit { get; set; }
        public InstitutionRank Rank { get; set; }
    }
}
