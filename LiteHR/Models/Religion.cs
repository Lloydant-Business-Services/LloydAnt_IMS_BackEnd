using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class Religion:BaseModel
    {
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}
