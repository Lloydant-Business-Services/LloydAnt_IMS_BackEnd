using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class StaffRetirement: BaseModel
    {
        public long StaffId { get; set; }

        public Staff Staff { get; set; }

        public bool IsRetired { get; set; }

        public DateTime DateOfRetirement { get; set; }
    }
}
