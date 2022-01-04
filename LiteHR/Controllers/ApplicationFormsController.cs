//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using LiteHR.Models;
//using LiteHR.Services;
//using LiteHR.Interface;
//using System.IO;
//using OfficeOpenXml;
//using LiteHR.Dtos;

//namespace LiteHR.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ApplicationFormsController : ControllerBase
//    {
//        private readonly HRContext _context;
//        private readonly IApplicationFormService _service;

//        public ApplicationFormsController(HRContext context, IApplicationFormService service)
//        {
//            _context = context;
//            _service = service;
//        }

//        // GET: api/ApplicationForms
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<ApplicationForm>>> GetAPPLICATION()
//        {
//            return await _context.APPLICATION.Include(x => x.Person)
//                .Include(x=>x.JobVacancy)
                
//                .OrderByDescending(x=>x.ApplicationScore)
//                .ToListAsync();
//        }
//        [HttpGet("/api/ApplicationForm/Invited")]
//        public async Task<ActionResult<IEnumerable<ApplicationForm>>> GetInvitedAPPLICATION()
//        {
//            return await _context.APPLICATION.Where(m=>m.InterviewEmailSent).Include(x => x.Person)
//                .Include(x => x.JobVacancy)
//                .OrderByDescending(x => x.ApplicationScore)
//                .ToListAsync();
//        }
//        [HttpGet("/api/ApplicationForm/UnInvited")]
//        public async Task<ActionResult<IEnumerable<ApplicationForm>>> GetUnInvitedAPPLICATION()
//        {
//            return await _context.APPLICATION.Where(m => m.InterviewEmailSent==false).Include(x => x.Person)
//                .Include(x => x.JobVacancy)

//                .OrderByDescending(x => x.ApplicationScore)
//                .ToListAsync();
//        }
//        // GET: api/ApplicationForms/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<ApplicationForm>> GetApplicationForm(long id)
//        {
//            //var applicationForm = await _context.APPLICATION
//            //    .Include(x => x.Person)
//            //    .Include(x => x.PersonCertifications)
//            //    .Include(x => x.PersonEducations)
//            //    .Include(x => x.PersonExperiences)
//            //    .Include(x => x.PersonJournals)
//            //    .Include(x => x.PersonProfessionalBodies)
//            //    .Include(x => x.PersonReferees)
//            //    .Include(x => x.PersonResearchGrants)
//            //    .Where(x => x.Id == id).FirstOrDefaultAsync();
//            var applicationForm =await _service.GetApplicationForm(id);

//            if (applicationForm == null)
//            {
//                return NotFound();
//            }

//            return applicationForm;
//        }

//        [HttpGet("[action]")]
//        public async Task<ActionResult<ApplicationForm>> GetApplicantCV(long personId)
//        {
//            try
//            {
//                ApplicationForm CV = await _service.GetCV(personId);
//                if (CV == null || (string.IsNullOrEmpty(CV.Person.Surname) && string.IsNullOrEmpty(CV.Person.Firstname)))
//                {
//                    return NoContent();
//                }
//                return CV;
//            }
//            catch (Exception ex) { throw ex; }
//        }

//        // PUT: api/ApplicationForms/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
//        // more details see https://aka.ms/RazorPagesCRUD.
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutApplicationForm(long id, ApplicationForm applicationForm)
//        {
//            if (id != applicationForm.Id)
//            {
//                return BadRequest();
//            }

//            _context.Entry(applicationForm).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!ApplicationFormExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/ApplicationForms
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
//        // more details see https://aka.ms/RazorPagesCRUD.
//        [HttpPost]
//        public async Task<ActionResult<ApplicationForm>> PostApplicationForm(ApplicationFormSubmit applicationForm)
//        {
//            //_context.APPLICATION.Add(applicationForm);
//            //var person = applicationForm.Person;
//            //_context.PERSON.Add(applicationForm.Person);

//            //for (int i = 0; i < applicationForm.PersonCertifications.ToList().Count; i++)
//            //{
//            //    applicationForm.PersonCertifications[i].Person = person;
//            //}
//            //for (int i = 0; i < applicationForm.PersonEducations.ToList().Count; i++)
//            //{
//            //    applicationForm.PersonEducations[i].Person = person;
//            //}
//            //for (int i = 0; i < applicationForm.PersonExperiences.ToList().Count; i++)
//            //{
//            //    applicationForm.PersonExperiences[i].Person = person;
//            //}
//            //for (int i = 0; i < applicationForm.PersonJournals.ToList().Count; i++)
//            //{
//            //    applicationForm.PersonJournals[i].Person = person;
//            //}
//            //for (int i = 0; i < applicationForm.PersonProfessionalBodies.ToList().Count; i++)
//            //{
//            //    applicationForm.PersonProfessionalBodies[i].Person = person;
//            //}
//            //for (int i = 0; i < applicationForm.PersonReferees.ToList().Count; i++)
//            //{
//            //    applicationForm.PersonReferees[i].Person = person;
//            //}
//            //for (int i = 0; i < applicationForm.PersonResearchGrants.ToList().Count; i++)
//            //{
//            //    applicationForm.PersonResearchGrants[i].Person = person;
//            //}
//            //_context.APPLICATION.Add(applicationForm);
//            //await _context.SaveChangesAsync();

//            //return CreatedAtAction("GetApplicationForm", new { id = applicationForm.Id }, applicationForm);
//            var newApplication=await _service.AddApplicationForm(applicationForm);
//            return CreatedAtAction("GetApplicationForm", new { id = newApplication.Id }, newApplication);
//        }

//        [HttpPost("[action]")]
//        public async Task<IActionResult> UploadDepartmentListFromExcelSheet(IFormFile file)
//        {
//            try
//            {
//                long size = file.Length;

//                if (size > 0)
//                {
//                    var filePath = Path.GetTempFileName();
//                    using (var stream = System.IO.File.Create(filePath))
//                    {
//                        await file.CopyToAsync(stream);

//                        //Initialize the Data from the excel sheet into stream
//                        ExcelPackage package = new ExcelPackage(stream);
//                        ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();

//                        if (worksheet != null)
//                        {
//                            //Give 2 row space from the top to allow for Excel Headers
//                            int totalRows = worksheet.Dimension.Rows;

//                            for (int i = 2; i <= totalRows; i++)
//                            {
//                                int serialNumber = Convert.ToInt32(worksheet.Cells[i, 1].Value);
//                                string departmentName = worksheet.Cells[i, 2].Value.ToString();

//                                var departmentExists = await  _context.INSTITUTION_DEPARTMENT
//                                                                      .Where(d => d.Name == departmentName)
//                                                                      .FirstOrDefaultAsync();

//                                if (departmentExists == null)
//                                {
//                                    var newDepartment = new InstitutionDepartment()
//                                    {
//                                        Name = departmentName,
//                                        Active = true
//                                    };

//                                    _context.Add(newDepartment);
//                                    await _context.SaveChangesAsync();
//                                }
//                            }
//                        }
//                        else
//                        {
//                            return BadRequest(new { Message = "Excel Sheet is empty and Invalid" });
//                        }
//                    }

//                    return Ok(new { Message = "Excel Sheet was backed up successfully" });
//                }

//                return NoContent();
//            }
//            catch (Exception ex) { throw ex; }
//        }

       
//        // DELETE: api/ApplicationForms/5
//        [HttpDelete("{id}")]
//        public async Task<ActionResult<ApplicationForm>> DeleteApplicationForm(long id)
//        {
//            var applicationForm = await _context.APPLICATION.FindAsync(id);
//            if (applicationForm == null)
//            {
//                return NotFound();
//            }

//            _context.APPLICATION.Remove(applicationForm);
//            await _context.SaveChangesAsync();

//            return applicationForm;
//        }

//        private bool ApplicationFormExists(long id)
//        {
//            return _context.APPLICATION.Any(e => e.Id == id);
//        }
//        [HttpPost("CreatApplicationForm")]
//        public async Task<ActionResult<long>> CreatApplicationForm(ApplicationFormDto applicationForm)
//        {

//            var newApplication = await _service.CreateApplicationForm(applicationForm);
//            return CreatedAtAction("GetApplicationForm", new { id = newApplication }, newApplication);
//        }
//    }
//}
