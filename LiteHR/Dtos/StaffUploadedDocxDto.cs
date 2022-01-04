using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class StaffUploadedDocxDto
    {
        public string DocumentName { get; set; }
        public string Department { get; set; }
        public DateTime DateEntered { get; set; }
        public string ImageUrl { get; set; }
        public bool isVerified { get; set; }
        public long  verifiedBy { get; set; }
        public long PersonId { get; set; }
        public long DocumentTypeId { get; set; }
        public string StaffName { get; set; }
        public string StaffNumber { get; set; }

    }
}
