using LiteHR.Dtos;
using LiteHR.Helpers;
using LiteHR.Infrastructure;
using LiteHR.Interface;
using LiteHR.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;



namespace LiteHR.Services
{
    public class StaffService : IStaffService
    {
        private readonly HRContext _context;
        private readonly IFileUpload _fileUpload;
        private readonly IConfiguration _configuration;
        private readonly string baseUrl;
        public StaffService(HRContext context, IFileUpload fileUpload, IConfiguration configuration)
        {
            _context = context;
            _fileUpload = fileUpload;
            _configuration = configuration;
            baseUrl = _configuration.GetValue<string>("Url:root");
        }
        AppReturnObject appReturnObject = new AppReturnObject()
        {
            Status = (long)StatusCode.Status,

        };

        public async Task<Staff> AddStaff(Staff staff)
        {
            try
            {
                _context.Add(staff);
                await _context.SaveChangesAsync();

                return await _context.STAFF
                    .Include(m => m.Category)
                    .Include(m => m.StaffType)
                    .Include(m => m.Rank)
                    .ThenInclude(m => m.InstitutionUnit)
                    .Include(m => m.Appointment)
                    //.Include(m => m.Unit)
                    .Include(m => m.Department)
                    .Include(m => m.Person)
                    .ThenInclude(p => p.Gender)
                    .Include(m => m.Person)
                    .ThenInclude(p => p.Lga)
                    .Include(m => m.Person)
                    .ThenInclude(p => p.State)
                    .Include(m => m.Person)
                    .ThenInclude(p => p.MaritalStatus)
                    .Include(m => m.Person)
                    .ThenInclude(p => p.Religion)
                    .Include(m => m.Person)
                    .ThenInclude(p => p.Gender)

                    .SingleOrDefaultAsync(m => m.Id == staff.Id);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<Staff> GetStaff(long id)
        {
            StaffSalaryCatRefDto staffSalaryCatRefDto = new StaffSalaryCatRefDto();
            try
            {
                await AutomateStaffStatus(id);
                var staffSalaryCat = await _context.STAFF_SALARY_CATEGORY_REFERENCE.Where(ss => ss.StaffId == id)
                    .Include(s => s.Staff)
                    .Include(s => s.SalaryGradeCategory)
                    .Include(s => s.SalaryLevel)
                    .Include(s => s.SalaryStep)
                    .FirstOrDefaultAsync();
                var staff = await _context.STAFF
                    .Include(m => m.Category)
                    .Include(m => m.StaffType)
                    .Include(m => m.Rank)
                    .ThenInclude(m => m.InstitutionUnit)
                    .Include(m => m.Appointment)
                    //.Include(m => m.Unit)
                    .Include(m => m.Department)
                    .ThenInclude(f => f.Faculty)
                    .Include(m => m.Person)
                    .ThenInclude(p => p.Gender)
                    .Include(m => m.Person)
                    .ThenInclude(p => p.Lga)
                    .Include(m => m.Person)
                    .ThenInclude(p => p.State)
                    .Include(m => m.Person)
                    .ThenInclude(p => p.MaritalStatus)
                    .Include(m => m.Person)
                    .ThenInclude(p => p.Religion)
                    .Include(m => m.Person)
                    .ThenInclude(p => p.Gender)
                    //.Include(m => m.Person).ThenInclude(p => p.PersonEducations)
                    .Include(m => m.AppointmentType)

                    .SingleOrDefaultAsync(m => m.Id == id);
                if (staff != null)
                    staff.Person.ImageUrl = staff.Person.ImageUrl != null ?  baseUrl + staff.Person.ImageUrl : null;
                //staffSalaryCatRefDto.SalaryCategoryId = staffSalaryCat.SalaryGradeCategoryId;
                //staffSalaryCatRefDto.SalaryLevelId = staffSalaryCat.SalaryLevelId;
                //staffSalaryCatRefDto.SalaryStepId = staffSalaryCat.SalaryStepId;
                return staff;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<List<Staff>> AllStaff()
        {
            return await _context.STAFF
                .Where(s => s.AppointmentTypeId != 2 && s.IsDisengaged == false)
               .Include(m => m.Category)
               .Include(m => m.StaffType)
               .Include(m => m.Rank)
               .ThenInclude(m => m.InstitutionUnit)
               //.Include(m => m.Unit)
               .Include(m => m.Department)
               .Include(m => m.Person)
               .ThenInclude(p => p.Gender)
               .Include(m => m.Person)
               .ThenInclude(p => p.Lga)
               .Include(m => m.Person)
               .ThenInclude(p => p.State)
               .Include(m => m.Person)
               .ThenInclude(p => p.MaritalStatus)
               .Include(m => m.Person)
               .ThenInclude(p => p.Religion)
               .Include(m => m.Person)

               .ThenInclude(p => p.Gender)
               .Include(m => m.AppointmentType).ToListAsync();
        }

        public async Task<List<Staff>> AddStaff(List<Staff> staffList)
        {
            var AddedList = new List<Staff>();

            staffList.ForEach(async staff =>
            {
                AddedList.Add(await AddStaff(staff));
            });

            return AddedList;
        }

        public async Task<bool> UpdateStaff(long id, Staff staff)
        {

            try
            {
                var status = 0;
                var _staff = await GetStaff(id);
                if (_staff != null)
                {
                    _staff.StaffNumber = staff.StaffNumber;
                    _staff.Person.ImageUrl = staff.Person.ImageUrl;
                    _staff.Person.Surname = staff.Person.Surname;
                    _staff.Person.Firstname = staff.Person.Firstname;
                    _staff.Person.Othername = staff.Person.Othername;
                    _staff.Person.BirthDay = staff.Person.BirthDay;
                    _staff.Person.Address = staff.Person.Address;
                    _staff.Person.Email = staff.Person.Email;
                    _staff.Person.PhoneNumber = staff.Person.PhoneNumber;
                    _staff.Person.StateId = staff.Person.StateId;
                    _staff.Person.LGAId = staff.Person.LGAId;
                    _staff.Person.MaritalStatusId = staff.Person.MaritalStatusId;
                    _staff.Person.ReligionId = staff.Person.ReligionId;
                    _staff.Person.GenderId = staff.Person.GenderId;
                    _staff.RankId = staff.RankId;
                    _staff.DepartmentId = staff.DepartmentId;
                    _staff.AppointmentId = staff.AppointmentId;
                    //_staff.UnitId = staff.UnitId;
                    _staff.Person.PersonEducations = staff.Person.PersonEducations;
                    _staff.StaffTypeId = staff.StaffTypeId;
                    _staff.CategoryId = staff.CategoryId;
                    _context.Update(_staff);
                    status = await _context.SaveChangesAsync();
                }

                return (status > 0) ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public async Task<int> GetStaffCount()
        {
            return await _context.STAFF
               .CountAsync();
        }
        public async Task<string> AutomateStaffStatus(long staffId)
        {
            string status = "";
            try
            {
               // var leaveStatus = await _context.STAFF_LEAVE_REQUEST.SingleOrDefaultAsync(m => m.StaffId == staffId && m.Approved && m.StartDate <= DateTime.Now && m.EndDate > DateTime.Now);
                //if (leaveStatus?.Id > 0)
                //{
                //    var existingStatus = await _context.STAFF_STATUS.SingleOrDefaultAsync(m => m.StaffId == leaveStatus.StaffId && m.Active);
                //    if (existingStatus?.Id > 0 && existingStatus.StatusName != "On-Leave")
                //    {
                //        return status = existingStatus.StatusName;
                //    }
                //    else if (existingStatus == null)
                //    {
                //        StaffStatus staffStatus = new StaffStatus()
                //        {
                //            Active = true,
                //            DateLogged = DateTime.Now,
                //            StaffId = leaveStatus.StaffId,
                //            StatusName = "On-Leave"
                //        };
                //        _context.Add(staffStatus);
                //        _context.SaveChanges();
                //        return status = "On-Leave";
                //    }
                //}
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return status;
        }

        public async Task<List<StaffGradeBenefitDto>> GetStaffPayrollByDepartmentMonthAndYear(int departmentId, int month, int year)
        {
            try
            {
                List<StaffGradeBenefitDto> staffByDepartment = new List<StaffGradeBenefitDto>();
                var staffGradeList = await _context.STAFF_GRADE.Where(sg => sg.Staff.Department.Id == departmentId && sg.DatePromoted.Month == month && sg.DatePromoted.Year == year)
                                                               .Include(sg => sg.Staff)

                                                               .ThenInclude(sg => sg.Department)
                                                               .Include(sg => sg.Staff.Person)
                                                               .ToListAsync();

                if (staffGradeList?.Count() > 0)
                {
                    foreach (var item in staffGradeList)
                    {
                        var staffBenefit = await _context.GRADE_BENEFIT.Where(sg => sg.SalaryGrade.Id == item.SalaryGradeId)
                                                                       .Include(sg => sg.SalaryType)
                                                                       .Include(sg => sg.SalaryGrade)
                                                                       .Include(sg => sg.SalaryGrade.SalaryGradeCategory)
                                                                       .Include(sg => sg.SalaryGrade.SalaryLevel)
                                                                       .Include(sg => sg.SalaryGrade.SalaryStep)
                                                                       .ToListAsync();

                        if (staffBenefit != null)
                        {
                            var staffExtraearning = await _context.SALARY_EXTRA_EARNING.Where(f => f.StaffId == item.StaffId).ToListAsync();
                            StaffGradeBenefitDto staffGradeBenefits = new StaffGradeBenefitDto();
                            staffGradeBenefits.Staff = item.Staff;
                            staffGradeBenefits.Department = item.Staff.Department;
                            staffGradeBenefits.GradeBenefits = staffBenefit;
                            staffGradeBenefits.SalaryGradeCategory = staffBenefit.FirstOrDefault().SalaryGrade.SalaryGradeCategory;
                            staffGradeBenefits.SalaryLevel = staffBenefit.FirstOrDefault().SalaryGrade.SalaryLevel;
                            staffGradeBenefits.SalaryStep = staffBenefit.FirstOrDefault().SalaryGrade.SalaryStep;
                            staffGradeBenefits.SalaryExtraEarnings = staffExtraearning;
                            staffByDepartment.Add(staffGradeBenefits);
                        }
                    }
                }
                return staffByDepartment;
            }
            catch (Exception ex) { throw ex; }
        }
        public async Task<Staff> CreateStaff(CreateStaffDto createStaffDto)
        {
            if (createStaffDto == null)
                return null;

            if (createStaffDto.Staff.StaffNumber != null)
            {
                var existingStaff = await _context.STAFF.Where(f => f.StaffNumber == createStaffDto.Staff.StaffNumber).FirstOrDefaultAsync();
                if (existingStaff != null)
                    throw new AppException("Staff Number Already Exist!");
            }

            string staffNumber = await ComposeStaffNumber();
            //Create a hash for the Password
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash("1234567", out passwordHash, out passwordSalt);
            Person person = new Person()
            {
                Address = createStaffDto.Person.Address,
                BirthDay = createStaffDto.Person.BirthDay,
                Email = createStaffDto.Person.Email,
                Firstname = createStaffDto.Person.Firstname,
                Gender = _context.GENDER.Where(f => f.Id == createStaffDto.Person.GenderId).FirstOrDefault(),
                ImageUrl = createStaffDto.Person.ImageUrl,
                Lga = _context.LGA.Where(f => f.Id == createStaffDto.Person.LGAId).FirstOrDefault(),
                MaritalStatus = _context.MARITAL_STATUS.Where(f => f.Id == createStaffDto.Person.MaritalStatusId).FirstOrDefault(),
                Othername = createStaffDto.Person.Othername,
                Surname = createStaffDto.Person.Surname,
                PhoneNumber = createStaffDto.Person.PhoneNumber,
                Religion = _context.RELIGION.Where(f => f.Id == createStaffDto.Person.ReligionId).FirstOrDefault(),
            };
            _context.Add(person);
            await _context.SaveChangesAsync();
            Staff staff = new Staff()
            {
                Active = true,
                Appointment = _context.INSTITUTION_APPOINTMENT.Where(f => f.Id == createStaffDto.Staff.AppointmentId).FirstOrDefault(),
                DateOfConfirmation = createStaffDto.Staff.DateOfConfirmation,
                AppointmentDate = createStaffDto.Staff.DateOfAppointment,
                Category = _context.INSTITUTION_STAFF_CATEGORY.Where(f => f.Id == createStaffDto.Staff.CategoryId).FirstOrDefault(),
                Department = _context.INSTITUTION_DEPARTMENT.Where(f => f.Id == createStaffDto.Staff.DepartmentId).FirstOrDefault(),
                GeneratedStaffNumber = staffNumber,
                Person = person,
                Rank = _context.INSTITUTION_RANK.Where(f => f.Id == createStaffDto.Staff.RankId).FirstOrDefault(),
                StaffNumber = createStaffDto.Staff.StaffNumber != null ? createStaffDto.Staff.StaffNumber : staffNumber,
                StaffType = _context.INSTITUTION_STAFF_TYPE.Where(f => f.Id == createStaffDto.Staff.StaffTypeId).FirstOrDefault(),
                AppointmentIsConfirmed = createStaffDto.Staff.AppointmentIsConfirmed,
                AppointmentType = _context.APPOINTMENT_TYPE.Where(f => f.Id == createStaffDto.Staff.AppointmentTypeId).FirstOrDefault(),

            };
            _context.Add(staff);
            await _context.SaveChangesAsync();
            User user = new User()
            {
                Username = staffNumber,
                Staff = staff,
                RoleId = 3,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Active = true,
            };
            _context.Add(user);
            var created = await _context.SaveChangesAsync();
            if (created > 0)
                return await GetStaff(staff.Id);
            return null;

        }
        public async Task<List<NewStaffDto>> GetTemporalStaff()
        {
            return await _context.STAFF


                    .Where(f => f.AppointmentTypeId == (long)AppointmentTypes.Temporal || f.AppointmentId == null)
                    .Select(f => new NewStaffDto
                    {
                        FirstName = f.Person.Firstname,
                        OtherName = f.Person.Othername,
                        Surname = f.Person.Surname,
                        Email = f.Person.Email,
                        StaffDepartment = f.Department.Name,
                        StaffRank = f.Rank.Name,
                        StaffDOB = f.Person.BirthDay,
                        UserName = f.GeneratedStaffNumber,
                        StaffIdentityNumber = f.StaffNumber,
                        StaffId = f.Id

                    }).ToListAsync();

        }
        public async Task<long> RegularizeStaff(StaffRegularizationDto staffRegularizationDto)
        {
            if (staffRegularizationDto?.StaffId == 0)
                throw new AppException("Provide required Parameter");
            var staff = await _context.STAFF.Where(f => f.Id == staffRegularizationDto.StaffId).FirstOrDefaultAsync();
            if (staff == null)
                return 0;
            staff.AppointmentType = _context.APPOINTMENT_TYPE.Where(f => f.Id == staffRegularizationDto.AppointmentTypeId).FirstOrDefault();
            staff.DateOfRegularization = staffRegularizationDto.DateOfRegulartization;
            _context.Update(staff);
            await _context.SaveChangesAsync();
            return 200;

        }
        //public async Task<ExcelUploadRecordCountDto> StaffBiometricUpload(IEnumerable<BiometricExcelModel> biometricDetails)
        //{
        //    ExcelUploadRecordCountDto excelUploadRecordCountDto = new ExcelUploadRecordCountDto();
        //    excelUploadRecordCountDto.SuccessfullUpload = 0;
        //    excelUploadRecordCountDto.FailedUpload = 0;
        //    excelUploadRecordCountDto.UpdatedRecords = 0;

        //    return excelUploadRecordCountDto;

        //}

        public async Task<ExcelUploadRecordCountDto> ProcessStaffDetailsFromExcel(IEnumerable<StaffExcelModel> staffDetails)
        {
            ExcelUploadRecordCountDto excelUploadRecordCountDto = new ExcelUploadRecordCountDto();
            StaffAdditionalInfo staffAdditionalInfo = new StaffAdditionalInfo();
            excelUploadRecordCountDto.SuccessfullUpload = 0;
            excelUploadRecordCountDto.FailedUpload = 0;
            excelUploadRecordCountDto.UpdatedRecords = 0;
            var strategy = _context.Database.CreateExecutionStrategy();
            //strategy.Execute();
          
            //using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (staffDetails?.Count() > 0)
                {
                    foreach (StaffExcelModel staff in staffDetails)
                    {
                        var salCat = Regex.Replace(staff.SalaryCategory, @"s", "");
                        var surname = Regex.Replace(staff.Surname, @"s", "");
                        var firstName = Regex.Replace(staff.FirstName, @"s", "");
                        var otherName = Regex.Replace(staff.OtherName, @"s", "");
                       // var DateOfConfirmation = Regex.Replace(staff.DateOfConfirmation, @"s", "");
                        //var DOBNew = Regex.Replace(staff.DOB, @"s", "");
                        var DOBNew = Regex.Replace(staff.DOB, "[ ().-]+", "");
                        var staffNumberNew = staff.StaffNumber.Trim();
                        var phoneNumber = Regex.Replace(staff.PhoneNumber, @"s", "");
                        var genderNew = Regex.Replace(staff.Gender, @"s", "");
                        //var dateOfEmployment = Regex.Replace(staff.DateofAssumption, @"s", "");
                        var dateOfEmployment = Regex.Replace(staff.DateOfEmployment, "[ ().-]+", "");
                        var DateOfConfirmation = Regex.Replace(staff.DateOfConfirmation, "[ ().-]+", "");
                        var dateOfLastPromotion = Regex.Replace(staff.DateOfLastPromotion, "[ ().-]+", "");
                        //var appointmentTypeCheck = Regex.Replace(staff.AppointmentType, @"s", "");
                        long appointmentType = 0;
                        //if (appointmentTypeCheck == "T")
                        //{
                        //    appointmentType = (long)AppointmentTypes.Temporal;
                        //}
                        //else if (appointmentTypeCheck == "R")
                        //{
                        //    appointmentType = (long)AppointmentTypes.Regular;
                        //}
                        //else
                        //{
                        //    appointmentType = (long)AppointmentTypes.Temporal;
                        //}

                        //CHECK TO SEE IF A SIMILAR STAFF ALREADY EXISTS
                        Staff staffSelector = await _context.STAFF.Where(s => s.StaffNumber == staffNumberNew).FirstOrDefaultAsync();
                        Person personExists = await _context.PERSON
                                                            .Where(p => p.Firstname == firstName && p.Surname == surname && p.Othername == otherName)
                                   .FirstOrDefaultAsync();



                        var levelSelector = await _context.SALARY_LEVEL.Where(sl => Convert.ToInt32(sl.Name) == staff.Level).FirstOrDefaultAsync();
                        var stepSelector = await _context.SALARY_STEP.Where(ss => Convert.ToInt32(ss.Name) == staff.Step).FirstOrDefaultAsync();
                        var categorySelector = await _context.SALARY_GRADE_CATEGORY.Where(g => g.Representation == salCat).FirstOrDefaultAsync();
                        //var validateLGA = await _context.LGA.Where(lg => lg.Id == staff.LGAId).FirstOrDefaultAsync();
                        //var validatePFAName = await _context.PFA_NAME.Where(p => p.Id == staff.PFANameId).FirstOrDefaultAsync();
                        //var validatePFAStatus = await _context.PFA_STATUS.Where(p => p.Id == staff.PFAStatusId).FirstOrDefaultAsync();
                        //var validateAreaOfSpecialization = await _context.AREA_OF_SPECIALIZATION.Where(p => p.Id == staff.AreaOfSpecializationId).FirstOrDefaultAsync();
                        if(staffSelector != null)
                        {
                            staffAdditionalInfo = await _context.STAFF_ADDITIONAL_INFO.Where(s => s.staffId == staffSelector.Id).FirstOrDefaultAsync();

                        }

                        if ((personExists != null && staffSelector == null) || (personExists == null && staffSelector != null))
                        {

                            excelUploadRecordCountDto.FailedUpload += 1;
                            var failedUploadExists = await _context.FAILED_STAFF_UPLOADS.Where(a => a.Firstname == firstName && a.Surname == surname && a.Othername == otherName && a.StaffNumber == staffNumberNew).FirstOrDefaultAsync();

                            if(failedUploadExists == null)
                            {
                                FailedStaffUploads failedStaffUploads = new FailedStaffUploads();
                                failedStaffUploads.Surname = surname;
                                failedStaffUploads.Firstname = firstName;
                                failedStaffUploads.Othername = otherName;
                                failedStaffUploads.StaffNumber = staffNumberNew;
                                //failedStaffUploads.AppointmentType = staff.AppointmentType;
                                failedStaffUploads.InstitutionDepartmentId = staff.DepartmentId;
                                failedStaffUploads.InstitutionRankId = staff.Rank;
                                failedStaffUploads.DOB = staff.DOB;
                                //failedStaffUploads.DOE = staff.DateofAssumption;
                                failedStaffUploads.DateOfConfirmation = DateOfConfirmation;
                                failedStaffUploads.PhoneNumber = phoneNumber;
                                failedStaffUploads.DateOfLastPromotion = staff.DateOfLastPromotion;
                                failedStaffUploads.InstitutionRankId = staff.Rank;
                                failedStaffUploads.SalaryGradeCategory = salCat;
                                failedStaffUploads.SalaryLevelId = staff.Level;
                                failedStaffUploads.SalaryStepId = staff.Step;
                                failedStaffUploads.Gender = staff.Gender;
                                failedStaffUploads.Active = true;

                                _context.Add(failedStaffUploads);
                                await _context.SaveChangesAsync();
                            }

                            
                        }
                        // UPDATE Record if Staff/Person exists
                        else if (personExists != null && staffSelector != null)
                        {
                            var deptCheck = await _context.INSTITUTION_DEPARTMENT.Where(d => d.Id == staff.DepartmentId).FirstOrDefaultAsync();
                            var rankPick = await _context.INSTITUTION_RANK.Where(r => r.Id == staff.Rank).FirstOrDefaultAsync();
                            StaffSalaryCategoryReference staffCategoryReference = await _context.STAFF_SALARY_CATEGORY_REFERENCE.Where(s => s.StaffId == staffSelector.Id).FirstOrDefaultAsync();
                            var validateAppointmentType = await _context.APPOINTMENT_TYPE.Where(a => a.Id == appointmentType).FirstOrDefaultAsync();

                            //Update Staff
                            staffSelector.DepartmentId = deptCheck?.Id;
                            //if (validateAppointmentType != null)
                            //{
                            //    staffSelector.AppointmentId = validateAppointmentType.Id;

                            //}
                            //else
                            //{
                            //    staffSelector.AppointmentId = 1;
                               

                            //}
                            staffSelector.RankId = rankPick?.Id;

                            DateTime assumptionDate = DateTime.Now;
                            if (ValidateDate(DateOfConfirmation))
                            {
                                staffSelector.DateOfConfirmation = ConvertToDate(DateOfConfirmation);
                            }
                            if (ValidateDate(dateOfEmployment))
                            {
                                staffSelector.DateOfEmployment = ConvertToDate(dateOfEmployment);
                            }
                            if (ValidateDate(dateOfLastPromotion))
                            {
                                staffSelector.DateOfLastPromotion = ConvertToDate(dateOfLastPromotion);
                            }


                            _context.Update(staffSelector);

                            //Update Person
                            //personExists.d = validateLGA?.Id > 0 ? validateLGA?.Id : personExists.LGAId;
                            personExists.PhoneNumber = phoneNumber;
                            //personExists.Email = Email;
                            DateTime DOB = DateTime.Now;
                            if (ValidateDate(DOBNew))
                            {
                                DOB = ConvertToDate(DOBNew);
                                personExists.BirthDay = DOB;
                            }
                             
                            _context.Update(personExists);


                            //Update Salary Info
                            if (staffCategoryReference != null)
                            {
                                staffCategoryReference.StaffId = staffSelector.Id;
                                staffCategoryReference.SalaryGradeCategoryId = categorySelector?.Id > 0 ? categorySelector.Id : staffCategoryReference.SalaryGradeCategoryId;
                                staffCategoryReference.SalaryLevelId = levelSelector?.Id > 0 ? levelSelector.Id : staffCategoryReference.SalaryLevelId;
                                staffCategoryReference.SalaryStepId = stepSelector?.Id > 0 ? stepSelector.Id : staffCategoryReference.SalaryStepId;
                                staffCategoryReference.DatePromoted = DateTime.Now;
                                _context.Update(staffCategoryReference);
                            }

                            if(staffAdditionalInfo != null)
                            {
                                //staffAdditionalInfo.PfaNameId = validatePFAName != null ? validatePFAName?.Id : null;
                                //staffAdditionalInfo.PfaStatusId = validatePFAStatus != null ? validatePFAStatus?.Id : null;
                                //staffAdditionalInfo.AreaOfSpecializationId = validateAreaOfSpecialization != null ? validateAreaOfSpecialization?.Id : null;
                                staffAdditionalInfo.RsaNumber = !String.IsNullOrWhiteSpace(staff.RSANumber) ? staff.RSANumber : null;
                                _context.Update(staffAdditionalInfo);
                            }
                            else
                            {
                                StaffAdditionalInfo staffAdditional = new StaffAdditionalInfo();
                                staffAdditional.staffId = staffSelector.Id;
                                //staffAdditional.PfaNameId = validatePFAName != null ? validatePFAName?.Id : null;
                                //staffAdditional.PfaStatusId = validatePFAStatus != null ? validatePFAStatus?.Id : null;
                                //staffAdditional.AreaOfSpecializationId = validateAreaOfSpecialization != null ? validateAreaOfSpecialization?.Id : null;
                                staffAdditional.RsaNumber = !String.IsNullOrWhiteSpace(staff.RSANumber) ? staff.RSANumber : null;
                                _context.Add(staffAdditional);
                            }
                            await _context.SaveChangesAsync();
                            excelUploadRecordCountDto.UpdatedRecords += 1;


                        }

                        else if (personExists == null & staffSelector == null)
                        {


                            var deptCheck = await _context.INSTITUTION_DEPARTMENT.Where(d => d.Id == staff.DepartmentId).FirstOrDefaultAsync();
                            var rankPick = await _context.INSTITUTION_RANK.Where(r => r.Id == staff.Rank).FirstOrDefaultAsync();



                            Gender gender = new Gender();
                            if (!String.IsNullOrEmpty(genderNew))
                            {
                                gender = await MapGenderCharacterToObject(staff.Gender);
                            }
                            DateTime DOB = DateTime.Now;
                            if (ValidateDate(DOBNew))
                            {
                                DOB = ConvertToDate(DOBNew);
                            }

                            //State state = await GetStateByName(staff.StateName);
                            //Lga lga = await GetLgaByName(staff.LGAName);
                            string staffNumber = await ComposeStaffNumber();



                            //Create new PERSON
                            Person newPerson = new Person();
                            newPerson.Address = null;
                            //newPerson.Email = staff.Email;
                            newPerson.Firstname = firstName;
                            newPerson.Othername = otherName;
                            newPerson.Surname = surname;
                            newPerson.PhoneNumber = phoneNumber;
                            //newPerson.LGAId = validateLGA?.Id;
                            if (gender.Id > 0)
                            {
                                newPerson.GenderId = gender?.Id;
                            }

                            if (staff.DOB != null)
                            {
                                newPerson.BirthDay = DOB;
                            }

                            _context.Add(newPerson);
                            await _context.SaveChangesAsync();

                            //Create new STAFF
                            Staff newStaff = new Staff();
                            newStaff.PersonId = newPerson.Id;
                            newStaff.StaffNumber = staff.StaffNumber;
                            newStaff.GeneratedStaffNumber = staffNumber;
                            newStaff.Active = true;
                            newStaff.DepartmentId = deptCheck?.Id;
                            //newStaff.AppointmentTypeId = appointmentType;
                            newStaff.RankId = rankPick?.Id;
                            DateTime assumptionDate = DateTime.Now;
                            if (ValidateDate(dateOfEmployment))
                            {
                                newStaff.DateOfEmployment = ConvertToDate(dateOfEmployment);
                            }
                            if (ValidateDate(DateOfConfirmation))
                            {
                                newStaff.DateOfEmployment = ConvertToDate(DateOfConfirmation);
                            }
                            if (ValidateDate(dateOfLastPromotion))
                            {
                                newStaff.DateOfLastPromotion = ConvertToDate(dateOfLastPromotion);
                            }

                            _context.Add(newStaff);
                            await _context.SaveChangesAsync();

                            //Create new USER
                            User newUser = new User();
                            newUser.Username = staffNumber;
                            newUser.StaffId = newStaff.Id;
                            newUser.RoleId = 3;
                            newUser.Active = true;


                            //Create a hash for the Password
                            byte[] passwordHash, passwordSalt;
                            CreatePasswordHash("1234567", out passwordHash, out passwordSalt);

                            newUser.PasswordHash = passwordHash;
                            newUser.PasswordSalt = passwordSalt;

                            _context.Add(newUser);
                            await _context.SaveChangesAsync();

                            //Create Staff Grade
                            if (categorySelector != null && levelSelector != null && stepSelector != null)
                            {
                                StaffSalaryCategoryReference staffCategoryReference = new StaffSalaryCategoryReference();
                                staffCategoryReference.StaffId = newStaff.Id;
                                staffCategoryReference.SalaryGradeCategoryId = categorySelector.Id;
                                staffCategoryReference.SalaryLevelId = levelSelector.Id;
                                staffCategoryReference.SalaryStepId = stepSelector.Id;
                                staffCategoryReference.DatePromoted = DateTime.Now;
                                _context.Add(staffCategoryReference);
                                await _context.SaveChangesAsync();
                            }
                            if (staff.RSANumber != null)
                            {
                                StaffAdditionalInfo staffAdditional = new StaffAdditionalInfo();
                                staffAdditional.staffId = newStaff.Id;
                                //staffAdditional.PfaNameId = validatePFAName != null ? validatePFAName?.Id : null;
                                ///staffAdditional.PfaStatusId = validatePFAStatus != null ? validatePFAStatus?.Id : null;
                                //staffAdditional.AreaOfSpecializationId = validateAreaOfSpecialization != null ? validateAreaOfSpecialization?.Id : null;
                                staffAdditional.RsaNumber = staff.RSANumber;
                            }
                            excelUploadRecordCountDto.SuccessfullUpload += 1;

                        }
                        else
                        {
                            excelUploadRecordCountDto.FailedUpload += 1;
                        }
                    }
                }
                //await transaction.CommitAsync();

            }
            catch (Exception ex) {
                //transaction.Rollback();
                throw ex;
            }
            
            return excelUploadRecordCountDto;
        }

       

        //No Action Methods
        public async Task<string> ComposeStaffNumber()
        {
            try
            {
                Staff lastStaff = new Staff();
                long newSerialNumber = 0;
                var lastNumberCommit = await _context.STAFF_DB_COMMIT_DETAILS.Where(s => s.Active).FirstOrDefaultAsync();
                if(lastNumberCommit != null)
                {
                    newSerialNumber = lastNumberCommit.LastSerialNumber +1;
                    var newUserName = "NAU/ST/" + newSerialNumber;
                    lastNumberCommit.LastSerialNumber = newSerialNumber;
                    _context.Update(lastNumberCommit);
                    await _context.SaveChangesAsync();
                    return newUserName;
                }
                else
                {
                    lastStaff = await _context.STAFF.Where(s => s.Id > 0).OrderBy(f => f.Id).LastOrDefaultAsync();
                    //string[] numberSplit = lastStaff.GeneratedStaffNumber.Split('/');
                    //newSerialNumber = Convert.ToInt64(numberSplit[2])+1;
                    var newUserName = "-";
                    //StaffDBCommitDetails serialNumberCommit = new StaffDBCommitDetails()
                    //{
                    //    LastSerialNumber = newSerialNumber
                    //};
                    //_context.Add(serialNumberCommit);
                    //await _context.SaveChangesAsync();
                    return newUserName;

                }

                //int range = 1;//When range gets set on the DB, Make this variable dynamic
                //string staffNumber = string.Format("NAU/ST/{0}", AddLeadingZeros(range));
                //while (await DoesStaffNumberExist(staffNumber) != null)
                //{
                //    range += 1;
                //    staffNumber = string.Format("NAU/ST/{0}", AddLeadingZeros(range));
                //}

               // return staffNumber;
            }
            catch (Exception ex) { throw ex; }
        }
        public async Task<Staff> DoesStaffNumberExist(string staffNumber)
        {
            try
            {
                return await _context.STAFF
                                     .Where(s => s.GeneratedStaffNumber != null && s.GeneratedStaffNumber == staffNumber)
                                     .FirstOrDefaultAsync();
            }
            catch (Exception ex) { throw ex; }
        }
        public string AddLeadingZeros(long number)
        {
            int noOfZeros = (5 - number.ToString().Length);
            string zeros = string.Empty;

            if (noOfZeros > 0)
            {
                for (int i = 0; i < noOfZeros; i++)
                {
                    zeros += 0;
                }
            }
            return string.Format("{0}{1}", zeros, number);
        }


        public async Task<long> AddSingleStaff(AddSingleStaffDto addSingleStaffDto)
        {
            if (addSingleStaffDto == null)
                throw new ArgumentNullException("Null object");
            if (!String.IsNullOrEmpty(addSingleStaffDto.StaffNumber))
            {
                var existingStaff = await _context.STAFF.Where(f => f.StaffNumber == addSingleStaffDto.StaffNumber).FirstOrDefaultAsync();
                if (existingStaff != null)
                    return StatusCodes.Status302Found;
            }
            string staffNumber = await ComposeStaffNumber();
            //string staffNumberAutomation = await AutomateStaffNumber(addSingleStaffDto.SalaryCategoryId, addSingleStaffDto.SalaryLevelId, addSingleStaffDto.SalaryStepId);

            //Create a hash for the Password
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash("1234567", out passwordHash, out passwordSalt);
            Person person = new Person()
            {
                Address = addSingleStaffDto.Address,
                BirthDay = addSingleStaffDto.DoB,
                Email = addSingleStaffDto.EmailAddress,
                Firstname = addSingleStaffDto.FirstName,
                Gender = addSingleStaffDto.GenderId > 0 ? _context.GENDER.Where(f => f.Id == addSingleStaffDto.GenderId).FirstOrDefault() : null,
                Lga = addSingleStaffDto.LgaId > 0 ? _context.LGA.Where(f => f.Id == addSingleStaffDto.LgaId).FirstOrDefault() : null,
                MaritalStatus = addSingleStaffDto.MaritalStatusId > 0 ? _context.MARITAL_STATUS.Where(f => f.Id == addSingleStaffDto.MaritalStatusId).FirstOrDefault() : null,
                Othername = addSingleStaffDto.OtherName,
                Surname = addSingleStaffDto.Surname,
                PhoneNumber = addSingleStaffDto.PhoneNo,
                Religion = addSingleStaffDto.ReligionId > 0 ? _context.RELIGION.Where(f => f.Id == addSingleStaffDto.ReligionId).FirstOrDefault() : null,
            };
            _context.Add(person);
            await _context.SaveChangesAsync();
            Staff staff = new Staff()
            {
                Active = true,
                Appointment = addSingleStaffDto.AppointmentId > 0 ? _context.INSTITUTION_APPOINTMENT.Where(f => f.Id == addSingleStaffDto.AppointmentId).FirstOrDefault() : null,
                DateOfConfirmation = addSingleStaffDto.DateOfConfirmation,
                AppointmentDate = addSingleStaffDto.DateOfAppointment,
                Category = addSingleStaffDto.CategoryId > 0 ? _context.INSTITUTION_STAFF_CATEGORY.Where(f => f.Id == addSingleStaffDto.CategoryId).FirstOrDefault() : null,
                Department = addSingleStaffDto.DepartmentId > 0 ? _context.INSTITUTION_DEPARTMENT.Where(f => f.Id == addSingleStaffDto.DepartmentId).FirstOrDefault() : null,
                GeneratedStaffNumber = staffNumber,
                Person = person,
                Rank = addSingleStaffDto.RankId > 0 ? _context.INSTITUTION_RANK.Where(f => f.Id == addSingleStaffDto.RankId).FirstOrDefault() : null,
                StaffNumber = addSingleStaffDto.StaffNumber,
                StaffType = addSingleStaffDto.StaffTypeId > 0 ? _context.INSTITUTION_STAFF_TYPE.Where(f => f.Id == addSingleStaffDto.StaffTypeId).FirstOrDefault() : null,
                AppointmentIsConfirmed = addSingleStaffDto.IsConfirmed,
                AppointmentType = addSingleStaffDto.AppointmentTypeId > 0 ? _context.APPOINTMENT_TYPE.Where(f => f.Id == addSingleStaffDto.AppointmentTypeId).FirstOrDefault() : null,

            };
            _context.Add(staff);
            await _context.SaveChangesAsync();
            //Add Staff Salary Details
            if(addSingleStaffDto.SalaryCategoryId > 0 && addSingleStaffDto.SalaryLevelId > 0 && addSingleStaffDto.SalaryStepId > 0)
            {
                StaffSalaryCategoryReference salaryInfo = new StaffSalaryCategoryReference();
                salaryInfo.StaffId = staff.Id;
                salaryInfo.SalaryGradeCategoryId = addSingleStaffDto.SalaryCategoryId;
                salaryInfo.SalaryLevelId = addSingleStaffDto.SalaryLevelId;
                salaryInfo.SalaryStepId = addSingleStaffDto.SalaryStepId;
                salaryInfo.DatePromoted = DateTime.Now;
                _context.Add(salaryInfo);
                await _context.SaveChangesAsync();
            }
            


            //if(addSingleStaffDto.SalaryCategoryId > 0 && addSingleStaffDto.SalaryLevelId > 0 && addSingleStaffDto.SalaryStepId > 0)
            //{
            //    StaffSalaryCategoryReference staffSalary
            //}
            if (addSingleStaffDto.QualificationId > 0)
            {
                PersonEducation personEducation = new PersonEducation()
                {
                    Course = addSingleStaffDto.CourseOfStudy != null ? addSingleStaffDto.CourseOfStudy : "-",
                    EducationalQualification = _context.EDUCATIONAL_QUALIFICATION.Where(f => f.Id == addSingleStaffDto.QualificationId).FirstOrDefault(),
                    Institution = addSingleStaffDto.InstitutionOfQualification,
                    Person = person,
                    Year = addSingleStaffDto.Year,
                };
                _context.Add(personEducation);
                await _context.SaveChangesAsync();
            }
            User user = new User()
            {
                Username = staffNumber,
                Staff = staff,
                RoleId = 3,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Active = true,
            };
            _context.Add(user);
            var created = await _context.SaveChangesAsync();
            if (created > 0)
                return StatusCodes.Status200OK;
            return 0;
        }






        public async Task<long> NewStaffNumberGeneration(AddSingleStaffDto addSingleStaffDto)
        {
            if (addSingleStaffDto == null)
                throw new ArgumentNullException("Null object");
            if (!String.IsNullOrEmpty(addSingleStaffDto.StaffNumber))
            {
                var existingStaff = await _context.STAFF.Where(f => f.StaffNumber == addSingleStaffDto.StaffNumber).FirstOrDefaultAsync();
                if (existingStaff != null)
                    throw new AppException("Staff Number Already Exist!");
            }
            string staffNumber = await ComposeStaffNumber();
            string staffNumberAutomation = await AutomateStaffNumber(addSingleStaffDto.SalaryCategoryId, addSingleStaffDto.SalaryLevelId, addSingleStaffDto.SalaryStepId);
            //Create a hash for the Password
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash("1234567", out passwordHash, out passwordSalt);
            Person person = new Person()
            {

                Firstname = addSingleStaffDto.FirstName,
                Othername = addSingleStaffDto.OtherName,
                Surname = addSingleStaffDto.Surname,

            };
            _context.Add(person);
            await _context.SaveChangesAsync();
            Staff staff = new Staff()
            {
                Active = true,
                Appointment = addSingleStaffDto.AppointmentId > 0 ? _context.INSTITUTION_APPOINTMENT.Where(f => f.Id == addSingleStaffDto.AppointmentId).FirstOrDefault() : null,
                DateOfConfirmation = addSingleStaffDto.DateOfConfirmation,
                AppointmentDate = addSingleStaffDto.DateOfAppointment,
                Category = addSingleStaffDto.CategoryId > 0 ? _context.INSTITUTION_STAFF_CATEGORY.Where(f => f.Id == addSingleStaffDto.CategoryId).FirstOrDefault() : null,
                Department = addSingleStaffDto.DepartmentId > 0 ? _context.INSTITUTION_DEPARTMENT.Where(f => f.Id == addSingleStaffDto.DepartmentId).FirstOrDefault() : null,
                GeneratedStaffNumber = staffNumber,
                Person = person,
                Rank = addSingleStaffDto.RankId > 0 ? _context.INSTITUTION_RANK.Where(f => f.Id == addSingleStaffDto.RankId).FirstOrDefault() : null,
                StaffNumber = staffNumberAutomation,
                StaffType = addSingleStaffDto.StaffTypeId > 0 ? _context.INSTITUTION_STAFF_TYPE.Where(f => f.Id == addSingleStaffDto.StaffTypeId).FirstOrDefault() : null,
                AppointmentIsConfirmed = addSingleStaffDto.IsConfirmed,
                AppointmentType = addSingleStaffDto.AppointmentTypeId > 0 ? _context.APPOINTMENT_TYPE.Where(f => f.Id == addSingleStaffDto.AppointmentTypeId).FirstOrDefault() : null,

            };
            _context.Add(staff);
            await _context.SaveChangesAsync();
            //Add Staff Salary Details
            StaffSalaryCategoryReference salaryInfo = new StaffSalaryCategoryReference();
            salaryInfo.StaffId = staff.Id;
            salaryInfo.SalaryGradeCategoryId = addSingleStaffDto.SalaryCategoryId;
            salaryInfo.SalaryLevelId = addSingleStaffDto.SalaryLevelId;
            salaryInfo.SalaryStepId = addSingleStaffDto.SalaryStepId;
            salaryInfo.DatePromoted = DateTime.Now;
            _context.Add(salaryInfo);
            await _context.SaveChangesAsync();


            if (addSingleStaffDto.QualificationId > 0)
            {
                PersonEducation personEducation = new PersonEducation()
                {
                    Course = addSingleStaffDto.CourseOfStudy,
                    EducationalQualification = _context.EDUCATIONAL_QUALIFICATION.Where(f => f.Id == addSingleStaffDto.QualificationId).FirstOrDefault(),
                    Institution = addSingleStaffDto.InstitutionOfQualification,
                    Person = person,
                    Year = addSingleStaffDto.Year,
                };
                _context.Add(personEducation);
                await _context.SaveChangesAsync();
            }
            GeneratedStaffNumberRecords generatedStaffNumber = new GeneratedStaffNumberRecords()
            {
                StaffId = staff.Id,
                DateGenerated = DateTime.Now,
                DefaultDepartment = addSingleStaffDto.DepartmentId > 0 ? _context.INSTITUTION_DEPARTMENT.Where(f => f.Id == addSingleStaffDto.DepartmentId).FirstOrDefault() : null,
            };
            _context.Add(generatedStaffNumber);
            await _context.SaveChangesAsync();

            User user = new User()
            {
                Username = staffNumber,
                Staff = staff,
                RoleId = 3,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Active = true,
            };
            _context.Add(user);
            var created = await _context.SaveChangesAsync();
            if (created > 0)
                return staff.Id;
            return 0;
        }


        //Staff Main Staff Number Automation

        public async Task<string> AutomateStaffNumber(long salCat, long salLevel, long salStep)
        {
            var salaryCategorySelector = await _context.SALARY_GRADE_CATEGORY.Where(sg => sg.Id == salCat).FirstOrDefaultAsync();
            var salaryLevelSelector = await _context.SALARY_LEVEL.Where(sl => sl.Id == salLevel).FirstOrDefaultAsync();
            var salaryStepSelector = await _context.SALARY_STEP.Where(ss => ss.Id == salStep).FirstOrDefaultAsync();

            var prefix = "";
            try
            {
                //CONTISS
                if (salaryCategorySelector.Representation == "H" && Convert.ToInt32(salaryLevelSelector.Name) <= 5)
                {
                    prefix = "JS-";
                }
                //CONTISS
                else if (salaryCategorySelector.Representation == "H" && Convert.ToInt32(salaryLevelSelector.Name) >= 5 && Convert.ToInt32(salaryLevelSelector.Name) <= 14)
                {
                    prefix = "SS-";
                }
                //CONUASS
                else if (salaryCategorySelector.Representation == "A" && Convert.ToInt32(salaryLevelSelector.Name) >= 1 && Convert.ToInt32(salaryLevelSelector.Name) <= 7)
                {
                    prefix = "SS-";
                }
                //ADJUNCT
                else if (salaryCategorySelector.Representation == "D" && Convert.ToInt32(salaryLevelSelector.Name) >= 3 && Convert.ToInt32(salaryLevelSelector.Name) <= 7)
                {
                    prefix = "SS-";
                }
                //CONHESS
                else if (salaryCategorySelector.Representation == "C" && Convert.ToInt32(salaryLevelSelector.Name) >= 7 && Convert.ToInt32(salaryLevelSelector.Name) <= 14)
                {
                    prefix = "SS-";
                }
                //TECHNOLOGIST II
                else if (salaryCategorySelector.Representation == "T" && Convert.ToInt32(salaryLevelSelector.Name) >= 7 && Convert.ToInt32(salaryLevelSelector.Name) <= 14)
                {
                    prefix = "SS-";
                }
                //CONMESS
                //else if (salaryCategorySelector.Representation == "C" && Convert.ToInt32(salaryLevelSelector.Name) >= 7 && Convert.ToInt32(salaryLevelSelector.Name) <= 14)
                //{
                //    prefix = "SS-";
                //}
                //EXPATRIATES
                else if (salaryCategorySelector.Representation == "EXP" && Convert.ToInt32(salaryLevelSelector.Name) >= 1 && Convert.ToInt32(salaryLevelSelector.Name) <= 7)
                {
                    prefix = "SS-";
                }
                else
                {
                    throw new AppException("Out of scope");
                }
                int range = 1;//When range gets set on the DB, Make this variable dynamic
                string staffNumberAuto = string.Format(prefix + "{0}", UseLeadingZeros(range));
                while (await NumberExistCheck(staffNumberAuto) != null)
                {
                    range += 1;
                    staffNumberAuto = string.Format(prefix + "{0}", UseLeadingZeros(range));
                }

                return staffNumberAuto;
            }
            catch (Exception ex) { throw ex; }
        }

        //private int OutOfRange()
        //{
        //    return StatusCodes.Status404NotFound;
        //}

        public async Task<Staff> NumberExistCheck(string staffNumberAuto)
        {
            try
            {
                return await _context.STAFF
                                     .Where(s => s.StaffNumber != null && s.StaffNumber == staffNumberAuto)
                                     .FirstOrDefaultAsync();
            }
            catch (Exception ex) { throw ex; }
        }
        public string UseLeadingZeros(long number)
        {
            int noOfZeros = (4 - number.ToString().Length);
            string zeros = string.Empty;

            if (noOfZeros > 0)
            {
                for (int i = 0; i < noOfZeros; i++)
                {
                    zeros += 0;
                }
            }
            return string.Format("{0}{1}", zeros, number);
        }



        public async Task<Lga> GetLgaByName(string lga)
        {
            try
            {
                return await _context.LGA.Where(lg => lg.Name == lga).FirstOrDefaultAsync();
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<State> GetStateByName(string state)
        {
            try
            {
                State stateExists = await _context.STATE.Where(s => s.Name == state).FirstOrDefaultAsync();
                if (stateExists == null)
                {
                    stateExists = new State()
                    {
                        Name = state,
                        Active = true
                    };

                    _context.Add(stateExists);
                    await _context.SaveChangesAsync();
                }
                return stateExists;
            }
            catch (Exception ex) { throw ex; }
        }
        public DateTime ConvertToDate(string date)
        {
            DateTime newDate = new DateTime();
          
                try
                {

                    string[] dateSplit = date.Split('/');
                    int year = Convert.ToInt16(dateSplit[2].Split(' ')[0]);
                    newDate = new DateTime(year, Convert.ToInt16(dateSplit[1]), Convert.ToInt32(dateSplit[0]));
                    //return newDate;

                }
                catch (Exception ex) { throw ex; }
            
               

            return newDate;
        }

        public bool ValidateDate(string date)
        {
            //string dateStrings = ("02/13/2020");
            //DateTime dateValue;
            
            var seperator = "/";
            int slashCount = date.Split(seperator).Length - 1;
            if ((date.Length == 10 || date.Length == 9 || date.Length == 8) && date.Substring(0, 2) != "00" && slashCount == 2 && !String.IsNullOrEmpty(date))
            {
                
                    return true;
                
                

            }
            return false;

        }

        public async Task<Gender> MapGenderCharacterToObject(string genderCharacter)
        {
            try
            {
                return await _context.GENDER.Where(g => g.Name.StartsWith(genderCharacter)).FirstOrDefaultAsync();
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<InstitutionDepartment> GetOrCreateDepartment(string departmentName)
        {
            try
            {
                InstitutionDepartment departmentExists = await _context.INSTITUTION_DEPARTMENT
                                                                       .Where(d => d.Name == departmentName)
                                                                       .FirstOrDefaultAsync();

                if (departmentExists == null)
                {
                    departmentExists = new InstitutionDepartment()
                    {
                        Name = departmentName,
                        Active = true
                    };

                    _context.Add(departmentExists);
                    await _context.SaveChangesAsync();
                }

                return departmentExists;

            }
            catch (Exception ex) { throw ex; }
        }


        public async Task<Faculty> GetOrCreateFaculty(string facultyName)
        {
            try
            {
                Faculty facultyExists = await _context.FACULTY
                                                                       .Where(d => d.Name == facultyName)
                                                                       .FirstOrDefaultAsync();

                if (facultyExists == null)
                {
                    facultyExists = new Faculty()
                    {
                        Name = facultyName,
                        Active = true
                    };

                    _context.Add(facultyExists);
                    await _context.SaveChangesAsync();
                }

                return facultyExists;

            }
            catch (Exception ex) { throw ex; }
        }
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }






        //Create Single Staff Service

        public async Task<StaffUpdateDto> GetStaffUpdate(long id)
        {

            StaffUpdateDto staffUpdateDto = new StaffUpdateDto();

            try
            {
                await AutomateStaffStatus(id);
                staffUpdateDto.Staff = await _context.STAFF
                     .Include(m => m.Category)
                     .Include(m => m.StaffType)
                     .Include(m => m.Rank)
                     .ThenInclude(m => m.InstitutionUnit)
                     .Include(m => m.Appointment)
                     //.Include(m => m.Unit)
                     .Include(m => m.Department)
                     .Include(m => m.Person)

                     .ThenInclude(p => p.Gender)
                     .Include(m => m.Person)
                     .ThenInclude(p => p.Lga)
                     .Include(m => m.Person)
                     .ThenInclude(p => p.State)
                     .Include(m => m.Person)
                     .ThenInclude(p => p.MaritalStatus)
                     .Include(m => m.Person)
                     .ThenInclude(p => p.Religion)
                     .Include(m => m.Person)
                     .ThenInclude(p => p.Gender)
                     //Miracle
                     .Include(m => m.AppointmentType)

                     .SingleOrDefaultAsync(m => m.Id == id);
                if (staffUpdateDto?.Staff?.Id > 0)
                {
                    staffUpdateDto.PersonEducations = await _context.PERSON_EDUCATION
                         .Include(f => f.EducationalQualification)
                         .Include(f => f.Person)
                         .Where(f => f.PersonId == staffUpdateDto.Staff.Person.Id)
                         .Select(f => new PersonEducationDto
                         {
                             CourseOfStudy = f.Course,
                             Id = f.Id,
                             InstitutionName = f.Institution,
                             PersonId = f.PersonId,
                             QualificationId = f.EducationalQualificationId,
                             Year = f.Year
                         })
                         .ToListAsync();

                    //var staffUpdate = await UpdateStaff(id, StaffUpdateDto);



                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return staffUpdateDto;

        }

        public async Task<bool> PostStaffUpdate(long id, NewStaffProfileEditDto staffUpdateDto)
        {
            try
            {
                var status = 0;
                var testDto = staffUpdateDto;
                //var _staff = await GetStaffUpdate(id);
                var _staff = await _context.STAFF.Where(s => s.Id == id)
                    .Include(p => p.Person)
                    .FirstOrDefaultAsync();

                //staffUpdateDto = _staff;

                var personEducations = await _context.PERSON_EDUCATION
                        .Include(f => f.EducationalQualification)
                        .Include(f => f.Person)
                        .Where(f => f.PersonId == _staff.PersonId)
                        .Select(f => new PersonEducationsDto
                        {
                            CourseOfStudy = f.Course,
                            Id = f.Id,
                            InstitutionName = f.Institution,
                            PersonId = f.PersonId,
                            QualificationId = f.EducationalQualificationId,
                            Year = f.Year
                        })
                        .ToListAsync();

                if (_staff != null)
                {
                     personEducations = staffUpdateDto?.PersonEducations;
                    _staff.StaffNumber = staffUpdateDto.StaffNumber;
                    _staff.Person.Firstname = staffUpdateDto.Firstname;
                    _staff.Person.Othername = staffUpdateDto.Othername;
                    _staff.Person.Surname = staffUpdateDto.Surname;
                    _staff.Person.BirthDay = staffUpdateDto.BirthDay;
                    _staff.Person.Address = staffUpdateDto.Address;
                    _staff.Person.Email = staffUpdateDto.Email;
                    _staff.Person.PhoneNumber = staffUpdateDto.PhoneNumber;
                    _staff.Person.StateId = staffUpdateDto.StateId > 0 ? staffUpdateDto?.StateId : null;
                    _staff.Person.LGAId = staffUpdateDto.LGAId > 0 ? staffUpdateDto?.LGAId : null;
                    _staff.Person.MaritalStatusId = staffUpdateDto.MaritalStatusId > 0 ? staffUpdateDto?.MaritalStatusId : null;
                    _staff.Person.ReligionId = staffUpdateDto.ReligionId > 0 ? staffUpdateDto?.ReligionId : null;
                    _staff.Person.GenderId = staffUpdateDto.GenderId > 0 ? staffUpdateDto?.GenderId : null;
                    _staff.RankId = staffUpdateDto.RankId > 0 ? staffUpdateDto?.RankId : null;
                    _staff.DepartmentId = staffUpdateDto.DepartmentId > 0 ? staffUpdateDto?.DepartmentId : null;
                    _staff.AppointmentId = staffUpdateDto.AppointmentId > 0 ? staffUpdateDto?.AppointmentId : null;
                    _staff.StaffTypeId = staffUpdateDto.StaffTypeId > 0 ? staffUpdateDto?.StaffTypeId : null;
                    _staff.CategoryId = staffUpdateDto.CategoryId > 0 ? staffUpdateDto?.CategoryId : null;
                    _staff.DateOfConfirmation = staffUpdateDto.DateOfConfirmation;
                    _staff.BiometricId = staffUpdateDto.BiometricNumber;

                    _context.Update(_staff);

                    var doesExit = await _context.STAFF_ADDITIONAL_INFO.Where(s => s.staffId == id).FirstOrDefaultAsync();
                    if(doesExit == null && (staffUpdateDto.PfaNameId > 0 || staffUpdateDto.PfaStatusId > 0 || staffUpdateDto.AreaOfSpecializationId > 0 || staffUpdateDto.RSANumber != null))
                    {
                       
                        StaffAdditionalInfo additionalInfo = new StaffAdditionalInfo();
                        additionalInfo.staffId = id;
                        additionalInfo.PfaNameId = staffUpdateDto.PfaNameId > 0 ? staffUpdateDto?.PfaNameId : null;
                        additionalInfo.PfaStatusId = staffUpdateDto.PfaStatusId > 0 ? staffUpdateDto?.PfaStatusId : null;
                        additionalInfo.AreaOfSpecializationId = staffUpdateDto.AreaOfSpecializationId > 0 ? staffUpdateDto?.AreaOfSpecializationId : null;
                        additionalInfo.RsaNumber = staffUpdateDto.RSANumber;
                        _context.Add(additionalInfo);
                    }
                    else
                    {
                        
                        doesExit.PfaNameId = staffUpdateDto.PfaNameId;
                        doesExit.PfaStatusId = staffUpdateDto.PfaStatusId;
                        doesExit.AreaOfSpecializationId = staffUpdateDto.AreaOfSpecializationId;
                        doesExit.RsaNumber = staffUpdateDto.RSANumber;
                        _context.Update(doesExit);
                    }


                    if (personEducations?.Count > 0)
                    {
                        foreach (var item in personEducations)
                        {
                            if (item.Id > 0)
                            {
                                var personEducation = await _context.PERSON_EDUCATION

                                .Where(f => f.Id == item.Id).FirstOrDefaultAsync();

                                //StaffSalaryCategoryReference staffSalaryExists = await _context.STAFF_SALARY_CATEGORY_REFERENCE.Where(ss => ss.StaffId == item.Id).FirstOrDefaultAsync();
                                if (personEducation?.Id > 0)
                                {
                                    personEducation.Year = item.Year;
                                    personEducation.Institution = item.InstitutionName;
                                    personEducation.EducationalQualificationId = item.QualificationId;
                                    personEducation.Course = item.CourseOfStudy;
                                }
                                _context.Update(personEducation);

                          }
                            else
                            {
                                if (item.PersonId > 0 && item.QualificationId > 0)
                                {
                                    var personEducation = await _context.PERSON_EDUCATION

                                .Where(f => f.PersonId == item.PersonId && f.EducationalQualificationId == item.QualificationId).FirstOrDefaultAsync();
                                    if (personEducation == null)
                                    {
                                        PersonEducation createPersonEducation = new PersonEducation()
                                        {
                                            Course = item.CourseOfStudy,
                                            EducationalQualificationId = item.QualificationId,
                                            Institution = item.InstitutionName,
                                            PersonId = item.PersonId,
                                            Year = item.Year,

                                        };
                                        _context.Add(createPersonEducation);
                                        //await _context.SaveChangesAsync();
                                    }
                                }
                            }

                        }
                    }



                    status = await _context.SaveChangesAsync();

                }
                return (status > 0) ? true : false;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<long> UploadStaffPassport(IFormFile file, long staffId, string filePath, string directory)
        {
            try
            {
                var saveimageLink = string.Empty;
                if (file != null)
                {
                    Staff staff = await _context.STAFF.Where(x => x.Id == staffId).FirstOrDefaultAsync();
                    if(staff != null)
                    {
                        Person person = await _context.PERSON.Where(p => p.Id == staff.PersonId).FirstOrDefaultAsync();
                        string fileNamePrefix = "Passport" + "_" + staff.Id + "_" + DateTime.Now.Millisecond;
                        saveimageLink = await _fileUpload.UploadPassport(file, filePath, directory, fileNamePrefix);

                        person.ImageUrl = saveimageLink;
                        _context.Update(person);
                        await _context.SaveChangesAsync();                       
                    }
                }
                return StatusCodes.Status200OK;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task<long> UpdateStaffProfile(StaffUpdateProfileDto staffUpdateProfileDto, string filePath, string directory)
        {
            if (staffUpdateProfileDto == null)
                throw new NullReferenceException("not found");
            if (staffUpdateProfileDto?.StaffId == 0)
                throw new NullReferenceException("not found");
            var staff = await _context.STAFF.Where(f => f.Id == staffUpdateProfileDto.StaffId).FirstOrDefaultAsync();
            if (staff == null)
                throw new NullReferenceException("not found");
            //upload passport if added
            var saveimageLink = string.Empty;
            if (staffUpdateProfileDto.Passport != null)
            {
                string fileNamePrefix = "Passport" + "_" + staff.Id + "_" + DateTime.Now.Millisecond;
                saveimageLink = await _fileUpload.UploadPassport(staffUpdateProfileDto.Passport, filePath, directory, fileNamePrefix);
            }
            staff.BiometricId = staffUpdateProfileDto.BiometricNo;
            _context.Update(staff);
            var person = await _context.PERSON.Where(f => f.Id == staff.PersonId).FirstOrDefaultAsync();
            if (person?.Id > 0)
            {
                person.PhoneNumber = staffUpdateProfileDto.Phone;
                person.Email = staffUpdateProfileDto.EmailAddress;
                person.Address = staffUpdateProfileDto.ContactAddress;
                //person.ImageUrl = saveimageLink;
                _context.Update(person);

                var doesExit = await _context.STAFF_ADDITIONAL_INFO.Where(s => s.staffId == staff.Id).FirstOrDefaultAsync();
                if (doesExit == null && (staffUpdateProfileDto.PFANameId > 0 || staffUpdateProfileDto.PFAStatusId > 0 || staffUpdateProfileDto.AreaOfSpecializationId > 0 || staffUpdateProfileDto.RSANumber != null))
                {
                    StaffAdditionalInfo additionalInfo = new StaffAdditionalInfo();
                    additionalInfo.staffId = staff.Id;
                    additionalInfo.PfaNameId = staffUpdateProfileDto.PFANameId > 0 ? staffUpdateProfileDto?.PFANameId : null;
                    additionalInfo.PfaStatusId = staffUpdateProfileDto.PFAStatusId > 0 ? staffUpdateProfileDto?.PFAStatusId : null;
                    additionalInfo.AreaOfSpecializationId = staffUpdateProfileDto.AreaOfSpecializationId > 0 ? staffUpdateProfileDto?.AreaOfSpecializationId : null;
                    additionalInfo.RsaNumber = staffUpdateProfileDto.RSANumber;
                    _context.Add(additionalInfo);
                }
            }
            await _context.SaveChangesAsync();
            return staff.Id;

        }

        public async Task<bool> DeleteStaff(long StaffId)
        {
            var user = await _context.USER
                .Include(f => f.Staff)
               .ThenInclude(f => f.Person)
               .ThenInclude(f => f.PersonEducations)
               .Include(f => f.Staff.Person)
               .ThenInclude(f => f.PersonCertifications)
               .Include(f => f.Staff.Person)
               .ThenInclude(f => f.PersonExperiences)
               .Include(f => f.Staff.Person)
               .ThenInclude(f => f.PersonJournals)
               .Include(f => f.Staff.Person)
               .ThenInclude(f => f.PersonProfessionalBodies)
               .Include(f => f.Staff.Person)
               .ThenInclude(f => f.PersonReferees)
               .Include(f => f.Staff.Person)
               .ThenInclude(f => f.PersonResearchGrants)
                .Where(f => f.StaffId == StaffId).FirstOrDefaultAsync();
            if (user == null)
                throw new NullReferenceException();
            _context.USER.Remove(user);
            _context.STAFF.Remove(user.Staff);

            _context.PERSON_EDUCATION.RemoveRange(user.Staff.Person.PersonEducations);
            _context.PERSON.Remove(user.Staff.Person);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<NewStaffDto>> GetStaffBy(long departmentId, long staffTypeId, long rankId, long appointmentTypeId)
        {

            if (departmentId > 0 && staffTypeId > 0 && rankId > 0 && appointmentTypeId > 0)
            {
                return await _context.STAFF
                .Where(f => f.StaffTypeId == staffTypeId && f.DepartmentId == departmentId && f.RankId == rankId)

                 .Select(f => new NewStaffDto
                 {
                     FirstName = f.Person.Firstname != null || String.IsNullOrWhiteSpace(f.Person.Firstname) ? f.Person.Firstname : "-",
                     OtherName = f.Person.Othername != null || String.IsNullOrWhiteSpace(f.Person.Othername) ? f.Person.Othername : "-",
                     Surname = f.Person.Surname != null || String.IsNullOrWhiteSpace(f.Person.Surname) ? f.Person.Surname : "-",
                     StaffIdentityNumber = f.StaffNumber != null || String.IsNullOrWhiteSpace(f.StaffNumber) ? f.StaffNumber : "-",

                     Email = f.Person.Email != null ? f.Person.Email : "-",
                     StaffDepartment = f.Department.Name != null ? f.Department.Name : "-",
                     StaffRank = f.Rank.Name != null ? f.Rank.Name : "-",
                     StaffDOB = f.Person.BirthDay,
                     UserName = f.GeneratedStaffNumber != null ? f.GeneratedStaffNumber : "-",
                     StaffId = f.Id,
                     StaffCategory = f.Category.Name != null ? f.Category.Name : "-",
                     StaffType = f.StaffType.Name != null ? f.StaffType.Name : "-",
                     StaffFullName = f.Person.Surname + " " + f.Person.Firstname + " " + f.Person.Othername,
                     AppointmentType = f.AppointmentType.Name,
                     BiometricNumber = f.BiometricId != null ? f.BiometricId : "-"



                 }).ToListAsync();
            }
            else if (departmentId > 0 && rankId > 0 && staffTypeId == 0)
            {
                return await _context.STAFF

             .Where(f => f.DepartmentId == departmentId && f.RankId == rankId)
              .Select(f => new NewStaffDto
              {
                  FirstName = f.Person.Firstname != null || String.IsNullOrWhiteSpace(f.Person.Firstname) ? f.Person.Firstname : "-",
                  OtherName = f.Person.Othername != null || String.IsNullOrWhiteSpace(f.Person.Othername) ? f.Person.Othername : "-",
                  Surname = f.Person.Surname != null || String.IsNullOrWhiteSpace(f.Person.Surname) ? f.Person.Surname : "-",
                  StaffIdentityNumber = f.StaffNumber != null || String.IsNullOrWhiteSpace(f.StaffNumber) ? f.StaffNumber : "-",

                  Email = f.Person.Email != null ? f.Person.Email : "-",
                  StaffDepartment = f.Department.Name != null ? f.Department.Name : "-",

                  StaffRank = f.Rank.Name != null ? f.Rank.Name : "-",
                  StaffDOB = f.Person.BirthDay,
                  UserName = f.GeneratedStaffNumber != null ? f.GeneratedStaffNumber : "-",
                  StaffId = f.Id,
                  StaffCategory = f.Category.Name != null ? f.Category.Name : "-",
                  StaffType = f.StaffType.Name != null ? f.StaffType.Name : "-",
                  StaffFullName = f.Person.Surname + " " + f.Person.Firstname + " " + f.Person.Othername,
                  AppointmentType = f.AppointmentType.Name,
                  BiometricNumber = f.BiometricId != null ? f.BiometricId : "-"




              }).ToListAsync();
            }
            else if (departmentId > 0 && staffTypeId > 0 && rankId == 0)
            {
                return await _context.STAFF

             .Where(f => f.DepartmentId == departmentId && f.StaffTypeId == staffTypeId)
             .Select(f => new NewStaffDto
             {
                 FirstName = f.Person.Firstname != null || String.IsNullOrWhiteSpace(f.Person.Firstname) ? f.Person.Firstname : "-",
                 OtherName = f.Person.Othername != null || String.IsNullOrWhiteSpace(f.Person.Othername) ? f.Person.Othername : "-",
                 Surname = f.Person.Surname != null || String.IsNullOrWhiteSpace(f.Person.Surname) ? f.Person.Surname : "-",
                 StaffIdentityNumber = f.StaffNumber != null || String.IsNullOrWhiteSpace(f.StaffNumber) ? f.StaffNumber : "-",

                 Email = f.Person.Email != null ? f.Person.Email : "-",
                 StaffDepartment = f.Department.Name != null ? f.Department.Name : "-",

                 StaffRank = f.Rank.Name != null ? f.Rank.Name : "-",
                 StaffDOB = f.Person.BirthDay,
                 UserName = f.GeneratedStaffNumber != null ? f.GeneratedStaffNumber : "-",
                 StaffId = f.Id,
                 StaffCategory = f.Category.Name != null ? f.Category.Name : "-",
                 StaffType = f.StaffType.Name != null ? f.StaffType.Name : "-",
                 StaffFullName = f.Person.Surname + " " + f.Person.Firstname + " " + f.Person.Othername,
                 AppointmentType = f.AppointmentType.Name,
                 BiometricNumber = f.BiometricId != null ? f.BiometricId : "-"




             }).ToListAsync();

            }
            else if (staffTypeId > 0 && rankId > 0 && departmentId == 0)
            {
                return await _context.STAFF

             .Where(f => f.StaffTypeId == staffTypeId && f.RankId == rankId)
             .Select(f => new NewStaffDto
             {
                 FirstName = f.Person.Firstname != null || String.IsNullOrWhiteSpace(f.Person.Firstname) ? f.Person.Firstname : "-",
                 OtherName = f.Person.Othername != null || String.IsNullOrWhiteSpace(f.Person.Othername) ? f.Person.Othername : "-",
                 Surname = f.Person.Surname != null || String.IsNullOrWhiteSpace(f.Person.Surname) ? f.Person.Surname : "-",
                 StaffIdentityNumber = f.StaffNumber != null || String.IsNullOrWhiteSpace(f.StaffNumber) ? f.StaffNumber : "-",

                 Email = f.Person.Email != null ? f.Person.Email : "-",
                 StaffDepartment = f.Department.Name != null ? f.Department.Name : "-",

                 StaffRank = f.Rank.Name != null ? f.Rank.Name : "-",
                 StaffDOB = f.Person.BirthDay,
                 UserName = f.GeneratedStaffNumber != null ? f.GeneratedStaffNumber : "-",
                 StaffId = f.Id,
                 StaffCategory = f.Category.Name != null ? f.Category.Name : "-",
                 StaffType = f.StaffType.Name != null ? f.StaffType.Name : "-",
                 StaffFullName = f.Person.Surname + " " + f.Person.Firstname + " " + f.Person.Othername,
                 AppointmentType = f.AppointmentType.Name,
                 BiometricNumber = f.BiometricId != null ? f.BiometricId : "-"




             }).ToListAsync();

            }
            else if (departmentId > 0 && rankId == 0 && staffTypeId == 0)
            {

                List<NewStaffDto> newStaffDtoList = new List<NewStaffDto>();
                var fetchStaff = await _context.STAFF
                    .Include(c => c.Category)
                    .Include(c => c.StaffType)
                    .Include(c => c.Rank)
                    .Include(c => c.Person)
                    .ThenInclude(e => e.PersonEducations)
                    .ThenInclude(e => e.EducationalQualification)
                    .Include(s => s.Person)
                    .ThenInclude(s => s.Lga)
                    .ThenInclude(s => s.State)
                    .Include(d => d.Department)
                     .Include(a => a.AppointmentType)

                    .Where(f => f.DepartmentId == departmentId && f.IsRetired == false && f.IsDisengaged == false)
                    .ToListAsync();

                if (fetchStaff?.Count() > 0)
                {
                    foreach (var staff in fetchStaff)
                    {
                        NewStaffDto newStaffDto = new NewStaffDto();
                        var staffSalaryCat = await _context.STAFF_SALARY_CATEGORY_REFERENCE.Where(c => c.StaffId == staff.Id)
                            .Include(c => c.SalaryGradeCategory)
                            .Include(c => c.SalaryLevel)
                            .Include(c => c.SalaryStep)

                            .FirstOrDefaultAsync();
                        if (staffSalaryCat?.Id > 0)
                        {
                            newStaffDto.SalaryCategory = staffSalaryCat.SalaryGradeCategory?.Representation + staffSalaryCat.SalaryLevel?.Name
                                + ", " + staffSalaryCat.SalaryStep?.Name;
                        }
                        newStaffDto.FirstName = staff.Person?.Firstname != null ? staff.Person?.Firstname : "-";
                        newStaffDto.OtherName = staff.Person?.Othername != null ? staff.Person?.Othername : "-";
                        newStaffDto.Surname = staff.Person?.Surname != null ? staff.Person?.Surname : "-";
                        newStaffDto.Email = staff.Person?.Email != null ? staff.Person?.Email : "-";
                        newStaffDto.StaffDepartment = staff.Department?.Name != null ? staff.Department?.Name : "-";
                        newStaffDto.StaffRank = staff.Rank?.Name != null ? staff.Rank?.Name : "-";
                        newStaffDto.UserName = staff.GeneratedStaffNumber != null ? staff.GeneratedStaffNumber : "-";
                        newStaffDto.StaffIdentityNumber = staff.StaffNumber != null ? staff.StaffNumber : "-";
                        newStaffDto.StaffType = staff.StaffType?.Name != null ? staff.StaffType?.Name : "-";
                        newStaffDto.StaffCategory = staff.Category?.Name != null ? staff.Category?.Name : "-";
                        newStaffDto.StaffId = staff.Id;
                        newStaffDto.IsDisengaged = staff.IsDisengaged;
                        newStaffDto.IsRetired = staff.IsRetired;
                        newStaffDto.StaffDOB = staff.Person?.BirthDay;
                        newStaffDto.Phone = staff.Person?.PhoneNumber;
                        newStaffDto.State = staff.Person?.Lga?.State?.Name;
                        newStaffDto.Lga = staff.Person?.Lga?.Name;
                        newStaffDto.DateOfEmployment = staff.DateOfConfirmation;
                        newStaffDto.AppointmentType = staff.AppointmentType?.Name;
                        newStaffDto.BiometricNumber = staff.BiometricId != null ? staff.BiometricId : "-";

                        if (staff.Person.BirthDay != null)
                        {
                            if (staff.StaffTypeId == (long)StaffType.AcademicStaff)
                            {

                                newStaffDto.DateOfRetirement = staff.Person?.BirthDay?.AddYears(70);

                            }
                            else if (staff.StaffTypeId == (long)StaffType.NonAcademicStaff)
                            {

                                newStaffDto.DateOfRetirement = staff.Person?.BirthDay?.AddYears(65);


                            }
                        }

                        newStaffDto.StaffFullName = staff.Person?.Surname + " " + staff.Person?.Firstname + " " + staff.Person?.Othername;
                        newStaffDto.PersonEducations = staff.Person?.PersonEducations.Select(u => new PersonEducationDtoNew
                        {
                            Institution = u.Institution,
                            EducationalQualificationId = u.EducationalQualificationId,
                            Course = u.Course,
                            Year = u.Year,
                            QualificationName = u.EducationalQualification.Name

                        }).ToList();


                        newStaffDtoList.Add(newStaffDto);
                    }
                }


                return newStaffDtoList;





            }
            else if (appointmentTypeId > 0 && departmentId == 0 && rankId == 0 && staffTypeId == 0)
            {

                List<NewStaffDto> newStaffDtoList = new List<NewStaffDto>();
                var fetchStaff = await _context.STAFF
                    .Include(c => c.Category)
                    .Include(c => c.StaffType)
                    .Include(c => c.Rank)
                    .Include(c => c.Person)
                    .ThenInclude(e => e.PersonEducations)
                    .ThenInclude(e => e.EducationalQualification)
                    .Include(s => s.Person)
                    .ThenInclude(s => s.Lga)
                    .ThenInclude(s => s.State)
                    .Include(d => d.Department)
                     .Include(a => a.AppointmentType)

                    .Where(f => f.AppointmentTypeId == appointmentTypeId && f.IsRetired == false && f.IsDisengaged == false)
                    .ToListAsync();

                if (fetchStaff?.Count() > 0)
                {
                    foreach (var staff in fetchStaff)
                    {
                        NewStaffDto newStaffDto = new NewStaffDto();
                        var staffSalaryCat = await _context.STAFF_SALARY_CATEGORY_REFERENCE.Where(c => c.StaffId == staff.Id)
                            .Include(c => c.SalaryGradeCategory)
                            .Include(c => c.SalaryLevel)
                            .Include(c => c.SalaryStep)

                            .FirstOrDefaultAsync();
                        if (staffSalaryCat?.Id > 0)
                        {
                            newStaffDto.SalaryCategory = staffSalaryCat.SalaryGradeCategory?.Representation + staffSalaryCat.SalaryLevel?.Name
                                + ", " + staffSalaryCat.SalaryStep?.Name;
                        }
                        newStaffDto.FirstName = staff.Person?.Firstname != null ? staff.Person?.Firstname : "-";
                        newStaffDto.OtherName = staff.Person?.Othername != null ? staff.Person?.Othername : "-";
                        newStaffDto.Surname = staff.Person?.Surname != null ? staff.Person?.Surname : "-";
                        newStaffDto.Email = staff.Person?.Email != null ? staff.Person?.Email : "-";
                        newStaffDto.StaffDepartment = staff.Department?.Name != null ? staff.Department?.Name : "-";
                        newStaffDto.StaffRank = staff.Rank?.Name != null ? staff.Rank?.Name : "-";
                        newStaffDto.UserName = staff.GeneratedStaffNumber != null ? staff.GeneratedStaffNumber : "-";
                        newStaffDto.StaffIdentityNumber = staff.StaffNumber != null ? staff.StaffNumber : "-";
                        newStaffDto.StaffType = staff.StaffType?.Name != null ? staff.StaffType?.Name : "-";
                        newStaffDto.StaffCategory = staff.Category?.Name != null ? staff.Category?.Name : "-";
                        newStaffDto.StaffId = staff.Id;
                        newStaffDto.IsDisengaged = staff.IsDisengaged;
                        newStaffDto.IsRetired = staff.IsRetired;
                        newStaffDto.StaffDOB = staff.Person?.BirthDay;
                        newStaffDto.Phone = staff.Person?.PhoneNumber;
                        newStaffDto.State = staff.Person?.Lga?.State?.Name;
                        newStaffDto.Lga = staff.Person?.Lga?.Name;
                        newStaffDto.DateOfEmployment = staff.DateOfConfirmation;
                        newStaffDto.AppointmentType = staff.AppointmentType?.Name;

                        if (staff.Person.BirthDay != null)
                        {
                            if (staff.StaffTypeId == (long)StaffType.AcademicStaff)
                            {

                                newStaffDto.DateOfRetirement = staff.Person?.BirthDay?.AddYears(70);

                            }
                            else if (staff.StaffTypeId == (long)StaffType.NonAcademicStaff)
                            {

                                newStaffDto.DateOfRetirement = staff.Person?.BirthDay?.AddYears(65);


                            }
                        }

                        newStaffDto.StaffFullName = staff.Person?.Surname + " " + staff.Person?.Firstname + " " + staff.Person?.Othername;
                        newStaffDto.PersonEducations = staff.Person?.PersonEducations.Select(u => new PersonEducationDtoNew
                        {
                            Institution = u.Institution,
                            EducationalQualificationId = u.EducationalQualificationId,
                            Course = u.Course,
                            Year = u.Year,
                            QualificationName = u.EducationalQualification.Name

                        }).ToList();


                        newStaffDtoList.Add(newStaffDto);
                    }
                }


                return newStaffDtoList;





            }
            else if (departmentId == 0 && rankId > 0 && staffTypeId == 0)
            {
                return await _context.STAFF

             .Where(f => f.RankId == rankId)
             .Select(f => new NewStaffDto
             {
                 FirstName = f.Person.Firstname != null || String.IsNullOrWhiteSpace(f.Person.Firstname) ? f.Person.Firstname : "-",
                 OtherName = f.Person.Othername != null || String.IsNullOrWhiteSpace(f.Person.Othername) ? f.Person.Othername : "-",
                 Surname = f.Person.Surname != null || String.IsNullOrWhiteSpace(f.Person.Surname) ? f.Person.Surname : "-",
                 StaffIdentityNumber = f.StaffNumber != null || String.IsNullOrWhiteSpace(f.StaffNumber) ? f.StaffNumber : "-",

                 Email = f.Person.Email != null ? f.Person.Email : "-",
                 StaffDepartment = f.Department.Name != null ? f.Department.Name : "-",

                 StaffRank = f.Rank.Name != null ? f.Rank.Name : "-",
                 StaffDOB = f.Person.BirthDay,
                 UserName = f.GeneratedStaffNumber != null ? f.GeneratedStaffNumber : "-",
                 StaffId = f.Id,
                 StaffCategory = f.Category.Name != null ? f.Category.Name : "-",
                 StaffType = f.StaffType.Name != null ? f.StaffType.Name : "-",
                 StaffFullName = f.Person.Surname + " " + f.Person.Firstname + " " + f.Person.Othername,


             }).ToListAsync();

            }
            else if (departmentId == 0 && rankId == 0 && staffTypeId > 0)
            {
                return await _context.STAFF

             .Where(f => f.StaffTypeId == staffTypeId)
             .Select(f => new NewStaffDto
             {
                 FirstName = f.Person.Firstname != null || String.IsNullOrWhiteSpace(f.Person.Firstname) ? f.Person.Firstname : "-",
                 OtherName = f.Person.Othername != null || String.IsNullOrWhiteSpace(f.Person.Othername) ? f.Person.Othername : "-",
                 Surname = f.Person.Surname != null || String.IsNullOrWhiteSpace(f.Person.Surname) ? f.Person.Surname : "-",
                 StaffIdentityNumber = f.StaffNumber != null || String.IsNullOrWhiteSpace(f.StaffNumber) ? f.StaffNumber : "-",

                 Email = f.Person.Email != null ? f.Person.Email : "-",
                 StaffDepartment = f.Department.Name != null ? f.Department.Name : "-",

                 StaffRank = f.Rank.Name != null ? f.Rank.Name : "-",
                 StaffDOB = f.Person.BirthDay,
                 UserName = f.GeneratedStaffNumber != null ? f.GeneratedStaffNumber : "-",
                 StaffId = f.Id,
                 StaffCategory = f.Category.Name != null ? f.Category.Name : "-",
                 StaffType = f.StaffType.Name != null ? f.StaffType.Name : "-",
                 StaffFullName = f.Person.Surname + " " + f.Person.Firstname + " " + f.Person.Othername,


             }).ToListAsync();

            }
            return null;
        }

        public async Task<Staff> GetStaffBy(string generatedStaffNumber)
        {
            try
            {
                var getRoleId = await _context.USER
                    .Where(r => r.Username == generatedStaffNumber).FirstOrDefaultAsync();


                var staff = await _context.STAFF
                    .Include(m => m.Category)
                    .Include(m => m.StaffType)
                    .Include(m => m.Rank)
                    .ThenInclude(m => m.InstitutionUnit)
                    .Include(m => m.Appointment)
                    .Include(m => m.Department)
                    .Include(m => m.Person)

                    .ThenInclude(p => p.Gender)
                    .Include(m => m.Person)
                    .ThenInclude(p => p.Lga)
                    .Include(m => m.Person)
                    .ThenInclude(p => p.State)
                    .Include(m => m.Person)
                    .ThenInclude(p => p.MaritalStatus)
                    .Include(m => m.Person)
                    .ThenInclude(p => p.Religion)
                    .Include(m => m.Person)
                    .ThenInclude(p => p.Gender)
                    .Include(m => m.AppointmentType)



                    .SingleOrDefaultAsync(m => m.GeneratedStaffNumber == generatedStaffNumber);
                if (staff != null)
                    staff.Person.ImageUrl = baseUrl + staff.Person.ImageUrl;

                return staff;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public async Task<AppReturnObject> AssignRole(AssignRoleDto assignRoleDto)
        {
            var getStaff = await _context.USER.Where(d => d.StaffId == assignRoleDto.StaffId).FirstOrDefaultAsync();

            if (assignRoleDto == null)
                throw new AppException("Fields are empty");

            getStaff.RoleId = assignRoleDto.RoleId;
            _context.Update(getStaff);
            await _context.SaveChangesAsync();

            return appReturnObject;


        }

        public async Task<long> StaffCount()
        {
            var staffCount = _context.STAFF;
            var sum = staffCount.Count();
            return sum;
        }

        public async Task<IEnumerable<NewStaffDto>> FetchStaff()
        {
            List<NewStaffDto> newStaffDtoList = new List<NewStaffDto>();
            var fetchStaff = await _context.STAFF
                .Include(c => c.Category)
                .Include(c => c.StaffType)
                .Include(c => c.Rank)
                .Include(c => c.Person)
                .ThenInclude(e => e.PersonEducations)
                .ThenInclude(e => e.EducationalQualification)
                .Include(s => s.Person)
                .ThenInclude(s => s.Lga)
                .ThenInclude(s => s.State)
                .Include(d => d.Department)
                .Include(a => a.AppointmentType)
               .Where(s => s.IsRetired == false && s.IsDisengaged == false)
                .ToListAsync();

            if (fetchStaff?.Count() > 0)
            {
                foreach (var staff in fetchStaff)
                {
                    NewStaffDto newStaffDto = new NewStaffDto();
                    var staffSalaryCat = await _context.STAFF_SALARY_CATEGORY_REFERENCE.Where(c => c.StaffId == staff.Id)
                        .Include(c => c.SalaryGradeCategory)
                        .Include(c => c.SalaryLevel)
                        .Include(c => c.SalaryStep)

                        .FirstOrDefaultAsync();
                    if (staffSalaryCat?.Id > 0)
                    {
                        newStaffDto.SalaryCategory = staffSalaryCat.SalaryGradeCategory?.Representation + staffSalaryCat.SalaryLevel?.Name
                            + ", " + staffSalaryCat.SalaryStep?.Name;
                    }
                    newStaffDto.FirstName = staff.Person?.Firstname != null ? staff.Person?.Firstname : "-";
                    newStaffDto.OtherName = staff.Person?.Othername != null ? staff.Person?.Othername : "-";
                    newStaffDto.Surname = staff.Person?.Surname != null ? staff.Person?.Surname : "-";
                    newStaffDto.Email = staff.Person?.Email != null ? staff.Person?.Email : "-";
                    newStaffDto.StaffDepartment = staff.Department?.Name != null ? staff.Department?.Name : "-";
                    newStaffDto.StaffRank = staff.Rank?.Name != null ? staff.Rank?.Name : "-";
                    newStaffDto.UserName = staff.GeneratedStaffNumber != null ? staff.GeneratedStaffNumber : "-";
                    newStaffDto.StaffIdentityNumber = staff.StaffNumber != null ? staff.StaffNumber : "-";
                    newStaffDto.StaffType = staff.StaffType?.Name != null ? staff.StaffType?.Name : "-";
                    newStaffDto.StaffCategory = staff.Category?.Name != null ? staff.Category?.Name : "-";
                    newStaffDto.StaffId = staff.Id;
                    newStaffDto.IsDisengaged = staff.IsDisengaged;
                    newStaffDto.IsRetired = staff.IsRetired;
                    newStaffDto.StaffFullName = staff.Person?.Surname + " " + staff.Person?.Firstname + " " + staff.Person?.Othername;
                    newStaffDto.StaffDOB = staff.Person?.BirthDay;
                    newStaffDto.Phone = staff.Person?.PhoneNumber;
                    newStaffDto.State = staff.Person?.Lga?.State?.Name;
                    newStaffDto.Lga = staff.Person?.Lga?.Name;
                    newStaffDto.DateOfEmployment = staff.DateOfConfirmation;
                    newStaffDto.AppointmentType = staff.AppointmentType?.Name;
                    newStaffDto.BiometricNumber = staff.BiometricId != null ? staff.BiometricId : "-";

                    if (staff.Person.BirthDay != null)
                    {
                        if (staff.StaffTypeId == (long)StaffType.AcademicStaff)
                        {

                            newStaffDto.DateOfRetirement = staff.Person?.BirthDay?.AddYears(70);

                        }
                        else if (staff.StaffTypeId == (long)StaffType.NonAcademicStaff)
                        {

                            newStaffDto.DateOfRetirement = staff.Person?.BirthDay?.AddYears(65);


                        }
                    }
                    newStaffDto.PersonEducations = staff.Person?.PersonEducations.Select(u => new PersonEducationDtoNew
                    {
                        Institution = u.Institution,
                        EducationalQualificationId = u.EducationalQualificationId,
                        Course = u.Course,
                        Year = u.Year,
                        QualificationName = u.EducationalQualification.Name

                    }).ToList();


                    newStaffDtoList.Add(newStaffDto);
                }
            }


            return newStaffDtoList;
        }

        public async Task<IEnumerable<StaffNominalRollDto>> FetchStaffForMonthlyNominalRoll(long departmentId)
        {
            List<StaffNominalRollDto> staffNominalRollDtoList = new List<StaffNominalRollDto>();
            var allDepartmentStaff = await _context.STAFF.Where(f => f.DepartmentId == departmentId)
                .Include(p => p.Person)
                .Include(s => s.StaffType)
                .Include(r => r.Rank)
                .Include(sc => sc.Category)
                .ToListAsync();
            if (allDepartmentStaff?.Count > 0)
            {
                foreach (var item in allDepartmentStaff)
                {
                    StaffNominalRollDto staffNominalRollDto = new StaffNominalRollDto();
                    var nominal = await _context.STAFF_NOMINAL_ROLL.Where(s => s.Month == DateTime.Now.Month && s.Year == DateTime.Now.Year && s.StaffId == item.Id)

                        .FirstOrDefaultAsync();

                    StaffSalaryCategoryReference salaryCategorySelector = await _context.STAFF_SALARY_CATEGORY_REFERENCE.Where(sg => sg.StaffId == item.Id)
                        .Include(d => d.SalaryGradeCategory)
                        .Include(l => l.SalaryLevel)
                        .Include(ss => ss.SalaryStep)
                        .FirstOrDefaultAsync();
                    if (nominal?.Id > 0)
                    {

                        staffNominalRollDto.IsCleared = nominal.IsCleared;
                        staffNominalRollDto.Comments = nominal.Comment;

                    }
                    else
                    {
                        staffNominalRollDto.IsCleared = false;
                    }

                    staffNominalRollDto.StaffName = item.Person?.Surname + " " + item.Person?.Firstname + " " + item.Person?.Othername;
                    staffNominalRollDto.UserName = item.GeneratedStaffNumber;
                    staffNominalRollDto.StaffIdentityNumber = item.StaffNumber;
                    staffNominalRollDto.StaffRank = item.Rank?.Name;
                    staffNominalRollDto.StaffId = item.Id;
                    staffNominalRollDto.StaffType = item.StaffType?.Name;
                    staffNominalRollDto.StaffCategory = item.Category?.Name;
                    staffNominalRollDto.Month = DateTime.Now.Month;
                    if (salaryCategorySelector != null)
                    {
                        staffNominalRollDto.StaffSalaryCategory = salaryCategorySelector.SalaryGradeCategory?.Representation + salaryCategorySelector.SalaryLevel?.Name + ", " + salaryCategorySelector.SalaryStep?.Name;

                    }

                    staffNominalRollDtoList.Add(staffNominalRollDto);

                }
            }
            return staffNominalRollDtoList;


        }



        public async Task<int> DisengageStaff(long staffId)
        {
            var selectStaff = await _context.STAFF.Where(s => s.Id == staffId && s.IsDisengaged == false).FirstOrDefaultAsync();
            if (selectStaff == null)
                throw new NullReferenceException();
            selectStaff.IsDisengaged = true;
            selectStaff.Active = false;
            _context.Update(selectStaff);
            await _context.SaveChangesAsync();
            return StatusCodes.Status200OK;
        }
        public async Task<int> ReinstateStaff(long staffId)
        {
            var selectStaff = await _context.STAFF.Where(s => s.Id == staffId && s.IsDisengaged == true).FirstOrDefaultAsync();
            if (selectStaff == null)
                throw new NullReferenceException();
            selectStaff.IsDisengaged = false;
            selectStaff.Active = true;
            _context.Update(selectStaff);
            await _context.SaveChangesAsync();
            return StatusCodes.Status200OK;
        }

        public async Task<IEnumerable<NewStaffDto>> DisengagedRetiredStaff(long serviceStatus)
        {
            if (serviceStatus == (long)StaffServiceStatus.Disengaged)
            {
                var fetchStaff = await _context.STAFF
                .Where(s => s.IsDisengaged == true)

                .Select(f => new NewStaffDto
                {
                    FirstName = f.Person.Firstname,
                    OtherName = f.Person.Othername,
                    Surname = f.Person.Surname,
                    Email = f.Person.Email,
                    StaffDepartment = f.Department.Name,
                    StaffRank = f.Rank.Name,
                    StaffDOB = f.Person.BirthDay,
                    UserName = f.GeneratedStaffNumber,
                    StaffIdentityNumber = f.StaffNumber,
                    StaffId = f.Id,
                    IsDisengaged = f.IsDisengaged,
                    IsRetired = f.IsRetired


                }).ToListAsync();

                return fetchStaff;
            }
            else if (serviceStatus == (long)StaffServiceStatus.Retired)
            {
                var fetchStaff = await _context.STAFF
                .Where(s => s.IsRetired == true)

                .Select(f => new NewStaffDto
                {
                    FirstName = f.Person.Firstname,
                    OtherName = f.Person.Othername,
                    Surname = f.Person.Surname,
                    Email = f.Person.Email,
                    StaffDepartment = f.Department.Name,
                    StaffRank = f.Rank.Name,
                    StaffDOB = f.Person.BirthDay,
                    UserName = f.GeneratedStaffNumber,
                    StaffIdentityNumber = f.StaffNumber,
                    StaffId = f.Id,
                    IsDisengaged = f.IsDisengaged,
                    IsRetired = f.IsRetired


                }).ToListAsync();

                return fetchStaff;
            }
            return null;


        }

        public async Task<IEnumerable<GenerateStaffNumberListDto>> GetGeneratedStaffNumberByDate(DateTime from, DateTime to)
        {
            List<GenerateStaffNumberListDto> staffNumberList = new List<GenerateStaffNumberListDto>();


            var generatedStaffNumbers = await _context.GENERATED_STAFFNUMBER_RECORDS
                .Where(s => (s.DateGenerated.Date == from || s.DateGenerated.Date >= from) && s.DateGenerated.Date <= to)
                .Include(s => s.Staff)
                .ThenInclude(p => p.Person)
                .Include(s => s.Staff)
                .ThenInclude(r => r.Rank)
                .Include(s => s.Staff)
                .ThenInclude(d => d.Department)
                .ToListAsync();

            if (generatedStaffNumbers?.Count > 0)
            {
                foreach (var item in generatedStaffNumbers)
                {
                    GenerateStaffNumberListDto staffNumberListDto = new GenerateStaffNumberListDto();

                    StaffSalaryCategoryReference salaryCategorySelector = await _context.STAFF_SALARY_CATEGORY_REFERENCE.Where(sg => sg.StaffId == item.StaffId)
                        .Include(d => d.SalaryGradeCategory)
                        .Include(l => l.SalaryLevel)
                        .Include(ss => ss.SalaryStep)
                        .FirstOrDefaultAsync();

                    staffNumberListDto.StaffName = item.Staff?.Person?.Surname + " " + item.Staff?.Person?.Firstname + " " + item.Staff?.Person?.Othername;
                    staffNumberListDto.StaffNumber = item.Staff.StaffNumber != null ? item.Staff.StaffNumber : "-";
                    staffNumberListDto.Department = item.Staff?.Department?.Name != null ? item.Staff.Department.Name : "-";
                    staffNumberListDto.DateOfAssumption = item.Staff?.DateOfConfirmation;
                    staffNumberListDto.Rank = item.Staff?.Rank?.Name != null ? item.Staff.Rank.Name : "-";
                    staffNumberListDto.StaffId = item.Staff.Id;


                    if (salaryCategorySelector != null)
                    {
                        staffNumberListDto.SalaryCategory = salaryCategorySelector.SalaryGradeCategory.Name;
                        staffNumberListDto.SalaryLevel = salaryCategorySelector.SalaryLevel.Name;
                        staffNumberListDto.SalaryStep = salaryCategorySelector.SalaryStep.Name;
                    }

                    staffNumberList.Add(staffNumberListDto);
                }


            }


            return staffNumberList;



        }


        public async Task<NewStaffDto> GetStaffLoginInfo(string surname, string staffNumber)
        {
            NewStaffDto newStaffDto = new NewStaffDto();

            var staffDetails = await _context.STAFF.Where(s => s.Person.Surname == surname && s.StaffNumber == staffNumber)
                .Include(p => p.Person)
                .FirstOrDefaultAsync();

            if (staffDetails != null)
            {

                newStaffDto.Surname = staffDetails.Person?.Surname;
                newStaffDto.FirstName = staffDetails.Person?.Firstname;
                newStaffDto.OtherName = staffDetails.Person?.Othername;
                newStaffDto.StaffIdentityNumber = staffDetails.StaffNumber;
                newStaffDto.UserName = staffDetails.GeneratedStaffNumber;
                newStaffDto.status = StatusCodes.Status200OK;

                return newStaffDto;

            }
            else
            {
                newStaffDto.status = StatusCodes.Status204NoContent;
            }

            return newStaffDto;
            //..
        }

        public async Task<int> ModifyUserPassword(ChangePassword dto)
        {
            try
            {
                if (dto == null)
                    throw new NullReferenceException("Null Object");


                var user = await _context.USER.Where(p => p.Id == dto.UserId).FirstOrDefaultAsync();
                if (user == null)
                    throw new NullReferenceException("Null Exception");
                byte[] passwordHash, passwordSalt;
                var isVerified = VerifyPasswordHash(dto.OldPassword, user.PasswordHash, user.PasswordSalt);
                if (!isVerified)
                    throw new NullReferenceException("Null Exception");

                CreatePasswordHash(dto.NewPassword, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;


                _context.Update(user);
                await _context.SaveChangesAsync();

                return StatusCodes.Status200OK;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

    }


}


