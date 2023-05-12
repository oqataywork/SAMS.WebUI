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


	public partial class AssetOperationIndicatorsMapsController : BaseController
	{



        [AcceptVerbs(HttpVerbs.Post)]
        [AccessLevelFilter(Action = AccessLevelEnum.Insert, TableName = "tbl_AssetOperationIndicatorsMaps")]
        public async Task<ActionResult> AssetOperationIndicatorsMaps_New([DataSourceRequest] DataSourceRequest request, AssetOperationIndicatorsMapModel mod, string assetid = "")
        {
            string _err = "";
            try
            {

                if (mod == null) _err = "AssetOperationIndicatorsMap is null";
                if (mod.AssetOperationIndicatorID != null && mod.AssetOperationIndicatorID.ToString() != "00000000-0000-0000-0000-000000000000") _err = "AssetOperationIndicatorsMap is not new";
                mod.AssetID = new Guid(assetid);
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


	}
}

