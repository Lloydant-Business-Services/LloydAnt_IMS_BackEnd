using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class Leave:BaseModel
    {
        public string Name { get; set; }
        public long StaffTypeId { get; set; }
        public InstitutionStaffType StaffType { get; set; }
        public bool Active { get; set; }
    }
}
