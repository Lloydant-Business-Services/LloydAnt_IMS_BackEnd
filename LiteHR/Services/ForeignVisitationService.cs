using LiteHR.Dtos;
using LiteHR.Infrastructure;
using LiteHR.Interface;
using LiteHR.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Services
{
    public class ForeignVisitationService : IForeignVisitationService
    {
        private readonly HRContext _context;
        private readonly IFileUpload _fileupload;

        private readonly string baseUrl;
        private readonly IConfiguration _configuration;
        public int _saved { get; set; } = 1;

        public ForeignVisitationService(HRContext context, IFileUpload fileupload, IConfiguration configuration)
        {
            _context = context;
            _fileupload = fileupload;
            _configuration = configuration;
            baseUrl = _configuration.GetValue<string>("Url:root");
        }

        public async Task<int> PostForeignVisitationForm(ForeignVisitationDto visitationDto)
        {
            long personId = 0;
            var isSaved = 0;
            try
            {
                ForeignVisitation foreignVisitation = new ForeignVisitation();
                Person person = new Person();
                PersonNextOfKin nextOfKin = new PersonNextOfKin();
                if (visitationDto.VisitationTypeId > 0)
                {
                  
                        person.Surname = visitationDto.Surname;
                        person.Firstname = visitationDto.Firstname;
                        person.Othername = visitationDto.Othername;
                        person.Email = visitationDto.Email;
                        person.ImageUrl = visitationDto.ImageUrl;
                        person.Address = visitationDto.Address;
                        person.BirthDay = visitationDto.BirthDay;
                        person.PhoneNumber = visitationDto.PhoneNumber;
                        _context.Add(person);
                        isSaved = await _context.SaveChangesAsync();
                    
                    if (isSaved == _saved)
                    {
                        personId = person.Id;
                        isSaved = 0;
                        foreignVisitation.PersonId = person.Id;
                        foreignVisitation.AddressSecond = visitationDto.AddressSecond;
                        foreignVisitation.PassportNumber = visitationDto.PassportNumber;
                        foreignVisitation.ProgrammeType = visitationDto.ProgrammeType;
                        foreignVisitation.AcademicYear = visitationDto.AcademicYear;
                        foreignVisitation.InstitutionDepartmentId = visitationDto.InstitutionDepartmentId;
                        foreignVisitation.DurationOfStay = visitationDto.DurationOfStay;
                        foreignVisitation.DegreeAwardDate = visitationDto.DegreeAwardDate;
                        foreignVisitation.AwardingInstitution = visitationDto.AwardingInstitution;
                        foreignVisitation.CurrentInstitutionDepartmentId = visitationDto.CurrentInstitutionDepartmentId;
                        foreignVisitation.CurrentYearOfStudy = visitationDto.CurrentYearOfStudy;
                        foreignVisitation.CurrentExpectedQualificationYear = visitationDto.CurrentExpectedQualificationYear;
                        foreignVisitation.Reason = visitationDto.Reason;
                        foreignVisitation.SponsorshipType = visitationDto.SponsorshipType;
                        foreignVisitation.SponsorshipOrganization = visitationDto.SponsorshipOrganization;
                        foreignVisitation.ApplicationNumber = "NULL";
                        foreignVisitation.VisitationTypeId = visitationDto.VisitationTypeId;
                        foreignVisitation.Active = true;
                        //foreignVisitation.isApproved = false;

                        _context.Add(foreignVisitation);
                        isSaved = await _context.SaveChangesAsync();

                       if(isSaved == _saved)
                        {
                            nextOfKin.Fullname = visitationDto.NextOfKinFullname;
                            nextOfKin.Email = visitationDto.NextOfKinEmail;
                            nextOfKin.Phone = visitationDto.NextOfKinPhone;
                            nextOfKin.Address = visitationDto.NextOfKinAddress;
                            nextOfKin.PersonId = person.Id;

                            _context.Add(nextOfKin);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            var getPerson = await _context.PERSON.Where(x => x.Id == person.Id).FirstOrDefaultAsync();
                            _context.Remove(getPerson);
                            return StatusCodes.Status500InternalServerError;
                        }
                    }

              
                    
                    return StatusCodes.Status200OK;
                }
                return StatusCodes.Status500InternalServerError;
            }
            catch(Exception ex)
            {
                var getPerson = await _context.PERSON.Where(x => x.Id == personId).FirstOrDefaultAsync();
                _context.Remove(getPerson);
                await _context.SaveChangesAsync();
                throw ex;
            }
        }
        public async Task<IEnumerable<ForeignVisitationDto>> ForeignVistationApplications()
        {
            return await _context.FOREIGN_VISITATION
                .Where(a => a.Active == true)
                .Include(p => p.Person)
                .Include(v => v.VisitationType)
                .Include(d => d.CurrentInstitutionDepartment)
                .Include(d => d.InstitutionDepartment)
                .Select(f => new ForeignVisitationDto
                {
                    FullName = f.Person.Surname + " " + f.Person.Firstname + " " + f.Person.Othername,
                    Email = f.Person.Email,
                    DurationOfStay = f.DurationOfStay,
                    ProgrammeType = f.ProgrammeType,
                    PhoneNumber = f.Person.PhoneNumber,
                    isApproved = f.isApproved,
                    Address = f.Person.Address,
                    AddressSecond = f.AddressSecond,
                    Reason = f.Reason,
                    SponsorshipType = f.SponsorshipType,
                    SponsorshipOrganization = f.SponsorshipOrganization,
                    VisitationType = f.VisitationType.name,
                    DegreeAwardDate = f.DegreeAwardDate,
                    AwardingInstitution = f.AwardingInstitution,
                    AcademicYear = f.AcademicYear,
                    PassportNumber = f.PassportNumber,
                    CurrentExpectedQualificationYear = f.CurrentExpectedQualificationYear,
                    ImageUrl = f.Person.ImageUrl,
                    InstitutionDepartment = f.InstitutionDepartment.Name,
                    CurrentInstitutionDepartment = f.CurrentInstitutionDepartment.Name,
                    BirthDay = f.Person.BirthDay,
                    NextOfKinAddress = "-",
                    NextOfKinFullname = "-",
                    NextOfKinPhone = "-",
                    Id = f.Id


                }).ToListAsync();

        }

        public async Task<int> ApproveVisitation(long visitationId)
        {
            try
            {
                ForeignVisitation foreignVisitation = new ForeignVisitation();
                foreignVisitation = _context.FOREIGN_VISITATION.Where(v => v.Id == visitationId).FirstOrDefault();
                if(foreignVisitation != null)
                {
                    foreignVisitation.isApproved = true;
                    _context.Update(foreignVisitation);
                    await _context.SaveChangesAsync();
                    return StatusCodes.Status200OK;
                }
                return StatusCodes.Status500InternalServerError;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> DeleteVisitaion(long visitationId)
        {
            try
            {
                ForeignVisitation foreignVisitation = new ForeignVisitation();
                foreignVisitation = _context.FOREIGN_VISITATION.Where(v => v.Id == visitationId).FirstOrDefault();
                if (foreignVisitation != null)
                {
                    foreignVisitation.Active = false;
                    _context.Update(foreignVisitation);
                    await _context.SaveChangesAsync();
                    return StatusCodes.Status200OK;
                }
                return StatusCodes.Status500InternalServerError;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
