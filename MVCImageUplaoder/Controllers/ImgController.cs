namespace MVCImageUplaoder.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Helper_Code.Objects;
    using Models;

 
    public class ImgController : Controller
    {
        #region Private Properties

   
        private db_imgEntities1 databaseManager = new db_imgEntities1();

        #endregion

        #region Index view method.

        #region Get: /Img/Index method.

     
        public ActionResult Index()
        {
         
            ImgViewModel model = new ImgViewModel { FileAttach = null, ImgLst = new List<ImgObj>() };

            try
            {
                // Settings
                model.ImgLst = this.databaseManager.sp_get_all_files().Select(p => new ImgObj
                {
                    FileId = p.file_id,
                    FileName = p.file_name,
                    FileContentType = p.file_ext
                }).ToList();
            }
            catch (Exception ex)
            {
                // Info
                Console.Write(ex);
            }

            // Info.
            return this.View(model);
        }

        #endregion

        #region POST: /Img/Index

       
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ImgViewModel model)
        {
            // Initialization.
            string filePath = string.Empty;
            string fileContentType = string.Empty;

            try
            {
                // Verification
                if (ModelState.IsValid)
                {
                    // Converting to bytes.
                    byte[] uploadedFile = new byte[model.FileAttach.InputStream.Length];
                    model.FileAttach.InputStream.Read(uploadedFile, 0, uploadedFile.Length);

                    // Initialization.
                    fileContentType = model.FileAttach.ContentType;
                    string folderPath = "~/Content/upload_files/";
                    this.WriteBytesToFile(this.Server.MapPath(folderPath), uploadedFile, model.FileAttach.FileName);
                    filePath = folderPath + model.FileAttach.FileName;

                    // Saving info.
                    this.databaseManager.sp_insert_file(model.FileAttach.FileName, fileContentType, filePath);
                }

                // Settings.
                model.ImgLst = this.databaseManager.sp_get_all_files().Select(p => new ImgObj
                {
                    FileId = p.file_id,
                    FileName = p.file_name,
                    FileContentType = p.file_ext
                }).ToList();
            }
            catch (Exception ex)
            {
                // Info
                Console.Write(ex);
            }

            // Info
            return this.View(model);
        }

        #endregion

        #endregion

        #region Download file methods

        #region GET: /Img/DownloadFile

      
        public ActionResult DownloadFile(int fileId)
        {
            // Model binding.
            ImgViewModel model = new ImgViewModel { FileAttach = null, ImgLst = new List<ImgObj>() };

            try
            {
                // Loading dile info.
                var fileInfo = this.databaseManager.sp_get_file_details(fileId).First();

                // Info.
                return this.GetFile(fileInfo.file_path);
            }
            catch (Exception ex)
            {
                // Info
                Console.Write(ex);
            }

            // Info.
            return this.View(model);
        }

        #endregion

        #endregion

        #region Helpers

        #region Get file method.

  
        private FileResult GetFile(string filePath)
        {
            // Initialization.
            FileResult file = null;

            try
            {
                // Initialization.
                string contentType = MimeMapping.GetMimeMapping(filePath);

                // Get file.
                file = this.File(filePath, contentType);
            }
            catch (Exception ex)
            {
                // Info.
                throw ex;
            }

            // info.
            return file;
        }

        #endregion

        #region Write to file

   
        private void WriteBytesToFile(string rootFolderPath, byte[] fileBytes, string filename)
        {
            try
            {
                // Verification.
                if (!Directory.Exists(rootFolderPath))
                {
                    // Initialization.
                    string fullFolderPath = rootFolderPath;

                    // Settings.
                    string folderPath = new Uri(fullFolderPath).LocalPath;

                    // Create.
                    Directory.CreateDirectory(folderPath);
                }

                // Initialization.                
                string fullFilePath = rootFolderPath + filename;

                // Create.
                FileStream fs = System.IO.File.Create(fullFilePath);

                // Close.
                fs.Flush();
                fs.Dispose();
                fs.Close();

                // Write Stream.
                BinaryWriter sw = new BinaryWriter(new FileStream(fullFilePath, FileMode.Create, FileAccess.Write));

                // Write to file.
                sw.Write(fileBytes);

                // Closing.
                sw.Flush();
                sw.Dispose();
                sw.Close();
            }
            catch (Exception ex)
            {
                // Info.
                throw ex;
            }
        }

        #endregion

        #endregion
    }
}