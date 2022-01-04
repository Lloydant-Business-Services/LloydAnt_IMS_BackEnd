using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class AddLeaveTypeRankDto
    {
        public long LeaveTypeId { get; set; }
        public long RankId { get; set; }
        public int DurationInDays { get; set; }
    }
}
