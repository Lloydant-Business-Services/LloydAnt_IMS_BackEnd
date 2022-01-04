using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class DepartmentTransfer:BaseModel
    {
        public string DeptFrom { get; set; }
        public string DeptTo { get; set; }
        public DateTime DateOfRequest { get; set; }
        public string Reasons { get; set; }
        public long StaffId { get; set; }
        public virtual Staff Staff { get; set; }
        public long NewDepartmentId { get; set; }
        public bool IsApproved { get; set; }
        public bool IsClosed { get; set; }

    }
}
