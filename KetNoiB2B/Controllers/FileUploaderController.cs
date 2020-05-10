using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KetNoiB2B.Utils;
using SETA.BusinessLogic;
using SETA.Common.Constants;
using SETA.Core.Singleton;
using SETA.DataAccess;

namespace KetNoiB2B.Controllers
{
    public class FileUploaderController : Controller
    {
        public ActionResult SaveUploadedFile()
        {
            bool isSavedSuccessfully = true;
            string fNameAndPath = "";            
            var folderUpload = SETA.Common.Utility.Utils.GetSetting(AppKeys.UPLOAD_FOLDER_PRODUCT,
                "{0}Images\\ProductImages");            
                      
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];                      
                    //Save file content goes here                    
                    if (file != null && file.ContentLength > 0)
                    {
                        var newFileName =
                            WebUtils.CleanFileName(Path.GetFileNameWithoutExtension(file.FileName) + DateTime.Now.Ticks +
                                                   Path.GetExtension(file.FileName));
                        fNameAndPath = newFileName;
                        var originalDirectory = new DirectoryInfo(string.Format(folderUpload, Server.MapPath(@"\")));
                        fNameAndPath = string.Format(folderUpload, @"\");

                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "imagepath");

                        var fileName1 = Path.GetFileName(newFileName);

                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);                        

                        var path = string.Format("{0}\\{1}", pathString, newFileName);
                        fNameAndPath = string.Format(@"{0}\{1}\{2}", fNameAndPath, "imagepath", newFileName);
                        file.SaveAs(path);
                        fNameAndPath = fNameAndPath.Replace(@"\", "/");
                    }

                }

            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
            }


            if (isSavedSuccessfully)
            {
                return Json(new { Message = fNameAndPath });
            }
            else
            {
                return Json(new { Message = "Error in saving file" });
            }
        }
    }
}