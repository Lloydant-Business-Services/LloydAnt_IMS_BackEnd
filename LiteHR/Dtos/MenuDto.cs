using LiteHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class MenuDto : BaseModel
    {
        public string Name { get; set; }
        public string Route { get; set; }
        public string RoleName { get; set; }
        public long RoleId { get; set; }
        public long ParentMenuId { get; set; }
        //public string ParentMenuName { get; set; }
        public bool Active { get; set; }


    }

    public class NewMenuDto
    {
        public string Name { get; set; }
        //public string RoleName { get; set; }
        public string Icon { get; set; }
        //public long RoleId { get; set; }
        public long Id { get; set; }
        public List<SubMenuDtos> SubMenus { get; set; }

    }

    public class SubMenuDtos
    {
        public string Name { get; set; }
        public string Route { get; set; }
        public long Id { get; set; }

    }
}
