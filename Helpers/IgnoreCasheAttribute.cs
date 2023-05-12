using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAMS.WebUI.Helpers
{
  

    public class IgnoreCasheAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                base.OnActionExecuting(filterContext);
                if (filterContext.HttpContext.Request.Params["isSourceUpdated"].ToString() == "true")
                {
                    filterContext.HttpContext.Response.Cache.SetNoServerCaching();
                    filterContext.HttpContext.Response.Cache.SetNoStore();
                }
                else
                    Console.WriteLine("cache used");
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

        //public override void OnResultExecuting(ResultExecutingContext filterContext)
        //{
        //    base.OnResultExecuting(filterContext);
        //    //filterContext.HttpContext.Response.Cache.AddValidationCallback(ValidatioCallback, filterContext.Result);
        //    if (filterContext.HttpContext.Request.Params["isSourceUpdated"].ToString() == "true")
        //    {
        //        Console.WriteLine("cache used");

        //    }
        //    else
        //    {
        //        filterContext.HttpContext.Response.Cache.SetNoServerCaching();
        //        filterContext.HttpContext.Response.Cache.SetNoStore();

        //    }
        //}

        private static void ValidatioCallback(HttpContext context, object data, ref HttpValidationStatus validationStatus)
        {
            var someValueFromGet = context.Request.Params["isSourceUpdated"];//context.Request.QueryString["isSourceUpdated"];
            if (bool.Parse(someValueFromGet))
            {
                validationStatus = HttpValidationStatus.IgnoreThisRequest;
                context.Response.Cache.SetNoServerCaching();
                context.Response.Cache.SetNoStore();
            }
            else
            {
                Console.WriteLine("cache used");
            }
            //var jsonResult = data as JsonResult;
            //if (jsonResult == null) return;

            //var response = jsonResult.Data as Response;
            //if (response == null) return;

            //if (response.ErrorCode != 0)
            //{
            //    //ignore [OutputCache] for this request
            //    validationStatus = HttpValidationStatus.IgnoreThisRequest;
            //    context.Response.Cache.SetNoServerCaching();
            //    context.Response.Cache.SetNoStore();
            //}
        }
    }
}