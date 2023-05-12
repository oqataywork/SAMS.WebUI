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
    public class DirectoriesController : BaseController
    {
        // GET: Directories
        public ActionResult Index()
        {
            List<MainMenu> mod = new List<MainMenu>();
            //mod.Add(new MainMenu { MenuName = "CharacteristicsCategoriesPartial", Text = "Категории характеристик активов" });
            //
            mod.Add(new MainMenu { MenuName = "CharacteristicCategoriesPartial", Text = "Категории Характеристик" });
            mod.Add(new MainMenu { MenuName = "CharacteristicsPartial", Text = "Характеристики активов" });
            mod.Add(new MainMenu { MenuName = "AssetAttributeCategoriesPartial", Text = "Категории атрибутов" });
            mod.Add(new MainMenu { MenuName = "AssetAttributesPartial", Text = "Аттрибуты активов" });
            mod.Add(new MainMenu { MenuName = "AssetTypesPartialView", Text = "Типы активов" });
            mod.Add(new MainMenu { MenuName = "AssetsPartial_View", Text = "Активы" });
            mod.Add(new MainMenu { MenuName = "AssetCategoriesTreePartial", Text = "Категории активов" });
            mod.Add(new MainMenu { MenuName = "DocumentTypesPartial", Text = "Типы документов" });
            mod.Add(new MainMenu { MenuName = "MeasurementUnitTypesPartial", Text = "Единицы измерений" });
            mod.Add(new MainMenu { MenuName = "ScheduleTypesPartial", Text = "Типы временных интервалов" });
            mod.Add(new MainMenu { MenuName = "OperationIndicatorsPartial", Text = "Индикаторы состояния" });
            //
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
                if (directory == "OperationIndicatorsPartial")
                {
                    int permis = CurrentUser.UserModel.Role.TblOperationIndicators;
                    ViewData["OperationIndicatorPermission"] = permis;
                }

                if (directory == "MeasurementUnitTypesPartial")
                {
                    int permis = CurrentUser.UserModel.Role.TblMeasurementUnitTypes;
                    ViewData["MeasurementUnitTypePermission"] = permis;
                }
                if (directory == "ScheduleTypesPartial")
                {
                    int permis = CurrentUser.UserModel.Role.TblScheduleTypes;
                    ViewData["ScheduleTypePermission"] = permis;
                }
                //

                if (directory == "AssetTypesPartialView")
                {
                    int permis = CurrentUser.UserModel.Role.TblAssetTypes;
                    ViewData["AssetTypePermission"] = permis;
                    PopulateScheduleTypes();
                }

                if (directory == "AssetCategoriesTreePartial")
                {
                    int permis = CurrentUser.UserModel.Role.TblAssetCategories;
                    ViewData["AssetCategoryPermission"] = permis;
                    ViewData["AssetCategoryAttributeMapPermission"] = permis;

                    ViewData["AssetCategoryCharacteristicsMapPermission"] = permis;
                    ViewData["AssetCategoryDefectMapPermission"] = permis;
                    ViewData["AssetCategoryDocumentTypeMapPermission"] = permis;
                    ViewData["AssetCategoryOperationIndicatorsMapPermission"] = permis;
                    ViewData["AssetCategoryServiceIntervalsMapPermission"] = permis;

                    PopulateAssetCategoriesist();
                    PopulateAssetAttributes();
                    PopulateCharacteristics();
                    PopulateUnits();
                    PopulateDefectTypesList();
                    PopulateDocumentTypesList();
                    PopulateOperationalIndicators();
                    PopulateMaintananceRepairTypes();
                }

                if (directory== "CharacteristicsPartial" )
                {
                    int permis = CurrentUser.UserModel.Role.TblCharacteristics;
                    ViewData["CharacteristicPermission"] = permis;
                    PopulatecharacteristicscategoriesList();
                }
                if ( directory == "CharacteristicCategoriesPartial")
                {
                    int permis = CurrentUser.UserModel.Role.TblCharacteristicCategories;
                    ViewData["CharacteristicCategoryPermission"] = permis;
                }
                if (directory == "DocumentTypesPartial")
                {
                    int permis = CurrentUser.UserModel.Role.TblDocumentTypes;
                    ViewData["DocumentTypePermission"] = permis;
                }
                if (directory == "AssetAttributeCategoriesPartial")
                {
                    int permis = CurrentUser.UserModel.Role.TblAssetAttributeCategories;
                    ViewData["AssetAttributeCategoryPermission"] = permis;
                    
                }
                if (directory == "AssetAttributesPartial")
                {
                    int permis = CurrentUser.UserModel.Role.TblAssetAttributes;
                    ViewData["AssetAttributePermission"] = permis;
                    PopulateAttributesCategoriesList();
                    PopulateAssetAttributeTypeEnumList();
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
        private async Task PopulateDepartments()
        {
            try
            {
                var cats = (await DepartmentsService.ReadDepartments()).Where(x => !x.Deactivate).Select(c => new { DepartmentID = c.DepartmentID, DepartmentName = c.DepartmentName }).ToList();
                ViewData["departmentslist"] = cats;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private async Task PopulateMaintananceRepairTypes()
        {
            try
            {
                var cats = (await MaintenanceRepairTypesService.ReadMaintenanceRepairTypes()).Where(x => !x.Deactivate).Select(c => new { MaintenanceRepairTypeID = c.MaintenanceRepairTypeID, MaintenanceRepairTypeShortName = c.MaintenanceRepairTypeShortName }).ToList();
                ViewData["maintanancerepairtypes_list"] = cats;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task PopulateOperationalIndicators()
        {
            try
            {
                var cats = (await OperationIndicatorsService.ReadOperationIndicators()).Where(x=>!x.Deactivate).Select(c => new { OperationIndicatorID = c.OperationIndicatorID, OperationIndicatorName = c.OperationIndicatorName }).ToList();
                ViewData["operationalindicators_list"] = cats;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task PopulateScheduleTypes()
        {
            try
            {
                var cats = (await ScheduleTypesService.ReadScheduleTypes()).Where(x => !x.Deactivate).Select(c => new { ScheduleTypeID = c.ScheduleTypeID, ScheduleTypeName = c.ScheduleTypeName }).ToList();
                ViewData["schedullertypes_list"] = cats;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task PopulateUnits()
        {
            try
            {
                var cats =( await MeasurementUnitTypesService.ReadMeasurementUnitTypes()).Where(x => !x.Deactivate).Select(c => new { MeasurementUnitTypeID = c.MeasurementUnitTypeID, UnitTypeName = c.UnitTypeName }).ToList();
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
                var cats = (await CharacteristicsService.ReadCharacteristics()).Where(x => !x.Deactivate).Select(c => new { CharacteristicID = c.CharacteristicID, CharacteristicName = c.CharacteristicName }).ToList();
                ViewData["characteristics"] = cats;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        //

        private async Task PopulateDocumentTypesList()
        {
            try
            {
                var cats = (await DocumentTypesService.ReadDocumentTypes()).Where(x => !x.Deactivate).Select(c => new { DocumentTypeID = c.DocumentTypeID, DocumentTypeName = c.DocumentTypeName }).ToList();
                ViewData["documenttypesslist"] = cats;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private async Task PopulateDefectTypesList()
        {
            try
            {
                var cats = (await DefectTypesService.ReadDefectTypes()).Where(x => !x.Deactivate).Select(c => new { DefectTypeID = c.DefectTypeID, DefectTypeName = c.DefectTypeName }).ToList();
                ViewData["defecttypesslist"] = cats;
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
                var cats = (await AssetAttributesService.ReadAssetAttributes()).Where(x => !x.Deactivate).Select(c => new { AssetAttributeID = c.AssetAttributeID, AssetAttributeName = c.AssetAttributeName }).ToList();
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
                List<AssetCategoryModel> _list = await AssetCategoriesService.ReadAssetCategorys() as List<AssetCategoryModel>;
                var cats = _list.Where(x => !x.Deactivate).Select(c => new { AssetCategoryID = c.AssetCategoryID, AssetCategoryName = c.AssetCategoryName }).ToList();
                ViewData["assetcategorieslist"] = cats;
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