using LiteHR.Dtos;
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
    public class StaffPostingService : IStaffPostingService
    {
        private readonly HRContext _context;

        public StaffPostingService(HRContext context)
        {
            _context = context;
        }

        public async Task<long> GetStaffCountByDepartmentAndRank(long departmentId, long rankId)
        {
            var staffSum = await _context.STAFF.Where(st => st.DepartmentId == departmentId && st.RankId == rankId).CountAsync();
            if(staffSum > 0)
            {
                return staffSum;
            }
            return 0;
        }

        public async Task<IEnumerable<GenerateStaffNumberListDto>> GetNewlyCreatedStaff(DateTime from, DateTime to)
        {
            List<GenerateStaffNumberListDto> staffNumberList = new List<GenerateStaffNumberListDto>();


            var generatedStaffNumbers = await _context.GENERATED_STAFFNUMBER_RECORDS
                 .Where(s => (s.DateGenerated.Date == from || s.DateGenerated.Date >= from) && s.DateGenerated.Date <= to)
                 .Include(d => d.DefaultDepartment)
                .Include(s => s.Staff)
                .ThenInclude(p => p.Person)
                .ThenInclude(s => s.Lga)
                .Include(s => s.Staff)
                .ThenInclude(r => r.Rank)
                .Include(s => s.Staff)
                .ThenInclude(d => d.Department)
                .ThenInclude(f=>f.Faculty)
                .ToListAsync();

            if (generatedStaffNumbers?.Count > 0)
            {
                foreach (var item in generatedStaffNumbers)
                {
                    GenerateStaffNumberListDto staffNumberListDto = new GenerateStaffNumberListDto();
                    var oldDeptCheck = await _context.GENERATED_STAFFNUMBER_RECORDS.Where(s => s.StaffId == item.StaffId).FirstOrDefaultAsync();

                    StaffSalaryCategoryReference salaryCategorySelector = await _context.STAFF_SALARY_CATEGORY_REFERENCE.Where(sg => sg.StaffId == item.StaffId)
                        .Include(d => d.SalaryGradeCategory)
                        .Include(l => l.SalaryLevel)
                        .Include(ss => ss.SalaryStep)
                        .Include(s => s.Staff)
                        .FirstOrDefaultAsync();
                    
                    var facultyCheck = item.Staff?.Department?.Faculty?.Name;
                    var departmentName = item.Staff?.Department?.Name;

                    staffNumberListDto.StaffName = item.Staff?.Person?.Surname + " " + item.Staff?.Person?.Firstname + " " + item.Staff?.Person?.Othername;
                    staffNumberListDto.StaffNumber = item.Staff?.StaffNumber != null ? item.Staff.StaffNumber : "-";
                    staffNumberListDto.Department = item.Staff?.Department?.Name != null ? item.Staff.Department.Name.Trim() : "-";
                    staffNumberListDto.DateOfAssumption = item.Staff?.DateOfConfirmation;
                    staffNumberListDto.Rank = item.Staff?.Rank?.Name != null ? item.Staff.Rank.Name : "-";
                    staffNumberListDto.PhoneNumber = item.Staff?.Person?.PhoneNumber;
                    staffNumberListDto.LgaId = item.Staff?.Person?.LGAId;
                    staffNumberListDto.DepartmentId = item.Staff?.DepartmentId;
                    staffNumberListDto.GenderId = item.Staff?.Person?.GenderId;
                    staffNumberListDto.StaffId = item.Staff.Id;
                    staffNumberListDto.StateId = item.Staff?.Person?.Lga?.StateId;
                    staffNumberListDto.DefaultDepartmentName = item?.DefaultDepartment?.Name != null ? item?.DefaultDepartment?.Name.Trim() : "-";
                    if(facultyCheck == null)
                    {
                        staffNumberListDto.HeadRep = "Unassigned";

                    }
                   else if (facultyCheck.Contains("Directorate"))
                    {
                        staffNumberListDto.HeadRep = "      Director" + ", " + departmentName;
                    }
                    else if (facultyCheck.Contains("Unit"))
                    {
                        staffNumberListDto.HeadRep = "      Deputy Registrar"+"("+departmentName.Trim()+")";
                    }
                    else if (facultyCheck.Contains("-"))
                    {
                        staffNumberListDto.HeadRep = "      HOD" + ", " + departmentName;
                    }
                    else if (departmentName.Contains("Dean"))
                    {
                        staffNumberListDto.HeadRep = "      Dean" + ", Faculty of " + facultyCheck;
                    }
                    else
                    {
                        staffNumberListDto.HeadRep = "      HOD" + ", " + departmentName;

                    }

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
        //Personnel SAPS Role
        public async Task<int> UpdateNewlyCreatedStaff(UpdateNewStaffDto updateNewStaffDto, long staffId)
        {
            var staff = await _context.STAFF.Where(s => s.Id == staffId).Include(s => s.Person).FirstOrDefaultAsync();
            var oldDeptRecord = await _context.GENERATED_STAFFNUMBER_RECORDS.Where(g => g.StaffId == staffId).FirstOrDefaultAsync();
            if(staff != null)
            {
                oldDeptRecord.DefaultDepartmentId = staff.DepartmentId;
                staff.Person.PhoneNumber = updateNewStaffDto.PhoneNumber != null ? updateNewStaffDto.PhoneNumber: staff.Person.PhoneNumber;
                staff.Person.StateId = updateNewStaffDto.StateId != null ? updateNewStaffDto.StateId : staff.Person.StateId;
                staff.Person.LGAId = updateNewStaffDto.LgaId != null ? updateNewStaffDto.LgaId : staff.Person.LGAId;
                staff.Person.GenderId = updateNewStaffDto.GenderId != null ? updateNewStaffDto.GenderId : staff.Person.GenderId;
                staff.DepartmentId = updateNewStaffDto.DepartmentId != null ? updateNewStaffDto.DepartmentId : staff.DepartmentId;

                _context.Update(staff);
                await _context.SaveChangesAsync();
                return StatusCodes.Status200OK;
            }
            return StatusCodes.Status400BadRequest;
        }
        public async Task<IEnumerable<NewStaffCountDto>> GetStaffRankCountWhole(long cadreId)
        {
            List<NewStaffCountDto> deptRankCountList = new List<NewStaffCountDto>();

            var depts = await _context.INSTITUTION_DEPARTMENT
                .OrderBy(d => d.Name.Trim())
                .ToListAsync();
            var getRank = await _context.INSTITUTION_RANK.Where(r => r.InstitutionUnitId == cadreId)
                .OrderBy(n => n.Name.Trim())
                .ToListAsync();
            

            //var getStaff = await _context.STAFF.ToListAsync();
            foreach (var item in depts)
            {
                NewStaffCountDto newStaffCountDto = new NewStaffCountDto();
                newStaffCountDto.DepartmentName= item?.Name;
                newStaffCountDto.RankStaffCounts = new List<RankStaffCount>();
                foreach (var rankItem in getRank)
                {
                    
                    var getRankCount = await _context.STAFF.Where(s => s.DepartmentId == item.Id && s.RankId == rankItem.Id)
                    .Include(d => d.Department)
                    .ToListAsync();
                    newStaffCountDto.Count = getRankCount.Count();

                    RankStaffCount rankStaffCount = new RankStaffCount()
                    {
                        Count = getRankCount.Count(),
                        RankName = rankItem.Name
                    };
                    newStaffCountDto.RankStaffCounts.Add(rankStaffCount);



                }
                
                deptRankCountList.Add(newStaffCountDto);

               

            }
            return deptRankCountList;
        }

        public async Task<DeptRankCountDto> GetAllDepartmentStaffRankCount(long rankId, long departmentId)
        {

            List<DeptRankCountDto> deptRankCountList = new List<DeptRankCountDto>();
            DeptRankCountDto deptRankCount = new DeptRankCountDto();
           
            
            var staffCount = await _context.STAFF.Where(s => s.RankId == rankId && s.DepartmentId == departmentId).CountAsync();
            var rankName = await _context.INSTITUTION_RANK.Where(s => s.Id == rankId).FirstOrDefaultAsync();
            var deptName = await _context.INSTITUTION_DEPARTMENT.Where(s => s.Id == departmentId).FirstOrDefaultAsync();

            deptRankCount.RankCount = staffCount;
            deptRankCount.RankName = rankName.Name;
            deptRankCount.DepartmentName = deptName.Name;
                

            return deptRankCount;
        }

    }
}
