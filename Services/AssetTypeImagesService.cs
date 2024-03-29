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

	public static partial class AssetTypeImagesService 
	{

        public static async Task<IList<AssetTypeImageModel>> ReadAssetTypeImages()
        {
            try
            {
              //  IList <AssetTypeImageModel> result = HttpContext.Current.Session["AssetTypeImages"] as IList<AssetTypeImageModel>;
  
              //if (result == null || refresh)
              //  {
              //      IEnumerable <AssetTypeImageModel> _list = null;
  
              //    using (var client = new HttpClient())
              //      {
              //          client.BaseAddress = new Uri(Settings.ApiAddress());
              //          client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
              //          client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

              //          HttpResponseMessage response = await client.GetAsync("api/GetAssetTypeImages");
              //          if (response.IsSuccessStatusCode)
              //          {
              //              _list = await response.Content.ReadAsAsync < IEnumerable <AssetTypeImageModel>>();
              //            result = _list.ToList();
              //              HttpContext.Current.Session["AssetTypeImages"] = result;
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




                IList<AssetTypeImageModel> result = HttpRuntime.Cache["AssetTypeImages"] as IList<AssetTypeImageModel>;// HttpContext.Current.Session["AssetTypeImages"] as IList<AssetTypeImageModel>;
                if (result == null || ShouldRefreshService.GetShouldRefreshAssetTypeImages())
                {
                    if (result != null)
                        HttpRuntime.Cache.Remove("AssetTypeImages");
                    var client = new HttpClient();
                    client.BaseAddress = new Uri(Settings.ApiAddress());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());
                    var request = client.GetAsync("api/GetAssetTypeImages");
                    var jsonres = request.ContinueWith(http =>
                        http.Result.Content.ReadAsAsync<List<AssetTypeImageModel>>());
                    result = jsonres.Unwrap().Result;
                    HttpRuntime.Cache.Insert("AssetTypeImages", result);
                    ShouldRefreshService.SetShouldRefreshAssetTypeImages(false);
                }

                return result;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public static async Task<ObjectResponse> CreateAssetTypeImage(AssetTypeImageModel mod)
        {
            ObjectResponse _resp = new ObjectResponse();
            try
            {

                AssetTypeImageModel _added;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Settings.ApiAddress());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

                var response = await client.PutAsJsonAsync("api/AssetTypeImage/add", mod).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    _added = JsonConvert.DeserializeObject<AssetTypeImageModel>(await response.Content.ReadAsStringAsync());
                    _resp.Response = _added;
                    ShouldRefreshService.SetShouldRefreshAssetTypeImages(true);
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

        public static async Task<ObjectResponse> UpdateAssetTypeImage(AssetTypeImageModel mod)
        {
            ObjectResponse _resp = new ObjectResponse();
            try
            {

                AssetTypeImageModel _added;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Settings.ApiAddress());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

                var response = await client.PostAsJsonAsync("api/AssetTypeImage/update", mod).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    _added = JsonConvert.DeserializeObject<AssetTypeImageModel>(await response.Content.ReadAsStringAsync());
                    _resp.Response = _added;
                    ShouldRefreshService.SetShouldRefreshAssetTypeImages(true);
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

        public static AssetTypeImageModel OneAssetTypeImage(Func<AssetTypeImageModel, bool> predicate)
        {
            return ReadAssetTypeImages().Result.FirstOrDefault(predicate);
        }

        public static AssetTypeImageModel OneAssetTypeImage(Guid id)
        {
            return ReadAssetTypeImages().Result.Where(x=>x.AssetTypeImageID==id).FirstOrDefault();
        }


        public static async Task<ObjectResponse> RemoveAssetTypeImage(Guid id)
        {
            ObjectResponse _resp = new ObjectResponse();
            try
            {
                bool _deleted = false;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Settings.ApiAddress());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

                var response = await client.DeleteAsync("api/AssetTypeImage/delete/" + id.ToString());

                if (response.IsSuccessStatusCode)
                {
                    _deleted = true;
                    ShouldRefreshService.SetShouldRefreshAssetTypeImages(true);
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

