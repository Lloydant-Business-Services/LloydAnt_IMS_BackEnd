using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class DeptRankCountDto
    {
        public string ?RankName { get; set; }
        public long ?RankCount { get; set; }
        public long ?RankId { get; set; }
        public string ?DepartmentName { get; set; }
    }
}
