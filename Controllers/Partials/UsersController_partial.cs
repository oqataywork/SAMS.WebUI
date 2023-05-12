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
using SAMS.WebApi.Helpers;

namespace SAMS.WebUI.Controllers
{


	public partial class UsersController : BaseController
	{

 

        [AccessLevelFilter(Action = AccessLevelEnum.Read, TableName = "tbl_Users")]
        public async Task<ActionResult> NotRegisteredPersons(string userid)
        {
            try
            {
                Guid userGuid = Guid.Empty;
                if (userid != null && userid != "")
                    userGuid = new Guid(userid);
                //IEnumerable<UserModel> cats = UsersService.ReadUsers();

                    //DataSourceResult result = cats.ToDataSourceResult(request);
                    //var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
                    //jsonResult.MaxJsonLength = int.MaxValue;
                    //return jsonResult;
                List<Guid> userguids = (await UsersService.ReadUsers()).Select(x => x.UserID).ToList();
                if(userguids.IndexOf(userGuid)!=-1)
                    userguids.Remove(userGuid);
                //var pers = PersonnelsService.ReadPersonnels().Where(x => !userguids.Contains(x.PersonnelID)).ToList();
                var pers = (await PersonnelsService.ReadPersonnels()).Where(x => !userguids.Contains(x.PersonnelID)).Select(c => new { PersonnelID = c.PersonnelID, PeronFullName = String.Format("{0} {1} {2}", c.PersonnelFirstName, c.PersonnelLastName, c.PersonnelMiddleName) });
                //var pers = PersonnelsService.ReadPersonnels().Where(x => !userguids.Contains(x.PersonnelID)).ToList();
                return Json(pers, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(null);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [AccessLevelFilter(Action = AccessLevelEnum.Insert, TableName = "tbl_Users")]
        public async Task<ActionResult> RegisterUser([DataSourceRequest] DataSourceRequest request, UserModel mod)
        {
            string _err = "";
            try
            {

                if (mod == null) _err = "User is null";
                if (String.IsNullOrEmpty(mod.Password)) _err = "Password is required";
                //if (mod.UserID != null && mod.UserID.ToString() != "00000000-0000-0000-0000-000000000000") _err = "User is not new";

                if (!ModelState.IsValid)
                {
                    var errMsg = ModelState.Values.Where(x => x.Errors.Count >= 1);
                    string errorstr = "";
                    foreach (var item in errMsg)
                    {
                        if (item.Errors[0].ErrorMessage != "The UserID field is required.")
                            errorstr += " " + item.Errors[0].ErrorMessage;
                    }


                    if (errorstr == null || errorstr.Trim() == "")
                        ModelState.Clear();

                }


                if (_err != "")
                    ModelState.AddModelError("exception", _err);

                if (ModelState.IsValid)
                {
                    string hash= Cryptography.Encrypt(mod.Password, null);
                    //mod.UserID = mod.Personnel.PersonnelID;
                    mod.PasswordHash = hash;
                    ObjectResponse resp = await UsersService.CreateUser(mod);
                    if (resp.Response != null)
                    {
                        mod = (UserModel)resp.Response;
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

