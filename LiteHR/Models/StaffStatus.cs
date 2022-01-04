using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class StaffStatus:BaseModel
    {
        public string StatusName { get; set; }
        public bool Active { get; set; }
        public long StaffId { get; set; }
        public Staff Staff { get; set; }
        public DateTime DateLogged { get; set; }
    }
}
