using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class DepartmentListDto
       
    {
        public string Name { get; set; }
        public string FacultyName { get; set; }
        public long Id { get; set; }
        public long? FacultyId { get; set; }
        public bool Active { get; set; }
    }
}
