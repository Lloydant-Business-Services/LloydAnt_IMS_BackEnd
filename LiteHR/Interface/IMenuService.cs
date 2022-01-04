using LiteHR.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Interface
{
   public interface IMenuService
    {
        Task<int> AddMenu(MenuDto menuDto);
        Task<IEnumerable<NewMenuDto>> GetMenuByRole(long roleId);
        Task<IEnumerable<MenuDto>> GetStandAloneMenuByRole(long roleId);
        Task<IEnumerable<MenuDto>> GetStandFreeRoleMenu(string parentMenuName);
        Task<IEnumerable<MenuDto>> GetNoRoleNoParentMenu();
    }
}
