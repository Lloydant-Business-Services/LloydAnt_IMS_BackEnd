using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class LeaveActionDto
    {
        //public long LeaveResponseId { get; set; }
        public long LeaveRequestId { get; set; }
       
        public string Remarks { get; set; }
    }
}
