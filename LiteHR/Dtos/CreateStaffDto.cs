using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class CreateStaffDto
    {
        public PersonDto Person { get; set; }
        public StaffDto Staff { get; set; }
    }
}
