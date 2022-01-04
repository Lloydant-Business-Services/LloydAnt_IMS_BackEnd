using LiteHR.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Interface
{
    public interface IChangeOfNameService
    {
        Task<int> MakeChangeOfNameReuest(ChangeOfNameDto changeOfNameDto, string filePath, string directory);
        Task<int> ApproveChangeOfNameRequest(long staffId, bool requestAction);
        Task<IEnumerable<ChangeofNameListDto>> AdminGetChangeOfNameRequestList();
        Task<IEnumerable<ChangeofNameListDto>> GetRequestListByStaff(long staffId);
    }
}
