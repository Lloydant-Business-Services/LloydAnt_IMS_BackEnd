using LiteHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class StaffLeaveRecordDto
    {

        public DateTime DateEntered { get; set; }
        public DateTime Start { get; set; }
        public long Progress { get; set; }
        public string Remarks { get; set; }
        public string LeaveName { get; set; }
        public float ProgressInPercentage { get; set; }
        public long OriginalLeaveDuration { get; set; }
        public long AppliedLeaveDuration { get; set; }
        public long RemainingLeaveDays { get; set; }

    }
}
