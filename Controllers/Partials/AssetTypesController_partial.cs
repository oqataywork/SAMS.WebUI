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
using SAMS.Context;
using SAMS.Core.ViewModels;
using System.Net;

namespace SAMS.WebUI.Controllers
{

//[Attributes.IsAuthenticated(Order = 0), Attributes.AuthorizeUserType(Order = 1)]
	public partial class AssetTypesController : BaseController
	{
        public ActionResult AjaxContent(String assetTypeID)
        {
            if(assetTypeID != null && assetTypeID != "")
            {
                ViewData["AssetTypeID"] = assetTypeID;
                return PartialView("AssetTypeConstructorPartial");
            }
                
            return null;
        }

        //[OutputCache(Duration = 60, VaryByParam = "AssetTypeID;isSourceUpdated")]
        //[OutputCache(Duration = 30, VaryByParam = "AssetTypeID")]
        //[IgnoreCasheAttribute]
        public JsonResult GetTree([DataSourceRequest] DataSourceRequest request, string assettypeid, bool isSourceUpdated=false)
        {
            if(assettypeid != null && assettypeid !="")
            {
                var result = AssetTypeRelationsService.GetAllAsssetTypeChildrenByID(new Guid(assettypeid)).ToTreeDataSourceResult(request,
                e => e.ID,
                e => e.ParentID
            );
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        //[OutputCache(Duration = 60)]
        public async Task<JsonResult> All([DataSourceRequest] DataSourceRequest request)
        {

            try
            {
                IEnumerable<AssetTypeModel> lst = await AssetTypesService.ReadAssetTypes();

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

        [HttpPost]
        [AccessLevelFilter(Action = AccessLevelEnum.Insert, TableName = "tbl_AssetTypeRelations")]
        public ActionResult CloneAssetType(string assetTypeID, string newAssetTypeName)
        {

            string _err = "";
            try
            {

                AssetTypeModel exdAssType = AssetTypesService.ReadAssetTypes().Result.Where(x => x.AssetTypeName.ToLower() == newAssetTypeName.ToLower()).FirstOrDefault();
                if (exdAssType != null)
                    return Json(new { success = false, responseText = "Bu adlı aktiv növü artıq mövcuddur." }, JsonRequestBehavior.AllowGet);

                ObjectResponse resp = AssetTypeRelationsService.CloneAssetType(new Guid(assetTypeID),  newAssetTypeName).Result;
                if (resp.Response != null)
                {
                    return Json(new { success = true, responseText = "Əməliyyat uğurla tamamlandı." }, JsonRequestBehavior.AllowGet);
                    //res = (AssetTypeRelationModel)resp.Response;
                }
                else
                    return Json(new { success = false, responseText = resp.error }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = ex.Message }, JsonRequestBehavior.AllowGet);
                //ModelState.AddModelError("exception", ex.Message);
            }

        }


        [HttpPost]
        [AccessLevelFilter(Action = AccessLevelEnum.Insert, TableName = "tbl_AssetTypeRelations")]
        public ActionResult AddNewAssetTypeRelation(string assetTypeID, string childAssetTypeID, string unitTypeID, decimal unitValue, string rootAssetTypeID)
        {
            AssetTypeRelationModel res = null;
            string _err = "";
            try
            {
                if (assetTypeID == childAssetTypeID)
                    return Json(new { success = false, responseText = "The child can not be same." }, JsonRequestBehavior.AllowGet);


                AssetTypeModel childAssType = AssetTypesService.ReadAssetTypes().Result.Where(x => x.AssetTypeID == new Guid(childAssetTypeID)).FirstOrDefault();
                if (childAssType == null)
                    return Json(new { success = false, responseText = "The child asset type not found." }, JsonRequestBehavior.AllowGet);


                if (!childAssType.SingleInstance)
                {
                    AssetTypeRelationModel exist = AssetTypeRelationsService.ReadAssetTypeRelations().Result.Where(x => x.AssetTypeID == new Guid(assetTypeID) && x.ChildAssetTypeID == new Guid(childAssetTypeID)).FirstOrDefault();
                    if (exist != null)
                        return Json(new { success = false, responseText = "Bu bağlantı artıq mövcuddur, sayını dəyişə bilərsiniz." }, JsonRequestBehavior.AllowGet);


                }
                //

                AssetTypeViewModel nmod = new AssetTypeViewModel { AssetTypeID = new Guid(assetTypeID), ChildAssetTypeID = new Guid(childAssetTypeID), UnitTypeID = new Guid(unitTypeID), UnitValue = unitValue, RootAssetTypeID = new Guid(rootAssetTypeID) };

                ObjectResponse resp = AssetTypeRelationsService.AddNewAssetTypeRelation(nmod).Result;
                if (resp.Response != null)
                {
                    return Json(new { success = true, responseText = "Əməliyyat uğurla tamamlandı." }, JsonRequestBehavior.AllowGet);
                    //res = (AssetTypeRelationModel)resp.Response;
                }
                else
                    return Json(new { success = false, responseText = resp.error }, JsonRequestBehavior.AllowGet);


                return Json(res, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = ex.Message }, JsonRequestBehavior.AllowGet);
                //ModelState.AddModelError("exception", ex.Message);
            }

        }

        [HttpPost]
        [AccessLevelFilter(Action = AccessLevelEnum.Insert, TableName = "tbl_AssetTypeRelations")]
        public ActionResult UpdateAssetTypeRelation(string assetTypeRelationID, string unitTypeID, decimal unitValue)
        {
            AssetTypeRelationModel res = null;
            string _err = "";
            try
            {

                AssetTypeViewModel nmod = new AssetTypeViewModel { AssetTypeRelationID = new Guid(assetTypeRelationID),  UnitTypeID = new Guid(unitTypeID), UnitValue = unitValue };

                ObjectResponse resp = AssetTypeRelationsService.UpdateUnitInAssetTypeRelation(nmod).Result;
                if (resp.Response != null)
                {
                    res = (AssetTypeRelationModel)resp.Response;
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
        public async Task<ActionResult> DeleteAssetTypeRelation(string assetTypeRelationID)
        {
            AssetTypeRelationModel res = null;
            string _err = "";
            try
            {

                ObjectResponse resp = await AssetTypeRelationsService.RemoveAssetTypeRelation(new Guid(assetTypeRelationID));
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

        public JsonResult GetImage(Guid assetTypeGuid)
        {
            AssetTypeImageModel mod = null;

            try
            {
                mod = AssetTypeImagesService.OneAssetTypeImage(assetTypeGuid);
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
                //Guid assetTypeid = new Guid(id);


                AssetTypeImageModel _roomimage = new AssetTypeImageModel { AssetTypeImageID = new Guid(id) };
                _roomimage.AssetTypeImageBytes = imageData;

                ObjectResponse resp = await AssetTypeRelationsService.UpdateOrInsertAssetTypeImage(_roomimage);
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
                //if (resp.Response != null)
                //{
                //    AssetTypeImageModel added = (AssetTypeImageModel)resp.Response;
                //    return Json(added);
                //}
                //else
                //{
                //    Response.StatusCode = (int)HttpStatusCode.NotModified;
                //    List<string> errors = new List<string>();
                //    errors.Add(resp.error);
                //    return Json(errors);
                //}

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

