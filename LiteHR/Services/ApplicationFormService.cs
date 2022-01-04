//using LiteHR.Dtos;
//using LiteHR.Infrastructure;
//using LiteHR.Interface;
//using LiteHR.Models;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace LiteHR.Services
//{

//    public class ApplicationFormService : IApplicationFormService
//    {
//        private readonly HRContext _context;
//        private readonly IEmailBodyService _emailService;

//        public ApplicationFormService(HRContext context, IEmailBodyService emailService)
//        {
//            _context = context;
//            _emailService = emailService;
//        }


//        public async Task<ApplicationForm> AddApplicationForm(ApplicationFormSubmit applicationForm)
//        {
//            try
//            {
//                var score = 0m;

//                var person = applicationForm.Person;
//                _context.PERSON.Add(applicationForm.Person);

//                for (int i = 0; i < applicationForm.PersonCertifications.ToList().Count; i++)
//                {
//                    applicationForm.PersonCertifications[i].Person = person;
//                    _context.PERSON_CERTIFICATION.Add(applicationForm.PersonCertifications[i]);
//                    _context.SaveChanges();
//                }
//                for (int i = 0; i < applicationForm.PersonEducations.ToList().Count; i++)
//                {
//                    applicationForm.PersonEducations[i].Person = person;
//                    _context.PERSON_EDUCATION.Add(applicationForm.PersonEducations[i]);
//                    _context.PERSON_EDUCATION.AsNoTracking();
//                }
//                for (int i = 0; i < applicationForm.PersonExperiences.ToList().Count; i++)
//                {
//                    applicationForm.PersonExperiences[i].Person = person;
//                    _context.PERSON_EXPERIENCE.Add(applicationForm.PersonExperiences[i]);
//                    _context.PERSON_EXPERIENCE.AsNoTracking();
//                }
//                for (int i = 0; i < applicationForm.PersonJournals.ToList().Count; i++)
//                {
//                    applicationForm.PersonJournals[i].Person = person;
//                    _context.PERSON_JOURNAL.Add(applicationForm.PersonJournals[i]);
//                    _context.PERSON_JOURNAL.AsNoTracking();
//                }
//                for (int i = 0; i < applicationForm.PersonProfessionalBodies.ToList().Count; i++)
//                {
//                    applicationForm.PersonProfessionalBodies[i].Person = person;
//                    _context.PERSON_PROFESSIONAL_BODY.Add(applicationForm.PersonProfessionalBodies[i]);
//                    _context.PERSON_PROFESSIONAL_BODY.AsNoTracking();
//                }
//                for (int i = 0; i < applicationForm.PersonReferees.ToList().Count; i++)
//                {
//                    applicationForm.PersonReferees[i].Person = person;
//                    _context.PERSON_REFEREE.Add(applicationForm.PersonReferees[i]);
//                    _context.PERSON_REFEREE.AsNoTracking();
//                }
//                for (int i = 0; i < applicationForm.PersonResearchGrants.ToList().Count; i++)
//                {
//                    applicationForm.PersonResearchGrants[i].Person = person;
//                    _context.PERSON_RESEARCH_GRANT.Add(applicationForm.PersonResearchGrants[i]);
//                    _context.PERSON_RESEARCH_GRANT.AsNoTracking();
//                }
//                var vacancy = await GetJobVacancy(applicationForm.JobVacancyId);
//                score = GradeApplicationSubmission(applicationForm, vacancy.ApplicationSectionWeight);
//                ApplicationForm creatApplicationForm = new ApplicationForm()
//                {
//                    DateSubmitted = DateTime.Now,
//                    ApplicationScore = score,
//                    Active = true,
//                    InterviewEmailSent = false,
//                    JobVacancyId = applicationForm.JobVacancyId,
//                    Person = applicationForm.Person,
//                    //PersonCertifications = applicationForm.PersonCertifications,
//                    //PersonEducations = applicationForm.PersonEducations,
//                    //PersonExperiences = applicationForm.PersonExperiences,
//                    //PersonProfessionalBodies = applicationForm.PersonProfessionalBodies,
//                    //PersonJournals = applicationForm.PersonJournals,
//                    //PersonReferees = applicationForm.PersonReferees,
//                    //PersonResearchGrants = applicationForm.PersonResearchGrants

//                };

//                //applicationForm.DateSubmitted = DateTime.Now;
//                //applicationForm.ApplicationScore = score;
//                _context.APPLICATION.Add(creatApplicationForm);
//                await _context.SaveChangesAsync();

//                var appForm= await _context.APPLICATION
//                    .Include(m => m.Person)
//                    .ThenInclude(m => m.PersonCertifications)
//                    .Include(m => m.Person.PersonEducations)
//                    .Include(m => m.Person.PersonExperiences)
//                    .Include(m => m.Person.PersonJournals)
//                    .Include(m => m.Person.PersonProfessionalBodies)
//                    .Include(m => m.Person.PersonReferees)
//                    .Include(m => m.Person.PersonResearchGrants)
//                    .SingleOrDefaultAsync(m => m.Id == creatApplicationForm.Id);

//                //Create an email body and send messag to the new applicant
//                StringBuilder sb = new StringBuilder(string.Format("Dear {0}, ", appForm.Person.Firstname));
//                sb.Append(string.Format("You have sucessfully applied for {0}.", appForm.JobVacancy.Name));
//                sb.Append("More details will be communicated to you shortly.");

//                EmailBody emailBody = new EmailBody()
//                {
//                    Subject = "Job Application Submission",
//                    Message = sb.ToString()
//                };

//                List<EmailBody> emailBodies = new List<EmailBody> { emailBody };
//                await _emailService.SendEmailMessage(emailBodies);

//                return appForm;
//            }
//            catch (Exception ex)
//            {

//                throw ex;
//            }

//        }

//        public async Task<ApplicationForm> GetApplicationForm(long id)
//        {
//            try
//            {
//                return await _context.APPLICATION
//                    .Include(m => m.Person)
//                    .Include(m => m.Person.PersonCertifications)
//                    .Include(m => m.Person.PersonEducations)
//                    .Include(m => m.Person.PersonExperiences)
//                    .Include(m => m.Person.PersonJournals)
//                    .Include(m => m.Person.PersonProfessionalBodies)
//                    .Include(m => m.Person.PersonReferees)
//                    .Include(m => m.Person.PersonResearchGrants)
//                    .SingleOrDefaultAsync(m => m.Id == id);
//            }
//            catch (Exception ex)
//            {

//                throw ex;
//            }

//        }

//        public async Task<List<ApplicationForm>> AllApplicationForm()
//        {
//            return await _context.APPLICATION
//                    .Include(m => m.Person)
//                    .Include(m => m.Person.PersonCertifications)
//                    .Include(m => m.Person.PersonEducations)
//                    .Include(m => m.Person.PersonExperiences)
//                    .Include(m => m.Person.PersonJournals)
//                    .Include(m => m.Person.PersonProfessionalBodies)
//                    .Include(m => m.Person.PersonReferees)
//                    .Include(m => m.Person.PersonResearchGrants)
//                    .OrderBy(p => p.DateSubmitted).ToListAsync();
//        }

//        public async Task<int> GetApplicationFormCount()
//        {
//            return await _context.APPLICATION
//               .CountAsync();
//        }
//        public async Task<JobVacancy> GetJobVacancy(long Id)
//        {
//            return await _context.JOB_VACANCY
//                .Include(m => m.ApplicationSectionWeight)
//                .SingleOrDefaultAsync(m => m.Id == Id);
//        }
//        public decimal GradeApplicationSubmission(ApplicationFormSubmit applicationForm, List<ApplicationSectionWeight> applicationSectionWeights)
//        {
//            var score = 0M;
//            try
//            {
//                foreach (var item in applicationSectionWeights)
//                {
//                    if (item.ApplicationSectionHeaderId == (int)ApplicationEnum.EducationandQualification && applicationForm.PersonEducations?.Count > 0)
//                    {
//                        score += item.Weight;
//                    }
//                    if (item.ApplicationSectionHeaderId == (int)ApplicationEnum.JournalandPublication && applicationForm.PersonJournals?.Count > 0)
//                    {
//                        score += item.Weight;
//                    }
//                    if (item.ApplicationSectionHeaderId == (int)ApplicationEnum.ProfessionalBody && applicationForm.PersonProfessionalBodies?.Count > 0)
//                    {
//                        score += item.Weight;
//                    }
//                    if (item.ApplicationSectionHeaderId == (int)ApplicationEnum.ProfessionalQualification && applicationForm.PersonCertifications?.Count > 0)
//                    {
//                        score += item.Weight;
//                    }
//                    if (item.ApplicationSectionHeaderId == (int)ApplicationEnum.Referee && applicationForm.PersonReferees?.Count > 0)
//                    {
//                        score += item.Weight;
//                    }
//                    if (item.ApplicationSectionHeaderId == (int)ApplicationEnum.ResearchGrant && applicationForm.PersonResearchGrants?.Count > 0)
//                    {
//                        score += item.Weight;
//                    }
//                    if (item.ApplicationSectionHeaderId == (int)ApplicationEnum.WorkExperience && applicationForm.PersonExperiences?.Count > 0)
//                    {
//                        score += item.Weight;
//                    }
//                }
//                return score;
//            }
//            catch (Exception ex)
//            {

//                throw ex;
//            }
//        }

//        public async Task<ApplicationForm> GetCV(long personId)
//        {
//            try
//            {
//                return await _context.APPLICATION
//                                     .Where(ap => ap.PersonId == personId)
//                                     .Include(ap => ap.JobVacancy)
//                                     .Include(ap => ap.Person)
//                                     .ThenInclude(ap => ap.MaritalStatus)
//                                     .Include(ap => ap.Person.Religion)
//                                     .Include(ap => ap.Person.State)
//                                     .Include(ap => ap.Person.Lga)
//                                     .Include(ap => ap.Person.Religion)
//                                     .Include(ap => ap.Person.PersonCertifications)
//                                     .Include(ap => ap.Person.PersonEducations)
//                                     .Include(ap => ap.Person.PersonJournals)
//                                     .Include(ap => ap.Person.PersonProfessionalBodies)
//                                     .Include(ap => ap.Person.PersonReferees)
//                                     .Include(ap => ap.Person.PersonResearchGrants)
//                                     .Include(ap => ap.Person.PersonExperiences)
//                                     .FirstOrDefaultAsync();
//            }
//            catch (Exception ex) { throw ex; }
//        }



     
//        public async Task<User> GetUserByUsername(string username)
//        {
//            try
//            {
//                return await _context.USER.Where(u => u.Username == username).FirstOrDefaultAsync();
//            }
//            catch (Exception ex) { throw ex; }
//        }

//        public async Task<Staff> DoesStaffNumberExist(string staffNumber)
//        {
//            try
//            {
//                return await _context.STAFF
//                                     .Where(s => s.GeneratedStaffNumber != null && s.GeneratedStaffNumber == staffNumber)
//                                     .FirstOrDefaultAsync();
//            }
//            catch (Exception ex) { throw ex; }
//        }

//        public async Task<Lga> GetLgaByName(string lga)
//        {
//            try
//            {
//                return await _context.LGA.Where(lg => lg.Name == lga).FirstOrDefaultAsync();
//            }
//            catch (Exception ex) { throw ex; }
//        }

//        public async Task<State> GetStateByName(string state)
//        {
//            try
//            {
//                State stateExists = await _context.STATE.Where(s => s.Name == state).FirstOrDefaultAsync();
//                if (stateExists == null)
//                {
//                    stateExists = new State() 
//                    {
//                        Name = state,
//                        Active = true
//                    };

//                    _context.Add(stateExists);
//                    await _context.SaveChangesAsync();
//                }
//                return stateExists;
//            }
//            catch (Exception ex) { throw ex; }
//        }

//        public async Task<Gender> MapGenderCharacterToObject(string genderCharacter)
//        {
//            try
//            {
//                return await _context.GENDER.Where(g => g.Name.StartsWith(genderCharacter)).FirstOrDefaultAsync();
//            }
//            catch (Exception ex) { throw ex; }
//        }

//        public async Task<InstitutionDepartment> GetOrCreateDepartment(string departmentName)
//        {
//            try
//            {
//                InstitutionDepartment departmentExists = await _context.INSTITUTION_DEPARTMENT
//                                                                       .Where(d => d.Name == departmentName)
//                                                                       .FirstOrDefaultAsync();

//                if (departmentExists == null)
//                {
//                    departmentExists = new InstitutionDepartment()
//                    {
//                        Name = departmentName,
//                        Active = true
//                    };

//                    _context.Add(departmentExists);
//                    await _context.SaveChangesAsync();
//                }

//                return departmentExists;

//            }
//            catch (Exception ex) { throw ex; }
//        }

//        public async Task<string> ComposeStaffNumber()
//        {
//            try
//            {
//                int range = 1;//When range gets set on the DB, Make this variable dynamic
//                string staffNumber = string.Format("NAU/ST/{0}", AddLeadingZeros(range));
//                while(await DoesStaffNumberExist(staffNumber) != null)
//                {
//                    range += 1;
//                    staffNumber = string.Format("NAU/ST/{0}", AddLeadingZeros(range));
//                }

//                return staffNumber;
//            }
//            catch (Exception ex) { throw ex; }
//        }

//        public string AddLeadingZeros(long number)
//        {
//            int noOfZeros = (5 - number.ToString().Length);
//            string zeros = string.Empty;

//            if (noOfZeros > 0)
//            {
//                for (int i = 0; i < noOfZeros; i++)
//                {
//                    zeros += 0;
//                }
//            }
//            return string.Format("{0}{1}", zeros, number);
//        }

//        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
//        {
//            if (password == null) throw new ArgumentNullException("password");
//            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

//            using (var hmac = new System.Security.Cryptography.HMACSHA512())
//            {
//                passwordSalt = hmac.Key;
//                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
//            }
//        }

//        public DateTime ConvertToDate(string date)
//        {
//            DateTime newDate = new DateTime();
//            try
//            {
//                string[] dateSplit = date.Split('/');
//                int year = Convert.ToInt16(dateSplit[2].Split(' ')[0]);
//                newDate = new DateTime(year, Convert.ToInt16(dateSplit[1]), Convert.ToInt32(dateSplit[0]));
//            }
//            catch (Exception ex) { throw ex; }

//            return newDate;
//        }
//        public async Task<long> CreateApplicationForm(ApplicationFormDto applicationFormDto)
//        {
//            try
//            {
//                var score = 0m;


//                Person person = new Person()
//                {
//                    Address = applicationFormDto.Person.Address,
//                    BirthDay = applicationFormDto.Person.BirthDay,
//                    Email = applicationFormDto.Person.Email,
//                    StateId = applicationFormDto.Person.StateId,
//                    Surname = applicationFormDto.Person.Surname,
//                    LGAId = applicationFormDto.Person.LGAId,
//                    Firstname = applicationFormDto.Person.Firstname,
//                    GenderId = applicationFormDto.Person.GenderId,
//                    ImageUrl = applicationFormDto.Person.ImageUrl,
//                    MaritalStatusId = applicationFormDto.Person.MaritalStatusId,
//                    Othername = applicationFormDto.Person.Othername,
//                    ReligionId = applicationFormDto.Person.ReligionId,
//                    PhoneNumber = applicationFormDto.Person.PhoneNumber,

//                };
//                _context.Add(person);
//                _context.SaveChanges();
//                if (applicationFormDto?.PersonCertification.Count > 0)
//                {
//                    foreach(var item in applicationFormDto.PersonCertification)
//                    {
//                        PersonCertification personCertification = new PersonCertification()
//                        {
//                            Issuer = item.Issuer,
//                            Name = item.Name,
//                            PersonId = person.Id,
//                            Year = item.Year
//                        };
//                        _context.Add(personCertification);
//                    }
                    
//                }
//                if (applicationFormDto.PersonEducation.Count > 0)
//                {
//                    foreach (var item in applicationFormDto.PersonEducation)
//                    {
//                        PersonEducation personEducation = new PersonEducation()
//                        {
//                            Course = item.CourseOfStudy,
//                            EducationalQualificationId = item.QualificationId,
//                            PersonId = person.Id,
//                            Year = item.Year,
//                            Institution=item.InstitutionName,
                            
//                        };
//                        _context.Add(personEducation);
//                    }

//                }

//                var vacancy = await GetJobVacancy(applicationFormDto.JobVacancyId);
//                //score = GradeApplicationSubmission(applicationFormDto, vacancy.ApplicationSectionWeight);
//                ApplicationForm creatApplicationForm = new ApplicationForm()
//                {
//                    DateSubmitted = DateTime.Now,
//                    ApplicationScore = score,
//                    Active = true,
//                    InterviewEmailSent = false,
//                    JobVacancyId = applicationFormDto.JobVacancyId,
//                    Person = person,
//                    //PersonCertifications = applicationForm.PersonCertifications,
//                    //PersonEducations = applicationForm.PersonEducations,
//                    //PersonExperiences = applicationForm.PersonExperiences,
//                    //PersonProfessionalBodies = applicationForm.PersonProfessionalBodies,
//                    //PersonJournals = applicationForm.PersonJournals,
//                    //PersonReferees = applicationForm.PersonReferees,
//                    //PersonResearchGrants = applicationForm.PersonResearchGrants

//                };

//                //applicationForm.DateSubmitted = DateTime.Now;
//                //applicationForm.ApplicationScore = score;
//                _context.Add(creatApplicationForm);
//                await _context.SaveChangesAsync();
//                return creatApplicationForm.Id;
//                //var appForm = await _context.APPLICATION
//                //    .Include(m => m.Person)
//                //    .ThenInclude(m => m.PersonCertifications)
//                //    .Include(m => m.Person.PersonEducations)
//                //    .Include(m => m.Person.PersonExperiences)
//                //    .Include(m => m.Person.PersonJournals)
//                //    .Include(m => m.Person.PersonProfessionalBodies)
//                //    .Include(m => m.Person.PersonReferees)
//                //    .Include(m => m.Person.PersonResearchGrants)
//                //    .SingleOrDefaultAsync(m => m.Id == creatApplicationForm.Id);

//                ////Create an email body and send messag to the new applicant
//                //StringBuilder sb = new StringBuilder(string.Format("Dear {0}, ", appForm.Person.Firstname));
//                //sb.Append(string.Format("You have sucessfully applied for {0}.", appForm.JobVacancy.Name));
//                //sb.Append("More details will be communicated to you shortly.");

//                //EmailBody emailBody = new EmailBody()
//                //{
//                //    Subject = "Job Application Submission",
//                //    Message = sb.ToString()
//                //};

//                //List<EmailBody> emailBodies = new List<EmailBody> { emailBody };
//                //await _emailService.SendEmailMessage(emailBodies);

//                //return appForm;
//            }
//            catch (Exception ex)
//            {

//                throw ex;
//            }

//        }
//    }
//}
