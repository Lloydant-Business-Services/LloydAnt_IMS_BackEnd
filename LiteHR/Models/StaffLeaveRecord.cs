using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class StaffLeaveRecord : BaseModel
    {
        public long StaffId { get; set; }
        public long LeaveResponseId { get; set; }
        public virtual LeaveResponse LeaveResponse {get; set;}
        public long Progress { get; set; }
    }
}
