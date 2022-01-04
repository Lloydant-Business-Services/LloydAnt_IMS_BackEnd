using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class LeaveTypeDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}
