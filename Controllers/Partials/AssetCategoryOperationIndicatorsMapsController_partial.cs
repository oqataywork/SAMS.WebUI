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


	public partial class AssetCategoryOperationIndicatorsMapsController : BaseController
	{
        

        [AcceptVerbs(HttpVerbs.Post)]
        [AccessLevelFilter(Action = AccessLevelEnum.Insert, TableName = "tbl_AssetCategoryOperationIndicatorsMaps")]
        public async Task<ActionResult> AssetCategoryOperationIndicatorsMaps_New([DataSourceRequest] DataSourceRequest request, AssetCategoryOperationIndicatorsMapModel mod)
        {
            string _err = "";
            try
            {

                if (mod == null) _err = "AssetCategoryOperationIndicatorsMap is null";
                if (mod.AssetCategoryOperationIndicatorID != null && mod.AssetCategoryOperationIndicatorID.ToString() != "00000000-0000-0000-0000-000000000000") _err = "AssetCategoryOperationIndicatorsMap is not new";

                if (!ModelState.IsValid)
                {
                    var errMsg = ModelState.Values.Where(x => x.Errors.Count >= 1);
                    string errorstr = "";
                    foreach (var item in errMsg)
                    {
                        if (item.Errors[0].ErrorMessage != "The AssetCategoryOperationIndicatorID field is required.")
                            errorstr += " " + item.Errors[0].ErrorMessage;
                    }


                    if (errorstr==null || errorstr.Trim() == "")
                        ModelState.Clear();

                }


                if (_err != "")
                    ModelState.AddModelError("exception", _err);

                if (ModelState.IsValid)
                {

                    ObjectResponse resp = await AssetCategoryOperationIndicatorsMapsService.CreateAssetCategoryOperationIndicatorsMap(mod);
                    if (resp.Response != null)
                    {
                        mod = (AssetCategoryOperationIndicatorsMapModel)resp.Response;
                        ShouldRefreshService.SetShouldRefreshAssetOperationIndicatorsMaps(true);
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


	}
}

