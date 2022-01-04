using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class StaffPfa
    {
        public long StaffId { get; set; }
        public virtual Staff Staff { get; set; }
        public long PfaId { get; set; }
        public virtual PfaName Pfa { get; set; }
        public long PfaStatusId { get; set; }
        public virtual PfaStatus PfaStatus { get; set; }
    }
}
