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


	public partial class OperationIndicatorsController : BaseController
	{
        public async Task<ActionResult> GetOperationIndicatorsList()
        {
            var cats = (await OperationIndicatorsService.ReadOperationIndicators()).Where(x => !x.Deactivate).Select(c => new { OperationIndicatorID = c.OperationIndicatorID, OperationIndicatorName = c.OperationIndicatorName }).ToList();
            return Json(cats, JsonRequestBehavior.AllowGet);
        }
    }
}

