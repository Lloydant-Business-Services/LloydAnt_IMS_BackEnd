using LiteHR.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Interface
{
    public interface IPfaService
    {
        Task<long> AddPFA(CommonDto postDto);
        Task<long> AddPFAStatus(CommonDto postDto);
        Task<long> AddAreaofSpecialization(CommonDto postDto);
        Task<long> EditPFAName(long id, CommonDto editDto);
        Task<long> EditPFAStatus(long id, CommonDto editDto);
        Task<long> EditAreaOfSpecialization(long id, CommonDto editDto);
        Task<long> DeletePFAName(long id);
        Task<long> DeletePFAStatus(long id);
        Task<long> DeleteAreaOfSpecialization(long id);
    }
}
