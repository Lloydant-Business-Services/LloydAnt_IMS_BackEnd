//using LiteHR.Interface;
//using LiteHR.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using LiteHR.Dtos;
//using Microsoft.AspNetCore.Http;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Mvc;
//using System.Transactions;
//using LiteHR.Infrastructure;
//using Microsoft.Extensions.Configuration;

//namespace LiteHR.Services
//{
//    public class InstituitionMemorandumService : IInstituitionMemorandumService
//    {
//        private readonly HRContext _context;
//        private readonly IFileUpload _fileupload;
//        private readonly string baseUrl;
//        private readonly IConfiguration _configuration;
//        public int _saved { get; set; } = 1;

//        public InstituitionMemorandumService(HRContext context, IFileUpload fileupload, IConfiguration configuration)
//        {
//            _context = context;
//            _fileupload = fileupload;
//            _configuration = configuration;
//            baseUrl = _configuration.GetValue<string>("Url:root");
//        }


//        public async Task<int> AddMemorandum(InstituitionMemorandumDto memorandumDto, string filePath, string directory)
//        {
//            try
//            {
//                //var getUser = new User();
//                User getUser = await _context.USER.Where(d => d.Id == memorandumDto.UserId)
//                       .Include(s => s.Staff).ThenInclude(d => d.Department)
//                       .FirstOrDefaultAsync();
//                InstitutionMemorandum instMemo = new InstitutionMemorandum();
//                var saveDocLink = string.Empty;
//                if (memorandumDto.Attachment != null)
//                {
//                    var docNamePrefix = "Memo" + "_" + memorandumDto.Title + "_" + DateTime.Now.Millisecond;
//                    saveDocLink = await _fileupload.UploadWithoutWaterMark(memorandumDto.Attachment, filePath, directory, docNamePrefix);
//                    getUser = await _context.USER.Where(d => d.Id == memorandumDto.UserId)
//                        .Include(s => s.Staff).ThenInclude(d => d.Department)
//                        .FirstOrDefaultAsync();
//                }

//                //if (memorandumDto != null)
//                if(getUser.RoleId == 6 && memorandumDto.RoleIds.Count > 0 && memorandumDto.RoleIds[0] <= 0)
//                {
//                    instMemo.Title = memorandumDto.Title;
//                    instMemo.Body = memorandumDto.Body;
//                    instMemo.RefNo = memorandumDto.RefNo;
//                    instMemo.DatePosted = DateTime.Now;
//                    instMemo.UserId = memorandumDto.UserId;
//                    instMemo.Active = true;
//                    instMemo.Attachment_Url = saveDocLink;
//                    _context.Add(instMemo);
//                    var isSaved = await _context.SaveChangesAsync();
//                    if (isSaved == _saved)
//                    {
//                        InstitutionMemorandumTarget memorandumTarget = new InstitutionMemorandumTarget();
//                        //if (getUser.RoleId == 7)
//                        //{
//                        memorandumTarget.InstitutionMemorandumId = instMemo.Id;
//                        //memorandumTarget.RoleId = 3;
//                        memorandumTarget.Active = true;
//                        //if (memorandumDto.DepartmentId > 0)
//                        //{
//                        //    memorandumTarget.InstitutionDepartmentId = getUser.Staff.DepartmentId;
//                        //}
//                        _context.Add(memorandumTarget);
//                        await _context.SaveChangesAsync();
//                        return StatusCodes.Status200OK;
//                        //}
//                    }
//                }
//                if (memorandumDto != null && memorandumDto.RoleIds.Count > 0 && memorandumDto.RoleIds[0] != 0)
//                {

//                    instMemo.Title = memorandumDto.Title;
//                    instMemo.Body = memorandumDto.Body;
//                    instMemo.RefNo = memorandumDto.RefNo;
//                    instMemo.DatePosted = DateTime.Now;
//                    instMemo.UserId = memorandumDto.UserId;
//                    instMemo.Active = true;
//                    instMemo.Attachment_Url = saveDocLink;
//                    _context.Add(instMemo);
//                    var isSaved = await _context.SaveChangesAsync();

//                    if (isSaved == _saved)
//                    {

//                        ActiveMemoDesk memoDesk = new ActiveMemoDesk();

//                        if (memorandumDto.RoleIds.Count() > 0)
//                        {
//                            foreach (var item in memorandumDto.RoleIds)
//                            {
//                                memoDesk.InstitutionMemorandumId = instMemo.Id;
//                                memoDesk.RoleId = memorandumDto.RoleIds[0];
//                                if (memorandumDto.DepartmentId > 0)
//                                {
//                                    memoDesk.InstitutionDepartmentId = memorandumDto.DepartmentId;

//                                }
//                                memoDesk.IsVetted = false;

//                                _context.Add(memoDesk);
//                                await _context.SaveChangesAsync();
//                            }
//                        }

                       
//                    }



//                }
//                else if(memorandumDto != null)
//                {
//                    instMemo.Title = memorandumDto.Title;
//                    instMemo.Body = memorandumDto.Body;
//                    instMemo.RefNo = memorandumDto.RefNo;
//                    instMemo.DatePosted = DateTime.Now;
//                    instMemo.UserId = memorandumDto.UserId;
//                    instMemo.Active = true;
//                    instMemo.Attachment_Url = saveDocLink;
//                    _context.Add(instMemo);
//                    var isSaved = await _context.SaveChangesAsync();
//                    if(isSaved == _saved)
//                    {
//                        InstitutionMemorandumTarget memorandumTarget = new InstitutionMemorandumTarget();
//                        //if (getUser.RoleId == 7)
//                        //{
//                            memorandumTarget.InstitutionMemorandumId = instMemo.Id;
//                            //memorandumTarget.RoleId = 3;
//                            memorandumTarget.Active = true;
//                        if(memorandumDto.DepartmentId > 0)
//                        {
//                            memorandumTarget.InstitutionDepartmentId = getUser.Staff.DepartmentId;
//                        }
//                            _context.Add(memorandumTarget);
//                            await _context.SaveChangesAsync();
//                            return StatusCodes.Status200OK;
//                        //}
//                    }
//                }
//                return StatusCodes.Status200OK;

//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        public async Task<IEnumerable<InstituitionMemorandumDto>> GetInstituitionMemorandum()
//        {
//            return await _context.INSTITUITION_MEMORANDUM
//                .Where(x => x.Active)
//                .Select(f => new InstituitionMemorandumDto
//                {
//                    Title = f.Title,
//                    Body = f.Body,
//                    DatePosted = f.DatePosted,
//                    UserId = f.UserId,
//                    Id = f.Id

//                }).ToListAsync();
                          
//        }

//        public async Task<IEnumerable<ActiveMemoDto>> GetMemoActions(long roleId, long departmentId)
//        {
//            try
//            {
//                if(roleId == 6)
//                {
//                    var memoDesk = await _context.ACTIVE_MEMO_DESK
//                    .Include(s => s.InstitutionMemorandum)
//                    .ThenInclude(f => f.User)
//                    .ThenInclude(r => r.Role)
//                    .Include(r => r.InstitutionMemorandum)
//                    .ThenInclude(s => s.User)
//                    .ThenInclude(s => s.Staff)
//                    .ThenInclude(d => d.Department)
//                    .Where(f => f.IsVetted == false && f.RoleId == roleId)
//                    .Select(s => new ActiveMemoDto
//                    {
//                        Title = s.InstitutionMemorandum.Title,
//                        Body = s.InstitutionMemorandum.Body,
//                        AttachmentUrl = s.InstitutionMemorandum.Attachment_Url != null ? baseUrl + s.InstitutionMemorandum.Attachment_Url : null,
//                        Id = s.Id,
//                        DatePosted = s.InstitutionMemorandum.DatePosted,
//                        RefNo = s.InstitutionMemorandum.RefNo,
//                        isVetted = s.IsVetted,
//                        Originator = s.InstitutionMemorandum.User.Role.Name + " " + s.InstitutionMemorandum.User.Staff.Department.Name,
//                        MemoId = s.InstitutionMemorandumId,
//                        OriginatorDepartmentId = s.InstitutionMemorandum.User.Staff.DepartmentId,
//                        Comments = s.Comments,
//                        ActiveDesk = s.Role.Name,
//                        ActiveDept = s.InstitutionDepartment.Name,
//                        isActed = s.IsActed,
//                        isPublished = s.IsPublished

//                    })
//                    .ToListAsync();
//                    return memoDesk;
//                }
//                else
//                {
//                    var memoDesk = await _context.ACTIVE_MEMO_DESK
//                    .Include(s => s.InstitutionMemorandum)
//                    .ThenInclude(f => f.User)
//                    .ThenInclude(r => r.Role)
//                    .Include(r => r.InstitutionMemorandum)
//                    .ThenInclude(s => s.User)
//                    .ThenInclude(s => s.Staff)
//                    .ThenInclude(d => d.Department)
//                    .Where(f => f.IsVetted == false && f.RoleId == roleId && f.InstitutionDepartmentId == departmentId)
//                    .Select(s => new ActiveMemoDto
//                    {
//                        Title = s.InstitutionMemorandum.Title,
//                        Body = s.InstitutionMemorandum.Body,
//                        AttachmentUrl = s.InstitutionMemorandum.Attachment_Url != null ? baseUrl + s.InstitutionMemorandum.Attachment_Url : null,
//                        Id = s.Id,
//                        DatePosted = s.InstitutionMemorandum.DatePosted,
//                        RefNo = s.InstitutionMemorandum.RefNo,
//                        isVetted = s.IsVetted,
//                        Originator = s.InstitutionMemorandum.User.Role.Name + " " + s.InstitutionMemorandum.User.Staff.Department.Name,
//                        MemoId = s.InstitutionMemorandumId,
//                        OriginatorDepartmentId = s.InstitutionMemorandum.User.Staff.DepartmentId,
//                        Comments = s.Comments,
//                        ActiveDesk = s.Role.Name,
//                        ActiveDept = s.InstitutionDepartment.Name,
//                        isActed = s.IsActed,
//                        isPublished = s.IsPublished
//                    })
//                    .ToListAsync();
//                    return memoDesk;
//                }
                

//                //return null;
//            }
//            catch(Exception ex)
//            {
//                throw ex;
//            }
//        }

//        public async Task<IEnumerable<ActiveMemoDto>> GetMemoOriginated(long roleId, long departmentId)
//        {
//            try
//            {
//                if (roleId == 6)
//                {
//                    var memoDesk = await _context.ACTIVE_MEMO_DESK
//                    .Include(s => s.InstitutionMemorandum)
//                    .ThenInclude(f => f.User)
//                    .ThenInclude(r => r.Role)
//                    .Include(r => r.InstitutionMemorandum)
//                    .ThenInclude(s => s.User)
//                    .ThenInclude(s => s.Staff)
//                    .ThenInclude(d => d.Department)
//                    .Where(f => f.InstitutionMemorandum.User.RoleId == roleId)
//                    .Select(s => new ActiveMemoDto
//                    {
//                        Title = s.InstitutionMemorandum.Title,
//                        Body = s.InstitutionMemorandum.Body,
//                        AttachmentUrl = s.InstitutionMemorandum.Attachment_Url != null ? baseUrl + s.InstitutionMemorandum.Attachment_Url : null,
//                        Id = s.Id,
//                        DatePosted = s.InstitutionMemorandum.DatePosted,
//                        RefNo = s.InstitutionMemorandum.RefNo,
//                        isVetted = s.IsVetted,
//                        Originator = s.InstitutionMemorandum.User.Role.Name + " " + s.InstitutionMemorandum.User.Staff.Department.Name,
//                        MemoId = s.InstitutionMemorandumId,
//                        OriginatorDepartmentId = s.InstitutionMemorandum.User.Staff.DepartmentId,
//                        Comments = s.Comments,
//                        ActiveDesk =  s.Role.Name,
//                        ActiveDept = s.InstitutionDepartment.Name,
//                        isActed = s.IsActed,
//                        isPublished = s.IsPublished

//                    })
//                    .ToListAsync();
//                    return memoDesk;
//                }
//                else
//                {
//                    var memoDesk = await _context.ACTIVE_MEMO_DESK
//                    .Include(s => s.InstitutionMemorandum)
//                    .ThenInclude(f => f.User)
//                    .ThenInclude(r => r.Role)
//                    .Include(r => r.InstitutionMemorandum)
//                    .ThenInclude(s => s.User)
//                    .ThenInclude(s => s.Staff)
//                    .ThenInclude(d => d.Department)
//                    .Where(f => f.InstitutionMemorandum.User.RoleId == roleId && f.InstitutionMemorandum.User.Staff.DepartmentId == departmentId)
//                    .Select(s => new ActiveMemoDto
//                    {
//                        Title = s.InstitutionMemorandum.Title,
//                        Body = s.InstitutionMemorandum.Body,
//                        AttachmentUrl = s.InstitutionMemorandum.Attachment_Url != null ? baseUrl + s.InstitutionMemorandum.Attachment_Url : null,
//                        Id = s.Id,
//                        DatePosted = s.InstitutionMemorandum.DatePosted,
//                        RefNo = s.InstitutionMemorandum.RefNo,
//                        isVetted = s.IsVetted,
//                        Originator = s.InstitutionMemorandum.User.Role.Name + " " + s.InstitutionMemorandum.User.Staff.Department.Name,
//                        MemoId = s.InstitutionMemorandumId,
//                        OriginatorDepartmentId = s.InstitutionMemorandum.User.Staff.DepartmentId,
//                        Comments = s.Comments,
//                        ActiveDesk = s.Role.Name,
//                        ActiveDept = s.InstitutionDepartment.Name,
//                        isActed = s.IsActed,
//                        isPublished = s.IsPublished
//                    })
//                    .ToListAsync();
//                    return memoDesk;
//                }


//                //return null;
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        public async Task<int> ForwardMemo(long memoId, long roleId, long departmentId, string comments, string fromDesk)
//        {
//            try
//            {
//                ActiveMemoDesk memorandum = await _context.ACTIVE_MEMO_DESK.Where(a => a.InstitutionMemorandumId == memoId).FirstOrDefaultAsync();

//                if(memorandum != null)
//                {
//                    if(departmentId > 0)
//                    {
//                        memorandum.InstitutionDepartmentId = departmentId;
//                    }
//                    else{
//                        memorandum.InstitutionDepartmentId = null;
//                    }
//                    if(comments != null)
//                    {
//                        memorandum.Comments = fromDesk + ": " + comments;
//                    }
                    
//                    memorandum.RoleId = roleId;
//                    _context.Update(memorandum);
//                    await _context.SaveChangesAsync();
//                    return StatusCodes.Status200OK;
//                }
//                return 0;
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        public async Task<int> ApproveMemoVetting(long memoId, long deskId)
//        {
//            try
//            {
//                ActiveMemoDesk activeMemo = await _context.ACTIVE_MEMO_DESK.Where(s => s.InstitutionMemorandumId == memoId && s.Id == deskId).FirstOrDefaultAsync();
//                if (activeMemo != null)
//                {
//                    activeMemo.IsVetted = true;
//                    activeMemo.IsActed = true;
//                    //activeMemo.InstitutionDepartmentId = originatorDepartmentId;
//                    _context.Update(activeMemo);
//                    await _context.SaveChangesAsync();
//                    return StatusCodes.Status200OK;
//                }
//                return 0;
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        public async Task<int> PublishMemo(PublishMemoDto publishDto)
//        {
//            try
//            {
//                InstitutionMemorandum memorandum = await _context.INSTITUITION_MEMORANDUM.Where(s => s.Id == publishDto.MemoId).FirstOrDefaultAsync();
//                ActiveMemoDesk memoDesk = await _context.ACTIVE_MEMO_DESK.Where(m => m.InstitutionMemorandumId == memorandum.Id).FirstOrDefaultAsync();
//                if (memorandum != null)
//                {
//                    InstitutionMemorandumTarget memorandumTarget = new InstitutionMemorandumTarget();
//                    //memorandumTarget.InstitutionMemorandumId = memorandum.Id;
//                    if (publishDto.RoleIds.Count > 0)
//                    {
//                        foreach (var item in publishDto.RoleIds)
//                        {
//                            memorandumTarget.Active = true;
//                            memorandumTarget.RoleId = item;
//                            memorandumTarget.InstitutionMemorandumId = memorandum.Id;

//                            if (publishDto.DepartmentId > 0)
//                            {
//                                memorandumTarget.InstitutionDepartmentId = publishDto.DepartmentId;
//                            }
//                            _context.Add(memorandumTarget);
//                            await _context.SaveChangesAsync();
//                        }


//                        if (memoDesk != null)
//                        {
//                            memoDesk.IsPublished = true;
//                            _context.Update(memoDesk);
//                            await _context.SaveChangesAsync();
//                        }

//                        return StatusCodes.Status200OK;


//                    }

//                    else
//                    {
//                        memorandumTarget.Active = true;
//                        //memorandumTarget.RoleId = item;
//                        memorandumTarget.InstitutionMemorandumId = memorandum.Id;
//                        if (publishDto.DepartmentId > 0)
//                        {
//                            memorandumTarget.InstitutionDepartmentId = publishDto.DepartmentId;
//                        }
//                        _context.Add(memorandumTarget);
//                        await _context.SaveChangesAsync();

//                        if (memoDesk != null)
//                        {
//                            memoDesk.IsPublished = true;
//                            _context.Update(memoDesk);
//                            await _context.SaveChangesAsync();
//                        }
//                        return StatusCodes.Status200OK;
//                    }

//                }
//                    return 0;
                
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        public async Task<long> PendingMemoAction(long roleId, long departmentId)
//        {
//            if(roleId == 6)
//            {
//                var memoDesk = await _context.ACTIVE_MEMO_DESK
//                  .Include(s => s.InstitutionMemorandum)
//                  .ThenInclude(f => f.User)
//                  .ThenInclude(r => r.Role)
//                  .Include(r => r.InstitutionMemorandum)
//                  .ThenInclude(s => s.User)
//                  .ThenInclude(s => s.Staff)
//                  .ThenInclude(d => d.Department)
//                  .Where(f => f.RoleId == roleId && f.IsVetted == false)
//                  .Select(s => new ActiveMemoDto
//                  {
//                      Title = s.InstitutionMemorandum.Title,
//                      Body = s.InstitutionMemorandum.Body,
//                      AttachmentUrl = baseUrl + s.InstitutionMemorandum.Attachment_Url,
//                      Id = s.Id,
//                      DatePosted = s.InstitutionMemorandum.DatePosted,
//                      RefNo = s.InstitutionMemorandum.RefNo,
//                      isVetted = s.IsVetted,
//                      Originator = s.InstitutionMemorandum.User.Role.Name + " " + s.InstitutionMemorandum.User.Staff.Department.Name,
//                      MemoId = s.InstitutionMemorandumId,
//                      OriginatorDepartmentId = s.InstitutionMemorandum.User.Staff.DepartmentId,
//                      Comments = s.Comments,
//                      ActiveDesk = s.Role.Name,
//                      ActiveDept = s.InstitutionDepartment.Name,
//                      isActed = s.IsActed,
//                      isPublished = s.IsPublished

//                  })
//                  .CountAsync();
//                return memoDesk;
//            }
//            else
//            {
//                var memoDesk = await _context.ACTIVE_MEMO_DESK
//                 .Include(s => s.InstitutionMemorandum)
//                 .ThenInclude(f => f.User)
//                 .ThenInclude(r => r.Role)
//                 .Include(r => r.InstitutionMemorandum)
//                 .ThenInclude(s => s.User)
//                 .ThenInclude(s => s.Staff)
//                 .ThenInclude(d => d.Department)
//                 .Where(f => f.RoleId == roleId && f.InstitutionDepartmentId == departmentId && f.IsVetted == false)
//                 .Select(s => new ActiveMemoDto
//                 {
//                     Title = s.InstitutionMemorandum.Title,
//                     Body = s.InstitutionMemorandum.Body,
//                     AttachmentUrl = baseUrl + s.InstitutionMemorandum.Attachment_Url,
//                     Id = s.Id,
//                     DatePosted = s.InstitutionMemorandum.DatePosted,
//                     RefNo = s.InstitutionMemorandum.RefNo,
//                     isVetted = s.IsVetted,
//                     Originator = s.InstitutionMemorandum.User.Role.Name + " " + s.InstitutionMemorandum.User.Staff.Department.Name,
//                     MemoId = s.InstitutionMemorandumId,
//                     OriginatorDepartmentId = s.InstitutionMemorandum.User.Staff.DepartmentId,
//                     Comments = s.Comments,
//                     ActiveDesk = s.Role.Name,
//                     ActiveDept = s.InstitutionDepartment.Name,
//                     isActed = s.IsActed,
//                     isPublished = s.IsPublished

//                 })
//                 .CountAsync();
//                return memoDesk;
//            }
           
           
//        }
//    }
//}
