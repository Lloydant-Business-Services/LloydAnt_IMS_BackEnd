using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class ChangeOfName:BaseModel
    {
        public string RequestedSurname { get; set; }
        public string RequestedFirstname { get; set; }
        public string RequestedOthername { get; set; }
        public string Attachment { get; set; }
        public DateTime DateOfRequest { get; set; }
        public string Comments { get; set; }
        public long  StaffId { get; set; }
        public virtual Staff Staff { get; set; }
        public bool IsApproved { get; set; }
        public bool IsClosed { get; set; }
    }
}
