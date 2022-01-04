using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class ArchiveMailDto
    {
        public long MailingId { get; set; }
        public long FileTypeId { get; set; }
        public long UserId { get; set; }
    }
}
