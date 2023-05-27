using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace WebOS.AuxiliaryClasses
{
    public static class UserFile
    {
        public static bool DeleteOldImage(string WebRootPath, string folder, string oldFileNameToBeDeleted)
        {
            var uploadfolder = Path.Combine(WebRootPath, folder);
            if (!string.IsNullOrEmpty(oldFileNameToBeDeleted))
            {
                var oldfile = Path.Combine(uploadfolder, oldFileNameToBeDeleted);
                if (File.Exists(oldfile))
                {
                    File.Delete(oldfile);
                    return true;
                }
            }
            return false;
        }

        public static bool DeleteOldFile(string WebRootPath, string folder, string oldFileNameToBeDeleted)
        {
            return DeleteOldImage(WebRootPath, folder, oldFileNameToBeDeleted);
        }

        public static async Task<string> UploadeNewImageAsync(string oldFileNameToBeDeleted, IFormFile uploadedFile, string WebRootPath, string folder, int width, int height)
        {
            if ((uploadedFile != null) && (uploadedFile.Length > 0))
            {
                if (!string.IsNullOrEmpty(oldFileNameToBeDeleted) && !string.IsNullOrWhiteSpace(oldFileNameToBeDeleted))
                    DeleteOldImage(WebRootPath, folder, oldFileNameToBeDeleted);
                string newfilename = Path.GetFileNameWithoutExtension(uploadedFile.FileName);
               // newfilename = newfilename + DateTime.Now.ToString("yymmssfff") + ".jpg";
                newfilename = Guid.NewGuid() + ".jpg";
                // newfilename = Guid.NewGuid() + ".jpg";
                var uploadfolder = Path.Combine(WebRootPath, folder);

                if (!Directory.Exists(uploadfolder))
                {
                    Directory.CreateDirectory(uploadfolder);
                }
                var newfilepath = Path.Combine(uploadfolder, newfilename);
                //using (var fileStream = new FileStream(newfilepath, FileMode.Create))
                //{
                //    using (Image img = Image.FromStream(uploadedFile.OpenReadStream()))
                //    {
                //        int imgHeight = img.Height;
                //        if (img.Height > 500) { imgHeight = 500; };

                //        if (img.Width > 500)
                //        {
                //            Stream ms = new MemoryStream(img.Resize(width, imgHeight).ToByteArray());
                //            FileStreamResult fsr = new FileStreamResult(ms, "image/jpg");
                //            await fsr.FileStream.CopyToAsync(fileStream);
                //        }
                //        else
                //        {
                //            Stream ms = new MemoryStream(img.ToByteArray());
                //            FileStreamResult fsr = new FileStreamResult(ms, "image/jpg");
                //            await fsr.FileStream.CopyToAsync(fileStream);
                //        }
                //                            }
                //}

                return newfilename;
            }
            return oldFileNameToBeDeleted;
        }

        internal static Task<string> UploadeNewImageAsync(string url, IFormFile myfile, string webRootPath, string pictures)
        {
            throw new NotImplementedException();
        }

        public static async Task<string> UploadImageResizeTo(string oldFileNameToBeDeleted, IFormFile uploadedFile, string WebRootPath, string folder, int width, int height)
        {
            if ((uploadedFile != null) && (uploadedFile.Length > 0))
            {
                if (!string.IsNullOrEmpty(oldFileNameToBeDeleted) && !string.IsNullOrWhiteSpace(oldFileNameToBeDeleted))
                    DeleteOldImage(WebRootPath, folder, oldFileNameToBeDeleted);
                string newfilename = Path.GetFileNameWithoutExtension(uploadedFile.FileName);
                newfilename = Guid.NewGuid() + DateTime.Now.ToString("yymmssfff") + ".jpg";
                //newfilename = Guid.NewGuid() + fileextention;
                // newfilename = Guid.NewGuid() + ".jpg";
                var uploadfolder = Path.Combine(WebRootPath, folder);

                if (!Directory.Exists(uploadfolder))
                {
                    Directory.CreateDirectory(uploadfolder);
                }
                var newfilepath = Path.Combine(uploadfolder, newfilename);
                using (var fileStream = new FileStream(newfilepath, FileMode.Create))
                {
                    //using (Image img = Image.FromStream(uploadedFile.OpenReadStream()))
                    //{
                    //    {
                    //        Stream ms = new MemoryStream(img.Resize(width, height).ToByteArray());
                    //        FileStreamResult fsr = new FileStreamResult(ms, "image/jpg");
                    //        await fsr.FileStream.CopyToAsync(fileStream);
                    //    }
                    //}
                }

                return newfilename;
            }
            return oldFileNameToBeDeleted;
        }

        public static async Task<string> UploadeNewFileAsync(string oldFileNameToBeDeleted, IFormFile uploadedFile, string WebRootPath, string folder)
        {
            if ((uploadedFile != null) && (uploadedFile.Length > 0))
            {
                if (!string.IsNullOrEmpty(oldFileNameToBeDeleted) && !string.IsNullOrWhiteSpace(oldFileNameToBeDeleted))
                    DeleteOldFile(WebRootPath, folder, oldFileNameToBeDeleted);
                string newfilename = Path.GetFileNameWithoutExtension(uploadedFile.FileName);
                string fileextention = Path.GetExtension(uploadedFile.FileName);
                newfilename = Guid.NewGuid() + fileextention;
                var uploadfolder = Path.Combine(WebRootPath, folder);

                if (!Directory.Exists(uploadfolder))
                {
                    Directory.CreateDirectory(uploadfolder);
                }
                var newfilepath = Path.Combine(uploadfolder, newfilename);
                using (var fileStream = new FileStream(newfilepath, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }

                return newfilename;
            }
            return oldFileNameToBeDeleted;
        }

        public static string GetContentType(string filename)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(filename).ToLowerInvariant();
            return types[ext];
        }

        private static Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt",    "text/plain"},
                {".pdf",    "application/pdf"},
                {".doc",    "application/vnd.ms-word"},
                {".docx",   "application/vnd.ms-word"},
                {".xls",    "application/vnd.ms-excel"},
                {".xlsx",   "application/vnd.openxmlformats officedocument.spreadsheetml.sheet"},
                {".png",    "image/png"},
                {".jpg",    "image/jpeg"},
                {".jpeg",   "image/jpeg"},
                {".gif",    "image/gif"},
                {".csv",    "text/csv"}
            };
        }

        public static async Task<string> UploadCertificateTemplate(string oldFileNameToBeDeleted, IFormFile uploadedFile, string WebRootPath, string folder)
        {
            if ((uploadedFile != null) && (uploadedFile.Length > 0))
            {
                if (!string.IsNullOrEmpty(oldFileNameToBeDeleted) && !string.IsNullOrWhiteSpace(oldFileNameToBeDeleted))
                    DeleteOldFile(WebRootPath, folder, oldFileNameToBeDeleted);
                string newfilename = Path.GetFileNameWithoutExtension(uploadedFile.FileName);
                string fileextention = Path.GetExtension(uploadedFile.FileName);
                //newfilename = uploadedFile.FileName;
                newfilename = Guid.NewGuid() + fileextention;
                var uploadfolder = Path.Combine(WebRootPath, folder);

                if (!Directory.Exists(uploadfolder))
                {
                    Directory.CreateDirectory(uploadfolder);
                }
                var newfilepath = Path.Combine(uploadfolder, newfilename);
                using (var fileStream = new FileStream(newfilepath, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }

                return newfilename;
            }
            return oldFileNameToBeDeleted;
        }

    }
}
