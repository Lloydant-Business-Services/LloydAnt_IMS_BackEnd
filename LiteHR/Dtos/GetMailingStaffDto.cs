using LiteHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class GetMailingStaffDto
    {
        public string StaffDetail { get; set; }
        public long StaffId { get; set; }
    }
}
