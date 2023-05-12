using SAMS.WebUI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAMS.WebUI.Attributes
{
    public class AuthorizeUserType : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Domain.Models.Accounts.UserSession login = (Domain.Models.Accounts.UserSession)filterContext.HttpContext.Session["UserSession"];
            //string acceptType = filterContext.HttpContext.Request.AcceptTypes != null ? filterContext.HttpContext.Request.AcceptTypes[0] : null;
            //bool isAjaxRequest = filterContext.HttpContext.Request.IsAjaxRequest();

            //string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower();
            //string actionName = filterContext.ActionDescriptor.ActionName;
            //string methodType = filterContext.HttpContext.Request.HttpMethod.ToUpper();

            //bool isUserEnterPage = true;

            //if (login.UserType != (int)Domain.Enums.EnumUserType.Administrator)
            //{
            //    if (login.UserType == (int)Domain.Enums.EnumUserType.NotDefined)
            //    {
            //        isUserEnterPage = false;
            //    }
            //    else if ((login.UserType == (int)Domain.Enums.EnumUserType.Operator && controllerName != "operators") || login.UserType != (int)Domain.Enums.EnumUserType.Operator && controllerName == "operators")
            //    {
            //        isUserEnterPage = false;
            //    }
            //    else if ((login.UserType == (int)Domain.Enums.EnumUserType.Receptionist && controllerName == "operators") || (login.UserType == (int)Domain.Enums.EnumUserType.Receptionist && controllerName == "reports") || (login.UserType == (int)Domain.Enums.EnumUserType.Receptionist && controllerName == "voicerecordings"))
            //    {
            //        isUserEnterPage = false;
            //    }

            //}
            //else if (controllerName == "operators" || controllerName == "registration")
            //{
            //    isUserEnterPage = false;

            //}


            //if (!isUserEnterPage)
            //{

            //    if (isAjaxRequest)
            //    {
            //        if (acceptType == "application/json")
            //        {
            //            JsonResult jsonResult = new JsonResult();
            //            string text = "Sizin bu səhifəyə giriş ixtiyarınız yoxdur";

            //            jsonResult.Data = new { message = "error", text = text };
            //            filterContext.Result = jsonResult;
            //        }
            //        else if (acceptType == "text/html")
            //        {
            //            filterContext.Result = new TransferResult("/Home/AccessDenied/");
            //        }
            //    }
            //    else
            //    {
            //        if (acceptType == "text/html")
            //        {

            //            filterContext.Result = new TransferResult("/Home/AccessDenied/");
            //        }
            //    }
            //}
        }
    }


}