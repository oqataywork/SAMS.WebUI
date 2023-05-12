using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
using SAMS.Entities.Services;
using SAMS.Model;
using SAMS.WebUI.Helpers;
using SAMS.WebUI.Models.Hubs;

namespace SAMS.WebUI.Hubs
{
    //public class SamsHub: Hub
    //{

    //}
    [HubName("samsHub")]
    public class SamsHub : Hub
    {
        //Logging logging = new Logging();

        private readonly SamsHubModel _samsHubModel;

        public SamsHub() :
            this(SamsHubModel.Instance)
        {
        }
        public SamsHub(SamsHubModel _model)
        {
            try
            {
                _samsHubModel = _model;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        //Operator connecting hub and getting hub session ID
        public bool Connect(string userGuid)
        {
            try
            {
                //string operatorsjson = "";
                //if (_samsHubModel.Users != null)
                //    operatorsjson = JsonConvert.SerializeObject(_samsHubModel.Users, Formatting.None);

                var id = Context.ConnectionId;   //Getting connection id from hub
                UserModel userModel = new UserModel();  //Declaring Operator as null
                if (!_samsHubModel.Users.Any(x => x.ConnectionId == id))
                {
                    userModel = _samsHubModel.Users.FirstOrDefault(x => x.UserID == Guid.Parse(userGuid));
                    if (userModel != null)
                    {
                        userModel.ConnectionId = id;
                        SAMS.WebUI.Services.UsersService.UserConnected(Guid.Parse(userGuid), true);
                        Clients.Others.update(userModel);
                    }
                    else
                    {
                        userModel = SAMS.WebUI.Services.UsersService.ReadSecretUsers().Result.Where(x => x.UserID == Guid.Parse(userGuid) && !x.Deactivate).FirstOrDefault();
                        if (userModel != null)
                        {
                            userModel.ConnectionId = id;
                            _samsHubModel.Users.Add(userModel);
                            SAMS.WebUI.Services.UsersService.UserConnected(Guid.Parse(userGuid), true);
                            Clients.Others.update(userModel);
                        }
                        else
                        {
                            Clients.Client(id).Alert("Istifadəçi tapılmadı.");
                            return false;
                        }

                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                StandardMessages.DisplayDictExceptionMessage(ex);
                return false;
            }
        }


        public override Task OnDisconnected(bool stopCalled)
        {
            try
            {
                //Logging.SaveHubLog($"QueueTickerHub--OnDisconnected-stopCalled");

                var item = _samsHubModel.Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
                if (item != null)
                {
                    item.ConnectionId = "";
                    //_samsHubModel.Users.Remove(item);
                    SAMS.WebUI.Services.UsersService.UserConnected(item.UserID, false);
                    Clients.Others.update(item);
                }
            }
            catch (Exception ex)
            {
                StandardMessages.DisplayDictExceptionMessage(ex);
            }
            return base.OnDisconnected(stopCalled);
        }

        public IEnumerable<UserModel> ConnectedUsers()
        {
            return _samsHubModel.Users;
        }

        public IEnumerable<UserModel> Read()
        {
            return _samsHubModel.Users;
        }

        public void Update(UserModel product)
        {
            Clients.Others.update(product);
        }

        public void Destroy(UserModel product)
        {
            Clients.Others.destroy(product);
        }

        public UserModel Create(UserModel product)
        {
            Clients.Others.create(product);
            return product;
        }
    }
}