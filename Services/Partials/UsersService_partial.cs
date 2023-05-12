using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SAMS.Model;
using SAMS.WebUI.Helpers;
using Newtonsoft.Json;
using System.Runtime.Caching;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Client;
using SAMS.WebUI.Hubs;
using SAMS.WebUI.Services.Partials;

namespace SAMS.WebUI.Services
{

	public static partial class UsersService 
	{

        public static async Task<IList<UserModel>> ReadSecretUsers()
        {
            try
            {
                IList<UserModel> result;
                    var client = new HttpClient();
                    client.BaseAddress = new Uri(Settings.ApiAddress());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var request = client.GetAsync("api/GetUsersWithSecret/?_secret=sercret_for_getting_users_list");
                    var jsonres = request.ContinueWith(http =>
                        http.Result.Content.ReadAsAsync<List<UserModel>>());
                    result = jsonres.Unwrap().Result;

                return result;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public static async Task UserConnected(Guid userGuid, bool _isconnected)
        {
            IList<Guid> result = HttpRuntime.Cache["ConnectedUsersGuids"] as IList<Guid>;
            if (result == null)
            {
                result = new List<Guid>();
                if(_isconnected)
                    result.Add(userGuid);
                HttpRuntime.Cache.Insert("AssetAttributeCategorys", result);
            }
            else 
            {
                if (result.IndexOf(userGuid) == -1)
                {
                    if (_isconnected)
                    {
                        result.Add(userGuid);
                        result = HttpRuntime.Cache["ConnectedUsersGuids"] as IList<Guid>;
                        Console.WriteLine(result.IndexOf(userGuid).ToString());
                    }
                }
                else
                {
                    if (!_isconnected)
                    {
                        result.Remove(userGuid);
                        result = HttpRuntime.Cache["ConnectedUsersGuids"] as IList<Guid>;
                        Console.WriteLine(result.IndexOf(userGuid).ToString());
                    }
                }

            }

        }

        private static  IHubProxy hub;

        private static readonly IHubContext<SamsHub> _hubContext;

        public static void SetUserConnectionID()
        {
            //_hubContext.Flatten()
        }

        //public static List<UserModel> ConnectedUsers()
        //{
        //    //return hub.On();
        //}


        public static async Task ConnectToHub()
        {
            //bool connected = false;
            //try
            //{
            //    string _HubUrl = "http://localhost:44359/";
            //    var url = _HubUrl;// "http://localhost:9792/";// "http://localhost/InRun/";
            //    var connection = new HubConnection(url);
            //    hub = connection.CreateHubProxy("samsHub");


            //    hub.On<RegistratorClassModel>("SetReadersStatuses", registr =>
            //    (() =>
            //    {
            //        //if (registr.RegistratorIP == "172.20.24.15")
            //        //    Console.WriteLine("stop");
            //        RegistratorClass reg = Registrators.Where(x => x.RegistratorID == registr.RegistratorID).FirstOrDefault();
            //        reg.IsOnline = registr.IsOnline;
            //        reg.IsConnected = registr.IsConnected;
            //        foreach (var ant in reg.RegistratorAntennaClasses)
            //        {
            //            ant.Connected = registr.RegistratorAntennaClasses.Where(x => x.RegistratorAntennaID == ant.RegistratorAntennaID).FirstOrDefault().Connected;
            //            Console.WriteLine("IP " + reg.RegistratorIP + ", ant " + ant.AntennaNumber + " is " + ant.Connected.ToString());
            //            reg.SetPropertyChanged("RegistratorAntennaClasses");
            //        }
            //        //reg.
            //    })
            //   );



            //    await connection.Start();
            //    await hub.Invoke("Connect");
            //    if (connection.State == ConnectionState.Connected)
            //    {
            //        connected = true;
            //        connection.Closed += Connection_Closed;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    System.Windows.MessageBox.Show(ex.Message);
            //}
            //return connected;

            var connection = new HubConnection("http://localhost:44359/");
            //Make proxy to hub based on hub name on server
            var myHub = connection.CreateHubProxy("SamsHub");
            //Start connection

            connection.Start().ContinueWith(task => {
                if (task.IsFaulted)
                {
                    Console.WriteLine("There was an error opening the connection:{0}", task.Exception.GetBaseException());
                }
                else
                {
                    Console.WriteLine("Connected");
                }

            }).Wait();

            myHub.Invoke<string>("ConnectedUsers", "").ContinueWith(task => {
                if (task.IsFaulted)
                {
                    Console.WriteLine("There was an error calling send: {0}",
                        task.Exception.GetBaseException());
                }
                else
                {
                    Console.WriteLine(task.Result);
                }
            });

            //myHub.On<string>("addMessage", param => {
            //    Console.WriteLine(param);
            //});

            //myHub.Invoke<string>("DoSomething", "I'm doing something!!!").Wait();


            connection.Stop();


        }
    }
}

