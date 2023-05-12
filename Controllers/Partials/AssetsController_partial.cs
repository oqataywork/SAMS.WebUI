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
using SAMS.Core.ViewModels;
using SAMS.WebUI.Services.Partials;

namespace SAMS.WebUI.Controllers
{
    public partial class AssetsController : BaseController
	{

        public ActionResult AjaxContent(String assetid)
        {
            if (assetid != null && assetid != "")
            {
                ViewData["AssetID"] = assetid;
                return PartialView("AssetConstructorPartial");
            }

            return null;
        }

        //[OutputCache(Duration = 60, VaryByParam = "AssetID")]
        public async Task<JsonResult> GetAssetTree([DataSourceRequest] DataSourceRequest request, string assetid)
        {
            if (assetid != null && assetid != "")
            {
                //List<AssetModel> assets = (await AssetsService.ReadAssets()).ToList();
                //AssetModel asset = assets.Where(x => x.AssetID == new Guid(AssetID))
                //    .FirstOrDefault();
                //if (asset == null) return null;
                //IEnumerable<AssetTypeConnetAssetViewModel> list = AssetTypeRelationsService.GetAllAsssetTypeChildrenByID(asset.AssetTypeID) as IEnumerable<AssetTypeConnetAssetViewModel>;

                //AssetTypeConnetAssetViewModel root = list.Where(x => x.AssetTypeID == asset.AssetTypeID)
                //    .FirstOrDefault();

                //if (root == null)
                //{
                //    return null;
                //}

                //root.Asset = asset;
                IEnumerable<AssetTypeConnetAssetViewModel> list = await AssetsService.GetAssetsTreeWithAssetTypes(new Guid(assetid));
                var result = list.ToTreeDataSourceResult(request,
                    e => e.ID,
                    e => e.ParentID
                );
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        public async Task<JsonResult> GetAsssetTreeByID([DataSourceRequest] DataSourceRequest request, string assetid, bool isSourceUpdated=false)
        {
            if (assetid != null && assetid != "")
            {
                IEnumerable<AssetTypeConnetAssetViewModel> list = await AssetsService.GetAsssetTreeByID(new Guid(assetid));
                var result = list.ToTreeDataSourceResult(request,
                    e => e.ID,
                    e => e.ParentID
                );
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        public async Task<JsonResult> NotAsignedAssets([DataSourceRequest] DataSourceRequest request, string assetid)
        {

            try
            {

                IEnumerable<AssetViewModel> lst = await AssetsService.GetNotAssignedAssets(new Guid(assetid));

                var result = lst.ToTreeDataSourceResult(request,
                    e => e.IDStr,
                    e => e.DummyParentID,
                    e => e
                );

                var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;

            }
            catch (System.Exception ex)
            {

                throw;
            }


        }

        public async Task<ActionResult> NotRegisteredAssets(string assetid)
        {
            try
            {
                Guid assetGuid = Guid.Empty;
                if (assetid != null && assetid != "")
                    assetGuid = new Guid(assetid);
  
                List<Guid> availguids =  Entities.Services.AssetsService.GetAllNotRegisteredAssetsIncludeID(assetGuid).ToList();
                IEnumerable<AssetTypeModel> cats = await AssetTypesService.ReadAssetTypes();
                var res = cats.Where(x=> availguids.Contains(x.AssetTypeID)).Select(x=> new { AssetTypeID =x.AssetTypeID , AssetTypeName =x.AssetTypeName}).ToList();

                return Json(res, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(null);
            }
        }

        public async Task<ActionResult> AssetAttributeMaps_ReadByID([DataSourceRequest] DataSourceRequest request, string assetid = "")
        {
            try
            {

                IEnumerable<AssetAttributeMapModel> cats = await AssetAttributeMapsService.ReadAssetAttributeMaps();
                DataSourceResult result = cats.Where(x=>x.AssetID== new Guid(assetid)).ToDataSourceResult(request);
                var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(null);
            }
        }

        public async Task<ActionResult> AssetCharacteristicsMaps_ReadByID([DataSourceRequest] DataSourceRequest request, string assetid = "")
        {
            try
            {

                IEnumerable<AssetCharacteristicModel> cats = await AssetCharacteristicsService.ReadAssetCharacteristics();
                DataSourceResult result = cats.Where(x => x.AssetID == new Guid(assetid)).ToDataSourceResult(request);
                var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(null);
            }
        }

        public async Task<ActionResult> AssetOperationalIndicatorsMaps_ReadByID([DataSourceRequest] DataSourceRequest request, string assetid = "")
        {
            try
            {

                IEnumerable<AssetOperationIndicatorsMapModel> cats = await AssetOperationIndicatorsMapsService.ReadAssetOperationIndicatorsMaps();
                DataSourceResult result = cats.Where(x => x.AssetID == new Guid(assetid)).ToDataSourceResult(request);
                var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(null);
            }
        }

        public async Task<ActionResult> AssetServiceIntervalsMaps_ReadByID([DataSourceRequest] DataSourceRequest request, string assetid = "")
        {
            try
            {

                IEnumerable<AssetServiceIntervalModel> cats = await AssetServiceIntervalsService.ReadAssetServiceIntervals();
                DataSourceResult result = cats.Where(x => x.AssetID == new Guid(assetid)).ToDataSourceResult(request);
                var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(null);
            }
        }

        public async Task<ActionResult> AssetDocuments_ReadByID([DataSourceRequest] DataSourceRequest request, string assetid = "")
        {
            try
            {

                IEnumerable<AssetDocumentModel> cats = await AssetDocumentsService.ReadAssetDocuments();
                DataSourceResult result = cats.Where(x => x.AssetID == new Guid(assetid)).ToDataSourceResult(request);
                var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(null);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [AccessLevelFilter(Action = AccessLevelEnum.Insert, TableName = "tbl_Assets")]
        public async Task<ActionResult> Assets_New([DataSourceRequest] DataSourceRequest request, AssetModel mod)
        {
            string _err = "";
            try
            {

                if (mod == null) _err = "Asset is null";
                if (mod.AssetID != null && mod.AssetID.ToString() != "00000000-0000-0000-0000-000000000000") _err = "Asset is not new";

                if (!ModelState.IsValid)
                {
                    var errMsg = ModelState.Values.Where(x => x.Errors.Count >= 1);
                    string errorstr = "";
                    foreach (var item in errMsg)
                    {
                        if (item.Errors[0].ErrorMessage != "The AssetID field is required.")
                            errorstr += " " + item.Errors[0].ErrorMessage;
                    }


                    if (errorstr == null || errorstr.Trim() == "")
                        ModelState.Clear();

                }


                if (_err != "")
                    ModelState.AddModelError("exception", _err);

                if (ModelState.IsValid)
                {

                    ObjectResponse resp = await AssetsService.CreateAsset(mod);
                    if (resp.Response != null)
                    {
                        mod = (AssetModel)resp.Response;
                        ShouldRefreshService.SetShouldRefreshAssetAttributeMaps(true);
                        ShouldRefreshService.SetShouldRefreshAssetCharacteristics(true);
                        ShouldRefreshService.SetShouldRefreshAssetDocuments(true);
                        ShouldRefreshService.SetShouldRefreshAssetOperationIndicatorsMaps(true);
                        ShouldRefreshService.SetShouldRefreshAssetServiceIntervals(true);

                    }
                    else
                        ModelState.AddModelError("exception", resp.error);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("exception", ex.Message);
            }
            return Json(new[] { mod }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [AccessLevelFilter(Action = AccessLevelEnum.Edit, TableName = "tbl_Assets")]
        public async Task<ActionResult> Assets_New_or_Update([DataSourceRequest] DataSourceRequest request, AssetModel mod)
        {
            try
            {
                string _err = "";
                AssetModel _updated;

                if (mod != null)
                {
                    if (!ModelState.IsValid)
                    {

                        if (ModelState["CreateDate"] != null && ModelState["CreateDate"].Errors != null)
                        {
                            var createdateerrors = ModelState["CreateDate"].Errors.ToList();
                            foreach (var error in createdateerrors)
                            {
                                ModelState["CreateDate"].Errors.Remove(error);
                            }
                        }

                        if (ModelState["ChangeDate"] != null && ModelState["ChangeDate"].Errors != null)
                        {
                            var createdateerrors = ModelState["CreateDate"].Errors.ToList();
                            foreach (var error in createdateerrors)
                            {
                                ModelState["ChangeDate"].Errors.Remove(error);
                            }
                        }

                    }


                    if (ModelState.IsValid)
                    {
                        ObjectResponse resp;
                        if (mod.AssetID == Guid.Empty)
                            resp = await AssetsService.CreateAsset(mod);
                        else
                            resp = await AssetsService.UpdateAsset(mod);

                        if (resp.Response != null)
                        {
                            mod = (AssetModel)resp.Response;
                            ShouldRefreshService.SetShouldRefreshAssetAttributeMaps(true);
                            ShouldRefreshService.SetShouldRefreshAssetCharacteristics(true);
                            ShouldRefreshService.SetShouldRefreshAssetDocuments(true);
                            ShouldRefreshService.SetShouldRefreshAssetOperationIndicatorsMaps(true);
                            ShouldRefreshService.SetShouldRefreshAssetServiceIntervals(true);
                        }
                        else
                            ModelState.AddModelError("exception", resp.error);
                    }
                    else
                    {
                        var errMsg = ModelState.Where(x => x.Value.Errors.Count >= 1);
                        string _errors = "";
                        foreach (var item in errMsg)
                            _errors += ("\n" + item.Value.Errors[0].ErrorMessage);

                        ModelState.AddModelError("exception", _errors);
                    }
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("exception", ex.Message);
            }
            return Json(new[] { mod }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        [AccessLevelFilter(Action = AccessLevelEnum.Insert, TableName = "tbl_Assets")]
        public async Task<ActionResult> AddNewAssetEmplacement(decimal quantity, string toassetID, string departmentID, string unitTypeID, string assetID, string assetTypeRelationID)
        {
            AssetEmplacementModel res = null;
            string _err = "";
            try
            {

                AssetEmplacementModel nmod;
                Guid _assetTypeRelationID;
                if (assetTypeRelationID != null && assetTypeRelationID != "")
                    _assetTypeRelationID = new Guid(assetTypeRelationID);
                else
                {
                    _assetTypeRelationID = Guid.Empty;
                }
                if (unitTypeID!=null && unitTypeID!="")
                    nmod = new AssetEmplacementModel { Quantity = quantity, EmplacementID = new Guid(toassetID), DepartmentID = new Guid(departmentID), AssetID = new Guid(assetID), EmplacementType = 1, UnitTypeID = new Guid(unitTypeID), AssetTypeRelationID = _assetTypeRelationID };
                else
                {
                    nmod = new AssetEmplacementModel { Quantity = quantity, EmplacementID = new Guid(toassetID), DepartmentID = new Guid(departmentID), AssetID = new Guid(assetID), EmplacementType = 1, AssetTypeRelationID = _assetTypeRelationID };
                }

                ObjectResponse resp = await AssetEmplacementsService.CreateAssetEmplacement(nmod);
                if (resp.Response != null)
                {
                    res = (AssetEmplacementModel)resp.Response;
                }
                else
                    ModelState.AddModelError("exception", resp.error);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("exception", ex.Message);
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AccessLevelFilter(Action = AccessLevelEnum.Insert, TableName = "tbl_Assets")]
        public async Task<ActionResult> AddNewOrUpdateAssetEmplacement(decimal quantity, string toassetID, string departmentID, string unitTypeID, string assetID, string assetTypeRelationID)
        {
            AssetEmplacementModel res = null;
            string _err = "";
            try
            {

                AssetEmplacementModel nmod;
                Guid _assetTypeRelationID;

                List<AssetEmplacementModel> empllist = (await AssetEmplacementsService.ReadAssetEmplacements()).ToList();

                AssetEmplacementModel esistempl = empllist.Where(x => x.AssetID == new Guid(assetID) && x.EmplacementID == new Guid(assetID)).FirstOrDefault();

                if (assetTypeRelationID != null && assetTypeRelationID != "")
                    _assetTypeRelationID = new Guid(assetTypeRelationID);
                else
                {
                    _assetTypeRelationID = Guid.Empty;
                }

                ObjectResponse resp;
                if (esistempl == null)
                {
                    if (unitTypeID != null && unitTypeID != "")
                        nmod = new AssetEmplacementModel { Quantity = quantity, EmplacementID = new Guid(toassetID), DepartmentID = new Guid(departmentID), AssetID = new Guid(assetID), EmplacementType = 1, UnitTypeID = new Guid(unitTypeID), AssetTypeRelationID = _assetTypeRelationID };
                    else
                    {
                        nmod = new AssetEmplacementModel { Quantity = quantity, EmplacementID = new Guid(toassetID), DepartmentID = new Guid(departmentID), AssetID = new Guid(assetID), EmplacementType = 1, AssetTypeRelationID = _assetTypeRelationID };
                    }
                    resp = await AssetEmplacementsService.CreateAssetEmplacement(nmod);
                }
                else
                {
                    esistempl.Quantity = quantity;
                    esistempl.EmplacementID = new Guid(toassetID);
                    esistempl.DepartmentID = new Guid(departmentID);
                    esistempl.EmplacementType = 1;
                    esistempl.AssetTypeRelationID = _assetTypeRelationID;
                    resp = await AssetEmplacementsService.UpdateAssetEmplacement(esistempl);
                }

                if (resp.Response != null)
                {
                    return Json(new { success = true, responseText = "Əməliyyat uğurla tamamlandı." }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(new { success = false, responseText = resp.error }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            
        }

        [HttpPost]
        public async Task<ActionResult> DeleteAssetRelation(string assetID, string assetTypeRelationID)
        {
            AssetTypeRelationModel res = null;
            string _err = "";
            try
            {

                ObjectResponse resp = await AssetsService.RemoveAssetEmplacemnt(new Guid(assetID),new Guid(assetTypeRelationID));
                if (!(bool)resp.Response)
                {
                    ModelState.AddModelError("exception", resp.error);
                }
                else
                {
                    ModelState.Clear();
                    return Json(true);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("exception", ex.Message);
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}

