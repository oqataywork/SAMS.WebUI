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

namespace SAMS.WebUI.Controllers
{

[Attributes.IsAuthenticated(Order = 0), Attributes.AuthorizeUserType(Order = 1)]
	public partial class AssetCategoryAttributeMapsController : BaseController
	{

        public ActionResult Index()
        {
            return View();
        }

        [AccessLevelFilter(Action = AccessLevelEnum.Read, TableName = "tbl_AssetCategoryAttributeMaps")]
        public async Task<ActionResult> AssetCategoryAttributeMaps_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                IEnumerable<AssetCategoryAttributeMapModel> cats =await AssetCategoryAttributeMapsService.ReadAssetCategoryAttributeMaps();

                DataSourceResult result = cats.ToDataSourceResult(request);
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

   
        public AssetCategoryAttributeMapModel GetAssetCategoryAttributeMapByID(Guid id )
        {
            AssetCategoryAttributeMapModel mod = null;
            try
            {
               mod = AssetCategoryAttributeMapsService.OneAssetCategoryAttributeMap(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                
            }
            return mod;
        }


        public ActionResult GetAssetCategoryAttributeMapReadForm( string id)
        {

            try
            {
                AssetCategoryAttributeMapModel mod = GetAssetCategoryAttributeMapByID(new Guid(id));
                if(mod!=null)
                {
                    ViewBag.AssetCategoryAttributeMapID = mod.AssetCategoryAttributeMapID;
                    return PartialView("AssetCategoryAttributeMapReadFormPartial", mod);
                }
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult GetAssetCategoryAttributeMapEditForm(string id)
        {

            try
            {
                AssetCategoryAttributeMapModel mod = GetAssetCategoryAttributeMapByID(new Guid(id));
                if (mod != null)
                {
                    ViewBag.AssetCategoryAttributeMapID = mod.AssetCategoryAttributeMapID;
                    return PartialView("AssetCategoryAttributeMapEditFormPartial", mod);
                }
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }


        public ActionResult GetAssetCategoryAttributeMapNewForm()
        {

            try
            {
                AssetCategoryAttributeMapModel mod = new AssetCategoryAttributeMapModel();
                if (mod != null)
                {
                    ViewBag.AssetCategoryAttributeMapID = mod.AssetCategoryAttributeMapID;
                    return PartialView("AssetCategoryAttributeMapEditFormPartial", mod);
                }
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }



        [AcceptVerbs(HttpVerbs.Post)]
        [AccessLevelFilter(Action = AccessLevelEnum.Insert, TableName = "tbl_AssetCategoryAttributeMaps")]
        public async Task<ActionResult> AssetCategoryAttributeMaps_Create([DataSourceRequest] DataSourceRequest request, AssetCategoryAttributeMapModel mod)
        {
            string _err = "";
            try
            {

                if (mod == null) _err = "AssetCategoryAttributeMap is null";
                if (mod.AssetCategoryAttributeMapID != null && mod.AssetCategoryAttributeMapID.ToString() != "00000000-0000-0000-0000-000000000000") _err = "AssetCategoryAttributeMap is not new";

                if (!ModelState.IsValid)
                {
                    var errMsg = ModelState.Values.Where(x => x.Errors.Count >= 1);
                    string errorstr = "";
                    foreach (var item in errMsg)
                    {
                        if (item.Errors[0].ErrorMessage != "The AssetCategoryAttributeMapID field is required.")
                            errorstr += " " + item.Errors[0].ErrorMessage;
                    }


                    if (errorstr==null || errorstr.Trim() == "")
                        ModelState.Clear();

                }


                if (_err != "")
                    ModelState.AddModelError("exception", _err);

                if (ModelState.IsValid)
                {

                    ObjectResponse resp = await AssetCategoryAttributeMapsService.CreateAssetCategoryAttributeMap(mod);
                    if (resp.Response != null)
                    {
                        mod = (AssetCategoryAttributeMapModel)resp.Response;
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
        [AccessLevelFilter(Action = AccessLevelEnum.Edit, TableName = "tbl_AssetCategoryAttributeMaps")]
        public async Task<ActionResult> AssetCategoryAttributeMaps_Update([DataSourceRequest] DataSourceRequest request, AssetCategoryAttributeMapModel mod)
        {
            try
            {
                string _err = "";
                AssetCategoryAttributeMapModel _updated;

                if (mod != null)
                {
                    if(!ModelState.IsValid)
                    {

                        if(ModelState["CreateDate"]!=null && ModelState["CreateDate"].Errors!=null)
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
                        var errMsg = ModelState.Where(x => x.Value.Errors.Count >= 1);
                        string _errors = "";
                        foreach (var item in errMsg)
                        {
                            if(!String.IsNullOrEmpty(item.Value.Errors[0].ErrorMessage))
                            {
                                _errors += ("\n" + item.Value.Errors[0].ErrorMessage);
                            }
                                
                        }
                            
                        if(!String.IsNullOrEmpty(_errors))
                            ModelState.AddModelError("exception", _errors);
                        else
                            ModelState.Clear();
                    }


                    if (ModelState.IsValid)
                    {
                        ObjectResponse resp = await AssetCategoryAttributeMapsService.UpdateAssetCategoryAttributeMap(mod);
                        if (resp.Response != null)
                        {
                            _updated = (AssetCategoryAttributeMapModel)resp.Response;
                            mod = _updated;
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

        [AcceptVerbs(HttpVerbs.Post)]
        [AccessLevelFilter(Action = AccessLevelEnum.Edit, TableName = "tbl_AssetCategoryAttributeMaps")]
        public async Task<ActionResult> AssetCategoryAttributeMaps_Create_or_Update([DataSourceRequest] DataSourceRequest request, AssetCategoryAttributeMapModel mod)
        {
            try
            {
                string _err = "";
                AssetCategoryAttributeMapModel _updated;

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
                        if (mod.AssetCategoryAttributeMapID==Guid.Empty)
                            resp = await AssetCategoryAttributeMapsService.CreateAssetCategoryAttributeMap(mod);      
                        else
                            resp = await AssetCategoryAttributeMapsService.UpdateAssetCategoryAttributeMap(mod);

                        if (resp.Response != null)
                            mod = (AssetCategoryAttributeMapModel)resp.Response;
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


        [AcceptVerbs(HttpVerbs.Post)]
        [AccessLevelFilter(Action = AccessLevelEnum.Delete, TableName = "tbl_AssetCategoryAttributeMaps")]
        public async Task<ActionResult> AssetCategoryAttributeMaps_Destroy([DataSourceRequest] DataSourceRequest request, AssetCategoryAttributeMapModel mod)
        {
            try
            {
                string _err = "";
                
                ObjectResponse resp = await AssetCategoryAttributeMapsService.RemoveAssetCategoryAttributeMap(mod.AssetCategoryAttributeMapID);
                if (!(bool)resp.Response)
                {
                    ModelState.AddModelError("exception", resp.error);
                }     
                else
                {
                    ModelState.Clear();
                }
                        
                //}
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("exception", ex.Message);
            }
            return Json(new[] { mod }.ToDataSourceResult(request, ModelState));
        }
	}
}

