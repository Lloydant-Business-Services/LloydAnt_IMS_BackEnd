using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteHR.Dtos;
using LiteHR.Interface;
using LiteHR.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LiteHR.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingRequestController : Controller
    {
        public readonly ITrainingRequestService _service;
        public TrainingRequestController(ITrainingRequestService service)
        {
            _service = service;
        }

        // GET: api/TrainingRequest
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StaffTrainingRequest>>> GetTrainingRequest()
        {
            return await _service.GetTrainingRequests();
        }

        // GET: api/TrainingRequest/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StaffTrainingRequest>> GetTrainingRequestById(long id)
        {
            return await _service.GetTrainingRequestById(id);
        }

        // GET: api/TrainingRequest/5
        [HttpGet("TrainingRequestByStaff/{id}")]
        public async Task<ActionResult<List<StaffTrainingRequest>>> GetTrainingRequestByStaffId(long id)
        {
            return await _service.GetTrainingRequests(new Staff { Id = id });
        }

        // POST: api/TrainingRequest
        [HttpPost]
        public async Task<int> MakeRequest(StaffTrainingDto trainingDto) => await _service.MakeRequest(trainingDto);
        //public async Task<ActionResult<StaffTrainingRequest>> PostTrainingRequest(StaffTrainingRequest staffTrainingRequest)
        //{
        //    await _service.MakeRequest(staffTrainingRequest);
        //    return CreatedAtAction("GetTrainingRequestById", new { id = staffTrainingRequest.Id }, staffTrainingRequest);
        //}

        // PUT: api/TrainingRequest/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTrainingRequest(long id, StaffTrainingRequest staffTrainingRequest)
        {
            if (id != staffTrainingRequest.Id)
            {
                return BadRequest();
            }

            try
            {
                await _service.UpdateRequest(id, staffTrainingRequest);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffRequestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<bool> DeleteTrainingRequest(long id)
        {
            return await _service.DeleteTrainingRequest(id);
        }

        private bool StaffRequestExists(long id)
        {
            return _service.GetTrainingRequestById(id).Result != null ? true : false;
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<TrainingScheduleDto>> GetTrainingSchedule(long DeptId) => await _service.GetTrainingSchedule(DeptId);

        [HttpGet("[action]")]
        public async Task<IEnumerable<TrainingScheduleDto>> GetTrainingScheduleFaculty(long FacId) => await _service.GetTrainingScheduleFaculty(FacId);
    }

}