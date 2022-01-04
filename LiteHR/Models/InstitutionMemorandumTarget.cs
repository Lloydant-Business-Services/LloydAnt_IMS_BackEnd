using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class InstitutionMemorandumTarget : BaseModel
    {
        public long InstitutionMemorandumId { get; set; }
        public virtual InstitutionMemorandum InstitutionMemorandum { get; set; }
        public long? RoleId { get; set; }
        public virtual Role Role { get; set; }
        public long? InstitutionDepartmentId { get; set; }
        public virtual InstitutionDepartment InstitutionDepartment { get; set; }
        public long? FacultyId { get; set; }
        public virtual Faculty Faculty { get; set; }
        public bool Active { get; set; }
    }
}
