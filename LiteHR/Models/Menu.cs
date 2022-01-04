using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class Menu : BaseModel
    {
        public string Name { get; set; }
        public string Route { get; set; }
        public Role Role { get; set; }
        public long RoleId { get; set; }
        public bool Active { get; set; }
        public long ParentMenuId { get; set; }
        public ParentMenu ParentMenu { get; set; }
    }
}
