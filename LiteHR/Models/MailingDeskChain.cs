using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class MailingDeskChain:BaseModel
    {
        public long MailingId { get; set; }
        public virtual Mailing Mailing { get; set; }
        public long? InstitutionDepartmentId { get; set; }
        public virtual InstitutionDepartment InstitutionDepartment { get; set; }
        public long? FacultyId { get; set; }
        public virtual Faculty Faculty { get; set; }
        public long? RoleId { get; set; }
        public long? StaffId { get; set; }
        public virtual Role Role { get; set; }
        public virtual Staff staff { get; set; }
        public bool IsRead { get; set; }
        public bool IsActive { get; set; }
        public bool Archived { get; set; }
        public bool IsActed { get; set; }
        public bool IsCopied { get; set; }
        //public string DeskAlias { get; set; }

    }
}
