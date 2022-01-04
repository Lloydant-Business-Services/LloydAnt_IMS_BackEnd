using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LiteHR.Dtos;
using LiteHR.Interface;
using LiteHR.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace LiteHR.Controllers
{

    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JobVacancyController : ControllerBase
    {
        private readonly IJobVacancyService _jobVacancyService;
        private readonly HRContext _context;


        public JobVacancyController(HRContext context, IJobVacancyService jobVacancyService)
        {
            _jobVacancyService = jobVacancyService;
            _context = context;

        }

       

        // GET: api/JobVacancys
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobVacancy>>> GetJOBVACANCY()
        {
            return await _jobVacancyService.AllJobVacancy();
        }
       

        // GET: api/JobVacancy/5
        [HttpGet("{id}")]
        public async Task<JobVacancyDto> GetJobVacancy(long id)
        {
            var getJob = await _context.JOB_VACANCY.Where(j => j.Id == id)
                .Select(f => new JobVacancyDto
                {
                    VacancyName = f.Name,
                    JobId = f.Id
                }).FirstOrDefaultAsync();
            return getJob;
        }

        // PUT: api/JobVacancy/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<int> PutJobVacancy(long id, JobVacancyDto jobVacancyDto)
        {
            var getJob = await _context.JOB_VACANCY.Where(j => j.Id == id).FirstOrDefaultAsync();
            if(getJob != null)
            {
                getJob.Name = jobVacancyDto.VacancyName;
                getJob.JobTypeId = jobVacancyDto.JobTypeId;
                _context.Update(getJob);
                await _context.SaveChangesAsync();
                return StatusCodes.Status200OK;
            }
            else
            {
                return StatusCodes.Status400BadRequest;
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchJobVacancy(long id, JobVacancy jobVacancy)
        {
            if (id != jobVacancy.Id)
            {
                return BadRequest();
            }
            try
            {
                await _jobVacancyService.UpdateJobVacancy(id, jobVacancy);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobVacancyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // POST: api/AssetTypes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<JobVacancy>> PostJobVacancy(AddJobVacancy jobVacancy)
        {
           var newJobVacancy=await _jobVacancyService.AddJobVacancy(jobVacancy);

            return CreatedAtAction("GetJobVacancy", new { id = newJobVacancy.Id }, newJobVacancy);

        }
        [HttpDelete("[action]")]
        public async Task<int> DeleteJobVacancy(long jobId)
        {
            var selectJob = await _context.JOB_VACANCY.Where(v => v.Id == jobId).FirstOrDefaultAsync();
            if(selectJob != null)
            {
                _context.Remove(selectJob);
                await _context.SaveChangesAsync();
                return StatusCodes.Status200OK;
            }
            else
            {
                return StatusCodes.Status400BadRequest;
            }
        }


        private bool JobVacancyExists(long id)
        {
            return _jobVacancyService.GetJobVacancy(id).Result != null ? true : false;
            
        }

        [HttpPost("CreateJobLink")]
        public async Task<int> CreateJobLink(JobRecipientDto jobRecipientDto) => await _jobVacancyService.CreateJobLink(jobRecipientDto);

        [HttpGet("GetJobRecipients")]
        public async Task<IEnumerable<JobRecipientDto>> FetchJobRecipients() => await _jobVacancyService.FetchJobRecipients();
        [HttpGet("VerifyGuid")]
        public async Task<long> VerifyGuidAndEmail(string guid, string email) => await _jobVacancyService.VerifyGuidAndEmail(guid, email);




        [HttpPost("[action]")]
        public async Task<IActionResult> ProcessApplicantDetailsFromExcel(IFormFile file, int jobVacancyId)
        {
            ExcelUploadRecordCountDto excelUploadRecordCountDto = new ExcelUploadRecordCountDto();
            try
            {
                long size = file.Length;
                if (size > 0)
                {
                    List<JobVacancyExcelDto> applicantDetails = new List<JobVacancyExcelDto>();

                    var filePath = Path.GetTempFileName();
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await file.CopyToAsync(stream);

                        //Initialize the Data from the excel sheet into stream
                        ExcelPackage package = new ExcelPackage(stream);
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();

                        if (worksheet != null)
                        {
                            //Give 2 rows space from the top to allow for Excel Headers
                            int totalRows = worksheet.Dimension.Rows;

                            for (int i = 2; i <= totalRows; i++)
                            {
                                JobVacancyExcelDto applicantDetail = new JobVacancyExcelDto();

                                int serialNumber = Convert.ToInt32(worksheet.Cells[i, 1].Value);
                                applicantDetail.FirstName = worksheet.Cells[i, 2].Value != null ? worksheet.Cells[i, 2].Value.ToString() : null;
                                applicantDetail.LastName = worksheet.Cells[i, 3].Value != null ? worksheet.Cells[i, 3].Value.ToString() : null;
                                applicantDetail.Email = worksheet.Cells[i, 4].Value != null ? worksheet.Cells[i, 4].Value.ToString() : null;


                                applicantDetails.Add(applicantDetail);
                            }

                            if (applicantDetails?.Count() > 0)
                            {

                                excelUploadRecordCountDto = await _jobVacancyService.ProcessApplicantDetailsFromExcel(applicantDetails, jobVacancyId);
                            }
                        }
                        else
                        {
                            return BadRequest(new { Message = "Excel Sheet is empty and Invalid" });
                        }
                    }

                    return Ok(new
                    {
                        Message = "Excel Sheet was Uploaded successfully." + excelUploadRecordCountDto.SuccessfullUpload + " : Success and" + excelUploadRecordCountDto.FailedUpload + " : Failed.",
                        Success = excelUploadRecordCountDto.SuccessfullUpload,
                        Failed = excelUploadRecordCountDto.FailedUpload,
                        Updated = excelUploadRecordCountDto.UpdatedRecords
                    });
                }

                return BadRequest(new { Message = "Oops...something went wrong" });
            }
            catch (Exception ex) { throw ex; }
        }

        [HttpPut("[action]")]
        public async Task<int> ActivateJob(long jobVacancyId) => await _jobVacancyService.ActivateJob(jobVacancyId);

        [HttpGet("[action]")]
        public async Task<List<JobVacancyDto>> DisplayJobVacancy()
        {
            return await _context.JOB_VACANCY.Where(v => v.Active == true)
                .Include(jt => jt.JobType)
               .Select(f => new JobVacancyDto
               {
                   VacancyName = f.Name,
                   JobId = f.Id,
                   JobType = f.JobType.Name
               }).ToListAsync();
            //return getJob;
        }
        [HttpDelete("[action]")]
        public async Task<int> DeleteJobRecipient(long id) => await _jobVacancyService.DeleteJobRecipient(id);
        [HttpPost("[action]")]
        public async Task<int> ResendEmailLink(long id, string guid) => await _jobVacancyService.ResendEmailLink(id, guid);


    }
}