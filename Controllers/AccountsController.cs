using SAMS.Domain.HelperModels;
using SAMS.WebUI.Helpers;
using SAMS.WebUI.Models.Forms;
using SAMS.WebUI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAMS.WebUI.Controllers
{
    public class AccountsController : BaseController
    {
        // GET: Accounts
        [HttpGet]
        public ActionResult Login()
        {
            //ViewBag.IpAddress = GetLocalIPAddress() + "------- MacAddress -- " + GetMacAddress();
            if (Session["UserSession"] != null)
            {
                return RedirectPermanent("/Home/Index");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Login(LoginForm loginForm)
        {
            Dictionary<string, string> respData = new Dictionary<string, string>();
            try
            {

                if (!CheckLicenseCount())
                    respData = StandardMessages.DisplayCustomMessage("License limitation");
                else
                {
                    if (ModelState.IsValid)
                    {
 
                            var user = AccountService.GetUserSession(loginForm);
                            if (user != null)
                            {

                                Session["UserSession"] = user;
                                respData.Add("status", "true");
                                respData.Add("redirectType", "1"); //1 by default user
                                
                            }
                            else
                            {
                                respData.Add("status", "false");
                                respData.Add("message", "Daxil etdiyiniz istifadəçi adı və ya şifrə düzgün deyildir.");
                            }
                        

                    }
                    else
                    {
                        respData = StandardMessages.DisplayModelStateOneError(ModelState);
                    }
                }

                
            }
            catch (Exception ex)
            {
                respData = StandardMessages.DisplayDictExceptionMessage(ex);
            }
            return Json(respData, JsonRequestBehavior.AllowGet);
        }


        private bool CheckLicenseCount()
        {
            //string _token = System.Configuration.ConfigurationManager.AppSettings["LicenseToken"].ToString();
            //string _to = System.Configuration.ConfigurationManager.AppSettings["LicenseTo"].ToString();
            //string _secret = "My_Unique_Application_String_for_QMS";

            //_token = _token.Replace("%2B", "+");
            //_token = _token.Replace("%3D", "=");
            //string _checksn = StringCipher.Decrypt(_token, _secret);
            //char delimiter = ':';

            //Char[] _reversedarr = _checksn.Reverse().ToArray();
            //string procid = new string(_reversedarr);
            //string[] ars = procid.Split(delimiter);
            //if (ars.Length != 2)
            //{
            //    return false;
            //}
            //int lisCount = int.Parse(ars[1]);
            //int orgCount;
            //using (OrganisationService organisationService = new OrganisationService())
            //{
            //    orgCount = organisationService.GetCachedOrganisationList().Count();
            //}
            //if (lisCount >= orgCount && _to == ars[0]) return true;
            //else return false;
            return true;
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        ////[Attributes.IsAuthenticated]
        //public JsonResult ChangePassword(ChangePasswordForm requestData)
        //{
        //    Dictionary<string, string> respData = new Dictionary<string, string>();

        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            AccountService accountService = new AccountService();
        //            respData = accountService.ChangePassword(requestData);
        //        }
        //        else
        //        {
        //            respData = StandardMessages.DisplayModelStateOneError(ModelState);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        respData.Clear();
        //        respData = StandardMessages.DisplayDictTryCatchExceptionMessage();
        //    }
        //    return Json(respData);

        //}

        //[HttpGet]
        //public ActionResult Logout()
        //{
        //    Session.Abandon();
        //    return RedirectToAction("Login", "Accounts");
        //}


        //[HttpGet]
        //public ActionResult ForgotPassword()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        ////[Attributes.RecaptchaFilter]
        //public JsonResult ForgotPassword(ForgotPasswordForm form)
        //{
        //    Dictionary<string, string> respData = new Dictionary<string, string>();
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            using (AccountService accountService = new AccountService())
        //            {
        //                respData = accountService.SendPasswordRecoveryFormToMail(form);
        //            }
        //        }
        //        else
        //        {
        //            respData = StandardMessages.DisplayModelStateOneError(ModelState);
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        respData = StandardMessages.DisplayDictExceptionMessage(ex);
        //    }
        //    return Json(respData);
        //}

        //[HttpGet]
        //public ActionResult RecoverPassword(string recKey)
        //{
        //    Dictionary<string, string> respData = new Dictionary<string, string>();
        //    try
        //    {
        //        recKey = Url.Encode(recKey);

        //        using (AccountService accountService = new AccountService())
        //        {
        //            respData = accountService.CheckRealiseTimeRecoveryKey(recKey);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        respData = StandardMessages.DisplayDictExceptionMessage(ex);
        //    }
        //    var status = respData.Keys.ElementAt(0);

        //    var message = respData.Keys.ElementAt(1);
        //    ViewBag.status = respData[status];
        //    ViewBag.message = respData[message];

        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult RecoverPassword(string password, string confirmPassword, string recoveryKey)
        //{
        //    Dictionary<string, string> respData = new Dictionary<string, string>();
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            if (string.Equals(password, confirmPassword))
        //            {
        //                recoveryKey = Url.Encode(recoveryKey);
        //                using (AccountService accountService = new AccountService())
        //                {
        //                    respData = accountService.ChangePassword(password, recoveryKey);
        //                }
        //                //respData = dalc.ChangePassword(ControllerContext, password, recoveryKey);
        //            }
        //            else
        //            {
        //                respData.Add("status", "false");
        //                respData.Add("message", "Yeni şifrə və yeni şifrənin təkrarı eyni olmalıdır!");
        //            }
        //        }
        //        else
        //        {
        //            respData = StandardMessages.DisplayModelStateOneError(ModelState);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        respData = StandardMessages.DisplayDictExceptionMessage(ex);
        //    }
        //    return Json(respData);

        //}return View();
        //}
    }
}