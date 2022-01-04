using LiteHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class FilterByRoleDto:BaseModel
    {
        public string Name { get; set; }
    }
}
