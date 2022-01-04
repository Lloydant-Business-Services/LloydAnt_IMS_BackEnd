using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class Holidays:BaseModel
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public bool Active { get; set; }
    }
}
