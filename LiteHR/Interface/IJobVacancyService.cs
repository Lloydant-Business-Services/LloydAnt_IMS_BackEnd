using LiteHR.Dtos;
using LiteHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Interface
{
    public interface IJobVacancyService
    {
        Task<JobVacancy> AddJobVacancy(AddJobVacancy jobVacancy);
        Task<List<JobVacancy>> AllJobVacancy();
        Task<JobVacancy> GetJobVacancy(long id);
        Task<bool> UpdateJobVacancy(long id, JobVacancy jobVacancy);
        Task<int> GetJobVacancyCount();
        
        Task<List<JobVacancy>> ActiveJobVacancy();
        Task<int> CreateJobLink(JobRecipientDto jobRecipientDto);
        Task<IEnumerable<JobRecipientDto>> FetchJobRecipients();
        Task<long> VerifyGuidAndEmail(string guid, string email);
        Task<ExcelUploadRecordCountDto> ProcessApplicantDetailsFromExcel(IEnumerable<JobVacancyExcelDto> applicantDetails, int jobVacancyId);
        Task<int> ActivateJob(long jobVacancyId);
        Task<int> DeleteJobRecipient(long id);
        Task<int> ResendEmailLink(long id, string guid);



    }
}
