using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class DepartmentTransfterListDto
    {
        public string CurrentDepartment { get; set; }
        public string NewDepartment { get; set; }
        public long NewDepartmentId { get; set; }
        public string Reasons { get; set; }
        public string StaffNumber { get; set; }
        public string StaffName { get; set; }
        public DateTime DateOfRequest { get; set; }
        public long StaffId { get; set; }
        public bool IsClosed { get; set; }
        public bool IsApproved { get; set; }
    }
}
