using LiteHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class GetMailDto
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string SignatureUrl { get; set; }
        public string AttachmentUrl { get; set; }
        public string ReferenceNuber { get; set; }
        public DateTime DateEntered { get; set; }
        public bool IsAcknowledged { get; set; }
        public bool IsClosed { get; set; }
        public bool IsCopied { get; set; }
        public bool IsConfidential { get; set; }
        public bool IsExternal { get; set; }
        public bool IsRejected { get; set; }
        public long OriginatorId { get; set; }
        public long? AcknowledgedUserId { get; set; }
        public string OriginatorInfo{ get; set; }
        public string AcknowledgedUserInfo { get; set; }
        public bool Active { get; set; }
        public bool? DeskIsRead { get; set; }
        public bool? DeskIsActive { get; set; }
        public long DeskId { get; set; }
        public long MailingId { get; set; }
        public long FileTypeId { get; set; }
        public string FileName { get; set; }
        public string FileNumber { get; set; }
        public string DeskAlias { get; set; }
        public string OriginatorName { get; set; }
        public string CurrentDeskName { get; set; }

    }

}
