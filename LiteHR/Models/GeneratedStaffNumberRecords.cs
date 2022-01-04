using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class GeneratedStaffNumberRecords:BaseModel
    {
        public long StaffId { get; set; }
        public long? DefaultDepartmentId { get; set; }
        public InstitutionDepartment DefaultDepartment { get; set; }
        public Staff Staff { get; set; }
        public DateTime DateGenerated { get; set; }
    }
}
