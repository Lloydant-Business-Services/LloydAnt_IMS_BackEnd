using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class MailArchiveFileType : BaseModel
    {
        public string Title { get; set; }
        public string FileNumber { get; set; }
        public bool Active { get; set; }
    }
}
