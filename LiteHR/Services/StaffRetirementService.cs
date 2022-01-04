using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteHR.Interface;
using LiteHR.Infrastructure;
using LiteHR.Models;
using Microsoft.EntityFrameworkCore;

namespace LiteHR.Services
{
    public class StaffRetirementService: IStaffRetirement
    {
        private readonly HRContext _context;

        public StaffRetirementService(HRContext context)
        {
            _context = context;
        }

        public async Task GetAcademicStaffDueForRetirement()
        {
            try
            {
                //Amount to New Year's day of the current year
                DateTime firstDayOfYear = new DateTime(DateTime.Now.Year, 1, 1);

                //Add Three days to the execution time. 
                //This allows for the chance that the HR department may go for New Year's Leave
                //and the Code May not be run within that period
                if (DateTime.Now >= firstDayOfYear && DateTime.Now <= firstDayOfYear.AddDays(3))
                {
                    //Get all the non-academic staff due for retirement
                    List<Staff> nonAcademicStaffToBeCheckedForRetirement = await _context.STAFF
                                                                              .Where(st => 
                                                                                    st.Active && 
                                                                                    st.Person.BirthDay != null && 
                                                                                    st.StaffTypeId == (long)StaffType.NonAcademicStaff &&
                                                                                    (DateTime.Now.Year - st.Person.BirthDay.Value.Year) >= 60 &&
                                                                                    (DateTime.Now.Year - st.Person.BirthDay.Value.Year) < 70)
                                                                              .Include(st => st.Person)
                                                                              .ToListAsync();

                    //Get all the academic staff due for retirement
                    List<Staff> academicStaffToBeCheckedForRetirement = await _context.STAFF
                                                                              .Where(st =>
                                                                                    st.Active &&
                                                                                    st.Person.BirthDay != null &&
                                                                                    (DateTime.Now.Year - st.Person.BirthDay.Value.Year) >= 70 &&
                                                                                    st.StaffTypeId == (long)StaffType.AcademicStaff)
                                                                              .Include(st => st.Person)
                                                                              .ToListAsync();

                    nonAcademicStaffToBeCheckedForRetirement.AddRange(academicStaffToBeCheckedForRetirement);
                    List<Staff> staffToBeCheckedForRetirement = nonAcademicStaffToBeCheckedForRetirement;

                    if (staffToBeCheckedForRetirement?.Count() > 0)
                    {
                        foreach (Staff staff in staffToBeCheckedForRetirement)
                        {
                            StaffRetirement staffRetirementExists = await _context.STAFF_RETIREMENT
                                                                                  .Where(st => st.StaffId == staff.Id)
                                                                                  .FirstOrDefaultAsync();
                            if (staffRetirementExists == null)
                            {
                                //Construct the date on which the staff is to retire
                                DateTime dateToRetire = new DateTime(DateTime.Now.Year, staff.Person.BirthDay.Value.Month, staff.Person.BirthDay.Value.Day);

                                StaffRetirement staffRetirement = new StaffRetirement()
                                {
                                    IsRetired = false,
                                    StaffId = staff.Id,
                                    DateOfRetirement = dateToRetire
                                };

                                _context.Add(staffRetirement);
                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                }
            }
            catch(Exception ex) { throw ex; }
        }

        public async Task<StaffRetirement> RetireStaffDueRetirement(long staffId)
        {
            try 
            {
                StaffRetirement staffRetirementRecord = null;
                Staff staffRecord = await _context.STAFF.Where(st => st.Id == staffId).FirstOrDefaultAsync();

                if (staffRecord != null)
                {
                    staffRetirementRecord = await _context.STAFF_RETIREMENT
                                                                      .Where(st => st.StaffId == staffId)
                                                                      .Include(st => st.Staff)
                                                                      .ThenInclude(st => st.Person)
                                                                      .FirstOrDefaultAsync();

                    if (staffRetirementRecord != null && DateTime.Now >= staffRetirementRecord.DateOfRetirement)
                    {
                        //Update records for the staff in the staffRetirement table
                        staffRetirementRecord.IsRetired = true;
                        _context.Update(staffRetirementRecord);
                        await _context.SaveChangesAsync();

                        //Update records for the staff in Staff table
                        staffRecord.Active = false;
                        staffRecord.RetirementReason = "RETIREMENT";
                        _context.Update(staffRecord);
                        await _context.SaveChangesAsync();
                    }
                }

                return staffRetirementRecord; 
            }
            catch(Exception ex) { throw ex; }
        }

        public async Task<bool> StaffRetirementOneYearCheck(long staffId)
        {
            try
            {
                bool staffPassedOneYearCheck = false;
                StaffRetirement staffRetirementExists = await _context.STAFF_RETIREMENT.Where(st => st.StaffId == staffId).FirstOrDefaultAsync();
                if (staffRetirementExists != null)
                {
                    if(DateTime.Now > staffRetirementExists.DateOfRetirement && 
                        staffRetirementExists.DateOfRetirement.Year == DateTime.Now.Year && 
                        (DateTime.Now.Month - staffRetirementExists.DateOfRetirement.Month > 3 && DateTime.Now.Month - staffRetirementExists.DateOfRetirement.Month <= 12))
                    {
                        staffPassedOneYearCheck = true;
                    }
                }

                return staffPassedOneYearCheck;
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<bool> StaffRetirementThreeMonthCheck(long staffId)
        {
            try
            {
                bool staffPassedOneYearCheck = false;
                StaffRetirement staffRetirementExists = await _context.STAFF_RETIREMENT.Where(st => st.StaffId == staffId).FirstOrDefaultAsync();
                if (staffRetirementExists != null)
                {
                    if (DateTime.Now > staffRetirementExists.DateOfRetirement &&
                        staffRetirementExists.DateOfRetirement.Year == DateTime.Now.Year &&
                        DateTime.Now.Month - staffRetirementExists.DateOfRetirement.Month <= 3)
                    {
                        staffPassedOneYearCheck = true;
                    }
                }

                return staffPassedOneYearCheck;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
