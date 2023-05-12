using SAMS.Domain.Enums;
using SAMS.Model;
using SAMS.WebUI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace SAMS.WebUI.Helpers
{
    public class AccessLevelFilter : ActionFilterAttribute, IActionFilter
    {
        public bool IsAdmin { get; set; }

        public string TableName { get; set; }
        public AccessLevelEnum Action { get; set; }



        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
               BaseController controller = (BaseController)filterContext.Controller;//filterContext.ControllerContext.Controller as BaseController;
                if(controller.CurrentUser==null)
                    throw new Exception("İstifadəçi təyin olunmayıb");
                UserModel _user = controller.CurrentUser.UserModel;
                string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToUpper();

                if (_user == null)
                {
                    throw new Exception("İstifadəçi təyin olunmayıb");
                    //filterContext.Result = new TransferResult("/Home/AccessDenied/");
                }

                AccessLevelEnum acclevel = AccessLevelEnum.NotDefined;


                if (!_user.Role.IsAdmin)
                {
                    try
                    {
                        acclevel = (AccessLevelEnum)_user.Role.GetType().GetProperty(TableName).GetValue(_user.Role);
                    }
                    catch (Exception)
                    {
                        JsonResult jsonResult = new JsonResult();
                        string text = "Sizin bu əməliyyatı icra etmək səlahiyyətiniz yoxdur";

                        jsonResult.Data = new { status = "false", message = text };
                        jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                        filterContext.Result = jsonResult;
                    }

                    if (acclevel < Action)
                    {
                        JsonResult jsonResult = new JsonResult();
                        string text = "Sizin bu əməliyyatı icra etmək səlahiyyətiniz yoxdur";

                        jsonResult.Data = new { status = "false", message = text };
                        jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                        filterContext.Result = jsonResult;
                    }
                }
            }
            catch (Exception ex)
            {
                JsonResult jsonResult = new JsonResult();
                string text = ex.Message;

                jsonResult.Data = new { status = "false", message = text };
                jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                //filterContext.Result = jsonResult;
                filterContext.Result = new TransferResult("/Home/Index/");
            }
 
        }

    }
}