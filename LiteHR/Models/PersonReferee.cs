using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class PersonReferee : BaseModel
    {
        public long PersonId { get; set; }
        public Person Person { get; set; }
        public string Name { get; set; }
        public string Organisation { get; set; }
        public string Designation { get; set; }
        public string Email { get; set; }
    }
}
