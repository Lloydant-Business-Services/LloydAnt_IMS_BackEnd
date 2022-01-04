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
    public class PersonEducationService :IPersonEducationService
    {
        private readonly HRContext _context;
        private readonly IFileUpload _fileUpload;
        private readonly IConfiguration _configuration;
        private readonly string baseUrl;
        public PersonEducationService(HRContext context, IFileUpload fileUpload, IConfiguration configuration)
        {
            _context = context;
            _fileUpload = fileUpload;
            _configuration = configuration;
            baseUrl = _configuration.GetValue<string>("Url:root");
        }

        public async Task<IEnumerable<GetPersonEducationDto>> GetPersonEducationByPersonId(long personId)
        {
            return await _context.PERSON_EDUCATION.Where(p => p.PersonId == personId).Include(q => q.EducationalQualification)
                .Select(f => new GetPersonEducationDto { 
                    QualificationName = f.EducationalQualification.Name,
                    QualificationId = f.EducationalQualificationId,
                    Institution = f.Institution,
                    YearObtained = f.Year,
                    Id = f.Id
                })
                .ToListAsync();
        }

        public async Task<long> AddPersonEducation(long personId, GetPersonEducationDto dto)
        {
            try
            {
                Person person = await _context.PERSON.Where(p => p.Id == personId).FirstOrDefaultAsync();
                if (person == null)
                    throw new NullReferenceException("User not found");
                PersonEducation personEducation = new PersonEducation()
                {
                    PersonId = personId,
                    EducationalQualificationId = dto.QualificationId,
                    Year = dto.YearObtained,
                    Institution = dto.Institution
                };
                _context.Add(personEducation);
                await _context.SaveChangesAsync();
                return StatusCodes.Status200OK;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task<int> UpdatePersonEducation(GetPersonEducationDto dto)
        {
            try
            {
                PersonEducation personEducation = await _context.PERSON_EDUCATION.Where(f => f.Id == dto.Id).FirstOrDefaultAsync();
                if (personEducation == null)
                    throw new NullReferenceException("record not found");
                personEducation.EducationalQualificationId = dto.QualificationId;
                personEducation.Year = dto.YearObtained;
                personEducation.Institution = dto.Institution;
                _context.Update(personEducation);
                await _context.SaveChangesAsync();
                return StatusCodes.Status200OK;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task<int> DeletePersonEducation(long personEducationId)
        {
            try
            {
                PersonEducation personEducation = await _context.PERSON_EDUCATION.Where(f => f.Id == personEducationId).FirstOrDefaultAsync();
                if (personEducation == null)
                    throw new NullReferenceException("record not found");        
                _context.Remove(personEducation);
                await _context.SaveChangesAsync();
                return StatusCodes.Status200OK;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
