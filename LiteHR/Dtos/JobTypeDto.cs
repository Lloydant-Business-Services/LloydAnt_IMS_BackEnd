using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class JobTypeDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public int Id { get; set; }
    }
}
