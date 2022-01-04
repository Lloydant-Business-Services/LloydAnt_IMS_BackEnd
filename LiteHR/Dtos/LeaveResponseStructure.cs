using LiteHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class LeaveResponseStructure
    {
        public long LeaveRequestId { get; set; }
        public long LeaveResponseId { get; set; }
        //public virtual LeaveRequest LeaveRequest { get; set; }

        public long LeaveResponseChainId { get; set; }
        public long DepartmentId { get; set; }
        //public virtual LeaveResponseChain LeaveResponseChain { get; set; }
        public string Remarks { get; set; }
        public DateTime? DateActed { get; set; }
        public bool IsApproved { get; set; }
        public bool IsActed { get; set; }
        public bool IsClosed { get; set; }
        public string StaffNumber { get; set; }
        public string StaffName { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime EnteredDate { get; set; }
        public long LeaveTypeId { get; set; }
    }
}
