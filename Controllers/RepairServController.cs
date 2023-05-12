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
    public class RepairServController : BaseController
    {
        // GET: Directories
        public ActionResult Index()
        {
            List<MainMenu> mod = new List<MainMenu>();
            mod.Add(new MainMenu { MenuName = "MaintenanceRepairTypesPartial", Text = "Типы обслуживаний" });
            mod.Add(new MainMenu { MenuName = "DefectTypesPartial", Text = "Категории неисправностей" });
            mod.Add(new MainMenu { MenuName = "DefectremovalwaysPartial", Text = "Типы устранения неисравностей" });
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

                
                if (directory == "DefectremovalwaysPartial")
                {
                    int permis = CurrentUser.UserModel.Role.TblDefectTypes;
                    ViewData["DefectremovalwayPermission"] = permis;
                }
                if (directory == "DefectTypesPartial")
                {
                    int permis = CurrentUser.UserModel.Role.TblDepartments;
                    ViewData["DefectTypePermission"] = permis;
                }
                if (directory == "MaintenanceRepairTypesPartial")
                {
                    int permis = CurrentUser.UserModel.Role.TblMaintenanceRepairTypes;
                    ViewData["MaintenanceRepairTypePermission"] = permis;
                }

                
                
                return PartialView(directory);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(null);
            }

        }

        //

        private async Task PopulateUnits()
        {
            try
            {
                IEnumerable<MeasurementUnitTypeModel> cats = await MeasurementUnitTypesService.ReadMeasurementUnitTypes();
                ViewData["units"] = cats;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private async Task PopulateCharacteristics()
        {
            try
            {
                IEnumerable<CharacteristicModel> cats = await CharacteristicsService.ReadCharacteristics();
                ViewData["characteristics"] = cats;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task PopulateAssetAttributes()
        {
            try
            {
                IEnumerable<AssetAttributeModel> cats = await AssetAttributesService.ReadAssetAttributes();
                ViewData["asset_attributes_list"] = cats;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task PopulateAssetCategoriesist()
        {
            try
            {
                List<AssetCategoryModel> cats = await AssetCategoriesService.ReadAssetCategorys() as List<AssetCategoryModel>;
                //IEnumerable<AssetCategoryModel> cats = AssetCategoriesService.ReadAssetCategorys();
                ViewData["assetcategories"] = cats;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task PopulateAssetAttributeTypeEnumList()
        {
            try
            {
                IEnumerable<IntKeyValue> cats = await PartialService.GetAssetCategoryTypesEnum();
                ViewData["asset_attribute_type_enumslist"] = cats;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task PopulateAttributesCategoriesList()
        {
            try
            {
                IEnumerable<AssetAttributeCategoryModel> cats = await AssetAttributeCategoriesService.ReadAssetAttributeCategorys();
                ViewData["assetattributescategories"] = cats.Where(x => !x.Deactivate).Select(c => new { AssetAttributeCategoryID = c.AssetAttributeCategoryID, AssetAttributeCategoryName = c.AssetAttributeCategoryName });
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task PopulatecharacteristicscategoriesList()
        {
            try
            {
                IEnumerable<CharacteristicCategoryModel> cats = await CharacteristicCategoriesService.ReadCharacteristicCategorys();
                ViewData["characteristicscategories"] = cats.Where(x=>!x.Deactivate).Select(c => new { CharacteristicCategoryID = c.CharacteristicCategoryID, CharacteristicCategoryName = c.CharacteristicCategoryName });
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<JsonResult> PersonnelsByDepartment(string DepartmentID)
        {
           var pers = (await PersonnelsService.ReadPersonnels()).Where(x => x.DepartmentID == new Guid(DepartmentID)).Select(c => new { PersonnelID = c.PersonnelID, PeronFullName = String.Format("{0} {1} {2}", c.PersonnelFirstName, c.PersonnelLastName, c.PersonnelMiddleName) }); 

            return Json(pers, JsonRequestBehavior.AllowGet);
        }
    }
}