using LiteHR.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Interface
{
    public interface IStaffPostingService
    {
        Task<long> GetStaffCountByDepartmentAndRank(long departmentId, long rankId);
        Task<IEnumerable<GenerateStaffNumberListDto>> GetNewlyCreatedStaff(DateTime from, DateTime to);
        Task<int> UpdateNewlyCreatedStaff(UpdateNewStaffDto updateNewStaffDto, long staffId);
        Task<DeptRankCountDto> GetAllDepartmentStaffRankCount(long rankId, long departmentId);
        Task<IEnumerable<NewStaffCountDto>> GetStaffRankCountWhole(long cadreId);
    }
}
