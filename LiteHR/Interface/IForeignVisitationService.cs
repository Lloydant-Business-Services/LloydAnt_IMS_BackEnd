using LiteHR.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Interface
{
    public interface IForeignVisitationService
    {
        Task<int> PostForeignVisitationForm(ForeignVisitationDto visitationDto);
        Task<IEnumerable<ForeignVisitationDto>> ForeignVistationApplications();
        Task<int> ApproveVisitation(long visitationId);
        Task<int> DeleteVisitaion(long visitationId);
    }
}
