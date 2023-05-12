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


	public partial class AssetCategoryDocumentTypeMapsController : BaseController
	{
        

        [AcceptVerbs(HttpVerbs.Post)]
        [AccessLevelFilter(Action = AccessLevelEnum.Insert, TableName = "tbl_AssetCategoryDocumentTypeMaps")]
        public async Task<ActionResult> AssetCategoryDocumentTypeMaps_New([DataSourceRequest] DataSourceRequest request, AssetCategoryDocumentTypeMapModel mod)
        {
            string _err = "";
            try
            {

                if (mod == null) _err = "AssetCategoryDocumentTypeMap is null";
                if (mod.AssetCategoryDocumentTypeMapID != null && mod.AssetCategoryDocumentTypeMapID.ToString() != "00000000-0000-0000-0000-000000000000") _err = "AssetCategoryDocumentTypeMap is not new";

                if (!ModelState.IsValid)
                {
                    var errMsg = ModelState.Values.Where(x => x.Errors.Count >= 1);
                    string errorstr = "";
                    foreach (var item in errMsg)
                    {
                        if (item.Errors[0].ErrorMessage != "The AssetCategoryDocumentTypeMapID field is required.")
                            errorstr += " " + item.Errors[0].ErrorMessage;
                    }


                    if (errorstr==null || errorstr.Trim() == "")
                        ModelState.Clear();

                }


                if (_err != "")
                    ModelState.AddModelError("exception", _err);

                if (ModelState.IsValid)
                {

                    ObjectResponse resp = await AssetCategoryDocumentTypeMapsService.CreateAssetCategoryDocumentTypeMap(mod);
                    if (resp.Response != null)
                    {
                        mod = (AssetCategoryDocumentTypeMapModel)resp.Response;
                        ShouldRefreshService.SetShouldRefreshAssetDocuments(true);
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

