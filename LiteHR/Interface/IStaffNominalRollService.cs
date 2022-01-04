using LiteHR.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Interface
{
    public interface IStaffNominalRollService
    {
        Task<int> StaffConfirmationForNominalRoll(IEnumerable<NominalRollDto> nominalRollDtos);
        Task<IEnumerable<StaffNominalRollDto>> StaffNominalRollByFaculty(long month, long year);
        Task<IEnumerable<StaffNominalRollDto>> GetNominalRollBy(long facultyId, long departmentId);
        Task<IEnumerable<StaffNominalRollDto>> PullStaffForMonthlyNominalRoll(long departmentId, long month, long year);
        Task<long> PostNominalRollComment(long staffId, string comment);

    }
}
