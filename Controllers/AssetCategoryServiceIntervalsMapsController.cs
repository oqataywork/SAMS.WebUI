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
	public partial class AssetCategoryServiceIntervalsMapsController : BaseController
	{

        public ActionResult Index()
        {
            return View();
        }

        [AccessLevelFilter(Action = AccessLevelEnum.Read, TableName = "tbl_AssetCategoryServiceIntervalsMaps")]
        public async Task<ActionResult> AssetCategoryServiceIntervalsMaps_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                IEnumerable<AssetCategoryServiceIntervalsMapModel> cats =await AssetCategoryServiceIntervalsMapsService.ReadAssetCategoryServiceIntervalsMaps();

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

   
        public AssetCategoryServiceIntervalsMapModel GetAssetCategoryServiceIntervalsMapByID(Guid id )
        {
            AssetCategoryServiceIntervalsMapModel mod = null;
            try
            {
               mod = AssetCategoryServiceIntervalsMapsService.OneAssetCategoryServiceIntervalsMap(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                
            }
            return mod;
        }


        public ActionResult GetAssetCategoryServiceIntervalsMapReadForm( string id)
        {

            try
            {
                AssetCategoryServiceIntervalsMapModel mod = GetAssetCategoryServiceIntervalsMapByID(new Guid(id));
                if(mod!=null)
                {
                    ViewBag.AssetCategoryServiceIntervalID = mod.AssetCategoryServiceIntervalID;
                    return PartialView("AssetCategoryServiceIntervalsMapReadFormPartial", mod);
                }
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult GetAssetCategoryServiceIntervalsMapEditForm(string id)
        {

            try
            {
                AssetCategoryServiceIntervalsMapModel mod = GetAssetCategoryServiceIntervalsMapByID(new Guid(id));
                if (mod != null)
                {
                    ViewBag.AssetCategoryServiceIntervalID = mod.AssetCategoryServiceIntervalID;
                    return PartialView("AssetCategoryServiceIntervalsMapEditFormPartial", mod);
                }
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }


        public ActionResult GetAssetCategoryServiceIntervalsMapNewForm()
        {

            try
            {
                AssetCategoryServiceIntervalsMapModel mod = new AssetCategoryServiceIntervalsMapModel();
                if (mod != null)
                {
                    ViewBag.AssetCategoryServiceIntervalID = mod.AssetCategoryServiceIntervalID;
                    return PartialView("AssetCategoryServiceIntervalsMapEditFormPartial", mod);
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
        [AccessLevelFilter(Action = AccessLevelEnum.Insert, TableName = "tbl_AssetCategoryServiceIntervalsMaps")]
        public async Task<ActionResult> AssetCategoryServiceIntervalsMaps_Create([DataSourceRequest] DataSourceRequest request, AssetCategoryServiceIntervalsMapModel mod)
        {
            string _err = "";
            try
            {

                if (mod == null) _err = "AssetCategoryServiceIntervalsMap is null";
                if (mod.AssetCategoryServiceIntervalID != null && mod.AssetCategoryServiceIntervalID.ToString() != "00000000-0000-0000-0000-000000000000") _err = "AssetCategoryServiceIntervalsMap is not new";

                if (!ModelState.IsValid)
                {
                    var errMsg = ModelState.Values.Where(x => x.Errors.Count >= 1);
                    string errorstr = "";
                    foreach (var item in errMsg)
                    {
                        if (item.Errors[0].ErrorMessage != "The AssetCategoryServiceIntervalID field is required.")
                            errorstr += " " + item.Errors[0].ErrorMessage;
                    }


                    if (errorstr==null || errorstr.Trim() == "")
                        ModelState.Clear();

                }


                if (_err != "")
                    ModelState.AddModelError("exception", _err);

                if (ModelState.IsValid)
                {

                    ObjectResponse resp = await AssetCategoryServiceIntervalsMapsService.CreateAssetCategoryServiceIntervalsMap(mod);
                    if (resp.Response != null)
                    {
                        mod = (AssetCategoryServiceIntervalsMapModel)resp.Response;
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
        [AccessLevelFilter(Action = AccessLevelEnum.Edit, TableName = "tbl_AssetCategoryServiceIntervalsMaps")]
        public async Task<ActionResult> AssetCategoryServiceIntervalsMaps_Update([DataSourceRequest] DataSourceRequest request, AssetCategoryServiceIntervalsMapModel mod)
        {
            try
            {
                string _err = "";
                AssetCategoryServiceIntervalsMapModel _updated;

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
                        ObjectResponse resp = await AssetCategoryServiceIntervalsMapsService.UpdateAssetCategoryServiceIntervalsMap(mod);
                        if (resp.Response != null)
                        {
                            _updated = (AssetCategoryServiceIntervalsMapModel)resp.Response;
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
        [AccessLevelFilter(Action = AccessLevelEnum.Edit, TableName = "tbl_AssetCategoryServiceIntervalsMaps")]
        public async Task<ActionResult> AssetCategoryServiceIntervalsMaps_Create_or_Update([DataSourceRequest] DataSourceRequest request, AssetCategoryServiceIntervalsMapModel mod)
        {
            try
            {
                string _err = "";
                AssetCategoryServiceIntervalsMapModel _updated;

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
                        if (mod.AssetCategoryServiceIntervalID==Guid.Empty)
                            resp = await AssetCategoryServiceIntervalsMapsService.CreateAssetCategoryServiceIntervalsMap(mod);      
                        else
                            resp = await AssetCategoryServiceIntervalsMapsService.UpdateAssetCategoryServiceIntervalsMap(mod);

                        if (resp.Response != null)
                            mod = (AssetCategoryServiceIntervalsMapModel)resp.Response;
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
        [AccessLevelFilter(Action = AccessLevelEnum.Delete, TableName = "tbl_AssetCategoryServiceIntervalsMaps")]
        public async Task<ActionResult> AssetCategoryServiceIntervalsMaps_Destroy([DataSourceRequest] DataSourceRequest request, AssetCategoryServiceIntervalsMapModel mod)
        {
            try
            {
                string _err = "";
                
                ObjectResponse resp = await AssetCategoryServiceIntervalsMapsService.RemoveAssetCategoryServiceIntervalsMap(mod.AssetCategoryServiceIntervalID);
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

