using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
using SAMS.Model;
using SAMS.WebUI.Helpers;
using SAMS.WebUI.Hubs;

namespace SAMS.WebUI.Models.Hubs
{
    public class SamsHubModel
    {
        internal List<UserModel> Users { get; set; }  //Connected Operators

        public static SamsHubModel Instance
        {
            get
            {
                return _instance.Value;
            }
        }
        // Singleton instance
        readonly static Lazy<SamsHubModel> _instance = new Lazy<SamsHubModel>
        (() => new SamsHubModel
        (
           usersClients: GlobalHost.ConnectionManager.GetHubContext<SamsHub>().Clients
        ));



        //Constructor
        SamsHubModel(IHubConnectionContext<dynamic> usersClients)
        {
            try
            {
                Users = SAMS.WebUI.Services.UsersService.ReadSecretUsers().Result.Where(x=>!x.Deactivate).ToList();
            }
            catch (Exception ex)
            {
                StandardMessages.DisplayDictExceptionMessage(ex);
            }
        } 


        #region IDisposable Support
        bool disposedValue = false; // To detect redundant calls
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    //_tableDependency.Stop();
                }

                disposedValue = true;
            }
        }
        ~SamsHubModel()
        {
            Dispose(false);
        }
        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}