using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class LeaveRequest:BaseModel
    {
        public string Remarks { get; set; }
        public bool Active { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime EnteredDate { get; set; }
        public string SupportDocumentUrl { get; set; }
        public long StaffId { get; set; }
        public virtual Staff Staff { get; set; }
        public long LeaveTypeRankId { get; set; }
        public virtual LeaveTypeRank LeaveTypeRank { get; set; }
        public long LeaveDaysApplied { get; set; }
        public long RemainingLeaveDays { get; set; }
        public bool IsCompleted { get; set; }
        public long Staus { get; set; }


        
    }
}
