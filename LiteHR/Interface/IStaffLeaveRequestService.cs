using LiteHR.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Interface
{
    public interface IStaffLeaveRequestService
    {
        Task<long> AddStaffLeaveRequest(AddStaffLeaveRequestDto addStaffLeaveRequestDto);
    }
}
