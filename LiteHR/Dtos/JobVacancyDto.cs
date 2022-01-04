using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class JobVacancyDto
    {
        public long JobId { get; set; }
        public long JobTypeId { get; set; }
        public string VacancyName { get; set; }
        public string JobType { get; set; }
    }
}
