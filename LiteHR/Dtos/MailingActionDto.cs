using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class MailingActionDto
    {
        public long MailingId { get; set; }
        //public bool IsAcknowledged { get; set; }
        //public bool IsClosed { get; set; }
        //public bool IsRejected { get; set; }
        //public bool IsDeskActive { get; set; }
        public bool IsTransition { get; set; }
        public string Comments { get; set; }
        public long ToRoleId { get; set; }
        public long ToDepartmentId { get; set; }
        public long ToFacultyId { get; set; }
        public long ActionType { get; set; }
        public long UserId { get; set; }
    }
}
