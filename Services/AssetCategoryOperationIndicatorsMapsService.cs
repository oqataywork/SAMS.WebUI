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

	public static partial class AssetCategoryOperationIndicatorsMapsService 
	{

        public static async Task<IList<AssetCategoryOperationIndicatorsMapModel>> ReadAssetCategoryOperationIndicatorsMaps()
        {
            try
            {
              //  IList <AssetCategoryOperationIndicatorsMapModel> result = HttpContext.Current.Session["AssetCategoryOperationIndicatorsMaps"] as IList<AssetCategoryOperationIndicatorsMapModel>;
  
              //if (result == null || refresh)
              //  {
              //      IEnumerable <AssetCategoryOperationIndicatorsMapModel> _list = null;
  
              //    using (var client = new HttpClient())
              //      {
              //          client.BaseAddress = new Uri(Settings.ApiAddress());
              //          client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
              //          client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

              //          HttpResponseMessage response = await client.GetAsync("api/GetAssetCategoryOperationIndicatorsMaps");
              //          if (response.IsSuccessStatusCode)
              //          {
              //              _list = await response.Content.ReadAsAsync < IEnumerable <AssetCategoryOperationIndicatorsMapModel>>();
              //            result = _list.ToList();
              //              HttpContext.Current.Session["AssetCategoryOperationIndicatorsMaps"] = result;
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




                IList<AssetCategoryOperationIndicatorsMapModel> result = HttpRuntime.Cache["AssetCategoryOperationIndicatorsMaps"] as IList<AssetCategoryOperationIndicatorsMapModel>;// HttpContext.Current.Session["AssetCategoryOperationIndicatorsMaps"] as IList<AssetCategoryOperationIndicatorsMapModel>;
                if (result == null || ShouldRefreshService.GetShouldRefreshAssetCategoryOperationIndicatorsMaps())
                {
                    if (result != null)
                        HttpRuntime.Cache.Remove("AssetCategoryOperationIndicatorsMaps");
                    var client = new HttpClient();
                    client.BaseAddress = new Uri(Settings.ApiAddress());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());
                    var request = client.GetAsync("api/GetAssetCategoryOperationIndicatorsMaps");
                    var jsonres = request.ContinueWith(http =>
                        http.Result.Content.ReadAsAsync<List<AssetCategoryOperationIndicatorsMapModel>>());
                    result = jsonres.Unwrap().Result;
                    HttpRuntime.Cache.Insert("AssetCategoryOperationIndicatorsMaps", result);
                    ShouldRefreshService.SetShouldRefreshAssetCategoryOperationIndicatorsMaps(false);
                }

                return result;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public static async Task<ObjectResponse> CreateAssetCategoryOperationIndicatorsMap(AssetCategoryOperationIndicatorsMapModel mod)
        {
            ObjectResponse _resp = new ObjectResponse();
            try
            {

                AssetCategoryOperationIndicatorsMapModel _added;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Settings.ApiAddress());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

                var response = await client.PutAsJsonAsync("api/AssetCategoryOperationIndicatorsMap/add", mod).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    _added = JsonConvert.DeserializeObject<AssetCategoryOperationIndicatorsMapModel>(await response.Content.ReadAsStringAsync());
                    _resp.Response = _added;
                    ShouldRefreshService.SetShouldRefreshAssetCategoryOperationIndicatorsMaps(true);
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

        public static async Task<ObjectResponse> UpdateAssetCategoryOperationIndicatorsMap(AssetCategoryOperationIndicatorsMapModel mod)
        {
            ObjectResponse _resp = new ObjectResponse();
            try
            {

                AssetCategoryOperationIndicatorsMapModel _added;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Settings.ApiAddress());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

                var response = await client.PostAsJsonAsync("api/AssetCategoryOperationIndicatorsMap/update", mod).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    _added = JsonConvert.DeserializeObject<AssetCategoryOperationIndicatorsMapModel>(await response.Content.ReadAsStringAsync());
                    _resp.Response = _added;
                    ShouldRefreshService.SetShouldRefreshAssetCategoryOperationIndicatorsMaps(true);
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

        public static AssetCategoryOperationIndicatorsMapModel OneAssetCategoryOperationIndicatorsMap(Func<AssetCategoryOperationIndicatorsMapModel, bool> predicate)
        {
            return ReadAssetCategoryOperationIndicatorsMaps().Result.FirstOrDefault(predicate);
        }

        public static AssetCategoryOperationIndicatorsMapModel OneAssetCategoryOperationIndicatorsMap(Guid id)
        {
            return ReadAssetCategoryOperationIndicatorsMaps().Result.Where(x=>x.AssetCategoryOperationIndicatorID==id).FirstOrDefault();
        }


        public static async Task<ObjectResponse> RemoveAssetCategoryOperationIndicatorsMap(Guid id)
        {
            ObjectResponse _resp = new ObjectResponse();
            try
            {
                bool _deleted = false;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Settings.ApiAddress());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

                var response = await client.DeleteAsync("api/AssetCategoryOperationIndicatorsMap/delete/" + id.ToString());

                if (response.IsSuccessStatusCode)
                {
                    _deleted = true;
                    ShouldRefreshService.SetShouldRefreshAssetCategoryOperationIndicatorsMaps(true);
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

