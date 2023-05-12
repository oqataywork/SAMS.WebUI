using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAMS.WebUI.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index(HandleErrorInfo exception)
        {
            //return View();
            ViewBag.Title = "Page Not Found";
            if (exception != null)
                return View(exception);
            else
                return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }
    }
}