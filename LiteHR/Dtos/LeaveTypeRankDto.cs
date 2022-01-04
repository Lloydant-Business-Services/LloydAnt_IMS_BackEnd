using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class LeaveTypeRankDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public string RankName { get; set; }
        public int DurationInDays { get; set; }
        public long LeaveTypeId { get; set; }
    }
}
