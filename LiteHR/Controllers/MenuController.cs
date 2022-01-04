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
    public class MenuController : ControllerBase
    {
        public readonly IMenuService _service;
        private readonly HRContext _context;


        public MenuController(IMenuService service, HRContext context)
        {
            _service = service;
            _context = context;
        }
        [HttpPost("AddMenu")]

        public async Task<int> AddMenu(MenuDto menuDto) => await _service.AddMenu(menuDto);



        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteMenu(long id)
        {
            var menu = await _context.MENU.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }

            _context.MENU.Remove(menu);
            await _context.SaveChangesAsync();

            return StatusCodes.Status200OK;
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<int>> ModifyMenu(long id, MenuDto menuDto)
        {
            var check = await _context.MENU.Where(d => d.Id == id).FirstOrDefaultAsync();
            
            if (check == null)
            {
                return BadRequest();
            }

            check.Name = menuDto.Name;
            check.Route = menuDto.Route;
            check.RoleId = menuDto.RoleId;
            check.ParentMenuId = menuDto.ParentMenuId;
            check.Active = true;
            _context.MENU.Update(check);
            await _context.SaveChangesAsync();
            return StatusCodes.Status200OK;

        }

        [HttpGet("GetMenuByRole")]
        public async Task<IEnumerable<NewMenuDto>> GetMenuByRole(long roleId) => await _service.GetMenuByRole(roleId);
        [HttpGet("StandAloneMenus")]
        public async Task<IEnumerable<MenuDto>> GetStandAloneMenuByRole(long roleId) => await _service.GetStandAloneMenuByRole(roleId);
        [HttpGet("GetFreeRoleMenu")]
        public async Task<IEnumerable<MenuDto>> GetStandFreeRoleMenu(string parentMenuName) => await _service.GetStandFreeRoleMenu(parentMenuName);
        [HttpGet("FreeMenu")]
        public async Task<IEnumerable<MenuDto>> GetNoRoleNoParentMenu() => await _service.GetNoRoleNoParentMenu();

        [HttpGet("GetAllMenu")]
        public async Task<IEnumerable<MenuDto>> GetAllMenu()
        {
            return await _context.MENU
                .Select(m => new MenuDto
                {
                    Name = m.Name,
                    Route = m.Route,
                    RoleName = m.Role.Name,
                    RoleId = m.RoleId,
                    ParentMenuId = m.ParentMenuId,
                    Id = m.Id
                }).ToListAsync();
        }


    }
}
