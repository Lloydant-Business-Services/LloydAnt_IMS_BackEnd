using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class CustomOkObject
    {
        public long Holidays { get; set; }
        public List<StaffAttendanceDto> attendanceList { get; set; }
    }
}
