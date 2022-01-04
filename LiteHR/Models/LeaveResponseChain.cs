using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class LeaveResponseChain:BaseModel
    {
        public long LeaveTypeId { get; set; }
        public bool Active { get; set; }
        public long RoleId { get; set; }
        public long Order { get; set; }
        public virtual Role Role { get; set; }
        public virtual LeaveType LeaveType { get; set; }
    }
}
