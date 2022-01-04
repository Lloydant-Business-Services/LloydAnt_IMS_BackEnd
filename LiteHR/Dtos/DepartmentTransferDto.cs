using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class DepartmentTransferDto
    {
        public string CurrentDepartment { get; set; }
        //public string NewDepartment { get; set; }
        //public long CurrentDepartmentId { get; set; }
        public long NewDepartmentId { get; set; }
        public string Reasons { get; set; }
        public long StaffId { get; set; }
    }
}
