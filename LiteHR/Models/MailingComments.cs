using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class MailingComments : BaseModel
    {
        public long MailingDeskChainId { get; set; }
        public virtual MailingDeskChain MailingDeskChain { get; set; }
        [MaxLength(2000)]
        public string Comments { get; set; }
        public bool Active { get; set; }
        public DateTime DateEntered { get; set; }
    }
}
