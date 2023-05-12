using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using SAMS.Entities.Services;
using SAMS.Model;
using SAMS.WebUI.Services.Partials;

namespace SAMS.WebUI.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadAssetDocument( Core.Models.AssetDocumentFileModel fileMod)//HttpPostedFileBase file)
        {
            try
            {
                HttpPostedFileBase file = fileMod.ImageFile;
                if (file.ContentLength > 0)
                {
                    //string _FileName = Path.GetFileName(file.FileName);
                    //string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                    //file.SaveAs(_path);
                    byte[] data;
                    using (Stream inputStream = file.InputStream)
                    {
                        MemoryStream memoryStream = inputStream as MemoryStream;
                        if (memoryStream == null)
                        {
                            memoryStream = new MemoryStream();
                            inputStream.CopyTo(memoryStream);
                        }
                        data = memoryStream.ToArray();
                    }

                    AssetDocumentModel doc = AssetDocumentsService.GetAssetDocument(new Guid(fileMod.AssetDocumentID));
                    doc.DocumentFile = data;
                    doc.FileName= fileMod.ImageFile.FileName;
                    AssetDocumentModel saved=AssetDocumentsService.UpdateAssetDocument(doc);
                    if (saved != null)
                    {
                        ViewBag.Message = "File Uploaded Successfully!!";
                        return new HttpStatusCodeResult(HttpStatusCode.OK);
                    }
                }
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return Json(null);
            }
        }

        [HttpPost]
        public async Task<JsonResult> UploadFile(string id)
        {
            try
            {
                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        //// get a stream
                        //var stream = fileContent.InputStream;
                        //// and optionally write the file to disk
                        //var fileName = Path.GetFileName(file);
                        //var path = Path.Combine(Server.MapPath("~/App_Data/Images"), fileName);
                        //byte[] data;
                        //using (var fileStream = System.IO.File.Create(path))
                        //{
                        //    stream.CopyTo(fileStream);
                        //}

                        byte[] data;
                        using (Stream inputStream = fileContent.InputStream)
                        {
                            MemoryStream memoryStream = inputStream as MemoryStream;
                            if (memoryStream == null)
                            {
                                memoryStream = new MemoryStream();
                                inputStream.CopyTo(memoryStream);
                            }
                            data = memoryStream.ToArray();
                        }

                        AssetDocumentModel doc = AssetDocumentsService.GetAssetDocument(new Guid(id));
                        doc.DocumentFile = data;
                        doc.FileName = Path.GetFileName(file);
                        AssetDocumentModel saved = AssetDocumentsService.UpdateAssetDocument(doc);
                        if (saved != null)
                        {
                            ShouldRefreshService.SetShouldRefreshAssetDocuments(true);
                            // return new HttpStatusCodeResult(HttpStatusCode.OK);
                        }
                    }
                }
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Upload failed");
            }

            return Json("File uploaded successfully");
        }
    }
}