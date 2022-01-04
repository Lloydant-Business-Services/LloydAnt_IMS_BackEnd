using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteHR.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LiteHR.Interface;
using LiteHR.Dtos;

namespace LiteHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstituitionMemorandumTargetController : ControllerBase
    {
        private readonly HRContext _context;
        private readonly IInstituitionMemorandumTargetService _service;

        public InstituitionMemorandumTargetController(HRContext context, IInstituitionMemorandumTargetService service)
        {
            _context = context;
            _service = service;
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<InstituitionMemorandumTargetListDto>> GetMemorandumByRoleId(int userId) => await _service.GetMemorandumByRoleId(userId);

    }
}
