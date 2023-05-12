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
	public partial class AssetTypeRelationsController : BaseController
	{

        public ActionResult Index()
        {
            return View();
        }

        [AccessLevelFilter(Action = AccessLevelEnum.Read, TableName = "tbl_AssetTypeRelations")]
        public async Task<ActionResult> AssetTypeRelations_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                IEnumerable<AssetTypeRelationModel> cats =await AssetTypeRelationsService.ReadAssetTypeRelations();

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

   
        public AssetTypeRelationModel GetAssetTypeRelationByID(Guid id )
        {
            AssetTypeRelationModel mod = null;
            try
            {
               mod = AssetTypeRelationsService.OneAssetTypeRelation(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                
            }
            return mod;
        }


        public ActionResult GetAssetTypeRelationReadForm( string id)
        {

            try
            {
                AssetTypeRelationModel mod = GetAssetTypeRelationByID(new Guid(id));
                if(mod!=null)
                {
                    ViewBag.AssetTypeRelationID = mod.AssetTypeRelationID;
                    return PartialView("AssetTypeRelationReadFormPartial", mod);
                }
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult GetAssetTypeRelationEditForm(string id)
        {

            try
            {
                AssetTypeRelationModel mod = GetAssetTypeRelationByID(new Guid(id));
                if (mod != null)
                {
                    ViewBag.AssetTypeRelationID = mod.AssetTypeRelationID;
                    return PartialView("AssetTypeRelationEditFormPartial", mod);
                }
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }


        public ActionResult GetAssetTypeRelationNewForm()
        {

            try
            {
                AssetTypeRelationModel mod = new AssetTypeRelationModel();
                if (mod != null)
                {
                    ViewBag.AssetTypeRelationID = mod.AssetTypeRelationID;
                    return PartialView("AssetTypeRelationEditFormPartial", mod);
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
        [AccessLevelFilter(Action = AccessLevelEnum.Insert, TableName = "tbl_AssetTypeRelations")]
        public async Task<ActionResult> AssetTypeRelations_Create([DataSourceRequest] DataSourceRequest request, AssetTypeRelationModel mod)
        {
            string _err = "";
            try
            {

                if (mod == null) _err = "AssetTypeRelation is null";
                if (mod.AssetTypeRelationID != null && mod.AssetTypeRelationID.ToString() != "00000000-0000-0000-0000-000000000000") _err = "AssetTypeRelation is not new";

                if (!ModelState.IsValid)
                {
                    var errMsg = ModelState.Values.Where(x => x.Errors.Count >= 1);
                    string errorstr = "";
                    foreach (var item in errMsg)
                    {
                        if (item.Errors[0].ErrorMessage != "The AssetTypeRelationID field is required.")
                            errorstr += " " + item.Errors[0].ErrorMessage;
                    }


                    if (errorstr==null || errorstr.Trim() == "")
                        ModelState.Clear();

                }


                if (_err != "")
                    ModelState.AddModelError("exception", _err);

                if (ModelState.IsValid)
                {

                    ObjectResponse resp = await AssetTypeRelationsService.CreateAssetTypeRelation(mod);
                    if (resp.Response != null)
                    {
                        mod = (AssetTypeRelationModel)resp.Response;
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
        [AccessLevelFilter(Action = AccessLevelEnum.Edit, TableName = "tbl_AssetTypeRelations")]
        public async Task<ActionResult> AssetTypeRelations_Update([DataSourceRequest] DataSourceRequest request, AssetTypeRelationModel mod)
        {
            try
            {
                string _err = "";
                AssetTypeRelationModel _updated;

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
                        ObjectResponse resp = await AssetTypeRelationsService.UpdateAssetTypeRelation(mod);
                        if (resp.Response != null)
                        {
                            _updated = (AssetTypeRelationModel)resp.Response;
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
        [AccessLevelFilter(Action = AccessLevelEnum.Edit, TableName = "tbl_AssetTypeRelations")]
        public async Task<ActionResult> AssetTypeRelations_Create_or_Update([DataSourceRequest] DataSourceRequest request, AssetTypeRelationModel mod)
        {
            try
            {
                string _err = "";
                AssetTypeRelationModel _updated;

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
                        if (mod.AssetTypeRelationID==Guid.Empty)
                            resp = await AssetTypeRelationsService.CreateAssetTypeRelation(mod);      
                        else
                            resp = await AssetTypeRelationsService.UpdateAssetTypeRelation(mod);

                        if (resp.Response != null)
                            mod = (AssetTypeRelationModel)resp.Response;
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
        [AccessLevelFilter(Action = AccessLevelEnum.Delete, TableName = "tbl_AssetTypeRelations")]
        public async Task<ActionResult> AssetTypeRelations_Destroy([DataSourceRequest] DataSourceRequest request, AssetTypeRelationModel mod)
        {
            try
            {
                string _err = "";
                
                ObjectResponse resp = await AssetTypeRelationsService.RemoveAssetTypeRelation(mod.AssetTypeRelationID);
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

