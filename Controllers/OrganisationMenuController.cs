using SAMS.Model;
using SAMS.WebUI.Helpers;
using SAMS.WebUI.Models;
using SAMS.WebUI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SAMS.WebUI.Controllers
{
    public class OrganisationMenuController : BaseController
    {
        // GET: OrganisationMenu
        public ActionResult Index()
        {
            List<MainMenu> mod = new List<MainMenu>();
            mod.Add(new MainMenu { MenuName = "OrganizationsPartial", Text = "Организации" });
            mod.Add(new MainMenu { MenuName = "DepartmentsPartial", Text = "Отделы" });
            mod.Add(new MainMenu { MenuName = "LocationsPartial", Text = "Локации" });
            mod.Add(new MainMenu { MenuName = "PositionsPartial", Text = "Позиции" });
            mod.Add(new MainMenu { MenuName = "PersonnelsPartialView", Text = "Работники" });
            mod.Add(new MainMenu { MenuName = "CountriesPartial", Text = "Страны" });
            mod.Add(new MainMenu { MenuName = "ContrAgentsPartial", Text = "Контрагенты" });
            return View(mod);

        }

        public ActionResult GetPartialView(string directory)
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
                //PopulateOrganizationsList();

                //if (directory == "PositionsPartial")
                //{
                //    int perms = CurrentUser.UserModel.Role.TblPositions;
                //    ViewData["PositionPermission"] = perms;
                //}

                //

                if (directory == "PersonnelsPartialView")
                {
                    int perms = CurrentUser.UserModel.Role.TblPersonnels;
                    ViewData["PersonnelPermission"] = perms;
                }

                if (directory == "DepartmentsPartial")
                {
                    int perms = CurrentUser.UserModel.Role.TblDepartments;
                    ViewData["DepartmentPermission"] = perms;
                }


                if (directory == "OrganizationsPartial")
                {
                    int perms = CurrentUser.UserModel.Role.TblOrganizations;
                    ViewData["OrganizationPermission"] = perms;
                }

                if (directory == "CharacteristicsPartial" || directory == "CharacteristicsCategoriesPartial")
                {
                    int characterisricsPermission = CurrentUser.UserModel.Role.TblCharacteristics;
                    ViewData["characterisricsPermission"] = characterisricsPermission;
                }
                
                if (directory == "PositionsPartial")
                {
                    int perms = CurrentUser.UserModel.Role.TblPositions;
                    ViewData["PositionPermission"] = perms;
                }
                if (directory == "CountriesPartial" )
                {
                    int perms = CurrentUser.UserModel.Role.TblCountries;
                    ViewData["CountryPermission"] = perms;
                }
                if (directory == "LocationsPartial")
                {
                    int perms = CurrentUser.UserModel.Role.TblLocations;
                    ViewData["LocationPermission"] = perms;
                }
                
                if (directory == "ContrAgentsPartial")
                {
                    int perms = CurrentUser.UserModel.Role.TblContrAgentIs;
                    ViewData["ContrAgentPermission"] = perms;
                    PopulateCountriesList();
                }
                return PartialView(directory); ;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(null);
            }

        }



        private async Task PopulateOrganizationsList()
        {
            try
            {
                IEnumerable<OrganizationModel> cats = await OrganizationsService.ReadOrganizations();
                ViewData["organizationslist"] = cats.Where(x => !x.Deactivate).Select(c => new { OrganizationID = c.OrganizationID, OrganizationName = c.OrganizationName });
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task PopulateCountriesList()
        {
            try
            {
                IEnumerable<CountryModel> cats = await CountriesService.ReadCountrys();
                ViewData["countries"] = cats;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}