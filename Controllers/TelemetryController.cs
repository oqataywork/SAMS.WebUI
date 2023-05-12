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
using SAMS.WebUI.Helpers;

namespace SAMS.WebUI.Controllers
{
    [Attributes.IsAuthenticated(Order = 0), Attributes.AuthorizeUserType(Order = 1)]
    public class TelemetryController : BaseController
    {
        // GET: Directories
        public ActionResult Index()
        {
            List<MainMenu> mod = new List<MainMenu>();

            mod.Add(new MainMenu { MenuName = "RF_readersPartial", Text = "Справочник ридеров" });
            mod.Add(new MainMenu { MenuName = "RF_scansPartial", Text = "Сканы" });
            mod.Add(new MainMenu { MenuName = "TrainRunsPartial", Text = "Пробеги" });
            //
            return View(mod);
        }



        public ActionResult GetPartialView(string  directory)
        {
            try
            {
                
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

                //if (directory == "MetroRunPartial")
                //{
                //    ViewData["VagonClassesUrl"] = Settings.MetroRunUrl() +"VagonClasses/index?token=" + Settings.Token();
                //    ViewData["VagonSeryClassesUrl"] = Settings.MetroRunUrl() + "VagonSeryClasses/index";
                //    ViewData["VagonTypeClassesUrl"] = Settings.MetroRunUrl() + "VagonTypeClasses/index";
                //    ViewData["VagonModelClassesUrl"] = Settings.MetroRunUrl() + "VagonModelClasses/index";
                //}
                if (directory == "RF_readersPartial")
                {
                    ViewData["RoutePlan"] = Settings.MetroRunWebServiceUrl();
                    directory = "MetroRunRoutePlanPartial";
                }

                if (directory == "RF_scansPartial")
                {
                    ViewData["RoutePlan"] = Settings.MetroRunWebServiceUrl() + "Home/Scans";
                    directory = "MetroRunRoutePlanPartial";
                }
                if (directory == "TrainRunsPartial")
                {
                    ViewData["RoutePlan"] = Settings.MetroRunWebServiceUrl() + "Home/Runs";
                    directory = "MetroRunRoutePlanPartial";
                }
                //
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