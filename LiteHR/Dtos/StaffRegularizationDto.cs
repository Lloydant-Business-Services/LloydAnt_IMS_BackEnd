using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class StaffRegularizationDto
    {
        public long StaffId { get; set; }
        public long AppointmentTypeId { get; set; }
        public DateTime DateOfRegulartization { get; set; }
    }
}
