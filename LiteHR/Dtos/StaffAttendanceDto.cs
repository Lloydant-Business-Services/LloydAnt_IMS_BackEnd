using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class StaffAttendanceDto
    {
       
        public string StaffName { get; set; }
        public string Department { get; set; }
        public long Early { get; set; }
        public long Late { get; set; }
        public long Present { get; set; }
        public long Absent { get; set; }


    }


}
