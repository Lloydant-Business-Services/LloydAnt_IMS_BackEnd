using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class AddStaffLeaveRequestDto
    {
        public string Comment { get; set; }
        public string Reason { get; set; }
        public string SupportDocument { get; set; }
        public string DepartmentName { get; set; }
        public string StaffNumber { get; set; }
        public string StaffName { get; set; }
        public long StaffId { get; set; }
        public long LeaveResponseId { get; set; }
        public long LeaveRequestId { get; set; }
        public long Status { get; set; }
        //public virtual Staff Staff { get; set; }
        public long DepartmentId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime EnteredDate { get; set; }
        public long LeaveTypeId { get; set; }
        public long LeaveTypeRankId { get; set; }
        public string LeaveType { get; set; }
        public long Duration { get; set; }
        public bool isActed { get; set; }
        public bool isApproved { get; set; }
        public float ProgressInPercentage { get; set; }
        public long OriginalLeaveDuration { get; set; }
        public long AppliedLeaveDuration { get; set; }
        public long RemainingLeaveDays { get; set; }

    }
    
}
