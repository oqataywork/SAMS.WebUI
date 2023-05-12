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
	public partial class AssetOperationIndicatorsMapsController : BaseController
	{

        public ActionResult Index()
        {
            return View();
        }

        [AccessLevelFilter(Action = AccessLevelEnum.Read, TableName = "tbl_AssetOperationIndicatorsMaps")]
        public async Task<ActionResult> AssetOperationIndicatorsMaps_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                IEnumerable<AssetOperationIndicatorsMapModel> cats =await AssetOperationIndicatorsMapsService.ReadAssetOperationIndicatorsMaps();

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

   
        public AssetOperationIndicatorsMapModel GetAssetOperationIndicatorsMapByID(Guid id )
        {
            AssetOperationIndicatorsMapModel mod = null;
            try
            {
               mod = AssetOperationIndicatorsMapsService.OneAssetOperationIndicatorsMap(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                
            }
            return mod;
        }


        public ActionResult GetAssetOperationIndicatorsMapReadForm( string id)
        {

            try
            {
                AssetOperationIndicatorsMapModel mod = GetAssetOperationIndicatorsMapByID(new Guid(id));
                if(mod!=null)
                {
                    ViewBag.AssetOperationIndicatorID = mod.AssetOperationIndicatorID;
                    return PartialView("AssetOperationIndicatorsMapReadFormPartial", mod);
                }
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult GetAssetOperationIndicatorsMapEditForm(string id)
        {

            try
            {
                AssetOperationIndicatorsMapModel mod = GetAssetOperationIndicatorsMapByID(new Guid(id));
                if (mod != null)
                {
                    ViewBag.AssetOperationIndicatorID = mod.AssetOperationIndicatorID;
                    return PartialView("AssetOperationIndicatorsMapEditFormPartial", mod);
                }
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }


        public ActionResult GetAssetOperationIndicatorsMapNewForm()
        {

            try
            {
                AssetOperationIndicatorsMapModel mod = new AssetOperationIndicatorsMapModel();
                if (mod != null)
                {
                    ViewBag.AssetOperationIndicatorID = mod.AssetOperationIndicatorID;
                    return PartialView("AssetOperationIndicatorsMapEditFormPartial", mod);
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
        [AccessLevelFilter(Action = AccessLevelEnum.Insert, TableName = "tbl_AssetOperationIndicatorsMaps")]
        public async Task<ActionResult> AssetOperationIndicatorsMaps_Create([DataSourceRequest] DataSourceRequest request, AssetOperationIndicatorsMapModel mod)
        {
            string _err = "";
            try
            {

                if (mod == null) _err = "AssetOperationIndicatorsMap is null";
                if (mod.AssetOperationIndicatorID != null && mod.AssetOperationIndicatorID.ToString() != "00000000-0000-0000-0000-000000000000") _err = "AssetOperationIndicatorsMap is not new";

                if (!ModelState.IsValid)
                {
                    var errMsg = ModelState.Values.Where(x => x.Errors.Count >= 1);
                    string errorstr = "";
                    foreach (var item in errMsg)
                    {
                        if (item.Errors[0].ErrorMessage != "The AssetOperationIndicatorID field is required.")
                            errorstr += " " + item.Errors[0].ErrorMessage;
                    }


                    if (errorstr==null || errorstr.Trim() == "")
                        ModelState.Clear();

                }


                if (_err != "")
                    ModelState.AddModelError("exception", _err);

                if (ModelState.IsValid)
                {

                    ObjectResponse resp = await AssetOperationIndicatorsMapsService.CreateAssetOperationIndicatorsMap(mod);
                    if (resp.Response != null)
                    {
                        mod = (AssetOperationIndicatorsMapModel)resp.Response;
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
        [AccessLevelFilter(Action = AccessLevelEnum.Edit, TableName = "tbl_AssetOperationIndicatorsMaps")]
        public async Task<ActionResult> AssetOperationIndicatorsMaps_Update([DataSourceRequest] DataSourceRequest request, AssetOperationIndicatorsMapModel mod)
        {
            try
            {
                string _err = "";
                AssetOperationIndicatorsMapModel _updated;

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
                        ObjectResponse resp = await AssetOperationIndicatorsMapsService.UpdateAssetOperationIndicatorsMap(mod);
                        if (resp.Response != null)
                        {
                            _updated = (AssetOperationIndicatorsMapModel)resp.Response;
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
        [AccessLevelFilter(Action = AccessLevelEnum.Edit, TableName = "tbl_AssetOperationIndicatorsMaps")]
        public async Task<ActionResult> AssetOperationIndicatorsMaps_Create_or_Update([DataSourceRequest] DataSourceRequest request, AssetOperationIndicatorsMapModel mod)
        {
            try
            {
                string _err = "";
                AssetOperationIndicatorsMapModel _updated;

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
                        if (mod.AssetOperationIndicatorID==Guid.Empty)
                            resp = await AssetOperationIndicatorsMapsService.CreateAssetOperationIndicatorsMap(mod);      
                        else
                            resp = await AssetOperationIndicatorsMapsService.UpdateAssetOperationIndicatorsMap(mod);

                        if (resp.Response != null)
                            mod = (AssetOperationIndicatorsMapModel)resp.Response;
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
        [AccessLevelFilter(Action = AccessLevelEnum.Delete, TableName = "tbl_AssetOperationIndicatorsMaps")]
        public async Task<ActionResult> AssetOperationIndicatorsMaps_Destroy([DataSourceRequest] DataSourceRequest request, AssetOperationIndicatorsMapModel mod)
        {
            try
            {
                string _err = "";
                
                ObjectResponse resp = await AssetOperationIndicatorsMapsService.RemoveAssetOperationIndicatorsMap(mod.AssetOperationIndicatorID);
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

