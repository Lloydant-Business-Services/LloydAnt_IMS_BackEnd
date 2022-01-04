using LiteHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class ActiveMemoDto : BaseModel
    {
        public string Title { get; set; }
        public string RefNo { get; set; }
        public string Body { get; set; }
        public DateTime DatePosted { get; set; }
        public long UserId { get; set; }
        public string ActiveDesk { get; set; }
        public string ActiveDept { get; set; }
        public long? OriginatorDepartmentId { get; set; }
        public long DepartmentId { get; set; }
        public long FacultyId { get; set; }
        public long MemoId { get; set; }
        //public string To { get; set; }
        public List<int> RoleIds { get; set; }
        //public int RoleIds { get; set; }
        public bool Active { get; set; }
        public bool isVetted { get; set; }
        public bool? isActed { get; set; }
        public bool? isPublished { get; set; }
        public string AttachmentUrl { get; set; }
        public string Originator { get; set; }
        public string Comments { get; set; }

    }
}
