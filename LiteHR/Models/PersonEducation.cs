using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class PersonEducation:BaseModel
    {
        public long PersonId { get; set; }
        public Person Person { get; set; }
        public string Institution { get; set; }
        public string Course { get; set; }
        public int Year { get; set; }
        public long EducationalQualificationId { get; set; }
        public EducationalQualification EducationalQualification { get; set; }
    }


}
