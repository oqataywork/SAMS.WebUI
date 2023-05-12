using Newtonsoft.Json;
using SAMS.Domain;
using SAMS.Domain.HelperModels;
using SAMS.Model;
using SAMS.WebUI.Models.Forms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace SAMS.WebUI.Services
{
    public  static class AccountService
    {
       

        public static UserSession GetUserSession(LoginForm loginForm)
        {
            try
            {
                UserCredential cred = new UserCredential {UserLogin= loginForm.txtUserLogin, Password= loginForm.txtPassword };
                UserModel result=null;
                UserSession  userSession=null;
                //HttpClient client = new HttpClient();
                string api = ConfigurationManager.AppSettings["ApiAddress"];


                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders
                      .UserAgent
                      .TryParseAdd("SAMS Application");
                    //User-Agent: Mike D's Agent
                    client.BaseAddress = new Uri(api);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    System.Net.ServicePointManager.Expect100Continue = false;
                    var response = client.PostAsJsonAsync("api/authorize", cred).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        result = JsonConvert.DeserializeObject<UserModel>(response.Content.ReadAsStringAsync().Result);
                        userSession = new UserSession();
                        userSession.UserModel = result;
                        userSession.CurrentOrganisationGuid = result.Personnel.Department.OrganizationID;
                        userSession.Deactive = result.Deactivate;
                        userSession.Email = result.Personnel.PersonnelEmail;
                        userSession.RoleGuid = result.RoleID;
                        userSession.UserGuid = result.UserID;
                        userSession.UserLastName = result.Personnel.PersonnelLastName;
                        userSession.UserLogin = result.UserLogin;
                        userSession.UserName = result.Personnel.PersonnelFirstName;
                        userSession.UserMiddleName = result.Personnel.PersonnelMiddleName;
                        userSession.Token = result.Token;

                    }
                    else
                        throw new Exception(response.ReasonPhrase);

                }


                //client.Agent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/93.0.4577.63 Safari/537.36";


                return userSession;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}