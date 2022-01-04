using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class LeaveStatusDetail
    {
        public long LeaveRequestId { get; set; }
        public LeaveRequest LeaveRequest { get; set; }
        public long LeaveDaysApplied { get; set; }
        public long RemainingDays { get; set; }
        public bool IsCompleted { get; set; }
    }
}
