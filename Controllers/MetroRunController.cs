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
    public class MetroRunController : BaseController
    {
        // GET: Directories
        public ActionResult Index()
        {
            List<MainMenu> mod = new List<MainMenu>();
            //mod.Add(new MainMenu { MenuName = "CharacteristicsCategoriesPartial", Text = "Категории характеристик активов" });
            //
            mod.Add(new MainMenu { MenuName = "MetroRunPartial", Text = "Справочник вагонов" });
            mod.Add(new MainMenu { MenuName = "MetroRunTrainsPartial", Text = "Составы" });
            //
            mod.Add(new MainMenu { MenuName = "MetroRunRoutesPartial", Text = "Список маршрутов" });
            //mod.Add(new MainMenu { MenuName = "MetroRunRoutePlanPartial", Text = "Маршрутный план" });
            mod.Add(new MainMenu { MenuName = "MetroRunRoutePlanByRoutesPartial", Text = "Маршрутные наряды" });
            mod.Add(new MainMenu { MenuName = "MetroRunRoutePlanByDriversPartial", Text = "Марш наряды по водителям" });
            mod.Add(new MainMenu { MenuName = "MetroRunRoutePlanByTrainsPartial", Text = "Марш наряды по составам" });
            mod.Add(new MainMenu { MenuName = "MetroRunRoutePlanPartial", Text = "Маршрутный план" });
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
                if (directory == "AssetsPartial_View")
                {
                    int permis = CurrentUser.UserModel.Role.TblAssets;
                    ViewData["AssetAttributeMapPermission"] = permis;
                    ViewData["AssetCharacteristicPermission"] = permis;
                    ViewData["AssetPermission"] = permis;
                    ViewData["AssetOperationIndicatorsMapPermission"] = permis;
                    ViewData["AssetServiceIntervalPermission"] = permis;
                    ViewData["AssetDocumentPermission"] = permis;
                    //PopulateDepartments();
                }
                if (directory == "MetroRunPartial")
                {
                    ViewData["VagonClassesUrl"] = Settings.MetroRunUrl() +"VagonClasses/index?token=" + Settings.Token();
                    ViewData["VagonSeryClassesUrl"] = Settings.MetroRunUrl() + "VagonSeryClasses/index";
                    ViewData["VagonTypeClassesUrl"] = Settings.MetroRunUrl() + "VagonTypeClasses/index";
                    ViewData["VagonModelClassesUrl"] = Settings.MetroRunUrl() + "VagonModelClasses/index";
                }
                if (directory == "MetroRunRoutePlanPartial")
                {
                    ViewData["RoutePlan"] = Settings.MetroRunUrl() + "RoutingPeriodClasses/index";
                }
                if (directory == "MetroRunRoutesPartial")
                {
                    ViewData["MetroRunRoutes"] = Settings.MetroRunUrl() + "RouteClasses/index";
                }
                if (directory == "MetroRunTrainsPartial")
                {
                    ViewData["TrainClassesPartial"] = Settings.MetroRunUrl() + "TrainClasses/index";
                }

                if (directory == "MetroRunRoutePlanByDriversPartial")
                {
                    ViewData["RoutePlan"] = Settings.MetroRunUrl() + "Driver_scheduler/DriverScheduler";
                    directory = "MetroRunRoutePlanPartial";
                }

                if (directory == "MetroRunRoutePlanByTrainsPartial")
                {
                    ViewData["RoutePlan"] = Settings.MetroRunUrl() + "Driver_scheduler/TrainScheduler";
                    directory = "MetroRunRoutePlanPartial";
                }
                if (directory == "MetroRunRoutePlanByRoutesPartial")
                {
                    ViewData["RoutePlan"] = Settings.MetroRunUrl() + "Driver_scheduler/Index";
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