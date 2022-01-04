using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class JobRecipientDto
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string guid { get; set; }
        public string JobVacancyName { get; set; }
        public long JobVacancyId { get; set; }
        public long Id { get; set; }
     
    }
}
