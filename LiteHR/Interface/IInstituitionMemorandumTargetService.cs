using LiteHR.Dtos;
using LiteHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Interface
{
    public interface IInstituitionMemorandumTargetService
    {
        Task<IEnumerable<InstituitionMemorandumTargetListDto>> GetMemorandumByRoleId(int userId);
    }
}
