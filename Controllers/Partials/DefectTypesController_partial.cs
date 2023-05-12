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


	public partial class DefectTypesController : BaseController
	{
        public async Task<ActionResult> GetDefectTypesList()
        {
            var cats = (await DefectTypesService.ReadDefectTypes()).Where(x => !x.Deactivate).Select(c => new { DefectTypeID = c.DefectTypeID, DefectTypeName = c.DefectTypeName }).ToList();

            return Json(cats, JsonRequestBehavior.AllowGet);
        }
    }
}

