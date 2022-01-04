using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class User:BaseModel
    {
        public string Username { get; set; }
        [NotMapped]
        public string Password { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public long StaffId { get; set; }
        public Staff Staff { get; set; }
        public long RoleId { get; set; }
        public Role Role { get; set; }
        public bool Active { get; set; }
        public string Reason { get; set; }
    }
}
