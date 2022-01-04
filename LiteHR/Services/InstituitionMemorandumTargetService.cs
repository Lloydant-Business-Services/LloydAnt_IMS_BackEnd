//using LiteHR.Dtos;
//using LiteHR.Interface;
//using LiteHR.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace LiteHR.Services
//{
//    public class InstituitionMemorandumTargetService : IInstituitionMemorandumTargetService
//    {
//        private readonly HRContext _context;
//        private readonly string baseUrl;
//        private readonly IConfiguration _configuration;


//        public InstituitionMemorandumTargetService(HRContext context, IConfiguration configuration)
//        {
//            _context = context;
//            _configuration = configuration;
//            baseUrl = _configuration.GetValue<string>("Url:root");
//        }

//        public async Task<IEnumerable<InstituitionMemorandumTargetListDto>> GetMemorandumByRoleId(int userId)
//        {
//            List<InstituitionMemorandumTargetListDto> memoList = new List<InstituitionMemorandumTargetListDto>();
//            User user = await _context.USER.Where(u => u.Id == userId).Include(d => d.Staff).ThenInclude(d => d.Department).FirstOrDefaultAsync();
//            memoList = await _context.INSTITUITION_MEMORANDUM_TARGET.Where(m => (m.RoleId == user.RoleId || (m.InstitutionDepartmentId == user.Staff.DepartmentId) || m.InstitutionDepartmentId == null && m.FacultyId == null && m.RoleId == null) && m.Active)
//                .Include(m => m.InstitutionMemorandum)
//                .ThenInclude(u => u.User)
//                .ThenInclude(r => r.Role)
//                .OrderByDescending(x => x.Id)
//                .Select(f => new InstituitionMemorandumTargetListDto
//                {
//                    Id = f.Id,
//                    Title = f.InstitutionMemorandum.Title,
//                    Body = f.InstitutionMemorandum.Body,
//                    RoleName = f.InstitutionMemorandum.User.Role.Name,
//                    RefNo = f.InstitutionMemorandum.RefNo,
//                    DatePosted = f.InstitutionMemorandum.DatePosted,
//                    InstitutionMemorandumId = f.InstitutionMemorandumId,
//                    AttachmentUrl = f.InstitutionMemorandum.Attachment_Url != null ? baseUrl + f.InstitutionMemorandum.Attachment_Url : null
                   
//                })
//                .ToListAsync();
//            return memoList;
//        }

//    }
//}
