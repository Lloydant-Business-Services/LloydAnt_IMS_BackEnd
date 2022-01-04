//using LiteHR.Dtos;
//using LiteHR.Models;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace LiteHR.Interface
//{
//    public interface IInstituitionMemorandumService
//    {
//        Task<int> AddMemorandum(InstituitionMemorandumDto memorandumDto, string filePath, string directory);
//        Task<IEnumerable<InstituitionMemorandumDto>> GetInstituitionMemorandum();
//        Task<IEnumerable<ActiveMemoDto>> GetMemoActions(long roleId, long departmentId);
//        Task<int> ForwardMemo(long memoId, long roleId, long departmentId, string comments, string fromDesk);
//        Task<int> ApproveMemoVetting(long memoId, long deskId);
//        Task<int> PublishMemo(PublishMemoDto publishDto);
//        Task<IEnumerable<ActiveMemoDto>> GetMemoOriginated(long roleId, long departmentId);
//        Task<long> PendingMemoAction(long roleId, long departmentId);
//    }
//}
