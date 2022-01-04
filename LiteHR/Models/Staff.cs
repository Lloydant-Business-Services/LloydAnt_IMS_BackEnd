using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class Staff : BaseModel
    {
        public long PersonId { get; set; }
        public string StaffNumber { get; set; }
        public string GeneratedStaffNumber { get; set; }
        public Person Person { get; set; }
        public long? RankId { get; set; }
        public long? DepartmentId { get; set; }
        //public long? FacultyId { get; set; }
        public long? AppointmentId { get; set; }
        public InstitutionRank Rank { get; set; }
        public InstitutionDepartment Department { get; set; }
        public InstitutionAppointment Appointment { get; set; }
        //public Faculty Faculty { get; set; }
        //public long? UnitId { get; set; }
        //public InstitutionUnit Unit { get; set; }
        public long? StaffTypeId { get; set; }
        public InstitutionStaffType StaffType { get; set; }
        public long? CategoryId { get; set; }
        public InstitutionStaffCategory Category { get; set; }
        //public virtual IEnumerable<StaffHistory> staffHistories { get; set; }

        public bool Active { get; set; }

        public string RetirementReason { get; set; }
        public bool AppointmentIsConfirmed { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public DateTime? DateOfRegularization { get; set; }
        
        public DateTime? DateOfConfirmation { get; set; }
        public long? AppointmentTypeId { get; set; }
        public AppointmentType AppointmentType { get; set; }
        public string BiometricId { get; set; }
        public bool IsDisengaged { get; set; }
        public bool IsRetired { get; set; }
        public Nullable<DateTime> DateOfLastPromotion { get; set; }
        public Nullable<DateTime> DateOfEmployment { get; set; }
        public Nullable<DateTime> DateOfAssumption { get; set; }
        public string Guid { get; set; }
        public Nullable<DateTime> DateOfLastReset { get; set; }

    }
}
