using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class StaffNominalRoll:BaseModel
    {
        public long StaffId { get; set; }
        public Staff Staff { get; set; }
        public long Month { get; set; }
        public long Year { get; set; }
        public bool IsCleared { get; set; }
        public string Comment { get; set; }

    }
}
