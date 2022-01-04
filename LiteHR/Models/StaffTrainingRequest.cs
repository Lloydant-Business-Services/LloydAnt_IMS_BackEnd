using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class StaffTrainingRequest:BaseModel
    {
        public TrainingType TrainingType { get; set; }
        public long TrainingTypeId { get; set; }
        public Staff Staff { get; set; }
        public long StaffId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; }
        public string AttachmentUrl { get; set; }
        public bool Approved { get; set; } = false;
        public DateTime? ApprovedDate { get; set; }
        public long? ApprovedById { get; set; }
        public Staff ApprovedBy { get; set; }
        public string Remarks { get; set; }
    }
}
