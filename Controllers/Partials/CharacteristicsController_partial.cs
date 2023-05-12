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
    public partial class CharacteristicsController : BaseController
	{
        public async Task<ActionResult> CharacteristicsList()
        {
            try
            {
                var cats = (await CharacteristicsService.ReadCharacteristics()).Where(x => !x.Deactivate).Select(c => new { CharacteristicID = c.CharacteristicID, CharacteristicName = c.CharacteristicName }).ToList();

                return Json(cats, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(null);

            }
        }
    }
}

