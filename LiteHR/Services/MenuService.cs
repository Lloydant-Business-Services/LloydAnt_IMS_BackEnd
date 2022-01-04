using LiteHR.Dtos;
using LiteHR.Infrastructure;
using LiteHR.Interface;
using LiteHR.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Services
{
    public class MenuService: IMenuService
    {
        public readonly HRContext _context;
        public MenuService(HRContext context)
        {
            _context = context;

        }


        public async Task<int> AddMenu(MenuDto menuDto)
        {
            if (menuDto.RoleId == 0 && menuDto.ParentMenuId == 0)
            {
                var intializeRole = await _context.ROLE.Where(r => r.Name == "-").FirstOrDefaultAsync();
                var intializeParentMenu = await _context.PARENT_MENU.Where(r => r.Id == 1).FirstOrDefaultAsync();

                Menu menuRoleInit = new Menu()
                {
                    Name = menuDto.Name,
                    Route = menuDto.Route,
                    RoleId = intializeRole.Id,
                    ParentMenuId = intializeParentMenu.Id,
                    Active = true
                };
                _context.Add(menuRoleInit);
                await _context.SaveChangesAsync();
                return StatusCodes.Status200OK;
            }

            if (menuDto.ParentMenuId == 0 && menuDto.RoleId > 0)
            {
                Menu menuInit = new Menu()
                {
                    Name = menuDto.Name,
                    Route = menuDto.Route,
                    RoleId = menuDto.RoleId,
                    ParentMenuId = 1,
                    Active = true
                };
                _context.Add(menuInit);
                await _context.SaveChangesAsync();
                return StatusCodes.Status200OK;
            }

            if (menuDto.RoleId == 0 && menuDto.ParentMenuId > 0)
            {
                var intializeRole = await _context.ROLE.Where(r => r.Name == "-").FirstOrDefaultAsync();

                Menu menuRoleInit = new Menu()
                {
                    Name = menuDto.Name,
                    Route = menuDto.Route,
                    RoleId = intializeRole.Id,
                    ParentMenuId = menuDto.ParentMenuId,
                    Active = true
                };
                _context.Add(menuRoleInit);
                await _context.SaveChangesAsync();
                return StatusCodes.Status200OK;
            }

       



            Menu menu = new Menu()
            {
                Name = menuDto.Name,
                Route = menuDto.Route,
                RoleId = menuDto.RoleId,
                ParentMenuId = menuDto.ParentMenuId,
                    Active = true
                };
                _context.Add(menu);
                await _context.SaveChangesAsync();
                return StatusCodes.Status200OK;
           
            
        }

        public async Task<IEnumerable<NewMenuDto>> GetMenuByRole(long roleId)
        {
            List<NewMenuDto> menuList = new List<NewMenuDto>();
           var getParentMenu = await _context.PARENT_MENU.Where(p => p.Active).ToListAsync();

            foreach(var item in getParentMenu)
            {
                var subMenus = await _context.MENU.Where(s => s.RoleId == roleId && s.ParentMenuId == item.Id && s.Active).Include(x => x.ParentMenu)
                    .Select(f => new SubMenuDtos
                    {
                        Name = f.Name,
                        Route = f.Route,
                        Id = f.Id
                    })
                    .ToListAsync();
                if(subMenus != null && subMenus.Count > 0)
                {
                    NewMenuDto menuDto = new NewMenuDto();
                    menuDto.Name = item.Name;
                    menuDto.Icon = item.Icon;
                    menuDto.Id = item.Id;
                    menuDto.SubMenus = subMenus;

                    menuList.Add(menuDto);
                }
            }
            //var getMenu = await _context.MENU.Where(s => s.RoleId == roleId && s.Active).Include(x => x.ParentMenu).OrderBy(f => f.Name)
            //    .Select(f => new NewMenuDto
            //    {
            //        Name = f.Name,
            //        Route = f.Route,
            //        RoleId = f.RoleId,
            //        ParentMenuId = f.ParentMenuId
            //    })
            //    .ToListAsync();

            //foreach(var item in getMenu)
            //{
            //    NewMenuDto menuDto = new NewMenuDto();
            //    menuDto.
            //}

            return menuList;

                
        }
        //Personnel Department (Documentation)
        //public async Task<IEnumerable<MenuDto>> GetMenuByPersonnelDocumentation(long roleId, string parentMenuName)
        //{
        //    var getParentMenu = await _context.PARENT_MENU.Where(p => p.Name == parentMenuName).FirstOrDefaultAsync();
        //    var getMenu = await _context.MENU.Where(s => s.RoleId == roleId && s.ParentMenuId == getParentMenu.Id).OrderBy(f => f.Name)
        //        .Select(f => new MenuDto
        //        {
        //            Name = f.Name,
        //            Route = f.Route,
        //            RoleId = f.RoleId,
        //            ParentMenuId = f.ParentMenuId
        //        }).ToListAsync();

        //    return getMenu;


        //}

        public async Task<IEnumerable<MenuDto>> GetStandAloneMenuByRole(long roleId)
        {
            var getMenu = await _context.MENU.Where(s => s.RoleId == roleId && s.ParentMenuId == (long)MenuList.StandAlone).OrderBy(f => f.Name)
                .Select(f => new MenuDto
                {
                    Name = f.Name,
                    Route = f.Route,
                    RoleId = f.RoleId,
                    ParentMenuId = f.ParentMenuId
                }).ToListAsync();

            return getMenu;


        }


        public async Task<IEnumerable<MenuDto>> GetStandFreeRoleMenu(string parentMenuName)

        {
            var getParentMenu = await _context.PARENT_MENU.Where(p => p.Name == parentMenuName).FirstOrDefaultAsync();

            var getMenu = await _context.MENU.Where(s => s.Role.Name == "-" && s.ParentMenuId == getParentMenu.Id).OrderBy(f => f.Name)
                .Select(f => new MenuDto
                {
                    Name = f.Name,
                    Route = f.Route,
                    RoleId = f.RoleId,
                    ParentMenuId = f.ParentMenuId
                }).ToListAsync();

            return getMenu;


        }

        public async Task<IEnumerable<MenuDto>> GetNoRoleNoParentMenu()
        {
            var getMenu = await _context.MENU.Where(s => s.Role.Name == "-" && s.ParentMenuId == 1).OrderBy(f => f.Name)
                .Select(f => new MenuDto
                {
                    Name = f.Name,
                    Route = f.Route,
                    RoleId = f.RoleId,
                    ParentMenuId = f.ParentMenuId
                }).ToListAsync();

            return getMenu;


        }
    }
}
