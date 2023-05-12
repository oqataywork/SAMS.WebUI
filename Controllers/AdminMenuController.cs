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
    [Attributes.IsAuthenticated(Order = 0), Attributes.AuthorizeUserType(Order = 1)]
    public class AdminMenuController : BaseController
    {
        // GET: Directories
        public ActionResult Index()
        {
            List<MainMenu> mod = new List<MainMenu>();
            mod.Add(new MainMenu { MenuName = "UsersPartialView", Text = "Пользователи" });
            mod.Add(new MainMenu { MenuName = "RolesPartial", Text = "Роли" });
            //mod.Add(new MainMenu { MenuName = "mn3", Text = "Локации" });
            //mod.Add(new MainMenu { MenuName = "mn4", Text = "Позиции" });
            //mod.Add(new MainMenu { MenuName = "mn5", Text = "Работники" });
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
                if(directory== "CharacteristicsPartial" || directory == "CharacteristicsCategoriesPartial")
                {
                    int characterisricsPermission = CurrentUser.UserModel.Role.TblCharacteristics;
                    ViewData["characterisricsPermission"] = characterisricsPermission;
                }

                if (directory == "RolesPartial")
                {
                    int perms;
                    if (CurrentUser.UserModel.Role.IsAdmin)
                        perms = 5;
                    else
                        perms = CurrentUser.UserModel.Role.TblRoles;

                    ViewData["RolePermission"] = perms;
                    PopulateOrganizationsList();
                }
                if (directory == "UsersPartialView")
                {
                    int perms;
                    if (CurrentUser.UserModel.Role.IsAdmin)
                        perms = 5;
                    else
                        perms = CurrentUser.UserModel.Role.TblUsers;
                    ViewData["UserPermission"] = perms;
                    PopulateRolesList();
                }
                return PartialView(directory); ;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(null);
            }

        }
        //

        private async Task PopulateRolesList()
        {
            try
            {
                IEnumerable<RoleModel> cats =await RolesService.ReadRoles();
                ViewData["roleslist"] = cats.Where(x => !x.Deactivate).Select(c => new { RoleID = c.RoleID, RoleName = c.RoleName });
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
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
    }
}