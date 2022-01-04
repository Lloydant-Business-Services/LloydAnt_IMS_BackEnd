using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LiteHR.Interface
{
    public interface IStaffAdditionalInfoService
    {
        Task<long> UploadStaffSignature(IFormFile file, long staffId, long pin, string filePath, string directory);
        Task<string> GetStaffSignatureByPin(long signaturePin, long staffId);
    }
}
