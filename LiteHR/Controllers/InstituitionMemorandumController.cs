//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using LiteHR.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using LiteHR.Interface;
//using LiteHR.Dtos;
//using Microsoft.Extensions.Hosting;
//using System.IO;
//using Microsoft.EntityFrameworkCore;

//namespace LiteHR.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class InstituitionMemorandumController : ControllerBase
//    {
//        private readonly HRContext _context;
//        private readonly IInstituitionMemorandumService  _service;
//        private readonly IHostEnvironment _hostingEnvironment;

//        public InstituitionMemorandumController(HRContext context, IInstituitionMemorandumService service, IHostEnvironment hostingEnvironment)
//        {
//            _context = context;
//            _service = service;
//            _hostingEnvironment = hostingEnvironment;
//        }

//        [HttpPost("[action]")]
//        public async Task<int> AddMemorandum([FromForm] InstituitionMemorandumDto memorandumDto)
//        {
//            var directory = Path.Combine("Resources", "Memos");
//            var filePath = Path.Combine(_hostingEnvironment.ContentRootPath, directory);
//            return await _service.AddMemorandum(memorandumDto, filePath, directory);
//        }

//        [HttpGet("[action]")]
//        public async Task<IEnumerable<InstituitionMemorandumDto>> GetInstituitionMemorandum() => await _service.GetInstituitionMemorandum();


//        [HttpGet("[action]")]
//        public async Task<IEnumerable<ActiveMemoDto>> GetMemoActions(long roleId, long departmentId) => await _service.GetMemoActions(roleId, departmentId);

//        [HttpPost("[action]")]
//        public async Task<int> ForwardMemo(long memoId, long roleId, long departmentId, string comments, string fromDesk) => await _service.ForwardMemo(memoId, roleId, departmentId, comments, fromDesk);

//        [HttpPost("[action]")]
//        public async Task<int> ApproveMemoVetting(long memoId, long deskId) => await _service.ApproveMemoVetting(memoId, deskId);

//        [HttpPost("[action]")]
//        public async Task<int> PublishMemo(PublishMemoDto publishDto) => await _service.PublishMemo(publishDto);
//        [HttpGet("[action]")]
//        public async Task<IEnumerable<ActiveMemoDto>> GetMemoOriginated(long roleId, long departmentId) => await _service.GetMemoOriginated(roleId, departmentId);
//        [HttpGet("[action]")]
//        public async Task<long> PendingMemoAction(long roleId, long departmentId) => await _service.PendingMemoAction(roleId, departmentId);
//        [HttpDelete("[action]")]
//        public async Task<long> DeleteMemo(long memoId)
//        {
//            var memo = await _context.INSTITUITION_MEMORANDUM.Where(d => d.Id == memoId).FirstOrDefaultAsync();
//            _context.Remove(memo);
//            await _context.SaveChangesAsync();
//            return StatusCodes.Status200OK;
//        }

//        [HttpDelete("[action]")]
//        public async Task<long> DeleteMemoDesk(long memoId)
//        {
//            var memo = await _context.ACTIVE_MEMO_DESK.Where(d => d.InstitutionMemorandumId == memoId).FirstOrDefaultAsync();
//            _context.Remove(memo);
//            await _context.SaveChangesAsync();
//            return StatusCodes.Status200OK;
//        }

//        [HttpDelete("[action]")]
//        public async Task<long> DeleteMemoTarget(long memoId)
//        {
//            var memo = await _context.INSTITUITION_MEMORANDUM_TARGET.Where(d => d.InstitutionMemorandumId == memoId).FirstOrDefaultAsync();
//            _context.Remove(memo);
//            await _context.SaveChangesAsync();
//            return StatusCodes.Status200OK;
//        }
//    }
//}
