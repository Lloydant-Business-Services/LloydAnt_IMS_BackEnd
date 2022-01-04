using LiteHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class TrailingCommentsDto : BaseModel
    {
        public string UserInfo { get; set; }
        public string Comments { get; set; }
        public DateTime DateEntered { get; set; }

    }
}
