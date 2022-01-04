using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class InstitutionCalendarDetail:BaseModel
    {
        public InstitutionCalendar Calendar { get; set; }
        public long RankId { get; set; }
        public long DepartmentId { get; set; }
        public long AppointmentId { get; set; }
        public InstitutionRank Rank { get; set; }
        public InstitutionDepartment Department { get; set; }
        public InstitutionAppointment Appointment { get; set; }
    }
}
