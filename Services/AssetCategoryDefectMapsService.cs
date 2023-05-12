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
using SAMS.WebUI.Services.Partials;

namespace SAMS.WebUI.Services
{

	public static partial class AssetCategoryDefectMapsService 
	{

        public static async Task<IList<AssetCategoryDefectMapModel>> ReadAssetCategoryDefectMaps()
        {
            try
            {
              //  IList <AssetCategoryDefectMapModel> result = HttpContext.Current.Session["AssetCategoryDefectMaps"] as IList<AssetCategoryDefectMapModel>;
  
              //if (result == null || refresh)
              //  {
              //      IEnumerable <AssetCategoryDefectMapModel> _list = null;
  
              //    using (var client = new HttpClient())
              //      {
              //          client.BaseAddress = new Uri(Settings.ApiAddress());
              //          client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
              //          client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

              //          HttpResponseMessage response = await client.GetAsync("api/GetAssetCategoryDefectMaps");
              //          if (response.IsSuccessStatusCode)
              //          {
              //              _list = await response.Content.ReadAsAsync < IEnumerable <AssetCategoryDefectMapModel>>();
              //            result = _list.ToList();
              //              HttpContext.Current.Session["AssetCategoryDefectMaps"] = result;
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




                IList<AssetCategoryDefectMapModel> result = HttpRuntime.Cache["AssetCategoryDefectMaps"] as IList<AssetCategoryDefectMapModel>;// HttpContext.Current.Session["AssetCategoryDefectMaps"] as IList<AssetCategoryDefectMapModel>;
                if (result == null || ShouldRefreshService.GetShouldRefreshAssetCategoryDefectMaps())
                {
                    if (result != null)
                        HttpRuntime.Cache.Remove("AssetCategoryDefectMaps");
                    var client = new HttpClient();
                    client.BaseAddress = new Uri(Settings.ApiAddress());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());
                    var request = client.GetAsync("api/GetAssetCategoryDefectMaps");
                    var jsonres = request.ContinueWith(http =>
                        http.Result.Content.ReadAsAsync<List<AssetCategoryDefectMapModel>>());
                    result = jsonres.Unwrap().Result;
                    HttpRuntime.Cache.Insert("AssetCategoryDefectMaps", result);
                    ShouldRefreshService.SetShouldRefreshAssetCategoryDefectMaps(false);
                }

                return result;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public static async Task<ObjectResponse> CreateAssetCategoryDefectMap(AssetCategoryDefectMapModel mod)
        {
            ObjectResponse _resp = new ObjectResponse();
            try
            {

                AssetCategoryDefectMapModel _added;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Settings.ApiAddress());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

                var response = await client.PutAsJsonAsync("api/AssetCategoryDefectMap/add", mod).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    _added = JsonConvert.DeserializeObject<AssetCategoryDefectMapModel>(await response.Content.ReadAsStringAsync());
                    _resp.Response = _added;
                    ShouldRefreshService.SetShouldRefreshAssetCategoryDefectMaps(true);
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

        public static async Task<ObjectResponse> UpdateAssetCategoryDefectMap(AssetCategoryDefectMapModel mod)
        {
            ObjectResponse _resp = new ObjectResponse();
            try
            {

                AssetCategoryDefectMapModel _added;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Settings.ApiAddress());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

                var response = await client.PostAsJsonAsync("api/AssetCategoryDefectMap/update", mod).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    _added = JsonConvert.DeserializeObject<AssetCategoryDefectMapModel>(await response.Content.ReadAsStringAsync());
                    _resp.Response = _added;
                    ShouldRefreshService.SetShouldRefreshAssetCategoryDefectMaps(true);
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

        public static AssetCategoryDefectMapModel OneAssetCategoryDefectMap(Func<AssetCategoryDefectMapModel, bool> predicate)
        {
            return ReadAssetCategoryDefectMaps().Result.FirstOrDefault(predicate);
        }

        public static AssetCategoryDefectMapModel OneAssetCategoryDefectMap(Guid id)
        {
            return ReadAssetCategoryDefectMaps().Result.Where(x=>x.AssetCategoryDefectMapID==id).FirstOrDefault();
        }


        public static async Task<ObjectResponse> RemoveAssetCategoryDefectMap(Guid id)
        {
            ObjectResponse _resp = new ObjectResponse();
            try
            {
                bool _deleted = false;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Settings.ApiAddress());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

                var response = await client.DeleteAsync("api/AssetCategoryDefectMap/delete/" + id.ToString());

                if (response.IsSuccessStatusCode)
                {
                    _deleted = true;
                    ShouldRefreshService.SetShouldRefreshAssetCategoryDefectMaps(true);
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

