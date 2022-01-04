using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class StaffDBCommitDetails : BaseModel
    {
        public long LastSerialNumber { get; set; }
        public bool Active { get; set; } = true;
    }
}
