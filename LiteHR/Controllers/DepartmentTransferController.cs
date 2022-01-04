using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteHR.Dtos;
using LiteHR.Interface;
using LiteHR.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LiteHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentTransferController : ControllerBase
    {
        private readonly HRContext _context;
        private readonly IDepartmentTransferService _service;

        public DepartmentTransferController(HRContext context, IDepartmentTransferService service)
        {
            _context = context;
            _service = service;
        }

        [HttpPost("MakeTransferRequest")]
        public async Task<int> MakeDepartmentTransferRequest(DepartmentTransferDto departmentTransferDto) => await _service.MakeDepartmentTransferRequest(departmentTransferDto);

        [HttpGet("GetByAdmin")]
        public async Task<IEnumerable<DepartmentTransfterListDto>> GetListByAdmin() => await _service.GetListByAdmin();
        [HttpGet("GetByStaff")]
        public async Task<IEnumerable<DepartmentTransfterListDto>> GetListByStaff(long staffId) => await _service.GetListByStaff(staffId);

        [HttpPost("ActOnRequest")]
        public async Task<int> ActOnTransferRequest(ActOnDeptTransferDto actOnDeptTransferDto) => await _service.ActOnTransferRequest(actOnDeptTransferDto);
    }
}
