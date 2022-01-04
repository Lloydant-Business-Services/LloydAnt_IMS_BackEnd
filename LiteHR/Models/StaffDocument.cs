using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class StaffDocument:BaseModel
    {
        public long PersonId { get; set; }
        public virtual Person Person { get; set; }
        public long DocumentTypeId { get; set; }
        public virtual DocumentType DocumentType { get; set; }
        public string Url { get; set; }
        public bool Active { get; set; }
        public DateTime DateEntered { get; set; }
        public bool isVerified { get; set; }

        //VerifiedBy collects the StaffId of the verification officer
        public long verifiedBy { get; set; }
    }
}
