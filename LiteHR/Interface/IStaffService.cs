using LiteHR.Dtos;
using LiteHR.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Interface
{
    public interface IStaffService
    {
       Task<Staff> AddStaff(Staff staff);
       Task<List<Staff>> AllStaff();
       Task<Staff> GetStaff(long id);
       Task<bool> UpdateStaff(long id, Staff staff);
       Task<int> GetStaffCount();
       Task<string> AutomateStaffStatus(long staffId);
        Task<Staff> CreateStaff(CreateStaffDto createStaffDto);
        Task<List<NewStaffDto>> GetTemporalStaff();
        Task<long> RegularizeStaff(StaffRegularizationDto staffRegularizationDto);
        Task<ExcelUploadRecordCountDto> ProcessStaffDetailsFromExcel(IEnumerable<StaffExcelModel> staffDetails);

       Task<List<StaffGradeBenefitDto>> GetStaffPayrollByDepartmentMonthAndYear(int departmentId, int month, int year);
        Task<long> AddSingleStaff(AddSingleStaffDto addSingleStaffDto);
        Task<bool> PostStaffUpdate(long id, NewStaffProfileEditDto staffUpdateDto);
        Task<StaffUpdateDto> GetStaffUpdate(long id);
        Task<long> UpdateStaffProfile(StaffUpdateProfileDto staffUpdateProfileDto,string filePath, string directory);
        Task<bool> DeleteStaff(long StaffId);
        Task<List<NewStaffDto>> GetStaffBy(long departmentId, long staffTypeId, long rankId, long appointmentTypeId);
        Task<Staff> GetStaffBy(string generatedStaffNumber);
        Task<AppReturnObject> AssignRole(AssignRoleDto assignRoleDto);
        Task<long> StaffCount();
        Task<IEnumerable<NewStaffDto>> FetchStaff();
        Task<IEnumerable<NewStaffDto>> DisengagedRetiredStaff(long serviceStatus);
        Task<int> DisengageStaff(long staffId);
        Task<int> ReinstateStaff(long staffId);
        Task<IEnumerable<StaffNominalRollDto>> FetchStaffForMonthlyNominalRoll(long departmentId);
        Task<long> NewStaffNumberGeneration(AddSingleStaffDto addSingleStaffDto);
        Task<IEnumerable<GenerateStaffNumberListDto>> GetGeneratedStaffNumberByDate(DateTime from, DateTime to);
        Task<NewStaffDto> GetStaffLoginInfo(string surname, string staffNumber);
        Task<long> UploadStaffPassport(IFormFile file, long staffId, string filePath, string directory);
        Task<int> ModifyUserPassword(ChangePassword dto);

    }
}
