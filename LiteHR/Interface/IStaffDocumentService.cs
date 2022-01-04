using LiteHR.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Interface
{
    public interface IStaffDocumentService
    {
        Task<int> StaffDocumentUpload(StaffDocumentUploadDto staffDocumentUploadDto, string filePath, string directory);
        Task<IEnumerable<StaffUploadedDocxDto>> StaffDocumentUploadHistory(long personId);
        Task<int> StaffDocumentUploadByAdmin(StaffDocumentUploadDto staffDocumentUploadDto, string filePath, string directory);
        Task<IEnumerable<StaffUploadedDocxDto>> GetStaffDocumentUploadHistoryByAdmin(string staffNumber);
        Task<int> VerifyStaffDocumentByAdmin(DocumentVerificationDto documentVerificationDto);
    }
}
