using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class PersonDto
    {
        public string ImageUrl { get; set; }
        public string Surname { get; set; }
        public string Firstname { get; set; }
        public string Othername { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public long? StateId { get; set; }
        public long? LGAId { get; set; }
        public long? MaritalStatusId { get; set; }
        public long? ReligionId { get; set; }
        public long? GenderId { get; set; }
    }
}
