using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class VisitationType : BaseModel
    {
        [MaxLength(100)]
        public string name { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }
        public bool Active { get; set; }
    }
}
