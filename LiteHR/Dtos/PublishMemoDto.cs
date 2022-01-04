using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class PublishMemoDto
    {
        public List<long> RoleIds { get; set; }
        public long DepartmentId { get; set; }
        public long MemoId { get; set; }
    }
}
