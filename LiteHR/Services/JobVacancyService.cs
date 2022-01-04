using LiteHR.Dtos;
using LiteHR.Helpers;
using LiteHR.Infrastructure;
using LiteHR.Interface;
using LiteHR.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Services
{
    public class JobVacancyService:IJobVacancyService
    {
        private readonly HRContext _context;
        private IEmailService _emailService;
        private readonly IFileUpload _fileUpload;



        public JobVacancyService(HRContext context, IEmailService emailService, IFileUpload fileUpload)
        {
            _context = context;
            _emailService = emailService;
            _fileUpload = fileUpload;


        }
        public async Task<JobVacancy> AddJobVacancy(AddJobVacancy addJobVacancy)
        {
            try
            {

                //jobVacancy.DateCreated = DateTime.Now;
                JobVacancy jobVacancy = new JobVacancy()
                {
                    JobTypeId = addJobVacancy.JobTypeId,
                    Name = addJobVacancy.Name,
                    Active = false,
                    DateCreated = DateTime.Now,
                    Description = addJobVacancy.Description,
                    UserId = addJobVacancy.UserId,
                };
                //jobVacancy.Active = false;
                _context.Add(jobVacancy);
                await _context.SaveChangesAsync();
                if (addJobVacancy.SectionHeaderWeight?.Count > 0)
                {
                    foreach(SectionHeaderWeight item in addJobVacancy.SectionHeaderWeight)
                    {
                        ApplicationSectionWeight applicationSectionWeight = new ApplicationSectionWeight()
                        {
                            Active = true,
                            JobVacancyId = jobVacancy.Id,
                            Weight = item.weight,
                            ApplicationSectionHeaderId=item.ApplicationSectionHeaderId,
                            

                        };
                        _context.Add(applicationSectionWeight);
                        await _context.SaveChangesAsync();
                    }
                    
                }
                var vacancyLoops= await _context.JOB_VACANCY
                    .Include(m => m.User)
                    .ThenInclude(m => m.Staff)
                    .Include(m => m.User.Staff.Person)
                    .Include(m => m.JobType)
                    .Include(m => m.ApplicationSectionWeight)
                    .SingleOrDefaultAsync(m => m.Id == jobVacancy.Id);
                foreach(var item in vacancyLoops.ApplicationSectionWeight)
                {
                   var header=await _context.APPLICATION_SECTION_HEADER
                     .SingleOrDefaultAsync(m => m.Id == item.ApplicationSectionHeaderId);
                    item.applicationSectionHeader = header;
                }
                return vacancyLoops;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public async Task<JobVacancy> GetJobVacancy(long id)
        {
            try
            {
                //return await _context.JOB_VACANCY
                //    .Include(m => m.JobType)
                //    .Include(m => m.User)
                //    .ThenInclude(m => m.Staff)
                //    .Include(m => m.User.Staff.Person)
                //    .Include(m=>m.ApplicationSectionWeight)
                //    .SingleOrDefaultAsync(m => m.Id == id);
                var vacancyLoops = await _context.JOB_VACANCY
                    .Include(m => m.User)
                    .ThenInclude(m => m.Staff)
                    .Include(m => m.User.Staff.Person)
                    .Include(m => m.JobType)
                    .Include(m => m.ApplicationSectionWeight)
                    .SingleOrDefaultAsync(m => m.Id == id);
                foreach (var item in vacancyLoops.ApplicationSectionWeight)
                {
                    var header = await _context.APPLICATION_SECTION_HEADER
                      .SingleOrDefaultAsync(m => m.Id == item.ApplicationSectionHeaderId);
                    item.applicationSectionHeader = header;
                }
                return vacancyLoops;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<List<JobVacancy>> AllJobVacancy()
        {
            return await _context.JOB_VACANCY
               .Include(m => m.JobType)
               .Include(m => m.User)
               .ThenInclude(m => m.Staff)
               .Include(m => m.User.Staff.Person)
               .Include(m => m.ApplicationSectionWeight)
               .OrderBy(p => p.DateCreated).ToListAsync();


        }
        public async Task<List<JobVacancy>> ActiveJobVacancy()
        {
            return await _context.JOB_VACANCY.Where(m => m.Active)
               .Include(m => m.JobType)
               .Include(m => m.User)
               .ThenInclude(m => m.Staff)
               .Include(m => m.User.Staff.Person)
               .Include(m => m.ApplicationSectionWeight)
               .OrderBy(p => p.DateCreated).ToListAsync();
        }

        public async Task<List<AddJobVacancy>> AddJobVacancy(List<JobVacancy> jobVacancyList)
        {
            var AddedList = new List<AddJobVacancy>();

            //jobVacancyList.ForEach(async jobVacancy =>
            //{
            //    AddedList.Add(await AddJobVacancy(jobVacancy));
            //});

            return AddedList;
        }

        public async Task<bool> UpdateJobVacancy(long id, JobVacancy jobVacancy)
        {

            try
            {
                var status = 0;
                var _jobVacancy = await GetJobVacancy(id);

                if (_jobVacancy != null)
                {
                    _jobVacancy.Active = jobVacancy.Active;
                    if (jobVacancy.DateCreated != null)
                    {
                        _jobVacancy.DateCreated = jobVacancy.DateCreated;
                    }
                    if (!string.IsNullOrEmpty(jobVacancy.Description))
                    {
                        _jobVacancy.Description = jobVacancy.Description;
                    }
                    if (jobVacancy.JobTypeId > 0)
                    {
                        _jobVacancy.JobTypeId = jobVacancy.JobTypeId;
                    }
                    if (!string.IsNullOrEmpty(jobVacancy.Name))
                    {
                        _jobVacancy.Name = jobVacancy.Name;
                    }
                    if (jobVacancy.UserId > 0)
                    {
                        _jobVacancy.UserId = jobVacancy.User.Id;
                    }
                    _context.Update(_jobVacancy);
                    status = await _context.SaveChangesAsync();
                }

                return (status > 0) ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public async Task<int> GetJobVacancyCount()
        {
            return await _context.JOB_VACANCY
               .CountAsync();
        }

        public async Task<int> CreateJobLink(JobRecipientDto jobRecipientDto)
        {
            //string Message;
            try
            {
                var verifyDetails = await _context.JOB_RECIPIENTS.Where(j => j.Email == jobRecipientDto.Email && j.JobVacancyId == jobRecipientDto.JobVacancyId)
                .FirstOrDefaultAsync();
                var vacantPosition = await _context.JOB_VACANCY.Where(n => n.Id == jobRecipientDto.JobVacancyId).FirstOrDefaultAsync();

                if (verifyDetails != null)
                {
                    return StatusCodes.Status208AlreadyReported;
                }
                else
                {
                    string guid = Convert.ToString(Guid.NewGuid());
                    JobRecipients jobRecipients = new JobRecipients();

                    jobRecipients.Firstname = jobRecipientDto.Firstname;
                    jobRecipients.Lastname = jobRecipientDto.Lastname;
                    jobRecipients.Email = jobRecipientDto.Email;
                    jobRecipients.JobVacancyId = jobRecipientDto.JobVacancyId;
                    jobRecipients.Guid = guid;


                    _context.Add(jobRecipients);
                    await _context.SaveChangesAsync();

                    SendEmailDto sendEmailDto = new SendEmailDto()
                    {
                        VerificationCategory = (int)VerificationCategories.JobRecipient,
                        ReceiverEmail = jobRecipientDto.Email,
                        ReceiverName = jobRecipientDto.Firstname,
                        VerificationGuid = guid,
                        VacancyName = vacantPosition.Name,
                    };
                    await _emailService.EmailFormatter(sendEmailDto);

                    return StatusCodes.Status200OK;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            
           

        }

        public async Task<int> ResendEmailLink(long id, string guid)
        {
            var selectRecipient = await _context.JOB_RECIPIENTS.Where(jr => jr.Id == id && jr.Guid == guid)
                .Include(j => j.JobVacancy)
                .FirstOrDefaultAsync();
            if(selectRecipient != null)
            {

                SendEmailDto sendEmailDto = new SendEmailDto()
                {
                    VerificationCategory = (int)VerificationCategories.JobRecipient,
                    ReceiverEmail = selectRecipient.Email,
                    ReceiverName = selectRecipient.Firstname,
                    VerificationGuid = selectRecipient.Guid,
                    VacancyName = selectRecipient.JobVacancy.Name,
                };
                await _emailService.EmailFormatter(sendEmailDto);
                return StatusCodes.Status200OK;

            }
            else
            {
                throw new AppException();
            }
        }

       

        public async Task<IEnumerable<JobRecipientDto>> FetchJobRecipients()
        {
            return await _context.JOB_RECIPIENTS
                .Include(jv => jv.JobVacancy)
                .Select(j => new JobRecipientDto
            {
                Firstname = j.Firstname,
                Lastname = j.Lastname,
                Email = j.Email,
                JobVacancyId = j.JobVacancyId,
                guid = j.Guid,
                Id = j.Id,
                JobVacancyName = j.JobVacancy.Name
            }).ToListAsync();
            
        }

        public async Task<long> VerifyGuidAndEmail(string guid, string email)
        {
            var verification = await _context.JOB_RECIPIENTS.Where(jr => jr.Guid == guid && jr.Email == email).FirstOrDefaultAsync();

            if(verification != null)
            {
                return verification.JobVacancyId;
            }
            else
            {
                return 0;
            }
        }


        public async Task<ExcelUploadRecordCountDto> ProcessApplicantDetailsFromExcel(IEnumerable<JobVacancyExcelDto> applicantDetails, int jobVacancyId)
        {
            ExcelUploadRecordCountDto excelUploadRecordCountDto = new ExcelUploadRecordCountDto();
            excelUploadRecordCountDto.SuccessfullUpload = 0;
            excelUploadRecordCountDto.FailedUpload = 0;
            excelUploadRecordCountDto.UpdatedRecords = 0;

            var vacantPosition = await _context.JOB_VACANCY.Where(n => n.Id == jobVacancyId).FirstOrDefaultAsync();

            try
            {
                if (applicantDetails?.Count() > 0)
                {
                    foreach (JobVacancyExcelDto recipient in applicantDetails)
                    {
                        //CHECK TO SEE IF Person ALREADY EXISTS
                        JobRecipients jobRecipients = await _context.JOB_RECIPIENTS.Where(j => j.Firstname == recipient.FirstName && j.Lastname == recipient.LastName && j.Email == recipient.Email).FirstOrDefaultAsync();
                        string guid = Convert.ToString(Guid.NewGuid());

                        if (jobRecipients != null )
                        {
                            excelUploadRecordCountDto.FailedUpload += 1;
                        }
                        else
                        {
                            JobRecipients newApplicant = new JobRecipients();
                            newApplicant.Email = recipient.Email;
                            newApplicant.Firstname = recipient.FirstName;
                            newApplicant.Lastname = recipient.LastName;
                            newApplicant.Guid = guid;
                            newApplicant.JobVacancyId = jobVacancyId;
                            _context.Add(newApplicant);
                            await _context.SaveChangesAsync();
                            excelUploadRecordCountDto.SuccessfullUpload += 1;
                        }
                        SendEmailDto sendEmailDto = new SendEmailDto()
                        {
                            VerificationCategory = (int)VerificationCategories.JobRecipient,
                            ReceiverEmail = recipient.Email,
                            ReceiverName = recipient.FirstName,
                            VerificationGuid = guid,
                            VacancyName = vacantPosition.Name,
                        };
                        await _emailService.EmailFormatter(sendEmailDto);


                    }
                }
            }
            catch (Exception ex) { throw ex; }
            return excelUploadRecordCountDto;
        }

        public async Task<int> ActivateJob(long jobVacancyId)
        {
            JobVacancy jobVacancy = await _context.JOB_VACANCY.Where(j => j.Id == jobVacancyId).FirstOrDefaultAsync();


            if(!jobVacancy.Active)
            {
                jobVacancy.Active = true;
                _context.Update(jobVacancy);
                await _context.SaveChangesAsync();
                return StatusCodes.Status200OK;
            }
            else if(jobVacancy.Active)
            {
                jobVacancy.Active = false;
                _context.Update(jobVacancy);
                await _context.SaveChangesAsync();
                return StatusCodes.Status200OK;
            }
            else
            {
                return StatusCodes.Status400BadRequest;
            }
        }

        public async Task<int> DeleteJobRecipient (long id)
        {
            var selectRecipient = await _context.JOB_RECIPIENTS.Where(r => r.Id == id).FirstOrDefaultAsync();
            if(selectRecipient != null)
            {
                _context.Remove(selectRecipient);
                await _context.SaveChangesAsync();
                return StatusCodes.Status200OK;
            }
            else
            {
                return StatusCodes.Status400BadRequest;
            }
        }

    }
}
