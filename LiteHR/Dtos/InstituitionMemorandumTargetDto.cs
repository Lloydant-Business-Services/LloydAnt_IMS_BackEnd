using LiteHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class InstituitionMemorandumTargetDto : BaseModel
    {
        public long InstitutionMemorandumId { get; set; }
        public long? RoleId { get; set; }
        public bool Active { get; set; }
        public string RoleName { get; set; }
    }


}
