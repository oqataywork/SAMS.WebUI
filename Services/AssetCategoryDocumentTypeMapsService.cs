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

	public static partial class AssetCategoryDocumentTypeMapsService 
	{

        public static async Task<IList<AssetCategoryDocumentTypeMapModel>> ReadAssetCategoryDocumentTypeMaps()
        {
            try
            {
              //  IList <AssetCategoryDocumentTypeMapModel> result = HttpContext.Current.Session["AssetCategoryDocumentTypeMaps"] as IList<AssetCategoryDocumentTypeMapModel>;
  
              //if (result == null || refresh)
              //  {
              //      IEnumerable <AssetCategoryDocumentTypeMapModel> _list = null;
  
              //    using (var client = new HttpClient())
              //      {
              //          client.BaseAddress = new Uri(Settings.ApiAddress());
              //          client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
              //          client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

              //          HttpResponseMessage response = await client.GetAsync("api/GetAssetCategoryDocumentTypeMaps");
              //          if (response.IsSuccessStatusCode)
              //          {
              //              _list = await response.Content.ReadAsAsync < IEnumerable <AssetCategoryDocumentTypeMapModel>>();
              //            result = _list.ToList();
              //              HttpContext.Current.Session["AssetCategoryDocumentTypeMaps"] = result;
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




                IList<AssetCategoryDocumentTypeMapModel> result = HttpRuntime.Cache["AssetCategoryDocumentTypeMaps"] as IList<AssetCategoryDocumentTypeMapModel>;// HttpContext.Current.Session["AssetCategoryDocumentTypeMaps"] as IList<AssetCategoryDocumentTypeMapModel>;
                if (result == null || ShouldRefreshService.GetShouldRefreshAssetCategoryDocumentTypeMaps())
                {
                    if (result != null)
                        HttpRuntime.Cache.Remove("AssetCategoryDocumentTypeMaps");
                    var client = new HttpClient();
                    client.BaseAddress = new Uri(Settings.ApiAddress());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());
                    var request = client.GetAsync("api/GetAssetCategoryDocumentTypeMaps");
                    var jsonres = request.ContinueWith(http =>
                        http.Result.Content.ReadAsAsync<List<AssetCategoryDocumentTypeMapModel>>());
                    result = jsonres.Unwrap().Result;
                    HttpRuntime.Cache.Insert("AssetCategoryDocumentTypeMaps", result);
                    ShouldRefreshService.SetShouldRefreshAssetCategoryDocumentTypeMaps(false);
                }

                return result;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public static async Task<ObjectResponse> CreateAssetCategoryDocumentTypeMap(AssetCategoryDocumentTypeMapModel mod)
        {
            ObjectResponse _resp = new ObjectResponse();
            try
            {

                AssetCategoryDocumentTypeMapModel _added;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Settings.ApiAddress());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

                var response = await client.PutAsJsonAsync("api/AssetCategoryDocumentTypeMap/add", mod).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    _added = JsonConvert.DeserializeObject<AssetCategoryDocumentTypeMapModel>(await response.Content.ReadAsStringAsync());
                    _resp.Response = _added;
                    ShouldRefreshService.SetShouldRefreshAssetCategoryDocumentTypeMaps(true);
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

        public static async Task<ObjectResponse> UpdateAssetCategoryDocumentTypeMap(AssetCategoryDocumentTypeMapModel mod)
        {
            ObjectResponse _resp = new ObjectResponse();
            try
            {

                AssetCategoryDocumentTypeMapModel _added;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Settings.ApiAddress());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

                var response = await client.PostAsJsonAsync("api/AssetCategoryDocumentTypeMap/update", mod).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    _added = JsonConvert.DeserializeObject<AssetCategoryDocumentTypeMapModel>(await response.Content.ReadAsStringAsync());
                    _resp.Response = _added;
                    ShouldRefreshService.SetShouldRefreshAssetCategoryDocumentTypeMaps(true);
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

        public static AssetCategoryDocumentTypeMapModel OneAssetCategoryDocumentTypeMap(Func<AssetCategoryDocumentTypeMapModel, bool> predicate)
        {
            return ReadAssetCategoryDocumentTypeMaps().Result.FirstOrDefault(predicate);
        }

        public static AssetCategoryDocumentTypeMapModel OneAssetCategoryDocumentTypeMap(Guid id)
        {
            return ReadAssetCategoryDocumentTypeMaps().Result.Where(x=>x.AssetCategoryDocumentTypeMapID==id).FirstOrDefault();
        }


        public static async Task<ObjectResponse> RemoveAssetCategoryDocumentTypeMap(Guid id)
        {
            ObjectResponse _resp = new ObjectResponse();
            try
            {
                bool _deleted = false;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Settings.ApiAddress());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

                var response = await client.DeleteAsync("api/AssetCategoryDocumentTypeMap/delete/" + id.ToString());

                if (response.IsSuccessStatusCode)
                {
                    _deleted = true;
                    ShouldRefreshService.SetShouldRefreshAssetCategoryDocumentTypeMaps(true);
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

