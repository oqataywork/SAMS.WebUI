using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using SAMS.Core.Helpers;
using SAMS.Core.ViewModels;
using SAMS.Domain.Enums;
using SAMS.Model;
using SAMS.WebUI.Helpers;
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
    public partial class AssetCategoriesController : BaseController
    {
        
        public ActionResult About()
        {
            return View();
        }

        //[OutputCache(Duration = CacheDuration.Days1)]
        public async Task<JsonResult> GetAssetCategoriesTree()
        {

            IEnumerable<AssetCategoryViewModel> cats = await PartialService.GetCategoriesTree();

            AssetCategoryViewModel root = new AssetCategoryViewModel {AssetCategoryID=Guid.Empty, AssetCategoryCode="0", AssetCategoryName="All categories" };
            root.ChildrenAssetCategories = cats.ToList();
            var jsonResult = Json(root, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }


        //

        //[AccessLevelFilter(Action = AccessLevelEnum.Read, TableName = "tbl_AssetCategoryAttributeMaps")]
        //public JsonResult GetPossibleParentCategories(string AssetCategoryID = "")
        //{
        //    try
        //    {
        //        List<Guid> alldesc = null;
        //        if (AssetCategoryID != "" && AssetCategoryID != "00000000-0000-0000-0000-000000000000")
        //            alldesc = PartialService.GetAllDescendantAssetCategoriesByID(AssetCategoryID);

        //        List<GuidKeyValue> result;
        //        IEnumerable<AssetCategoryModel> cats = AssetCategoriesService.ReadAssetCategorys();
        //        if (alldesc != null && alldesc.Count > 0)
        //            result = cats.Where(x => !alldesc.Contains(x.AssetCategoryID)).Select(c => new GuidKeyValue  { Key = c.AssetCategoryID, Value = c.AssetCategoryName }).ToList();
        //        else
        //            result = cats.Select(c => new GuidKeyValue { Key = c.AssetCategoryID, Value = c.AssetCategoryName }).ToList();
        //        var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
        //        return jsonResult;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return Json(null);
        //    }
        //}

        public async Task<JsonResult> GetPossibleParentCategories(string assetcategoryid = "")
        {
            try
            {
                List<Guid> alldesc = null;
                if (assetcategoryid != "" && assetcategoryid != "00000000-0000-0000-0000-000000000000")
                    alldesc = await PartialService.GetAllDescendantAssetCategoriesByID(assetcategoryid);

                List<AssetCategoryModel> result;
                
                IEnumerable<AssetCategoryModel> cats = await AssetCategoriesService.ReadAssetCategorys();
                if (alldesc != null && alldesc.Count > 0)
                    result = cats.Where(x => !alldesc.Contains(x.AssetCategoryID)).Select(c => new AssetCategoryModel { AssetCategoryID = c.AssetCategoryID, AssetCategoryName = c.AssetCategoryName }).ToList();
                else
                    result = cats.Select(c => new AssetCategoryModel { AssetCategoryID = c.AssetCategoryID, AssetCategoryName = c.AssetCategoryName }).ToList();
                var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
                return jsonResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(null);
            }
        }

        //[AccessLevelFilter(Action = AccessLevelEnum.Read, TableName = "tbl_AssetCategories")]
        public async Task<ActionResult> AssetCategoryAttributeMaps_ReadByID([DataSourceRequest] DataSourceRequest request, string id = "")
        {
            try
            {
                List<Guid> allparents = null;
                if (id != "" && id!="00000000-0000-0000-0000-000000000000")
                    allparents =await PartialService.GetAllParentAssetCategoriesByID(id);

                DataSourceResult result;
                IEnumerable<AssetCategoryAttributeMapModel> cats = await AssetCategoryAttributeMapsService.ReadAssetCategoryAttributeMaps();
                if (allparents != null && allparents.Count > 0)
                    result = await cats.Where(x => allparents.Contains(x.AttrAssetCategoryID)).ToDataSourceResultAsync(request);
                else
                    result = await cats.ToDataSourceResultAsync(request);
                var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
                return jsonResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(null);
            }
        }


        //[AccessLevelFilter(Action = AccessLevelEnum.Read, TableName = "tbl_AssetCategories")]
        public async Task<ActionResult> AssetCategoryCharacteristicsMaps_ReadByID([DataSourceRequest] DataSourceRequest request, string id = "")
        {
            try
            {
                List<Guid> allparents = null;
                if (id != "" && id != "00000000-0000-0000-0000-000000000000")
                    allparents = await PartialService.GetAllParentAssetCategoriesByID(id);

                DataSourceResult result;
                IEnumerable<AssetCategoryCharacteristicsMapModel> cats = await AssetCategoryCharacteristicsMapsService.ReadAssetCategoryCharacteristicsMaps();

                if (allparents != null && allparents.Count > 0)
                    result = cats.Where(x => allparents.Contains(x.CatAssetCategoryID)).ToDataSourceResult(request);
                else
                    result = cats.ToDataSourceResult(request);

                var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
                return jsonResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(null);
            }
        }


        //[AccessLevelFilter(Action = AccessLevelEnum.Read, TableName = "tbl_AssetCategories")]
        public async Task<ActionResult> AssetCategoryDefectMaps_ReadByID([DataSourceRequest] DataSourceRequest request, string id = "")
        {
            try
            {
                List<Guid> allparents = null;
                if (id != "" && id != "00000000-0000-0000-0000-000000000000")
                    allparents = await PartialService.GetAllParentAssetCategoriesByID(id);

                DataSourceResult result;
                IEnumerable<AssetCategoryDefectMapModel> cats = await AssetCategoryDefectMapsService.ReadAssetCategoryDefectMaps();

                if (allparents != null && allparents.Count > 0)
                    result = cats.Where(x => allparents.Contains(x.DefAssetCategoryID)).ToDataSourceResult(request);
                else
                    result = cats.ToDataSourceResult(request);

                var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
                return jsonResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(null);
            }
        }

        //[AccessLevelFilter(Action = AccessLevelEnum.Read, TableName = "tbl_AssetCategories")]
        public async Task<ActionResult> AssetCategoryDocumentTypeMaps_ReadByID([DataSourceRequest] DataSourceRequest request, string id = "")
        {
            try
            {
                List<Guid> allparents = null;
                if (id != "" && id != "00000000-0000-0000-0000-000000000000")
                    allparents = await PartialService.GetAllParentAssetCategoriesByID(id);

                DataSourceResult result;
                IEnumerable<AssetCategoryDocumentTypeMapModel> cats = await AssetCategoryDocumentTypeMapsService.ReadAssetCategoryDocumentTypeMaps();

                if (allparents != null && allparents.Count > 0)
                    result = cats.Where(x => allparents.Contains(x.DocTypeAssetCategoryID)).ToDataSourceResult(request);
                else
                    result = cats.ToDataSourceResult(request);

                var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
                return jsonResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(null);
            }
        }

        public async Task<ActionResult> AssetCategoryOperationalIndicatorsMaps_ReadByID([DataSourceRequest] DataSourceRequest request, string id = "")
        {
            try
            {
                List<Guid> allparents = null;
                if (id != "" && id != "00000000-0000-0000-0000-000000000000")
                    allparents = await PartialService.GetAllParentAssetCategoriesByID(id);

                DataSourceResult result;
                IEnumerable<AssetCategoryOperationIndicatorsMapModel> cats = await AssetCategoryOperationIndicatorsMapsService.ReadAssetCategoryOperationIndicatorsMaps();

                if (allparents != null && allparents.Count > 0)
                    result = cats.Where(x => allparents.Contains(x.OperIndAssetCategoryID)).ToDataSourceResult(request);
                else
                    result = cats.ToDataSourceResult(request);

                var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
                return jsonResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(null);
            }
        }

        public async Task<ActionResult> AssetCategoryServiceIntervalsMaps_ReadByID([DataSourceRequest] DataSourceRequest request, string id = "")
        {
            try
            {
                List<Guid> allparents = null;
                if (id != "" && id != "00000000-0000-0000-0000-000000000000")
                    allparents = await PartialService.GetAllParentAssetCategoriesByID(id);

                DataSourceResult result;
                IEnumerable<AssetCategoryServiceIntervalsMapModel> cats = await AssetCategoryServiceIntervalsMapsService.ReadAssetCategoryServiceIntervalsMaps();

                if (allparents != null && allparents.Count > 0)
                    result = cats.Where(x => allparents.Contains(x.ServIntAssetCategoryID)).ToDataSourceResult(request);
                else
                    result = cats.ToDataSourceResult(request);

                var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
                return jsonResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(null);
            }
        }

        //AssetCategoriesPartial

        public async Task<ActionResult> AssetCategoriesPartial(string directory)
        {
            try
            {
                //int permis = CurrentUser.UserModel.Role.TblAssetCategories;
                //ViewData["AssetCategoryPermission"] = permis;
                return PartialView("AssetCategoriesPartial");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(null);
            }

        }
    }
}