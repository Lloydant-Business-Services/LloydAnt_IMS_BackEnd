using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using LiteHR.Infrastructure;
using LiteHR.Interface;
using LiteHR.Models;
using Microsoft.EntityFrameworkCore;
using LiteHR.Dtos;
using Microsoft.AspNetCore.Http;

namespace LiteHR.Services
{
    public class SalaryGradingSystemService : ISalaryGradingSystem
    {
        private readonly HRContext _context;


        public SalaryGradingSystemService(HRContext context)
        {
            _context = context;
        }
        public async Task<int> AddSalaryGrade(SalaryGradeDto gradeDto)
        {
            SalaryGrade salaryGrade = new SalaryGrade();
            salaryGrade.SalaryGradeCategoryId = gradeDto.SalaryGradeCategoryId;
            salaryGrade.SalaryStepId = gradeDto.SalaryStepId;
            salaryGrade.SalaryLevelId = gradeDto.SalaryLevelId;
            salaryGrade.Amount = gradeDto.Amount;
            salaryGrade.Active = true;

            _context.Add(salaryGrade);
            await _context.SaveChangesAsync();
            return StatusCodes.Status200OK;

        }

        public async Task<int> UpdateSalaryGrade(long gradeId, SalaryGradeDto gradeDto)
        {
            SalaryGrade salaryGrade = await _context.SALARY_GRADE.Where(s => s.Id == gradeId).FirstOrDefaultAsync();
            if(salaryGrade != null && salaryGrade.Id > 0)
            {
                salaryGrade.SalaryGradeCategoryId = gradeDto.SalaryGradeCategoryId;
                salaryGrade.SalaryLevelId = gradeDto.SalaryLevelId;
                salaryGrade.SalaryStepId = gradeDto.SalaryStepId;
                salaryGrade.Amount = gradeDto.Amount;

                _context.Update(salaryGrade);
                await _context.SaveChangesAsync();
                return StatusCodes.Status200OK;
            }
            return StatusCodes.Status500InternalServerError;
        }

        public async Task<IEnumerable<SalaryGradeDto>> GetSalaryGrade()
        {
            return await _context.SALARY_GRADE
                .Where(x => x.Active)
                .Include(g => g.SalaryGradeCategory)
                .Include(g => g.SalaryLevel)
                .Include(g => g.SalaryStep)
                .Select(f => new SalaryGradeDto
                {
                    Id = f.Id,
                    SalaryLevelName = f.SalaryLevel.Name,
                    SalaryStepName = f.SalaryStep.Name,
                    SalaryGradeCategoryName = f.SalaryGradeCategory.Name,
                    SalaryLevelId = f.SalaryLevelId,
                    SalaryStepId = f.SalaryStepId,
                    SalaryGradeCategoryId = f.SalaryGradeCategoryId,
                    Amount = f.Amount/12,
                    Active = f.Active                  

                }).ToListAsync();

        }

        public async Task<List<GradeBenefit>> AddGradeBenefit(List<long> salaryTypes, long salaryStepId, long salaryLevelId, long salaryGradeCategoryId)
        {
            List<GradeBenefit> gradeBenefitList = new List<GradeBenefit>();
            try
            {
                SalaryStep salaryStep = await GetSalaryStepById(salaryStepId);
                SalaryLevel salaryLevel = await GetSalaryLevelById(salaryLevelId);
                SalaryGradeCategory salaryGradeCategory = await GetSalaryGradeCategoryId(salaryGradeCategoryId);

                if (salaryStep != null && salaryLevel != null && salaryGradeCategory != null)
                {
                    SalaryGrade salaryGradeExists = await GetSalaryGradeByLevelStepAndCategory(salaryStepId, salaryLevelId, salaryGradeCategoryId);

                    if (salaryGradeExists == null)
                    {
                        List<GradeBenefit> gradeBenefits = new List<GradeBenefit>();

                        //Create a record for SalaryGrade
                        SalaryGrade salaryGrade = new SalaryGrade()
                        {
                            SalaryGradeCategoryId = salaryGradeCategoryId,
                            SalaryLevelId = salaryLevelId,
                            SalaryStepId = salaryStepId,
                            Active = true

                        };

                        _context.Add(salaryGrade);

                        await _context.SaveChangesAsync();

                        for (int i = 0; i < salaryTypes.Count(); i++)
                        {
                            //Save the newly created GradeBenefit
                            GradeBenefit gradeBenefit = new GradeBenefit()
                            {
                                SalaryGradeId = salaryGrade.Id,
                                SalaryTypeId = salaryTypes[i],
                                Active = true
                            };

                            _context.Add(gradeBenefit);

                            await _context.SaveChangesAsync();

                        }

                        return await GetGradeBenfitsBySalaryGradeId(salaryGrade.Id);
                    }
                }

                return gradeBenefitList;
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<List<GradeBenefit>> GetGradeBenfitsBySalaryGradeId(long salaryGradeId)
        {
            try
            {
                return await _context.GRADE_BENEFIT
                    .Include(m => m.SalaryGrade)
                    .ThenInclude(m => m.SalaryGradeCategory)
                    .Include(m => m.SalaryGrade.SalaryLevel)
                    .Include(m => m.SalaryGrade.SalaryStep)
                    .Include(m => m.SalaryType)

                    .Where(gb => gb.SalaryGradeId == salaryGradeId).ToListAsync();
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<GradeBenefit> GetGradeBenefitById(long id)
        {
            try
            {
                return await _context.GRADE_BENEFIT.Where(gb => gb.Id == id).SingleOrDefaultAsync();
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<SalaryType> GetSalaryTypeById(long id)
        {
            try
            {
                return await _context.SALARY_TYPE.Where(st => st.Id == id).SingleOrDefaultAsync();
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<SalaryLevel> GetSalaryLevelById(long id)
        {
            try
            {
                return await _context.SALARY_LEVEL.Where(sl => sl.Id == id).SingleOrDefaultAsync();
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<SalaryGradeCategory> GetSalaryGradeCategoryId(long id)
        {
            try
            {
                return await _context.SALARY_GRADE_CATEGORY.Where(sgc => sgc.Id == id).SingleOrDefaultAsync();
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<SalaryStep> GetSalaryStepById(long id)
        {
            try
            {
                return await _context.SALARY_STEP.Where(ss => ss.Id == id).SingleOrDefaultAsync();
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<SalaryGrade> GetSalaryGradeByLevelStepAndCategory(long salaryStepId, long salaryLevelId, long salaryGradeCategoryId)
        {
            try
            {
                return await _context.SALARY_GRADE
                                     .Where(ss => ss.SalaryGradeCategoryId == salaryStepId && ss.SalaryLevelId == salaryLevelId && ss.SalaryGradeCategoryId == salaryGradeCategoryId && ss.Active)
                                     .SingleOrDefaultAsync();
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<List<SalaryGradeModel>> GetGradeBenefitsBySalaryGradeCategoryId(long salaryGradeCategoryId)
        {
            List<SalaryGradeModel> salaryGradeModelList = new List<SalaryGradeModel>();
            try
            {
                var salaryGrades = await _context.SALARY_GRADE.Where(f => f.SalaryGradeCategoryId == salaryGradeCategoryId)
                    .Include(f => f.SalaryGradeCategory)
                    .Include(f => f.SalaryLevel)
                    .Include(f => f.SalaryStep)
                    .ToListAsync();
                if (salaryGrades?.Count > 0)
                {
                    foreach (var item in salaryGrades)
                    {
                        SalaryGradeModel salaryGradeModel = new SalaryGradeModel();
                        salaryGradeModel.SalaryGrade = item;
                        List<GradeBenefit> gradeBenefits = await _context.GRADE_BENEFIT.Where(f => f.SalaryGradeId == item.Id)
                                                                                    .Include(gb => gb.SalaryType)
                                                                                   .ToListAsync();
                        salaryGradeModel.GradeBenefit.AddRange(gradeBenefits);
                        salaryGradeModelList.Add(salaryGradeModel);
                    }
                }




                return salaryGradeModelList;
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<List<GradeBenefit>> GetGradeBenefitsByStaffId(long staffId)
        {
            try
            {
                List<GradeBenefit> gradeBenefitList = new List<GradeBenefit>();
                var staffGrade = await _context.STAFF_GRADE.Where(sg => sg.StaffId == staffId)
                                                            .Select(sg => new { sg.SalaryGradeId })
                                                            .FirstOrDefaultAsync();

                if (staffGrade != null)
                {
                    //Use the SalaryGrade Id to query for GradeCategories
                    List<GradeBenefit> gradeBenefits = await _context.GRADE_BENEFIT.Where(gb => gb.SalaryGradeId == staffGrade.SalaryGradeId)
                                                                                   .Include(gb => gb.SalaryType)
                                                                                   .Include(gb => gb.SalaryGrade)
                                                                                   .ThenInclude(gb => gb.SalaryLevel)
                                                                                   .Include(gb => gb.SalaryGrade.SalaryStep)
                                                                                   .ToListAsync();

                    if (gradeBenefits.Count() > 0)
                    {
                        gradeBenefitList = gradeBenefits;
                    }
                }

                return gradeBenefitList;
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<StaffSalaryDto> GetStaffPayCheque(long staffId)
        {
            try
            {
                SalaryGrade salaryGrade = new SalaryGrade();
                StaffSalaryDto salaryDto = new StaffSalaryDto();
                StaffSalaryCategoryReference salaryReference = await _context.STAFF_SALARY_CATEGORY_REFERENCE.Where(s => s.StaffId == staffId)
                    .Include(s => s.SalaryGradeCategory)
                    .Include(s => s.SalaryLevel)
                    .Include(s => s.SalaryStep)
                    .FirstOrDefaultAsync();
                salaryGrade = await _context.SALARY_GRADE.Where(g => g.SalaryGradeCategoryId == salaryReference.SalaryGradeCategoryId && g.SalaryStepId == salaryReference.SalaryStepId && g.SalaryLevelId == salaryReference.SalaryLevelId).FirstOrDefaultAsync();
                if(salaryGrade != null)
                {
                    var res_amt = Math.Abs(Convert.ToInt32(salaryGrade.Amount)) / 12;
                    salaryDto.Amount = res_amt;
                    salaryDto.AmountInWords = NumberToWords(res_amt);
                    salaryDto.StaffSalaryDetails = salaryReference.SalaryGradeCategory.Name + " " + salaryReference.SalaryLevel.Name + " " + salaryReference.SalaryStep.Name;
                }
                return salaryDto;
            }
            
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task<StaffGradeBenefitDto> GetStaffCurrentMonthPayCheckId(long staffId, int month, int year)
        {
            try
            {

                StaffGradeBenefitDto staffGradeBenefitDto = new StaffGradeBenefitDto();
                var staffGrade = await _context.STAFF_GRADE.Where(sg => sg.StaffId == staffId && sg.DatePromoted.Month==month && sg.DatePromoted.Year==year && sg.Active)
                                                            .Include(sf=>sf.Staff)
                                                            .FirstOrDefaultAsync();

                if (staffGrade != null)
                {
                    //Use the SalaryGrade Id to query for GradeCategories
                    List<GradeBenefit> gradeBenefits = await _context.GRADE_BENEFIT.Where(gb => gb.SalaryGradeId == staffGrade.SalaryGradeId)
                                                                                   .Include(gb => gb.SalaryType)
                                                                                   .Include(gb => gb.SalaryGrade)
                                                                                   .ThenInclude(gb => gb.SalaryLevel)   
                                                                                   .Include(gb => gb.SalaryGrade.SalaryStep)
                                                                                   
                                                                                   .ToListAsync();
                    var staffExtraEarning=await _context.SALARY_EXTRA_EARNING.Where(s => s.StaffId == staffId && s.DateCreated.Month == month && s.DateCreated.Year == year)
                        .Include(sy=>sy.SalaryExtraType).ToListAsync();
                    if (gradeBenefits.Count() > 0)
                    {

                        staffGradeBenefitDto.GradeBenefits = gradeBenefits;
                        staffGradeBenefitDto.SalaryExtraEarnings = staffExtraEarning;
                        staffGradeBenefitDto.Staff = staffGrade.Staff;
                    }
                }
                
                return staffGradeBenefitDto;
            }
            catch (Exception ex) { throw ex; }
        }
        
        public async Task<List<GradeBenefit>> SetStaffGrade(long staffId, long salaryGradeId)
        {
            List<GradeBenefit> gradeBenefits = new List<GradeBenefit>();
            try
            {

                var staffExist=await _context.STAFF.Where(f => f.Id == staffId).FirstOrDefaultAsync();
                if (staffExist?.Id > 0)
                {
                    var existingStaffGrade = _context.STAFF_GRADE.Where(m => m.Active && m.SalaryGradeId == salaryGradeId && m.StaffId == staffId).ToList();
                    if (existingStaffGrade.Count == 0)
                    {
                        StaffGrade staffGrade = new StaffGrade()
                        {
                            Active = true,
                            DatePromoted = DateTime.Now,
                            SalaryGradeId = salaryGradeId,
                            StaffId = staffId
                        };
                        _context.STAFF_GRADE.Add(staffGrade);
                        await _context.SaveChangesAsync();
                    }
                    gradeBenefits= await GetGradeBenefitsByStaffId(staffId);
                }
               

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return gradeBenefits;
        }

        public async Task<StaffSalaryReportDto> GetStaffMonthlySalary(int month, int year)
        {
            try
            {
              
                StaffSalaryCategoryReference categoryReference = new StaffSalaryCategoryReference();
                List<SalaryLoad> reportDtoList = new List<SalaryLoad>();
                StaffSalaryReportDto returnModel = new StaffSalaryReportDto();
                long total_payable_sum = 0;

                if (DateTime.Now.Month == month && DateTime.Now.Year == year)
                {
                    var salaryList = await _context.STAFF_SALARY_CATEGORY_REFERENCE.Where(s => s.StaffId > 0)
                    .Include(s => s.Staff)
                    .ThenInclude(p => p.Department)
                    .Include(s => s.Staff)
                    .ThenInclude(p => p.Person)
                    .ToListAsync();
                    if (salaryList.Count > 0)
                    {
                        foreach (var item in salaryList)
                        {
                            SalaryLoad reportDto = new SalaryLoad();
                            SalaryGrade salaryGrade = await _context.SALARY_GRADE.Where(g => g.SalaryGradeCategoryId == item.SalaryGradeCategoryId && g.SalaryLevelId == item.SalaryLevelId && g.SalaryStepId == item.SalaryStepId)
                                .Include(g => g.SalaryGradeCategory)
                                .Include(l => l.SalaryLevel)
                                .Include(s => s.SalaryStep)
                                .FirstOrDefaultAsync();
                            if (salaryGrade != null && salaryGrade.Id > 0)
                            {

                                var resolved_amount = salaryGrade.Amount > 0 ? Math.Abs(Convert.ToInt32(salaryGrade.Amount)) / 12 : 0;
                                reportDto.StaffName = item.Staff.Person.Surname + " " + item.Staff.Person.Firstname + " " + item.Staff.Person.Othername;
                                reportDto.Department = item.Staff.Department != null ? item.Staff.Department.Name : "-";
                                reportDto.Amount = resolved_amount;
                                //var am_words = salaryGrade.Amount > 0 ? NumberToWords(Convert.ToInt32(salaryGrade.Amount)/12) : "-";
                                //reportDto.AmountInWords = am_words;
                                reportDto.SalaryGrade = item.SalaryGradeCategory.Name + " " + item.SalaryLevel.Name + " " + item.SalaryStep.Name;
                                total_payable_sum += resolved_amount;
                                reportDtoList.Add(reportDto);

                            }
                        }
                        returnModel.SalaryReport = reportDtoList;
                        returnModel.Total = total_payable_sum;

                    }
                }
                
                return returnModel;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " Million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " Thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "And ";

                var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
                var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }
        //public async Task<StaffGrade> CheckForExistingStaffGrade(long staffId, long salaryGradeId)
        //{
        //    try
        //    {
        //        var existingStaffGrade = _context.STAFF_GRADE.Where(m => m.Active && m.SalaryGradeId == salaryGradeId && m.StaffId == staffId).ToList();
        //        if (existingStaffGrade?.Count > 0)
        //        {

        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
    }
}
