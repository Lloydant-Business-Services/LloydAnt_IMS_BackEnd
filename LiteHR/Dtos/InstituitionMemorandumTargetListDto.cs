using LiteHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class InstituitionMemorandumTargetListDto : BaseModel
    {
        public long InstitutionMemorandumId { get; set; }
        public bool Active { get; set; }
        public string RoleName { get; set; }
        public string Title { get; set; }
        public string RefNo { get; set; }
        public string Body { get; set; }
        public string AttachmentUrl { get; set; }
        public DateTime DatePosted { get; set; }
        //public long UserId { get; set; }
        //public string To { get; set; }
        
    }
}
