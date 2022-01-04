using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class Mailing:BaseModel
    {
        [MaxLength(500)]
        public string Title { get; set; }
        public string Body { get; set; }
        public string AttachmentUrl { get; set; }
        public string ReferenceNuber { get; set; }
        public DateTime DateEntered { get; set; }
        public bool IsAcknowledged { get; set; }
        public bool IsExternal { get; set; }
        public bool IsClosed { get; set; }
        public bool IsRejected { get; set; }
        public long? OriginatorId { get; set; }
        public long? FileTypeId { get; set; }
        public virtual User Originator { get; set; }
        public long? AcknowledgedUserId { get; set; }
        public string OriginatorSignatureUrl { get; set; }

        public virtual User AcknowledgedUser { get; set; }
        public virtual MailArchiveFileType FileType { get; set; }

        [MaxLength(200)]
        public string OriginatorRoleDepartment { get; set; }
        public string AcknowledgedRoleOffice { get; set; }
        public string AcknowledgedUserRoleDepartment { get; set; }
        public string OriginatorEmail { get; set; }
        public bool Active { get; set; }
        public bool IsConfidential { get; set; }


    }
}
