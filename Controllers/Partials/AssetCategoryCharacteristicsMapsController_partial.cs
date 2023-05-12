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


	public partial class AssetCategoryCharacteristicsMapsController : BaseController
	{
        

        [AcceptVerbs(HttpVerbs.Post)]
        [AccessLevelFilter(Action = AccessLevelEnum.Insert, TableName = "tbl_AssetCategoryCharacteristicsMaps")]
        public async Task<ActionResult> AssetCategoryCharacteristicsMaps_New([DataSourceRequest] DataSourceRequest request, AssetCategoryCharacteristicsMapModel mod)
        {
            string _err = "";
            try
            {

                if (mod == null) _err = "AssetCategoryCharacteristicsMap is null";
                if (mod.AssetCategoryCharacteristicID != null && mod.AssetCategoryCharacteristicID.ToString() != "00000000-0000-0000-0000-000000000000") _err = "AssetCategoryCharacteristicsMap is not new";

                if (!ModelState.IsValid)
                {
                    var errMsg = ModelState.Values.Where(x => x.Errors.Count >= 1);
                    string errorstr = "";
                    foreach (var item in errMsg)
                    {
                        if (item.Errors[0].ErrorMessage != "The AssetCategoryCharacteristicID field is required.")
                            errorstr += " " + item.Errors[0].ErrorMessage;
                    }


                    if (errorstr==null || errorstr.Trim() == "")
                        ModelState.Clear();

                }


                if (_err != "")
                    ModelState.AddModelError("exception", _err);

                if (ModelState.IsValid)
                {

                    ObjectResponse resp = await AssetCategoryCharacteristicsMapsService.CreateAssetCategoryCharacteristicsMap(mod);
                    if (resp.Response != null)
                    {
                        mod = (AssetCategoryCharacteristicsMapModel)resp.Response;
                        ShouldRefreshService.SetShouldRefreshAssetCharacteristics(true);
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
        //[AccessLevelFilter(Action = AccessLevelEnum.Edit, TableName = "tbl_AssetCategoryCharacteristicsMaps")]
        //public async Task<ActionResult> AssetCategoryCharacteristicsMaps_Update([DataSourceRequest] DataSourceRequest request, AssetCategoryCharacteristicsMapModel mod)
        //{
        //    try
        //    {
        //        string _err = "";
        //        AssetCategoryCharacteristicsMapModel _updated;

        //        if (mod != null)
        //        {
        //            if(!ModelState.IsValid)
        //            {

        //                if(ModelState["CreateDate"]!=null && ModelState["CreateDate"].Errors!=null)
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
        //                var errMsg = ModelState.Where(x => x.Value.Errors.Count >= 1);
        //                string _errors = "";
        //                foreach (var item in errMsg)
        //                {
        //                    if(!String.IsNullOrEmpty(item.Value.Errors[0].ErrorMessage))
        //                    {
        //                        _errors += ("\n" + item.Value.Errors[0].ErrorMessage);
        //                    }
                                
        //                }
                            
        //                if(!String.IsNullOrEmpty(_errors))
        //                    ModelState.AddModelError("exception", _errors);
        //                else
        //                    ModelState.Clear();
        //            }


        //            if (ModelState.IsValid)
        //            {
        //                ObjectResponse resp = await AssetCategoryCharacteristicsMapsService.UpdateAssetCategoryCharacteristicsMap(mod);
        //                if (resp.Response != null)
        //                {
        //                    _updated = (AssetCategoryCharacteristicsMapModel)resp.Response;
        //                    mod = _updated;
        //                }
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

