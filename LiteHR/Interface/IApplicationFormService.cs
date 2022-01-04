using LiteHR.Dtos;
using LiteHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Interface
{
    public interface IApplicationFormService
    {
        Task<ApplicationForm> AddApplicationForm(ApplicationFormSubmit applicationForm);
        Task<long> CreateApplicationForm(ApplicationFormDto applicationFormDto);
        
        Task<List<ApplicationForm>> AllApplicationForm();

        Task<ApplicationForm> GetApplicationForm(long id);

        Task<ApplicationForm> GetCV(long personId);

        //Task ProcessStaffDetailsFromExcel(IEnumerable<StaffExcelModel> staffDetails);

        Task<string> ComposeStaffNumber();

        Task<Staff> DoesStaffNumberExist(string staffNumber);

        string AddLeadingZeros(long number);

        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);

        DateTime ConvertToDate(string date);

        Task<Lga> GetLgaByName(string lga);

        Task<State> GetStateByName(string state);

        Task<Gender> MapGenderCharacterToObject(string genderCharacter);

        Task<InstitutionDepartment> GetOrCreateDepartment(string departmentName);
    }
}
