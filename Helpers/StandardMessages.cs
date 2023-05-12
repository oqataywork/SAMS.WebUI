using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAMS.WebUI.Helpers
{
    public class StandardMessages
    {
        public static Dictionary<string, string> DisplayDictTryCatchExceptionMessage(bool isEx = false, Exception ex = null)
        {
            Dictionary<string, string> respData = new Dictionary<string, string>();

            respData.Add("status", "false");
            string message = "Xəta baş verdi. Zəhmət olmasa, səhifəni yeniləyin.";


            if (isEx)
            {
                message += ex.ToString();
            }
            respData.Add("message", message);

            return respData;
        }

        public static Dictionary<string, string> DisplayCustomMessage(string message)
        {
            Dictionary<string, string> respData = new Dictionary<string, string>();

            respData.Add("status", "false");
            respData.Add("message", message);
            return respData;
        }

        public static Dictionary<string, string> DisplayDictExceptionMessage(Exception ex, int? UserID = null, string addinfo = "")
        {
            Dictionary<string, string> respData = new Dictionary<string, string>();
            respData.Add("status", "false");
            string errorMessage = "Xəta baş verdi. Zəhmət olmasa, səhifəni yeniləyin. ";
            try
            {
                //string userGuid = Domain.Models.Accounts.UserSession.GetLoginInfo != null ? Domain.Models.Accounts.UserSession.GetLoginInfo.UserGuid.ToString() : null;
                //errorMessage = Logging.SaveErrorLog(ex, userGuid, null, addinfo);

                respData.Add("message", errorMessage + ", Exception: " + ex.Message);
            }
            catch (Exception exc)
            {
                respData.Add("message", errorMessage + ", Exception: " + exc.Message);
            }
            return respData;

        }


        public static Dictionary<string, string> DisplayModelStateOneError(ModelStateDictionary modelState)
        {
            Dictionary<string, string> respData = new Dictionary<string, string>();

            string errorMessage = modelState.Values.SelectMany(m => m.Errors).FirstOrDefault().ErrorMessage;
            respData.Add("status", "false");
            respData.Add("message", errorMessage);

            return respData;
        }

    }
}