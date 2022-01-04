using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class StaffDto
    {
        public string StaffNumber { get; set; }
        public long RankId { get; set; }
        public long DepartmentId { get; set; }
        public long AppointmentId { get; set; }
        public long InstitutionUnit { get; set; }
        public long StaffTypeId { get; set; }
        public long CategoryId { get; set; }

        public long StaffCadreId { get; set; }
        public bool AppointmentIsConfirmed { get; set; }
        public long AppointmentTypeId { get; set; }
        public DateTime? DateOfConfirmation { get; set; }
        public DateTime? DateOfAppointment { get; set; }
    }
}
