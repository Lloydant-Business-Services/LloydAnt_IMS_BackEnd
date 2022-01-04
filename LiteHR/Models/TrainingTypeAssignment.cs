using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class TrainingTypeAssignment:BaseModel
    {
        public TrainingType Training { get; set; }
        public long LeaveId { get; set; }
        public InstitutionRank Rank { get; set; }
        public long RankId { get; set; }
        public int Duration { get; set; }
    }
}
