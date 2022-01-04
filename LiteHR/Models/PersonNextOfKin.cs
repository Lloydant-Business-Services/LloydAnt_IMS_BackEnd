using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class PersonNextOfKin : BaseModel
    {
        public long PersonId { get; set; }
        public Person Person { get; set; }
        [MaxLength(100)]
        public string Fullname { get; set; }
        [MaxLength(30)]
        public string Email { get; set; }
        [MaxLength(30)]
        public string Phone { get; set; }
        [MaxLength(200)]
        public string Address { get; set; }
    }
}
