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
using SAMS.WebUI.Services.Partials;

namespace SAMS.WebUI.Controllers
{


	public partial class AssetCategoryAttributeMapsController : BaseController
	{
        
        [AcceptVerbs(HttpVerbs.Post)]
        [AccessLevelFilter(Action = AccessLevelEnum.Insert, TableName = "tbl_AssetCategoryAttributeMaps")]
        public async Task<ActionResult> AssetCategoryAttributeMaps_New([DataSourceRequest] DataSourceRequest request, AssetCategoryAttributeMapModel mod)
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
                        ShouldRefreshService.SetShouldRefreshAssetAttributeMaps(true);
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




        //[AcceptVerbs(HttpVerbs.Post)]
        //[AccessLevelFilter(Action = AccessLevelEnum.Edit, TableName = "tbl_AssetCategoryAttributeMaps")]
        //public async Task<ActionResult> AssetCategoryAttributeMaps_Create_or_Update([DataSourceRequest] DataSourceRequest request, AssetCategoryAttributeMapModel mod)
        //{
        //    try
        //    {
        //        string _err = "";
        //        AssetCategoryAttributeMapModel _updated;

        //        if (mod != null)
        //        {
        //            if (!ModelState.IsValid)
        //            {

        //                if (ModelState["CreateDate"] != null && ModelState["CreateDate"].Errors != null)
        //                {
        //                    var createdateerrors = ModelState["CreateDate"].Errors.ToList();
        //                    foreach (var error in createdateerrors)
        //                    {
        //                        ModelState["CreateDate"].Errors.Remove(error);
        //                    }
        //                }

        //                if (ModelState["ChangeDate"] != null && ModelState["ChangeDate"].Errors != null)
        //                {
        //                    var createdateerrors = ModelState["CreateDate"].Errors.ToList();
        //                    foreach (var error in createdateerrors)
        //                    {
        //                        ModelState["ChangeDate"].Errors.Remove(error);
        //                    }
        //                }

        //            }


        //            if (ModelState.IsValid)
        //            {
        //                ObjectResponse resp;
        //                if (mod.AssetCategoryAttributeMapID==Guid.Empty)
        //                    resp = await AssetCategoryAttributeMapsService.CreateAssetCategoryAttributeMap(mod);      
        //                else
        //                    resp = await AssetCategoryAttributeMapsService.UpdateAssetCategoryAttributeMap(mod);

        //                if (resp.Response != null)
        //                    mod = (AssetCategoryAttributeMapModel)resp.Response;
        //                else
        //                    ModelState.AddModelError("exception", resp.error);
        //            }
        //            else
        //            {
        //                var errMsg = ModelState.Where(x => x.Value.Errors.Count >= 1);
        //                string _errors = "";
        //                foreach (var item in errMsg)
        //                    _errors += ("\n" + item.Value.Errors[0].ErrorMessage);

        //                ModelState.AddModelError("exception", _errors);
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError("exception", ex.Message);
        //    }
        //    return Json(new[] { mod }.ToDataSourceResult(request, ModelState));
        //}

        
	}
}

