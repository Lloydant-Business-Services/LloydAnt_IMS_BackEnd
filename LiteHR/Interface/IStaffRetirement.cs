using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteHR.Models;

namespace LiteHR.Interface
{
    public interface IStaffRetirement
    {
        Task GetAcademicStaffDueForRetirement();

        Task<StaffRetirement> RetireStaffDueRetirement(long staffId);

        //Task<StaffRetirement> StaffRetirementOneYearCheck(long staffId);

        Task<bool> StaffRetirementOneYearCheck(long staffId);

        //Task<StaffRetirement> StaffRetirementThreeMonthCheck(long staffId);

        Task<bool> StaffRetirementThreeMonthCheck(long staffId);
    }
}
