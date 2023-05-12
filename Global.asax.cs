using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SAMS.WebUI.App_Start;

namespace SAMS.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.Add(typeof(DateTime), new CustomDateBinder());
            
        }

        //protected void Application_Error(object sender, EventArgs e)
        //{
        //    Exception exception = Server.GetLastError();
        //    Server.ClearError();
        //    Response.Redirect("/Home/Error");
        //}

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //System.Globalization.CultureInfo newCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            //newCulture.DateTimeFormat.ShortDatePattern = "dd.mm.yyyy";
            //newCulture.DateTimeFormat.DateSeparator = ".";
            //System.Threading.Thread.CurrentThread.CurrentCulture = newCulture;
        }
    }

    internal class CustomDateBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (value != null)
            {
                if (value.AttemptedValue.StartsWith("/Date("))
                {
                    try
                    {


                        DateTime date = new DateTime(1970, 01, 01, 0, 0, 0, DateTimeKind.Utc).ToUniversalTime();
                        string attemptedValue = value.AttemptedValue.Replace("/Date(", "").Replace(")/", "");
                        double milliSecondsOffset = Convert.ToDouble(attemptedValue);
                        DateTime result = date.AddMilliseconds(milliSecondsOffset);
                        result = result.ToUniversalTime();
                        return result;
                    }
                    catch
                    {
                    }
                }
                else
                {
                    string[] formats = {"M/d/yyyy h:mm:ss tt", "M/d/yyyy h:mm tt",
                    "MM/dd/yyyy hh:mm:ss", "M/d/yyyy h:mm:ss",
                    "M/d/yyyy hh:mm tt", "M/d/yyyy hh tt",
                    "M/d/yyyy h:mm", "M/d/yyyy h:mm",
                    "MM/dd/yyyy hh:mm", "M/dd/yyyy hh:mm",
                    "MM/d/yyyy HH:mm:ss.ffffff","dd.MM.yyyy" };
                    DateTime dateResult;
                    for (int i = 0; i < formats.Length; i++)
                    {
                        bool b = DateTime.TryParseExact(value.AttemptedValue, formats[i],
                                 CultureInfo.InvariantCulture,
                                 DateTimeStyles.None,
                                 out dateResult);
                        if (b)
                            return dateResult;
                    }

                    //if (value.Culture.Name== "en-US")
                    //{
                    //    DateTimeStyles styles;
                    //    DateTime dateResult;
                    //    styles = DateTimeStyles.None;
                    //    if (DateTime.TryParse(value.AttemptedValue, value.Culture, styles, out dateResult))
                    //    {
                    //        return dateResult;
                    //    }
                    //        //Console.WriteLine("{0} converted to {1} {2}.",
                    //        //                  dateString, dateResult, dateResult.Kind);

                    //}
                }
            }
            return base.BindModel(controllerContext, bindingContext);
        }
    }
}
