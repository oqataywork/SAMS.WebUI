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


	public partial class MeasurementUnitTypesController : BaseController
	{

        [HttpGet]
        public async Task<ActionResult> GetUnitsList()
        {
            var cats = (await MeasurementUnitTypesService.ReadMeasurementUnitTypes()).Where(x => !x.Deactivate).Select(c => new { MeasurementUnitTypeID = c.MeasurementUnitTypeID, UnitTypeName = c.UnitTypeName }).ToList();

            return Json(cats, JsonRequestBehavior.AllowGet);
        }

    }
}

