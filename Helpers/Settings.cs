using SAMS.Domain.HelperModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SAMS.WebUI.Helpers
{
    public static class Settings
    {
        private static string api_addr;
        private static string metrorunUrl;
        private static string _token;
        public static string ApiAddress()
        {
            if (api_addr == null || api_addr == "")
            {
                if (ConfigurationManager.AppSettings["ApiAddress"] != null)
                    api_addr = ConfigurationManager.AppSettings["ApiAddress"];
                else
                    api_addr = "http://localhost:44324/";
            }
            return api_addr;
        }

        public static string Token()
        {
            if (HttpContext.Current!=null && HttpContext.Current.Session!=null && HttpContext.Current.Session["UserSession"]!=null)
            {
                UserSession userSession = HttpContext.Current.Session["UserSession"] as UserSession;
                return userSession.Token;
            }
            return "";
        }

        public static string MetroRunUrl()
        {
            if (metrorunUrl == null || metrorunUrl == "")
            {
                if (ConfigurationManager.AppSettings["MetroRunUrl"] != null)
                    metrorunUrl = ConfigurationManager.AppSettings["MetroRunUrl"];
                else
                    metrorunUrl = "http://localhost:57566/";
            }
            return metrorunUrl;
        }

        public static string MetroRunWebServiceUrl()
        {
            if (metrorunUrl == null || metrorunUrl == "")
            {
                if (ConfigurationManager.AppSettings["MetroRunWebServiceUrl"] != null)
                    metrorunUrl = ConfigurationManager.AppSettings["MetroRunWebServiceUrl"];
                else
                    metrorunUrl = "http://localhost:57566/";
            }
            return metrorunUrl;
        }
    }
}