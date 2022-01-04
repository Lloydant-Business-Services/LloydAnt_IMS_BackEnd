using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class PersonProfessionalBody : BaseModel
    {
        public long PersonId { get; set; }
        public Person Person { get; set; }
        public string Name { get; set; }
        public string Comments { get; set; }
        public int Year { get; set; }
    }
}
