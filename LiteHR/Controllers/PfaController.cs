using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteHR.Dtos;
using LiteHR.Interface;
using LiteHR.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LiteHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PfaController : ControllerBase
    {
        private readonly HRContext _context;
        private readonly IPfaService _service;

        public PfaController(HRContext context, IPfaService service)
        {
            _context = context;
            _service = service;
        }

        [HttpPost("AddPFA")]
        public async Task<long> AddPFA(CommonDto postDto) => await _service.AddPFA(postDto);
        [HttpGet("GetPFA")]
        public async Task<ActionResult<IEnumerable<PfaName>>> GetPFA()
        {
            return await _context.PFA_NAME.Where(p => p.Active).ToListAsync();
        }
        [HttpPost("AddPFAStatus")]
        public async Task<long> AddPFAStatus(CommonDto postDto) => await _service.AddPFAStatus(postDto);
        [HttpPost("AddAreaOfSpecialization")]
        public async Task<long> AddAreaofSpecialization(CommonDto postDto) => await _service.AddAreaofSpecialization(postDto);
        [HttpGet("GetPFAStatus")]
        public async Task<ActionResult<IEnumerable<PfaStatus>>> GetPFAStatus()
        {
            return await _context.PFA_STATUS.Where(p => p.Active).ToListAsync();
        }
        [HttpGet("GetAreaOfSpecialization")]
        public async Task<ActionResult<IEnumerable<AreaOfSpecialization>>> GetAOS()
        {
            return await _context.AREA_OF_SPECIALIZATION.Where(p => p.Active).ToListAsync();
        }
        [HttpPut("[action]")]
        public async Task<long> EditPFAName(long id, CommonDto editDto) => await _service.EditPFAName(id, editDto);
        [HttpPut("[action]")]
        public async Task<long> EditPFAStatus(long id, CommonDto editDto) => await _service.EditPFAStatus(id, editDto);
        [HttpPut("[action]")]
        public async Task<long> EditAreaOfSpecialization(long id, CommonDto editDto) => await _service.EditAreaOfSpecialization(id, editDto);
        [HttpDelete("[action]")]
        public async Task<long> DeletePFAName(long id) => await _service.DeletePFAName(id);

        [HttpDelete("[action]")]
        public async Task<long> DeletePFAStatus(long id) => await _service.DeletePFAStatus(id);

        [HttpDelete("[action]")]
        public async Task<long> DeleteAreaOfSpecialization(long id) => await _service.DeleteAreaOfSpecialization(id);
    }
}
