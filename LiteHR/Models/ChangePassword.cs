using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class ChangePassword
    {
        public string OldPassword { get; set; }
        public long UserId { get; set; }
        public string NewPassword { get; set; }
    }
}
