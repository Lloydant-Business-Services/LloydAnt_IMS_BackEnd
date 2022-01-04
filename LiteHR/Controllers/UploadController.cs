using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LiteHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private IHostingEnvironment _hostingEnvironment;

        public UploadController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost("[action]"), DisableRequestSizeLimit]
        public async Task<IActionResult> UploadSingleFile(IFormFile file)
        {
            try
            {
                var folderName = Path.Combine("Resources", "Images");//Set the destination folder for the Uploaded file
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var fileExt = file.ContentType.ToLower();
                var fileSize = file.Length;
                var validFileSize = (50 * 1024);//50kb
                var ext = fileExt.Split("/")[1];

                if (file.Length > 0)
                {
                    if(fileSize <= validFileSize)
                    {
                        if (fileExt.EndsWith("jpg") || fileExt.EndsWith("jpeg") || fileExt.EndsWith("png") || fileExt.EndsWith("gif"))
                        {
                            string fileName = string.Format("{0}.{1}", Guid.NewGuid(), ext);
                            var filePath = Path.Combine(_hostingEnvironment.ContentRootPath, folderName);

                            var fullPath = Path.Combine(filePath, fileName);
                            var dbPath = Path.Combine(folderName, fileName);

                            FileInfo fileExists = new FileInfo(fileName);
                            if (fileExists.Exists)
                            {
                                fileExists.Delete();
                            }

                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }

                            return Ok(new { dbPath = dbPath.Replace('\\', '/') });
                        }
                        else
                        {
                            return BadRequest(new { Message = "Invalid file type...Accepted formats are jpg, jpeg, png and gif" });
                        }
                    }
                    else
                    {
                        return BadRequest(new { Message = "Image Size exceeds 50Kb" });
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch(Exception ex) 
            {
                return StatusCode(500, $"Something went wrong: {ex}");
            }
        }

        [HttpPost("[action]"), DisableRequestSizeLimit]
        public async Task<IActionResult> UploadDocument(IFormFile document)
        {
            try
            {
                var folderName = Path.Combine("Resources", "Documents");//Set the destination folder for the Uploaded file
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var fileExt = document.ContentType.ToLower();
                var fileSize = document.Length;
                var validFileSize = (100 * 1024);//1mb
                var ext = fileExt.Split("/")[1];

                if (document.Length > 0)
                {
                    if (fileSize <= validFileSize)
                    {
                        if (fileExt.EndsWith("pdf") || fileExt.EndsWith("jpg") || fileExt.EndsWith("jpeg") || fileExt.EndsWith("png") || fileExt.EndsWith("gif"))
                        {
                            string fileName = string.Format("{0}.{1}", Guid.NewGuid(), ext);
                            var filePath = Path.Combine(_hostingEnvironment.ContentRootPath, folderName);

                            var fullPath = Path.Combine(filePath, fileName);
                            var dbPath = Path.Combine(folderName, fileName);

                            FileInfo fileExists = new FileInfo(fileName);
                            if (fileExists.Exists)
                            {
                                fileExists.Delete();
                            }

                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                await document.CopyToAsync(stream);
                            }

                            return Ok(new { dbPath = dbPath.Replace('\\', '/') });
                        }
                        else
                        {
                            return BadRequest(new { Message = "Invalid file type...Accepted formats are pdf, jpg, jpeg, png and gif" });
                        }
                    }
                    else
                    {
                        return BadRequest(new { Message = "Document Size exceeds 1mb" });
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Something went wrong: {ex}");
            }
        }

        [HttpPost("[action]"), DisableRequestSizeLimit]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadMultipleDocuments(List<IFormFile> files)
        {
            try
            {
                long size = files.Sum(f => f.Length);

                List<string> filePathList = new List<string>();

                if (size > 0)
                {
                    foreach (var document in files)
                    {
                        var folderName = Path.Combine("Resources", "Documents");//Set the destination folder for the Uploaded file
                        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                        var fileExt = document.ContentType.ToLower();
                        var fileSize = document.Length;
                        var validFileSize = (100 * 1024);//1mb
                        var ext = fileExt.Split("/")[1];

                        if (document.Length > 0)
                        {
                            if (fileSize <= validFileSize)
                            {
                                if (fileExt.EndsWith("pdf") || fileExt.EndsWith("jpg") || fileExt.EndsWith("jpeg") || fileExt.EndsWith("png") || fileExt.EndsWith("gif"))
                                {
                                    string fileName = string.Format("{0}.{1}", Guid.NewGuid(), ext);
                                    var filePath = Path.Combine(_hostingEnvironment.ContentRootPath, folderName);

                                    var fullPath = Path.Combine(filePath, fileName);
                                    var dbPath = Path.Combine(folderName, fileName);

                                    FileInfo fileExists = new FileInfo(fileName);
                                    if (fileExists.Exists)//delete file if it already exists
                                    {
                                        fileExists.Delete();
                                    }

                                    using (var stream = new FileStream(fullPath, FileMode.Create))//save the file
                                    {
                                        await document.CopyToAsync(stream);
                                    }

                                    if (!filePathList.Contains(fullPath))//get saved paths, to be returned as output
                                    {
                                        filePathList.Add(dbPath.Replace('\\', '/'));
                                    }
                                }
                                else
                                {
                                    return BadRequest(new { Message = "Invalid file type...Accepted formats are pdf, jpg, jpeg, png and gif" });
                                }
                            }
                            else
                            {
                                return BadRequest(new { Message = "Document Size exceeds 1mb" });
                            }
                        }
                        else
                        {
                            return BadRequest();
                        }
                    }
                }

                //int index = 0;
                //List<string> filePaths = new List<string>();
                //foreach (var document in files)
                //{
                //    index += 1;
                //    using (var stream = new FileStream(filePathList[index], FileMode.Create))
                //    {
                //        await document.CopyToAsync(stream);
                //    }
                //}

                return Ok(new { dbPaths = filePathList, count = filePathList.Count() });
            }
            catch(Exception ex) 
            {
                return StatusCode(500, $"Something went wrong: {ex}");
            }
        }
    }
}
