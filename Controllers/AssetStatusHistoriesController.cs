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
	public partial class AssetStatusHistoriesController : BaseController
	{

        public ActionResult Index()
        {
            return View();
        }

        [AccessLevelFilter(Action = AccessLevelEnum.Read, TableName = "tbl_AssetStatusHistories")]
        public async Task<ActionResult> AssetStatusHistories_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                IEnumerable<AssetStatusHistoryModel> cats =await AssetStatusHistoriesService.ReadAssetStatusHistorys();

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

   
        public AssetStatusHistoryModel GetAssetStatusHistoryByID(Guid id )
        {
            AssetStatusHistoryModel mod = null;
            try
            {
               mod = AssetStatusHistoriesService.OneAssetStatusHistory(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                
            }
            return mod;
        }


        public ActionResult GetAssetStatusHistoryReadForm( string id)
        {

            try
            {
                AssetStatusHistoryModel mod = GetAssetStatusHistoryByID(new Guid(id));
                if(mod!=null)
                {
                    ViewBag.AssetStatusHistoryID = mod.AssetStatusHistoryID;
                    return PartialView("AssetStatusHistoryReadFormPartial", mod);
                }
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult GetAssetStatusHistoryEditForm(string id)
        {

            try
            {
                AssetStatusHistoryModel mod = GetAssetStatusHistoryByID(new Guid(id));
                if (mod != null)
                {
                    ViewBag.AssetStatusHistoryID = mod.AssetStatusHistoryID;
                    return PartialView("AssetStatusHistoryEditFormPartial", mod);
                }
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }


        public ActionResult GetAssetStatusHistoryNewForm()
        {

            try
            {
                AssetStatusHistoryModel mod = new AssetStatusHistoryModel();
                if (mod != null)
                {
                    ViewBag.AssetStatusHistoryID = mod.AssetStatusHistoryID;
                    return PartialView("AssetStatusHistoryEditFormPartial", mod);
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
        [AccessLevelFilter(Action = AccessLevelEnum.Insert, TableName = "tbl_AssetStatusHistories")]
        public async Task<ActionResult> AssetStatusHistories_Create([DataSourceRequest] DataSourceRequest request, AssetStatusHistoryModel mod)
        {
            string _err = "";
            try
            {

                if (mod == null) _err = "AssetStatusHistory is null";
                if (mod.AssetStatusHistoryID != null && mod.AssetStatusHistoryID.ToString() != "00000000-0000-0000-0000-000000000000") _err = "AssetStatusHistory is not new";

                if (!ModelState.IsValid)
                {
                    var errMsg = ModelState.Values.Where(x => x.Errors.Count >= 1);
                    string errorstr = "";
                    foreach (var item in errMsg)
                    {
                        if (item.Errors[0].ErrorMessage != "The AssetStatusHistoryID field is required.")
                            errorstr += " " + item.Errors[0].ErrorMessage;
                    }


                    if (errorstr==null || errorstr.Trim() == "")
                        ModelState.Clear();

                }


                if (_err != "")
                    ModelState.AddModelError("exception", _err);

                if (ModelState.IsValid)
                {

                    ObjectResponse resp = await AssetStatusHistoriesService.CreateAssetStatusHistory(mod);
                    if (resp.Response != null)
                    {
                        mod = (AssetStatusHistoryModel)resp.Response;
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
        [AccessLevelFilter(Action = AccessLevelEnum.Edit, TableName = "tbl_AssetStatusHistories")]
        public async Task<ActionResult> AssetStatusHistories_Update([DataSourceRequest] DataSourceRequest request, AssetStatusHistoryModel mod)
        {
            try
            {
                string _err = "";
                AssetStatusHistoryModel _updated;

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
                        ObjectResponse resp = await AssetStatusHistoriesService.UpdateAssetStatusHistory(mod);
                        if (resp.Response != null)
                        {
                            _updated = (AssetStatusHistoryModel)resp.Response;
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
        [AccessLevelFilter(Action = AccessLevelEnum.Edit, TableName = "tbl_AssetStatusHistories")]
        public async Task<ActionResult> AssetStatusHistories_Create_or_Update([DataSourceRequest] DataSourceRequest request, AssetStatusHistoryModel mod)
        {
            try
            {
                string _err = "";
                AssetStatusHistoryModel _updated;

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
                        if (mod.AssetStatusHistoryID==Guid.Empty)
                            resp = await AssetStatusHistoriesService.CreateAssetStatusHistory(mod);      
                        else
                            resp = await AssetStatusHistoriesService.UpdateAssetStatusHistory(mod);

                        if (resp.Response != null)
                            mod = (AssetStatusHistoryModel)resp.Response;
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
        [AccessLevelFilter(Action = AccessLevelEnum.Delete, TableName = "tbl_AssetStatusHistories")]
        public async Task<ActionResult> AssetStatusHistories_Destroy([DataSourceRequest] DataSourceRequest request, AssetStatusHistoryModel mod)
        {
            try
            {
                string _err = "";
                
                ObjectResponse resp = await AssetStatusHistoriesService.RemoveAssetStatusHistory(mod.AssetStatusHistoryID);
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

