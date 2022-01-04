using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class StaffAttendance : BaseModel
    {
        public Staff Staff { get; set; }
        public long StaffId { get; set; }
        public TimeSpan? ClockIn { get; set; }
        public TimeSpan? ClockOut { get; set; }
        public DateTime? Date { get; set; }
        //public string Username { get; set; }
        //public InstitutionDepartment InstitutionDepartment { get; set; }
        //public long InstitutionDepartmentId { get; set; }
        public bool Absent { get; set; }
        
    }
}
