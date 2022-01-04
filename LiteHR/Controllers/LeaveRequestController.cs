//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using LiteHR.Interface;
//using LiteHR.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace LiteHR.Controllers
//{
//    [Authorize]
//    [Route("api/[controller]")]
//    [ApiController]
//    public class LeaveRequestController : Controller
//    {
//        public readonly ILeaveRequestService _service;
//        public LeaveRequestController(ILeaveRequestService service)
//        {
//            _service = service;
//        }

//        // GET: api/LeaveRequest
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<StaffLeaveRequest>>> GetLeaveRequest()
//        {
//            return await _service.GetLeaveRequests();
//        }

//        // GET: api/LeaveRequest/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<StaffLeaveRequest>> GetLeaveRequestById(long id)
//        {
//            return await _service.GetLeaveRequestById(id);
//        }

//        // GET: api/LeaveRequest/5
//        [HttpGet("LeaveRequestByStaff/{id}")]
//        public async Task<ActionResult<List<StaffLeaveRequest>>> GetLeaveRequestByStaffId(long id)
//        {
//            return await _service.GetLeaveRequests(new Staff { Id = id });
//        }

//        // POST: api/LeaveRequest
//        [HttpPost]
//        public async Task<ActionResult<StaffLeaveRequest>> PostLeaveRequest(StaffLeaveRequest staffLeaveRequest)
//        {
//            staffLeaveRequest = await _service.MakeRequest(staffLeaveRequest);
//            return CreatedAtAction("GetLeaveRequestById", new { id = staffLeaveRequest.Id }, staffLeaveRequest);
//        }

//        // PUT: api/LeaveRequest/5
//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdateLeaveRequest(long id, StaffLeaveRequest staffLeaveRequest)
//        {
//            if (id != staffLeaveRequest.Id)
//            {
//                return BadRequest();
//            }

//            try
//            {
//                await _service.UpdateRequest(id, staffLeaveRequest);
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!StaffRequestExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // DELETE: api/ApiWithActions/5
//        [HttpDelete("{id}")]
//        public async Task<bool> DeleteLeaveRequest(long id)
//        {
//            return await _service.DeleteLeaveRequest(id);
//        }

//        private bool StaffRequestExists(long id)
//        {
//            return  _service.GetLeaveRequestById(id).Result != null ? true : false;
//        }
//    }
//}