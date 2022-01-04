using LiteHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class GetPersonEducationDto : BaseModel
    {
        public string QualificationName { get; set; }
        public int YearObtained { get; set; }
        public string Institution { get; set; }
        public long QualificationId { get; set; }
    }
}
