using LiteHR.Interface;
using LiteHR.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Services
{
    public class StaffPfaService : IStaffPfaService
    {
        private readonly HRContext _context;
        public StaffPfaService(HRContext context)
        {
            _context = context;
        }
    }
}
