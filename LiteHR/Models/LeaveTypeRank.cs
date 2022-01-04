using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class LeaveTypeRank:BaseModel
    {
        public long RankId { get; set; }
        public long LeaveTypeId { get; set; }
        public int Duration { get; set; }
        public bool Active { get; set; }
        public virtual InstitutionRank Rank { get; set; }
        public virtual LeaveType LeaveType{ get; set; }
    }
}
