using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class ChangeofNameListDto
    {
        public string Surname { get; set; }
        public string Firstname { get; set; }
        public string Othername { get; set; }
        public string StaffNumber { get; set; }
        public string Attachment { get; set; }
        public DateTime DateOfRequest { get; set; }
        public string Comments { get; set; }
        public long StaffId { get; set; }
        public bool IsApproved { get; set; }
        public bool IsClosed { get; set; }
    }
}
