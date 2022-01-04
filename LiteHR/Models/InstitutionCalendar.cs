using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class InstitutionCalendar:BaseModel
    {
        public DateTime Date { get; set; }
        public string Detail { get; set; }
        public Staff AddedBy { get; set; }
    }
}
