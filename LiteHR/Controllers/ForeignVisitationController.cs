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
using Microsoft.Extensions.Hosting;

namespace LiteHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForeignVisitationController : ControllerBase
    {
        private readonly HRContext _context;
        private readonly IForeignVisitationService _service;
        private readonly IHostEnvironment _hostingEnvironment;

        public ForeignVisitationController(HRContext context, IForeignVisitationService service, IHostEnvironment hostingEnvironment)
        {
            _context = context;
            _service = service;
            _hostingEnvironment = hostingEnvironment;
        }


        [HttpPost("[action]")]
        public async Task<int> PostForeignVisitationForm(ForeignVisitationDto visitationDto) => await _service.PostForeignVisitationForm(visitationDto);

        [HttpPost("[action]")]
        public async Task<int> AddVistationType(string name, string description)
        {
            VisitationType visitationType = new VisitationType();
            visitationType.name = name;
            visitationType.Description = description;
            visitationType.Active = true;
            _context.Add(visitationType);
            await _context.SaveChangesAsync();
            return StatusCodes.Status200OK;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<VisitationType>>> GetVisitationType()
        {
            return await _context.VISITATION_TYPE.Where(a => a.Active).ToListAsync();
        }

        [HttpPut("[action]")]
        public async Task<int> EditVistationType(long id, string name, string description)
        {
            VisitationType visitationType = new VisitationType();
            visitationType = await _context.VISITATION_TYPE.Where(v => v.Id == id).FirstOrDefaultAsync();
            if (visitationType != null)
            {
                visitationType.Description = description;
                visitationType.Description = name;
                visitationType.Active = true;
            }

            _context.Update(visitationType);
            await _context.SaveChangesAsync();
            return StatusCodes.Status200OK;
        }
        [HttpDelete("[action]")]
        public async Task<int> EditVistationType(long id)
        {
            VisitationType visitationType = new VisitationType();
            visitationType = await _context.VISITATION_TYPE.Where(v => v.Id == id).FirstOrDefaultAsync();
            if (visitationType != null)
            {
                _context.Remove(visitationType);
                await _context.SaveChangesAsync();
            }
            return StatusCodes.Status200OK;
        }
        [HttpGet("[action]")]
        public async Task<IEnumerable<ForeignVisitationDto>> ForeignVistationApplications() => await _service.ForeignVistationApplications();

        [HttpPost("[action]")]
        public async Task<int> ApproveVisitation(long visitationId) => await _service.ApproveVisitation(visitationId);

        [HttpDelete("[action]")]
        public async Task<int> DeleteVisitaion(long visitationId) => await _service.DeleteVisitaion(visitationId);
    }
    
}
