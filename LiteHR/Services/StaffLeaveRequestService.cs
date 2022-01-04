using LiteHR.Dtos;
using LiteHR.Interface;
using LiteHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Services
{
    public class StaffLeaveRequestService: IStaffLeaveRequestService
    {
        public readonly HRContext _context;
        public StaffLeaveRequestService(HRContext context)
        {
            _context = context;
        }

        public Task<long> AddStaffLeaveRequest(AddStaffLeaveRequestDto addStaffLeaveRequestDto)
        {
            throw new NotImplementedException();
        }
    }
}
