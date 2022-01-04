using LiteHR.Dtos;
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
    public class StaffNominalRollService:IStaffNominalRollService
    {
        public readonly HRContext _context;
        public StaffNominalRollService(HRContext context)
        {
            _context = context;

        }


        public async Task<int>  StaffConfirmationForNominalRoll(IEnumerable<NominalRollDto> nominalRollDtos)
        {
            //List<StaffNominalRoll> staffNominalRollList = new List<StaffNominalRoll>();


            if(nominalRollDtos?.Count() > 0)
            {
                foreach(var staff in nominalRollDtos)
                {
                    StaffNominalRoll staffNominalRoll = new StaffNominalRoll();
                    var doesExist = await _context.STAFF_NOMINAL_ROLL.Where(n => n.StaffId == staff.StaffId && n.Month == DateTime.Now.Month && n.Year == DateTime.Now.Year).FirstOrDefaultAsync();


                    staffNominalRoll.StaffId = staff.StaffId;
                    staffNominalRoll.IsCleared = true;
                    staffNominalRoll.Year = DateTime.Now.Year;
                    staffNominalRoll.Month = DateTime.Now.Month;
                    _context.Add(staffNominalRoll);
                }
                    await _context.SaveChangesAsync();

                
                return StatusCodes.Status200OK;
            }
            return 0;
        }
        public async Task<long> PostNominalRollComment(long staffId, string comment)
        {
            try
            {
                StaffNominalRoll staffNominalRoll = new StaffNominalRoll();
                var doesExist = await _context.STAFF_NOMINAL_ROLL.Where(n => n.StaffId == staffId && n.Month == DateTime.Now.Month && n.Year == DateTime.Now.Year).FirstOrDefaultAsync();
                if (doesExist != null)
                {
                    doesExist.Comment = comment;
                    _context.Update(doesExist);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    staffNominalRoll.Comment = comment;
                    staffNominalRoll.StaffId = staffId;
                    staffNominalRoll.IsCleared = false;
                    staffNominalRoll.Year = DateTime.Now.Year;
                    staffNominalRoll.Month = DateTime.Now.Month;
                    _context.Add(staffNominalRoll);
                    await _context.SaveChangesAsync();

                }
                return StatusCodes.Status200OK;
            }
           catch(Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IEnumerable<StaffNominalRollDto>> StaffNominalRollByFaculty(long month, long year)
        {
            List<StaffNominalRollDto> staffNominalRollDtoList = new List<StaffNominalRollDto>();

            var nominal = await _context.STAFF_NOMINAL_ROLL.Where(s => s.Month == month && s.Year == year)
                .Include(s => s.Staff)
                .ThenInclude(p => p.Person)
                .Include(r => r.Staff)
                .ThenInclude(r => r.Rank)
                .ToListAsync();

                foreach(var item in nominal)
            {
                StaffNominalRollDto staffNominalRollDto = new StaffNominalRollDto();
                var getDept = await _context.INSTITUTION_DEPARTMENT.Where(d => d.Id == item.Staff.DepartmentId).FirstOrDefaultAsync();

                StaffSalaryCategoryReference salaryCategorySelector = await _context.STAFF_SALARY_CATEGORY_REFERENCE.Where(sg => sg.StaffId == item.StaffId)
                       .Include(d => d.SalaryGradeCategory)
                       .Include(l => l.SalaryLevel)
                       .Include(s => s.SalaryStep)
                       .Include(s => s.Staff)
                       .ThenInclude(d=>d.Department)
                       .Include(s => s.Staff)
                       .ThenInclude(d => d.Category)
                       .Include(s => s.Staff)
                       .ThenInclude(d => d.StaffType)
                       .FirstOrDefaultAsync();
                staffNominalRollDto.StaffName = item.Staff?.Person?.Surname + " " + item.Staff?.Person?.Firstname + " " + item.Staff?.Person?.Othername;
                staffNominalRollDto.UserName = item.Staff.GeneratedStaffNumber !=null ? item.Staff?.GeneratedStaffNumber : "-";
                staffNominalRollDto.StaffIdentityNumber = item.Staff?.StaffNumber != null ? item.Staff?.StaffNumber : "-";
                staffNominalRollDto.StaffRank = item.Staff.Rank?.Name != null ? item.Staff.Rank?.Name : "-";
                staffNominalRollDto.StaffId = item.Id;
                staffNominalRollDto.StaffType = item.Staff?.StaffType?.Name != null ? item.Staff?.StaffType?.Name : "-";
                staffNominalRollDto.StaffCategory = item.Staff?.Category?.Name != null ? item.Staff?.Category?.Name : "-";
                staffNominalRollDto.StaffDepartment = getDept?.Name != null ? getDept?.Name : "-";
                staffNominalRollDto.Month = DateTime.Now.Month;
                if (salaryCategorySelector != null)
                {
                    staffNominalRollDto.StaffSalaryCategory = salaryCategorySelector.SalaryGradeCategory?.Representation + salaryCategorySelector.SalaryLevel?.Name + ", " + salaryCategorySelector.SalaryStep?.Name;

                }
                else
                {
                    staffNominalRollDto.StaffSalaryCategory = "-";
                }

                staffNominalRollDtoList.Add(staffNominalRollDto);
            }
                


            return staffNominalRollDtoList;
        }

        public async Task<IEnumerable<StaffNominalRollDto>> PullStaffForMonthlyNominalRoll(long departmentId, long month, long year)
        {
            List<StaffNominalRollDto> staffNominalRollDtoList = new List<StaffNominalRollDto>();
           if(departmentId > 0)
            {
                var allDepartmentStaff = await _context.STAFF.Where(f => f.DepartmentId == departmentId)
               .Include(p => p.Person)
               .ThenInclude(l => l.Lga)
               .ThenInclude(s => s.State)
               .Include(s => s.StaffType)
               .Include(r => r.Rank)
               .Include(sc => sc.Category)
               .Include(a => a.AppointmentType)
               .Include(d => d.Department)
               .ToListAsync();
                if (allDepartmentStaff?.Count > 0)
                {
                    foreach (var item in allDepartmentStaff)
                    {
                        StaffNominalRollDto staffNominalRollDto = new StaffNominalRollDto();
                        var nominal = await _context.STAFF_NOMINAL_ROLL.Where(s => s.Month == month && s.Year == year && s.StaffId == item.Id)
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
                        staffNominalRollDto.AppointmentType = item.AppointmentType?.Name;
                        staffNominalRollDto.Month = DateTime.Now.Month;
                        staffNominalRollDto.StaffDepartment = item.Department?.Name != null ? item.Department?.Name : "-";
                        staffNominalRollDto.Email = item.Person?.Email != null ? item.Person?.Email : "-";
                        staffNominalRollDto.Phone = item.Person?.PhoneNumber != null ? item.Person?.PhoneNumber : "-";
                        staffNominalRollDto.LGA = item.Person?.Lga?.Name != null ? item.Person?.Lga?.Name : "-";
                        staffNominalRollDto.State = item.Person?.Lga?.State?.Name != null ? item.Person?.Lga?.State?.Name : "-";
                        staffNominalRollDto.StaffDOB = item.Person?.BirthDay;
                        staffNominalRollDto.DateOfEmployment = item.DateOfConfirmation;
                        if (item.Person?.BirthDay != null)
                        {
                            if (item.StaffTypeId == (long)StaffType.AcademicStaff)
                            {

                                staffNominalRollDto.DateOfRetirement = item.Person?.BirthDay?.AddYears(70);

                            }
                            else if (item.StaffTypeId == (long)StaffType.NonAcademicStaff)
                            {

                                staffNominalRollDto.DateOfRetirement = item.Person?.BirthDay?.AddYears(65);


                            }
                        }
                        if (salaryCategorySelector != null)
                        {
                            staffNominalRollDto.StaffSalaryCategory = salaryCategorySelector.SalaryGradeCategory?.Representation + salaryCategorySelector.SalaryLevel?.Name + ", " + salaryCategorySelector.SalaryStep?.Name;

                        }

                        staffNominalRollDtoList.Add(staffNominalRollDto);

                    }
                }
                return staffNominalRollDtoList;
            }
            else
            {
                var allDepartmentStaff = await _context.STAFF
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
                        var nominal = await _context.STAFF_NOMINAL_ROLL.Where(s => s.Month == month && s.Year == year && s.StaffId == item.Id)
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


        }

        public async Task<IEnumerable<StaffNominalRollDto>> GetNominalRollBy(long facultyId, long departmentId)
        {
            if(facultyId > 0 && departmentId == 0)
            {
                List<StaffNominalRollDto> staffNominalRollDtoList = new List<StaffNominalRollDto>();

                var nominal = await _context.STAFF_NOMINAL_ROLL.Where(s => s.Staff.Department.FacultyId == facultyId)
                    .Include(s => s.Staff)
                    .ThenInclude(p => p.Person)
                    .ThenInclude(e => e.PersonEducations)
                    .Include(s => s.Staff)
                    .ThenInclude(p => p.Person)
                    .ThenInclude(l => l.Lga)
                    .ThenInclude(s => s.State)
                    .Include(r => r.Staff)
                    .ThenInclude(r => r.Rank)

                    .ToListAsync();

                foreach (var item in nominal)
                {
                    StaffNominalRollDto staffNominalRollDto = new StaffNominalRollDto();
                    var getDept = await _context.INSTITUTION_DEPARTMENT.Where(d => d.Id == item.Staff.DepartmentId).FirstOrDefaultAsync();


                    StaffSalaryCategoryReference salaryCategorySelector = await _context.STAFF_SALARY_CATEGORY_REFERENCE.Where(sg => sg.StaffId == item.StaffId)
                           .Include(d => d.SalaryGradeCategory)
                           .Include(l => l.SalaryLevel)
                           .Include(s => s.SalaryStep)
                           .Include(s => s.Staff)
                           .ThenInclude(d => d.Department)
                           .Include(s => s.Staff)
                           .ThenInclude(d => d.Category)
                           .Include(s => s.Staff)
                           .ThenInclude(d => d.StaffType)
                           .FirstOrDefaultAsync();
                    staffNominalRollDto.StaffName = item.Staff?.Person?.Surname + " " + item.Staff?.Person?.Firstname + " " + item.Staff?.Person?.Othername;
                    staffNominalRollDto.UserName = item.Staff.GeneratedStaffNumber != null ? item.Staff?.GeneratedStaffNumber : "-";
                    staffNominalRollDto.StaffIdentityNumber = item.Staff?.StaffNumber != null ? item.Staff?.StaffNumber : "-";
                    staffNominalRollDto.StaffRank = item.Staff.Rank?.Name != null ? item.Staff.Rank?.Name : "-";
                    staffNominalRollDto.StaffId = item.Id;
                    staffNominalRollDto.StaffType = item.Staff?.StaffType?.Name != null ? item.Staff?.StaffType?.Name : "-";
                    staffNominalRollDto.StaffCategory = item.Staff?.Category?.Name != null ? item.Staff?.Category?.Name : "-";
                    staffNominalRollDto.StaffDepartment = item.Staff?.Department?.Name != null ? item.Staff?.Department?.Name : "-";
                    staffNominalRollDto.Email = item.Staff?.Person.Email != null ? item.Staff?.Person.Email : "-";
                    staffNominalRollDto.Phone = item.Staff?.Person?.PhoneNumber != null ? item.Staff?.Person?.PhoneNumber : "-";
                    staffNominalRollDto.LGA = item.Staff?.Person?.Lga?.Name != null ? item.Staff?.Person?.Lga?.Name : "-";
                    staffNominalRollDto.State = item.Staff?.Person?.Lga?.State?.Name!= null ? item.Staff?.Person?.Lga?.State?.Name : "-";
                    staffNominalRollDto.StaffDOB = item.Staff?.Person?.BirthDay;
                    staffNominalRollDto.DateOfEmployment = item.Staff?.DateOfConfirmation;

                    staffNominalRollDto.Month = DateTime.Now.Month;
                    if (salaryCategorySelector != null)
                    {
                        staffNominalRollDto.StaffSalaryCategory = salaryCategorySelector.SalaryGradeCategory?.Representation + salaryCategorySelector.SalaryLevel?.Name + ", " + salaryCategorySelector.SalaryStep?.Name;

                    }
                    else
                    {
                        staffNominalRollDto.StaffSalaryCategory = "-";
                    }
                    if (item.Staff?.Person?.BirthDay != null)
                    {
                        if (item.Staff.StaffTypeId == (long)StaffType.AcademicStaff)
                        {

                            staffNominalRollDto.DateOfRetirement = item.Staff?.Person?.BirthDay?.AddYears(70);

                        }
                        else if (item.Staff.StaffTypeId == (long)StaffType.NonAcademicStaff)
                        {

                            staffNominalRollDto.DateOfRetirement = item.Staff?.Person?.BirthDay?.AddYears(65);


                        }
                    }
                    //staffNominalRollDto.PersonEducations = item.Staff?.Person?.PersonEducations.Select(u => new PersonEducationDtoNew
                    //{
                    //    Institution = u.Institution,
                    //    EducationalQualificationId = u.EducationalQualificationId,
                    //    Course = u.Course,
                    //    Year = u.Year,
                    //    QualificationName = u.EducationalQualification.Name

                    //}).ToList();



                    staffNominalRollDtoList.Add(staffNominalRollDto);
                }
                return staffNominalRollDtoList;
                
             
            }
            else if (facultyId == 0 && departmentId > 0)
            {
                List<StaffNominalRollDto> staffNominalRollDtoList2 = new List<StaffNominalRollDto>();

                var nominal2 = await _context.STAFF_NOMINAL_ROLL.Where(s => s.Staff.DepartmentId == departmentId)
                    .Include(s => s.Staff)
                    .ThenInclude(p => p.Person)
                    .Include(r => r.Staff.Rank).ToListAsync();

                foreach (var item in nominal2)
                {
                    StaffNominalRollDto staffNominalRollDto = new StaffNominalRollDto();
                    var getDept = await _context.INSTITUTION_DEPARTMENT.Where(d => d.Id == item.Staff.DepartmentId).FirstOrDefaultAsync();

                    StaffSalaryCategoryReference salaryCategorySelector = await _context.STAFF_SALARY_CATEGORY_REFERENCE.Where(sg => sg.StaffId == item.StaffId)
                           .Include(d => d.SalaryGradeCategory)
                           .Include(l => l.SalaryLevel)
                           .Include(s => s.SalaryStep)
                           .Include(s => s.Staff)
                           .ThenInclude(d => d.Department)
                           .Include(s => s.Staff)
                           .ThenInclude(d => d.Category)
                           .Include(s => s.Staff)
                           .ThenInclude(d => d.StaffType)
                           .FirstOrDefaultAsync();
                    staffNominalRollDto.StaffName = item.Staff?.Person?.Surname + " " + item.Staff?.Person?.Firstname + " " + item.Staff?.Person?.Othername;
                    staffNominalRollDto.UserName = item.Staff.GeneratedStaffNumber != null ? item.Staff?.GeneratedStaffNumber : "-";
                    staffNominalRollDto.StaffIdentityNumber = item.Staff?.StaffNumber != null ? item.Staff?.StaffNumber : "-";
                    staffNominalRollDto.StaffRank = item.Staff.Rank?.Name != null ? item.Staff.Rank?.Name : "-";
                    staffNominalRollDto.StaffId = item.Id;
                    staffNominalRollDto.StaffType = item.Staff?.StaffType?.Name != null ? item.Staff?.StaffType?.Name : "-";
                    staffNominalRollDto.StaffCategory = item.Staff?.Category?.Name != null ? item.Staff?.Category?.Name : "-";
                    staffNominalRollDto.StaffDepartment = getDept?.Name != null ? getDept?.Name : "-";
                    staffNominalRollDto.Month = DateTime.Now.Month;
                    if (salaryCategorySelector != null)
                    {
                        staffNominalRollDto.StaffSalaryCategory = salaryCategorySelector.SalaryGradeCategory?.Representation + salaryCategorySelector.SalaryLevel?.Name + ", " + salaryCategorySelector.SalaryStep?.Name;

                    }
                    else
                    {
                        staffNominalRollDto.StaffSalaryCategory = "-";
                    }

                    staffNominalRollDtoList2.Add(staffNominalRollDto);
                }
                return staffNominalRollDtoList2;
            }

            return null;
        }

    }
}
