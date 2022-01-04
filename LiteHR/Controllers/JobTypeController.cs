using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteHR.Dtos;
using LiteHR.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LiteHR.Controllers
{

    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JobTypeController : ControllerBase
    {
        private readonly HRContext _context;

        public JobTypeController(HRContext context)
        {
            _context = context;
        }

        // GET: api/JobType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobType>>> GetJOBTYPE()
        {
            return await _context.JOB_TYPE.ToListAsync();
        }

        // GET: api/JobType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobType>> GetJobType(long id)
        {
            var jobType = await _context.JOB_TYPE.FindAsync(id);

            if (jobType == null)
            {
                return NotFound();
            }

            return jobType;
        }

        // PUT: api/JobVacancy/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<int> PutJobType(long id, JobTypeDto jobTypeDto)
        {
            var getJob = await _context.JOB_TYPE.Where(j => j.Id == id).FirstOrDefaultAsync();
            if (getJob != null)
            {
                getJob.Name = jobTypeDto.Name;
                _context.Update(getJob);
                await _context.SaveChangesAsync();
                return StatusCodes.Status200OK;
            }
            else
            {
                return StatusCodes.Status400BadRequest;
            }
        }

        // POST: api/JobTypes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<int> PostJobType(JobTypeDto jobTypeDto)
        {
            JobType jobType = new JobType();
         
                jobType.Name = jobTypeDto.Name;
                
                _context.Update(jobType);
                await _context.SaveChangesAsync();
                return StatusCodes.Status200OK;
            
          
        }

        // DELETE: api/JobType/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<JobType>> DeleteJobType(long id)
        {
            var jobType = await _context.JOB_TYPE.FindAsync(id);
            if (jobType == null)
            {
                return NotFound();
            }

            _context.JOB_TYPE.Remove(jobType);
            await _context.SaveChangesAsync();

            return jobType;
        }

        private bool JobTypeExists(long id)
        {
            return _context.JOB_TYPE.Any(e => e.Id == id);
        }
    }
}