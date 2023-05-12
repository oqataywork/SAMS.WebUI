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

	public static partial class AssetEmplacementsService 
	{

        public static async Task<IList<AssetEmplacementModel>> ReadAssetEmplacements()
        {
            try
            {
              //  IList <AssetEmplacementModel> result = HttpContext.Current.Session["AssetEmplacements"] as IList<AssetEmplacementModel>;
  
              //if (result == null || refresh)
              //  {
              //      IEnumerable <AssetEmplacementModel> _list = null;
  
              //    using (var client = new HttpClient())
              //      {
              //          client.BaseAddress = new Uri(Settings.ApiAddress());
              //          client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
              //          client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

              //          HttpResponseMessage response = await client.GetAsync("api/GetAssetEmplacements");
              //          if (response.IsSuccessStatusCode)
              //          {
              //              _list = await response.Content.ReadAsAsync < IEnumerable <AssetEmplacementModel>>();
              //            result = _list.ToList();
              //              HttpContext.Current.Session["AssetEmplacements"] = result;
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




                IList<AssetEmplacementModel> result = HttpRuntime.Cache["AssetEmplacements"] as IList<AssetEmplacementModel>;// HttpContext.Current.Session["AssetEmplacements"] as IList<AssetEmplacementModel>;
                if (result == null || ShouldRefreshService.GetShouldRefreshAssetEmplacements())
                {
                    if (result != null)
                        HttpRuntime.Cache.Remove("AssetEmplacements");
                    var client = new HttpClient();
                    client.BaseAddress = new Uri(Settings.ApiAddress());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());
                    var request = client.GetAsync("api/GetAssetEmplacements");
                    var jsonres = request.ContinueWith(http =>
                        http.Result.Content.ReadAsAsync<List<AssetEmplacementModel>>());
                    result = jsonres.Unwrap().Result;
                    HttpRuntime.Cache.Insert("AssetEmplacements", result);
                    ShouldRefreshService.SetShouldRefreshAssetEmplacements(false);
                }

                return result;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public static async Task<ObjectResponse> CreateAssetEmplacement(AssetEmplacementModel mod)
        {
            ObjectResponse _resp = new ObjectResponse();
            try
            {

                AssetEmplacementModel _added;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Settings.ApiAddress());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

                var response = await client.PutAsJsonAsync("api/AssetEmplacement/add", mod).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    _added = JsonConvert.DeserializeObject<AssetEmplacementModel>(await response.Content.ReadAsStringAsync());
                    _resp.Response = _added;
                    ShouldRefreshService.SetShouldRefreshAssetEmplacements(true);
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

        public static async Task<ObjectResponse> UpdateAssetEmplacement(AssetEmplacementModel mod)
        {
            ObjectResponse _resp = new ObjectResponse();
            try
            {

                AssetEmplacementModel _added;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Settings.ApiAddress());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

                var response = await client.PostAsJsonAsync("api/AssetEmplacement/update", mod).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    _added = JsonConvert.DeserializeObject<AssetEmplacementModel>(await response.Content.ReadAsStringAsync());
                    _resp.Response = _added;
                    ShouldRefreshService.SetShouldRefreshAssetEmplacements(true);
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

        public static AssetEmplacementModel OneAssetEmplacement(Func<AssetEmplacementModel, bool> predicate)
        {
            return ReadAssetEmplacements().Result.FirstOrDefault(predicate);
        }

        public static AssetEmplacementModel OneAssetEmplacement(Guid id)
        {
            return ReadAssetEmplacements().Result.Where(x=>x.AssetEmplacementID==id).FirstOrDefault();
        }


        public static async Task<ObjectResponse> RemoveAssetEmplacement(Guid id)
        {
            ObjectResponse _resp = new ObjectResponse();
            try
            {
                bool _deleted = false;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Settings.ApiAddress());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

                var response = await client.DeleteAsync("api/AssetEmplacement/delete/" + id.ToString());

                if (response.IsSuccessStatusCode)
                {
                    _deleted = true;
                    ShouldRefreshService.SetShouldRefreshAssetEmplacements(true);
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

