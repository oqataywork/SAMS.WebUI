﻿using System;
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
using SAMS.WebUI.Services.Partials;

namespace SAMS.WebUI.Services
{

	public static partial class MaintenanceRepairTypesService 
	{

        public static async Task<IList<MaintenanceRepairTypeModel>> ReadMaintenanceRepairTypes()
        {
            try
            {
              //  IList <MaintenanceRepairTypeModel> result = HttpContext.Current.Session["MaintenanceRepairTypes"] as IList<MaintenanceRepairTypeModel>;
  
              //if (result == null || refresh)
              //  {
              //      IEnumerable <MaintenanceRepairTypeModel> _list = null;
  
              //    using (var client = new HttpClient())
              //      {
              //          client.BaseAddress = new Uri(Settings.ApiAddress());
              //          client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
              //          client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

              //          HttpResponseMessage response = await client.GetAsync("api/GetMaintenanceRepairTypes");
              //          if (response.IsSuccessStatusCode)
              //          {
              //              _list = await response.Content.ReadAsAsync < IEnumerable <MaintenanceRepairTypeModel>>();
              //            result = _list.ToList();
              //              HttpContext.Current.Session["MaintenanceRepairTypes"] = result;
              //          }
              //          else
              //              throw new Exception(response.ReasonPhrase);
              //      }
              //  }
              //  return result;


                //var cache = MemoryCache.Default;
                //if (cache.Contains("AssetCategorys"))
                //{
                //    return (IList<AssetCategoryModel>)cache.Get("AssetCategorys");
                //}

                //IList<AssetCategoryModel> result=null;// = HttpContext.Current.Cache["AssetCategorys"] as IList<AssetCategoryModel>;

                //if (result == null || refresh)
                //{
                //    var client = new HttpClient();
                //    client.BaseAddress = new Uri(Settings.ApiAddress());
                //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //    client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());
                //    var request = client.GetAsync("api/GetAssetCategorys");
                //    var jsonres = request.ContinueWith(http =>
                //        http.Result.Content.ReadAsAsync<List<AssetCategoryModel>>());
                //    result = jsonres.Unwrap().Result;
                //    cache.Add("AssetCategorys", result, new CacheItemPolicy());
                //}

                //return result;




                IList<MaintenanceRepairTypeModel> result = HttpRuntime.Cache["MaintenanceRepairTypes"] as IList<MaintenanceRepairTypeModel>;// HttpContext.Current.Session["MaintenanceRepairTypes"] as IList<MaintenanceRepairTypeModel>;
                if (result == null || ShouldRefreshService.GetShouldRefreshMaintenanceRepairTypes())
                {
                    if (result != null)
                        HttpRuntime.Cache.Remove("MaintenanceRepairTypes");
                    var client = new HttpClient();
                    client.BaseAddress = new Uri(Settings.ApiAddress());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());
                    var request = client.GetAsync("api/GetMaintenanceRepairTypes");
                    var jsonres = request.ContinueWith(http =>
                        http.Result.Content.ReadAsAsync<List<MaintenanceRepairTypeModel>>());
                    result = jsonres.Unwrap().Result;
                    HttpRuntime.Cache.Insert("MaintenanceRepairTypes", result);
                    ShouldRefreshService.SetShouldRefreshMaintenanceRepairTypes(false);
                }

                return result;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public static async Task<ObjectResponse> CreateMaintenanceRepairType(MaintenanceRepairTypeModel mod)
        {
            ObjectResponse _resp = new ObjectResponse();
            try
            {

                MaintenanceRepairTypeModel _added;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Settings.ApiAddress());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

                var response = await client.PutAsJsonAsync("api/MaintenanceRepairType/add", mod).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    _added = JsonConvert.DeserializeObject<MaintenanceRepairTypeModel>(await response.Content.ReadAsStringAsync());
                    _resp.Response = _added;
                    ShouldRefreshService.SetShouldRefreshMaintenanceRepairTypes(true);
                }
                else
                {
                    ErrorResponse _err = JsonConvert.DeserializeObject<ErrorResponse>(response.Content.ReadAsStringAsync().Result);
                    _resp.error = _err.Message;
                }
            }
            catch (Exception ex)
            {
                _resp.error = ex.Message;
            }
            return _resp;

        }

        public static async Task<ObjectResponse> UpdateMaintenanceRepairType(MaintenanceRepairTypeModel mod)
        {
            ObjectResponse _resp = new ObjectResponse();
            try
            {

                MaintenanceRepairTypeModel _added;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Settings.ApiAddress());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

                var response = await client.PostAsJsonAsync("api/MaintenanceRepairType/update", mod).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    _added = JsonConvert.DeserializeObject<MaintenanceRepairTypeModel>(await response.Content.ReadAsStringAsync());
                    _resp.Response = _added;
                    ShouldRefreshService.SetShouldRefreshMaintenanceRepairTypes(true);
                }
                else
                {
                    ErrorResponse _err = JsonConvert.DeserializeObject<ErrorResponse>(await response.Content.ReadAsStringAsync());
                    _resp.error = _err.Message;
                }
            }
            catch (Exception ex)
            {

                _resp.error = ex.Message;
            }
            return _resp;
        }

        public static MaintenanceRepairTypeModel OneMaintenanceRepairType(Func<MaintenanceRepairTypeModel, bool> predicate)
        {
            return ReadMaintenanceRepairTypes().Result.FirstOrDefault(predicate);
        }

        public static MaintenanceRepairTypeModel OneMaintenanceRepairType(Guid id)
        {
            return ReadMaintenanceRepairTypes().Result.Where(x=>x.MaintenanceRepairTypeID==id).FirstOrDefault();
        }


        public static async Task<ObjectResponse> RemoveMaintenanceRepairType(Guid id)
        {
            ObjectResponse _resp = new ObjectResponse();
            try
            {
                bool _deleted = false;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Settings.ApiAddress());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

                var response = await client.DeleteAsync("api/MaintenanceRepairType/delete/" + id.ToString());

                if (response.IsSuccessStatusCode)
                {
                    _deleted = true;
                    ShouldRefreshService.SetShouldRefreshMaintenanceRepairTypes(true);
                }
                else
                {
                    ErrorResponse _err = JsonConvert.DeserializeObject<ErrorResponse>(await response.Content.ReadAsStringAsync());
                    _resp.Response = false;
                    _resp.error = _err.Message;
                }
                _resp.Response = _deleted;
            }
            catch (Exception ex)
            {
                _resp.Response = false;
                _resp.error = ex.Message;
            }
            return _resp;
        }
	}
}

