using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class HolidayDto
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public List<DateDto> holidayModel { get; set; }
    }
}
