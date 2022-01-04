using LiteHR.Infrastructure;
using LiteHR.Interface;
using LiteHR.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Services
{
    public class StaffAdditionalInfoService : IStaffAdditionalInfoService
    {
        private readonly HRContext _context;
        private readonly IFileUpload _fileUpload;
        private readonly IConfiguration _configuration;
        private readonly string baseUrl;


        public StaffAdditionalInfoService(HRContext context, IFileUpload fileUpload, IConfiguration configuration)
        { 
            _context = context;
            _fileUpload = fileUpload;
            _configuration = configuration;
            baseUrl = _configuration.GetValue<string>("Url:root");

        }
        public async Task<long> UploadStaffSignature(IFormFile file, long staffId, long pin, string filePath, string directory)
        {
            try
            {
                StaffAdditionalInfo newStaffInfo = new StaffAdditionalInfo();
                var saveimageLink = string.Empty;
                if (file != null)
                {
                    Staff staff = await _context.STAFF.Where(x => x.Id == staffId).FirstOrDefaultAsync();
                    StaffAdditionalInfo staffInfo = await _context.STAFF_ADDITIONAL_INFO.Where(s => s.staffId == staffId).FirstOrDefaultAsync();

                    if(staff != null && staffInfo == null)
                    {
                        string fileNamePrefix = "Signature" + "_" + staff.Id + "_" + DateTime.Now.Millisecond;
                        

                        saveimageLink = await _fileUpload.UploadPassport(file, filePath, directory, fileNamePrefix);
                        var dd = GetTransparentArrayFromFileWithDelete(saveimageLink);

                        newStaffInfo.SignaturePin = pin;
                        newStaffInfo.SignatureUrl = saveimageLink;
                        //newStaffInfo.SignatureUrl = dd;
                        newStaffInfo.staffId = staff.Id;
                        _context.Add(newStaffInfo);
                        await _context.SaveChangesAsync();
                        return StatusCodes.Status200OK;

                    }
                    if (staff != null && staffInfo != null)
                    {
                        string fileNamePrefix = "Signature" + "_" + staff.Id + "_" + DateTime.Now.Millisecond;
                        saveimageLink = await _fileUpload.UploadPassport(file, filePath, directory, fileNamePrefix);

                        staffInfo.SignaturePin = pin;
                        staffInfo.SignatureUrl = saveimageLink;
                        _context.Update(staffInfo);
                        await _context.SaveChangesAsync();
                        return StatusCodes.Status200OK;

                    }
                }
                return StatusCodes.Status400BadRequest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static byte[] GetTransparentArrayFromFileWithDelete(string pathToFile)
        {
            byte[] newImage = new byte[0];
            var error = string.Empty;
            using (Bitmap bmp = new Bitmap(pathToFile))
            {
                Color pixel = bmp.GetPixel(0, 0);
                if (pixel.A != 0)
                {
                    Color backColor = bmp.GetPixel(0, 0);
                    Color backColorGray = Color.Gray;
                    Color backColorGrayLight = Color.LightGray;
                    Color backColorWhiteSmoke = Color.WhiteSmoke;
                    Color backColorWhite = Color.White;
                    Color backColorWheat = Color.Wheat;

                    //Make backColor transparent for myBitmap.
                    bmp.MakeTransparent(Color.Transparent);
                    bmp.MakeTransparent(backColor);

                    bmp.MakeTransparent(backColorGray);
                    bmp.MakeTransparent(backColorGrayLight);
                    bmp.MakeTransparent(backColorWhiteSmoke);

                    ImageConverter converter = new ImageConverter();
                    newImage = (byte[])converter.ConvertTo(bmp, typeof(byte[]));
                    bmp.Dispose();
                }
                else
                {
                    FileStream fs = new FileStream(pathToFile, FileMode.OpenOrCreate, FileAccess.Read);
                    newImage = new byte[fs.Length];
                    fs.Read(newImage, 0, System.Convert.ToInt32(fs.Length));
                    fs.Close();
                }
            }
            try
            {
                File.Delete(pathToFile);
            }
            catch
            {
            }
            return newImage;
        }

        public async Task<string> GetStaffSignatureByPin(long signaturePin, long staffId)
        {
            StaffAdditionalInfo staffAdditionalInfo = await _context.STAFF_ADDITIONAL_INFO.Where(s => s.staffId == staffId && s.SignaturePin == signaturePin).FirstOrDefaultAsync();
            if(staffAdditionalInfo != null)
            {
                return baseUrl + staffAdditionalInfo.SignatureUrl;
            }
            return "not found";
        }

    }
}
