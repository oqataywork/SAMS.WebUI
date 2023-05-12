using SAMS.WebUI.Helpers;
using SAMS.WebUI.Models.Forms;
using SAMS.WebUI.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAMS.WebUI.Attributes
{
    public class IsAuthenticated : ActionFilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)

        {
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower();
            string actionName = filterContext.ActionDescriptor.ActionName.ToLower(); ;

            if (ConfigurationManager.AppSettings["login"] != null && ConfigurationManager.AppSettings["login"].ToString().ToUpper() == "LOCAL")
            {
                //Authenticate();
            }
            string acceptType = filterContext.HttpContext.Request.AcceptTypes != null ? filterContext.HttpContext.Request.AcceptTypes[0] : null;
            bool isAjaxRequest = filterContext.HttpContext.Request.IsAjaxRequest();
            string url = filterContext.HttpContext.Request.RawUrl;

            // TODO: Add your action filter's tasks here

#if (DEBUG)
            try
            {
                if (HttpContext.Current.Session["UserSession"] == null)
                {
                    LoginForm loginForm = new LoginForm { txtPassword = "admin", txtUserLogin = "admin" };
                    var user = AccountService.GetUserSession(loginForm);
                    if (user != null)
                    {

                        HttpContext.Current.Session["UserSession"] = user;

                    }
                    else
                    {
                        filterContext.Result = new RedirectResult($"/accounts/login/?return={url}");
                    }
                }

            }
            catch (Exception ex)
            {
                filterContext.Result = new RedirectResult($"/accounts/login/?return={url}");//StandardMessages.DisplayDictExceptionMessage(ex);
            }

#elif (RELEASE)
            if (HttpContext.Current.Session["UserSession"] == null)
            {
                filterContext.HttpContext.Session.Clear();

                if (isAjaxRequest)
                {
                    if (acceptType == "application/json")
                    {
                        JsonResult jsonResult = new JsonResult();
                        string text = string.Empty;

                        jsonResult.Data = new { status = "false", message = "Sessiya müddəti bitmişdir. Zəhmət olmasa, yenidən daxil olun.", text = text }; //text = "Giriş etməmisiz" };
                        jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                        filterContext.Result = jsonResult;
                    }
                    else
                    {
                        JsonResult jsonResult = new JsonResult();
                        string text = string.Empty;

                        jsonResult.Data = new { status = "false", message = "Sessiya müddəti bitmişdir. Zəhmət olmasa, yenidən daxil olun.", text = text }; //text = "Giriş etməmisiz" };
                        jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                        filterContext.Result = jsonResult;
                    }
                }
                else
                {
                    filterContext.Result = new RedirectResult($"/accounts/login/?return={url}");
                }
            }
#endif



        }

        //public static void Authenticate()
        //{
        //    try
        //    {
        //        if (HttpContext.Current.Session != null || HttpContext.Current.Session["LoginInfo"] == null)
        //        {
        //            //db login
        //            HttpContext.Current.Session["LoginInfo"] = loginInfo;
        //        }
        //    }
        //    catch (Exception ee)
        //    {
        //        //
        //    }
        //}
    }
}