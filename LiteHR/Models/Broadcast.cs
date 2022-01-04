using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class Broadcast:BaseModel
    {
        public string Subject { get; set; }
        public string Details { get; set; }
        public string AttachmentUrl { get; set; }
        public long? UnitId { get; set; }
        public InstitutionUnit InstitutionUnit { get; set; }
        public long? RankId { get; set; }
        public InstitutionRank InstitutionRank { get; set; }
        public DateTime Date { get; set; }
    }
}
