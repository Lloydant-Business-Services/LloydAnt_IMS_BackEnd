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
    public class ParentMenuController : ControllerBase
    {
        private readonly HRContext _context;
        public ParentMenuController(HRContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParentMenu>>> GET_PARENTMENU()
        {
            return await _context.PARENT_MENU.Where(f => f.Id != 1).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ParentMenu>> GetParentMenu(long id)
        {
            var parMenu = await _context.PARENT_MENU.FindAsync(id);

            if (parMenu == null)
            {
                return NotFound();
            }

            return parMenu;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<int>> PutParentMenu(long id, ParentMenu parentMenu)
        {
            if (id != parentMenu.Id)
            {
                return BadRequest();
            }

            _context.PARENT_MENU.Update(parentMenu);
            await _context.SaveChangesAsync();
            return StatusCodes.Status200OK;
        }

        [HttpPost]
        public async Task<ActionResult<int>> PostParentMenu(ParentMenu parentMenu)
        {
            _context.PARENT_MENU.Add(parentMenu);
            await _context.SaveChangesAsync();

            return StatusCodes.Status200OK;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteParentMenu(long id)
        {
            var parentMenu = await _context.PARENT_MENU.FindAsync(id);
            if (parentMenu == null)
            {
                return NotFound();
            }

            _context.PARENT_MENU.Remove(parentMenu);
            await _context.SaveChangesAsync();

            return StatusCodes.Status200OK;
        }
    }
}
