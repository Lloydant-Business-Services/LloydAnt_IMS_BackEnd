using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class LeaveResponse:BaseModel
    {
        public long LeaveRequestId { get; set; }
        public virtual LeaveRequest LeaveRequest { get; set; }
        //public long LeaveResponseChainId { get; set; }
        //public virtual LeaveResponseChain LeaveResponseChain { get; set; }
        public string Remarks { get; set; }
        public DateTime? DateActed { get; set; }
        public bool IsApproved { get; set; }
        public bool IsActed { get; set; }
        public bool IsClosed { get; set; }
        public bool ActiveDesk { get; set; }
    }
}
