using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Infrastructure
{
    public interface IFileUpload
    {
        Task<string> UploadPassport(IFormFile file, string filePath, string directory, string givenFileName);
        Task<string> UploadDocument(IFormFile file, string filePath, string directory, string givenFileName);
        Task<string> UploadDocumentGeneral(IFormFile file, string filePath, string directory, string givenFileName);
        Task<string> UploadWithoutWaterMark(IFormFile file, string filePath, string directory, string givenFileName);
    }
}
