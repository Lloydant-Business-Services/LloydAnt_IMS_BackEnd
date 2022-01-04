using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteHR.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LiteHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolidayController : ControllerBase
    {
        private readonly HRContext _context;

        public HolidayController(HRContext context)
        {
            _context = context;
        }


        [HttpPost("[action]")]
        public async Task<long> AddHoliday(Holidays holidays)
        {
            _context.HOLIDAYS.Add(holidays);
            await _context.SaveChangesAsync();
            return StatusCodes.Status201Created;
        }

        [HttpGet("GetHolidays")]
        public async Task<ActionResult<IEnumerable<Holidays>>> GetHolidays()
        {
            return await _context.HOLIDAYS.Where(p => p.Active).ToListAsync();
        }

        [HttpPut("[action]")]
        public async Task<long> EditHoliday(Holidays holidays, long id)
        {
            var holiday = await _context.HOLIDAYS.Where(h => h.Id == id).FirstOrDefaultAsync();
            holiday.Name = holidays.Name;
            holiday.Date = holidays.Date;
            _context.HOLIDAYS.Update(holiday);
            await _context.SaveChangesAsync();
            return StatusCodes.Status201Created;
        }


        [HttpDelete("[action]")]
        public async Task<long> DeleteHoliday(long id)
        {
            var holiday = await _context.HOLIDAYS.Where(h => h.Id == id).FirstOrDefaultAsync();
            holiday.Active = false;
            _context.HOLIDAYS.Remove(holiday);
            await _context.SaveChangesAsync();
            return StatusCodes.Status201Created;
        }

    }
}
