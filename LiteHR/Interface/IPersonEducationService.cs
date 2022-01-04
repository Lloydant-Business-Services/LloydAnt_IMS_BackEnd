using LiteHR.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Interface
{
    public interface IPersonEducationService
    {
        Task<IEnumerable<GetPersonEducationDto>> GetPersonEducationByPersonId(long personId);
        Task<long> AddPersonEducation(long personId, GetPersonEducationDto dto);
        Task<int> UpdatePersonEducation(GetPersonEducationDto dto);
        Task<int> DeletePersonEducation(long personEducationId);
    }
}
