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


	public partial class DepartmentsController : BaseController
	{
        public async Task<JsonResult> GetPossibleParentDepartments(string departmentid = "")
        {
            try
            {
                List<Guid> alldesc = null;
                if (departmentid != "" && departmentid != "00000000-0000-0000-0000-000000000000")
                    alldesc = await PartialService.GetAllDescendantDepartmentsByID(departmentid);

                IEnumerable<DepartmentModel> cats = await DepartmentsService.ReadDepartments();
                if (alldesc != null && alldesc.Count > 0)
                {
                    var result = cats.Where(x => !alldesc.Contains(x.DepartmentID)).Select(c => new  { DepartmentID = c.DepartmentID, DepartmentName = c.DepartmentName }).ToList();
                    var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
                    return jsonResult;
                }
                else
                {
                    var result = cats.Select(c => new { DepartmentID = c.DepartmentID, DepartmentName = c.DepartmentName }).ToList();
                    var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
                    return jsonResult;
                }
                    

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(null);
            }
        }
        
    }
}

