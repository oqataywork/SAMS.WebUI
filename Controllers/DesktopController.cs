using SAMS.Core.Helpers;
using SAMS.Model;
using SAMS.WebUI.Models;
using SAMS.WebUI.Services;
using SAMS.WebUI.Services.Partials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SAMS.WebUI.Controllers
{
    [Attributes.IsAuthenticated(Order = 0), Attributes.AuthorizeUserType(Order = 1)]
    public class DesktopController : BaseController
    {
        // GET: Directories
        public ActionResult Index()
        {
            List<MainMenu> mod = new List<MainMenu>();
            mod.Add(new MainMenu { MenuName = "ChatPartial", Text = "Коммуникация" });
            mod.Add(new MainMenu { MenuName = "TasksToMePartial", Text = "Задачи поставленные мне" });
            mod.Add(new MainMenu { MenuName = "TasksOfMyDependantsPartial", Text = "Задачи моих подчиненных" });

            return View(mod);
        }



        public ActionResult GetPartialView(string  directory)
        {
            try
            {
                //UsersService.ConnectToHub();
                ViewBag.IDLabel = "ID";
                ViewBag.RateLabel = "Rate name";
                ViewBag.Add = "Add";
                ViewBag.Delete = "Delete";
                ViewBag.Update = "Update";
                ViewBag.Cancel = "Cancel";
                ViewBag.ExportToExcel = "Export To Excel";
                ViewBag.ExportToPdf = "Export To Pdf";
                ViewBag.Edit = "Edit";
                //

                if (directory == "OperationIndicatorsPartial")
                {
                    int permis = CurrentUser.UserModel.Role.TblOperationIndicators;
                    ViewData["OperationIndicatorPermission"] = permis;
                }

                return PartialView(directory);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(null);
            }

        }
        
    }
}