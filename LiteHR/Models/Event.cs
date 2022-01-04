using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class Event:BaseModel
    {
        public string Name { get; set; }
        public string Venue { get; set; }
        public DateTime Date { get; set; }
    }
}
