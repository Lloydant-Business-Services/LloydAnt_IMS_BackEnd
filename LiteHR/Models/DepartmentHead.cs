using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class DepartmentHead:BaseModel
    {
        public long StaffId { get; set; }
        public virtual Staff Staff { get; set; }
        public long InstitutionDepartmentId { get; set; }
        public virtual InstitutionDepartment InstitutionDepartment { get; set; }

        public bool Active { get; set; }
    }
}
