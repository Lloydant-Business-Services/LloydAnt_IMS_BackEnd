using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class MailArchive : BaseModel
    {
        public long MailingId { get; set; }
        public long  MailArchiveFileTypeId { get; set; }
        public long ArchivedById { get; set; }
        public virtual Mailing Mailing { get; set; }
        public virtual MailArchiveFileType MailArchiveFileType { get; set; }
        public virtual User ArchivedBy { get; set; }
        public DateTime DateArchived { get; set; }
        public bool Active { get; set; }
    }
}
