//using LiteHR.Dtos;
//using LiteHR.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace LiteHR.Interface
//{
//    public interface IMailingService
//    {
//        Task<long> PostNewMail(NewMailingDto mailingDto, string filePath, string directory);
//        Task<IEnumerable<GetMailDto>> GetMailInbox(long RoleId, long DeptId, long FacultyId, long staffId);
//        Task<int> PostMailingAction(MailingActionDto actionDto);
//        Task<int> ActivateRead(long deskChainId);
//        Task<IEnumerable<TrailingCommentsDto>> GetMailTrailingCommentsByMailId(long mailingId);
//        Task<IEnumerable<FilterByRoleDto>> FilterByRole(long roleId);
//        Task<MailingStatusDto> GetMailingDeskStatus(long mailingId);
//        Task<IEnumerable<GetMailDto>> GetSentMail(long userId);
//        Task<long> GetNewMailCount(long RoleId, long DeptId, long FacultyId);
//        Task<IEnumerable<GetMailingStaffDto>> GetSpecificMailingStaff(long departmentId);
//        Task<IEnumerable<GetMailDto>> GetMailActionArchive(long RoleId, long DeptId, long FacultyId, long staffId);
//        Task<int> CopyMailToAction(CopyListDto mailingDto, long mailingId);
//        Task<IEnumerable<GetMailingStaffDto>> SearchRecipient(string search);
//        Task<long> ArchiveMail(ArchiveMailDto dto);
//        Task<IEnumerable<MailArchiveFileType>> GetMailingFileType();
//        Task<IEnumerable<GetMailDto>> GetMailArchives(long fileTypeId, string searchParam, DateTime dateFrom, DateTime dateTo);
//        Task<string> SignatureToBase64(string signatureUrl);
//        Task<IEnumerable<GetMailDto>> GetConfidentialMail();
//        Task<IEnumerable<GetMailDto>> SearchMailInbox(long RoleId, long DeptId, long FacultyId, long staffId, long fileTypeId, string searchParam, DateTime dateFrom, DateTime dateTo);
//        Task<IEnumerable<GetMailDto>> SearchSentMail(long userId, long fileTypeId, string searchParam, DateTime dateFrom, DateTime dateTo);
//        Task<IEnumerable<GetMailDto>> GetAllMailArchives();
//       Task<IEnumerable<GetMailDto>> GetSentMailExternal(string originatorEmail);
//    }
//}
