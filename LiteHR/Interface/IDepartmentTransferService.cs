using LiteHR.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Interface
{
    public interface IDepartmentTransferService
    {
        Task<int> MakeDepartmentTransferRequest(DepartmentTransferDto departmentTransferDto);
        Task<IEnumerable<DepartmentTransfterListDto>> GetListByAdmin();
        Task<IEnumerable<DepartmentTransfterListDto>> GetListByStaff(long staffId);
        Task<int> ActOnTransferRequest(ActOnDeptTransferDto actOnDeptTransferDto);
    }
}
