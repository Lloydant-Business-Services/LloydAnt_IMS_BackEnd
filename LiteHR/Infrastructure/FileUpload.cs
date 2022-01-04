using LiteHR.Helpers;
using Microsoft.AspNetCore.Http;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Infrastructure
{
    public class FileUpload : IFileUpload
    {
        public async Task<string> UploadDocument(IFormFile file, string filePath, string directory, string givenFileName)
        {
            var documentUrl = string.Empty;
            //Define allowed property of the uploaded file

            var validFileSize = (1024 * 1024);//1mb
            List<string> validFileExtension = new List<string>();
            validFileExtension.Add(".jpg");
            validFileExtension.Add(".png");
            validFileExtension.Add(".jpeg");
            validFileExtension.Add(".pdf");
            validFileExtension.Add(".docx");
            validFileExtension.Add(".xlsx");

            if (file.Length > 0)
            {

                var extType = Path.GetExtension(file.FileName);
                var fileSize = file.Length;
                if (fileSize <= validFileSize)
                {
                  
                    if (validFileExtension.Contains(extType))
                    {
                        string fileName = string.Format("{0}{1}", givenFileName + "_" + DateTime.Now.Millisecond, extType);
                        //create file path if it doesnt exist
                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        var fullPath = Path.Combine(filePath, fileName);
                        //here
                        

                        documentUrl = Path.Combine(directory, fileName);
                        //Delete if file exist
                        FileInfo fileExists = new FileInfo(fullPath);
                        if (fileExists.Exists)
                        {
                            fileExists.Delete();
                        }

                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        PdfDocument doc = new PdfDocument();
                        PdfSection section = doc.Sections.Add();
                        PdfPageBase page = doc.Pages.Add();
                        PdfImage image = PdfImage.FromFile(fullPath);
                        float widthFitRate = image.PhysicalDimension.Width / page.Canvas.ClientSize.Width;
                        float heightFitRate = image.PhysicalDimension.Height / page.Canvas.ClientSize.Height;
                        float fitRate = Math.Max(widthFitRate, heightFitRate);
                        float fitWidth = image.PhysicalDimension.Width / fitRate;
                        float fitHeight = image.PhysicalDimension.Height / fitRate;
                        page.Canvas.DrawImage(image, 20, 30, fitWidth, fitHeight);
                        //page.Canvas.DrawString("Nnamdi Azikiwe University", new PdfFont(PdfFontFamily.Helvetica, 30f), new PdfSolidBrush(Color.Gray), 60, 140);

                        PdfTilingBrush brush = new PdfTilingBrush(new SizeF(page.Canvas.ClientSize.Width / 2, page.Canvas.ClientSize.Height / 3));
                        brush.Graphics.SetTransparency(0.3f);
                        brush.Graphics.Save();
                        brush.Graphics.TranslateTransform(brush.Size.Width / 2, brush.Size.Height / 2);
                        brush.Graphics.RotateTransform(-45);
                        brush.Graphics.DrawString(" " + " NNAMDI AZIKIWE UNIVERSITY", new PdfFont(PdfFontFamily.Helvetica, 18), PdfBrushes.Blue, 0, 0, new PdfStringFormat(PdfTextAlignment.Center));
                        brush.Graphics.Restore();
                        brush.Graphics.SetTransparency(1);
                        page.Canvas.DrawRectangle(brush, new RectangleF(new PointF(0, 0), page.Canvas.ClientSize));
                        doc.SaveToFile(filePath + "/" + givenFileName + "_" + ".pdf");
                        doc.Close();
                        
                        FileInfo defaultImg = new FileInfo(fullPath);
                        if (defaultImg.Exists)
                        {
                            defaultImg.Delete();
                        }

                   
                        
                        string pdfName = string.Format("{0}{1}", givenFileName + "_",  ".pdf");

                        string pdfUrl = Path.Combine(directory, pdfName);

                        return pdfUrl = pdfUrl.Replace('\\', '/');





                    }
                    else
                    {
                        throw new AppException("Bad Upload Format");
                    }
                }
            }
            return documentUrl;
        }
        public async Task<string> UploadPassport(IFormFile file, string filePath, string directory, string givenFileName)
        {

            var passportUrl = string.Empty;
            //Define allowed property of the uploaded file

            var validFileSize = (1024 * 1024);//1mb
            List<string> validFileExtension = new List<string>();
            validFileExtension.Add(".jpg");
            validFileExtension.Add(".jpeg");
            validFileExtension.Add(".png");


            if (file.Length > 0)
            {

                var extType = Path.GetExtension(file.FileName);
                var fileSize = file.Length;
                if (fileSize <= validFileSize)
                {

                    if (validFileExtension.Contains(extType))
                    {
                        string fileName = string.Format("{0}{1}", givenFileName + "_" + DateTime.Now.Millisecond, extType);
                        //create file path if it doesnt exist
                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        var fullPath = Path.Combine(filePath, fileName);
                        passportUrl = Path.Combine(directory, fileName);
                        //Delete if file exist
                        FileInfo fileExists = new FileInfo(fullPath);
                        if (fileExists.Exists)
                        {
                            fileExists.Delete();
                        }

                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        return passportUrl = passportUrl.Replace('\\', '/');





                    }
                    else
                    {
                        throw new BadImageFormatException("File format is not supported");
                    }
                }
            }
            return passportUrl;
        }

        public async Task<string> UploadDocumentGeneral(IFormFile file, string filePath, string directory, string givenFileName)
        {

            var documentUrl = string.Empty;
            //Define allowed property of the uploaded file

            var validFileSize = (1024 * 1024);//1mb
            List<string> validFileExtension = new List<string>();
            validFileExtension.Add(".jpg");
            validFileExtension.Add(".jpeg");
            validFileExtension.Add(".png");

            if (file.Length > 0)
            {

                var extType = Path.GetExtension(file.FileName);
                var fileSize = file.Length;
                if (fileSize <= validFileSize)
                {

                    if (validFileExtension.Contains(extType))
                    {
                        string fileName = string.Format("{0}{1}", givenFileName + "_" + DateTime.Now.Millisecond, extType);
                        //create file path if it doesnt exist
                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        var fullPath = Path.Combine(filePath, fileName);
                        documentUrl = Path.Combine(directory, fileName);
                        //Delete if file exist
                        FileInfo fileExists = new FileInfo(fullPath);
                        if (fileExists.Exists)
                        {
                            fileExists.Delete();
                        }

                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        //hhhh
                        PdfDocument doc = new PdfDocument();
                        PdfSection section = doc.Sections.Add();
                        PdfPageBase page = doc.Pages.Add();
                        PdfImage image = PdfImage.FromFile(fullPath);
                        float widthFitRate = image.PhysicalDimension.Width / page.Canvas.ClientSize.Width;
                        float heightFitRate = image.PhysicalDimension.Height / page.Canvas.ClientSize.Height;
                        float fitRate = Math.Max(widthFitRate, heightFitRate);
                        float fitWidth = image.PhysicalDimension.Width / fitRate;
                        float fitHeight = image.PhysicalDimension.Height / fitRate;
                        page.Canvas.DrawImage(image, 20, 30, fitWidth, fitHeight);
                  
                       
                        doc.SaveToFile(filePath + "/" + givenFileName + "_" + ".pdf");
                        doc.Close();

                        FileInfo defaultImg = new FileInfo(fullPath);
                        if (defaultImg.Exists)
                        {
                            defaultImg.Delete();
                        }
                        string pdfName = string.Format("{0}{1}", givenFileName + "_", ".pdf");

                        string pdfUrl = Path.Combine(directory, pdfName);

                        return pdfUrl = pdfUrl.Replace('\\', '/');






                    }
                    else
                    {
                        throw new BadImageFormatException("File format is not supported");
                    }
                }
            }
            return documentUrl;
        }


        public async Task<string> UploadWithoutWaterMark(IFormFile file, string filePath, string directory, string givenFileName)
        {

            var documentUrl = string.Empty;
            //Define allowed property of the uploaded file

            var validFileSize = (1024 * 1024);//1mb
            List<string> validFileExtension = new List<string>();
            validFileExtension.Add(".jpg");
            validFileExtension.Add(".jpeg");
            validFileExtension.Add(".png");
            validFileExtension.Add(".pdf");
            validFileExtension.Add(".docx");
            validFileExtension.Add(".xlsx");

            if (file.Length > 0)
            {

                var extType = Path.GetExtension(file.FileName);
                var fileSize = file.Length;
                if (fileSize <= validFileSize)
                {

                    if (validFileExtension.Contains(extType))
                    {
                        string fileName = string.Format("{0}{1}", givenFileName + "_" + DateTime.Now.Millisecond, extType);
                        //create file path if it doesnt exist
                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        var fullPath = Path.Combine(filePath, fileName);
                        documentUrl = Path.Combine(directory, fileName);
                        //Delete if file exist
                        FileInfo fileExists = new FileInfo(fullPath);
                        if (fileExists.Exists)
                        {
                            fileExists.Delete();
                        }

                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        //hhhh
                        
                        //string pdfName = string.Format("{0}{1}", givenFileName + "_", ".pdf");

                        //string pdfUrl = Path.Combine(directory, pdfName);

                        return documentUrl = documentUrl.Replace('\\', '/');
                        






                    }
                    else
                    {
                        throw new BadImageFormatException("File format is not supported");
                    }
                }
            }
            return documentUrl;
        }

    }
}
