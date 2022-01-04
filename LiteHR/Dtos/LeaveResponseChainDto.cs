using LiteHR.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class LeaveResponseChainDto
    {
        public long LeaveTypeId { get; set; }
        public long Id { get; set; }
        //public virtual LeaveType LeaveType { get; set; }
        public long RoleId { get; set; }
        //public virtual Role Role { get; set; }
        public bool Active { get; set; }
        public long Order { get; set; }
        public string LeaveName { get; set; }
        public string RoleName { get; set; }
    }
}
