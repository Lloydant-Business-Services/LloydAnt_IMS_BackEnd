using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class PersonExperience : BaseModel
    {
        public long PersonId { get; set; }
        public Person Person { get; set; }
        public string Organisation { get; set; }
        public string Role { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
