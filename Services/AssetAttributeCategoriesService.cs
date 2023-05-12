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

	public static partial class AssetAttributeCategoriesService 
	{

        public static async Task<IList<AssetAttributeCategoryModel>> ReadAssetAttributeCategorys()
        {
            try
            {
              //  IList <AssetAttributeCategoryModel> result = HttpContext.Current.Session["AssetAttributeCategorys"] as IList<AssetAttributeCategoryModel>;
  
              //if (result == null || refresh)
              //  {
              //      IEnumerable <AssetAttributeCategoryModel> _list = null;
  
              //    using (var client = new HttpClient())
              //      {
              //          client.BaseAddress = new Uri(Settings.ApiAddress());
              //          client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
              //          client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

              //          HttpResponseMessage response = await client.GetAsync("api/GetAssetAttributeCategorys");
              //          if (response.IsSuccessStatusCode)
              //          {
              //              _list = await response.Content.ReadAsAsync < IEnumerable <AssetAttributeCategoryModel>>();
              //            result = _list.ToList();
              //              HttpContext.Current.Session["AssetAttributeCategorys"] = result;
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




                IList<AssetAttributeCategoryModel> result = HttpRuntime.Cache["AssetAttributeCategorys"] as IList<AssetAttributeCategoryModel>;// HttpContext.Current.Session["AssetAttributeCategorys"] as IList<AssetAttributeCategoryModel>;
                if (result == null || ShouldRefreshService.GetShouldRefreshAssetAttributeCategorys())
                {
                    if (result != null)
                        HttpRuntime.Cache.Remove("AssetAttributeCategorys");
                    var client = new HttpClient();
                    client.BaseAddress = new Uri(Settings.ApiAddress());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());
                    var request = client.GetAsync("api/GetAssetAttributeCategorys");
                    var jsonres = request.ContinueWith(http =>
                        http.Result.Content.ReadAsAsync<List<AssetAttributeCategoryModel>>());
                    result = jsonres.Unwrap().Result;
                    HttpRuntime.Cache.Insert("AssetAttributeCategorys", result);
                    ShouldRefreshService.SetShouldRefreshAssetAttributeCategorys(false);
                }

                return result;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public static async Task<ObjectResponse> CreateAssetAttributeCategory(AssetAttributeCategoryModel mod)
        {
            ObjectResponse _resp = new ObjectResponse();
            try
            {

                AssetAttributeCategoryModel _added;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Settings.ApiAddress());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

                var response = await client.PutAsJsonAsync("api/AssetAttributeCategory/add", mod).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    _added = JsonConvert.DeserializeObject<AssetAttributeCategoryModel>(await response.Content.ReadAsStringAsync());
                    _resp.Response = _added;
                    ShouldRefreshService.SetShouldRefreshAssetAttributeCategorys(true);
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

        public static async Task<ObjectResponse> UpdateAssetAttributeCategory(AssetAttributeCategoryModel mod)
        {
            ObjectResponse _resp = new ObjectResponse();
            try
            {

                AssetAttributeCategoryModel _added;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Settings.ApiAddress());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

                var response = await client.PostAsJsonAsync("api/AssetAttributeCategory/update", mod).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    _added = JsonConvert.DeserializeObject<AssetAttributeCategoryModel>(await response.Content.ReadAsStringAsync());
                    _resp.Response = _added;
                    ShouldRefreshService.SetShouldRefreshAssetAttributeCategorys(true);
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

        public static AssetAttributeCategoryModel OneAssetAttributeCategory(Func<AssetAttributeCategoryModel, bool> predicate)
        {
            return ReadAssetAttributeCategorys().Result.FirstOrDefault(predicate);
        }

        public static AssetAttributeCategoryModel OneAssetAttributeCategory(Guid id)
        {
            return ReadAssetAttributeCategorys().Result.Where(x=>x.AssetAttributeCategoryID==id).FirstOrDefault();
        }


        public static async Task<ObjectResponse> RemoveAssetAttributeCategory(Guid id)
        {
            ObjectResponse _resp = new ObjectResponse();
            try
            {
                bool _deleted = false;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Settings.ApiAddress());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

                var response = await client.DeleteAsync("api/AssetAttributeCategory/delete/" + id.ToString());

                if (response.IsSuccessStatusCode)
                {
                    _deleted = true;
                    ShouldRefreshService.SetShouldRefreshAssetAttributeCategorys(true);
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
