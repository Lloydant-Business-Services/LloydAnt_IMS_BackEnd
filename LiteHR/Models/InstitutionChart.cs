using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class InstitutionChart:BaseModel
    {
        public InstitutionUnit Unit { get; set; }
        public int Position { get; set; }
    }
}
