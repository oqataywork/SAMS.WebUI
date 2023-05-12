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


	public partial class AssetAttributeCategoriesController : BaseController
	{
        public async Task<ActionResult> AttributeCategoriesList()
        {
            try
            {
                IEnumerable<AssetAttributeCategoryModel> cats = await AssetAttributeCategoriesService.ReadAssetAttributeCategorys();
                var res = cats.Where(x => !x.Deactivate).Select(c => new { AssetAttributeCategoryID = c.AssetAttributeCategoryID, AssetAttributeCategoryName = c.AssetAttributeCategoryName });

                return Json(res, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(null);

            }
        }
    }
}

