using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class StaffAdditionalInfo: BaseModel
    {
        public long staffId { get; set; }
        public long? PfaNameId { get; set; }
        public virtual PfaName PfaName { get; set; }
        public long? PfaStatusId { get; set; }
        public virtual PfaStatus PfaStatus { get; set; }
        public string RsaNumber { get; set; }
        public long? AreaOfSpecializationId { get; set; }
        public virtual AreaOfSpecialization AreaOfSpecialization { get; set; }
        public string SignatureUrl { get; set; }
        public long? SignaturePin { get; set; }

        //public Nullable<DateTime> DateOfLastPromotion { get; set; }
    }
}
