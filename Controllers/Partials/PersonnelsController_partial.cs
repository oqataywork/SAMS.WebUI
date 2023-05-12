using SAMS.Model;
using SAMS.WebUI.Models;
using SAMS.WebUI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using SAMS.Domain.Enums;
using SAMS.WebUI.Helpers;
using Kendo.Mvc.Extensions;
using System.Threading.Tasks;
using System.Net;

namespace SAMS.WebUI.Controllers
{


	public partial class PersonnelsController : BaseController
	{

        public JsonResult GetImage(Guid PersonnelGuid)
        {
            PersonnelImageModel mod = null;

            try
            {
                mod = PersonnelImagesService.OnePersonnelImage(PersonnelGuid);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            return Json(mod, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateImage(string id, string imagebase64)
        {

            try
            {
                string[] pd = imagebase64.Split(',');
                byte[] imageData = System.Convert.FromBase64String(pd[1]);
                PersonnelImageModel _roomimage = new PersonnelImageModel { PersonnelImageID = new Guid(id) };
                _roomimage.PersonnelImageBytes = imageData;

                ObjectResponse resp = await PersonnelImagesService.UpdateOrInsertPersonnelImage(_roomimage);
                //if (resp.Response != null)
                //{
                //    PersonnelImageModel added = (PersonnelImageModel)resp.Response;
                //    return Json(added);
                //}
                //else
                //{
                //    Response.StatusCode = (int)HttpStatusCode.NotModified;
                //    List<string> errors = new List<string>();
                //    errors.Add(resp.error);
                //    return Json(errors);
                //}

                if (resp.Response == null)
                {
                    //  Send "false"
                    return Json(new { success = false, responseText = resp.error }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //  Send "Success"
                    return Json(new { success = true, responseText = "Image saved successfully" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (System.Exception ex)
            {
                //Response.StatusCode = (int)HttpStatusCode.NotModified;
                //List<string> errors = new List<string>();
                //errors.Add(ex.Message);
                //return Json(errors);
                return Json(new { success = false, responseText = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}

