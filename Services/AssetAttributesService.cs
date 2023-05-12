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

	public static partial class AssetAttributesService 
	{

        public static async Task<IList<AssetAttributeModel>> ReadAssetAttributes()
        {
            try
            {
              //  IList <AssetAttributeModel> result = HttpContext.Current.Session["AssetAttributes"] as IList<AssetAttributeModel>;
  
              //if (result == null || refresh)
              //  {
              //      IEnumerable <AssetAttributeModel> _list = null;
  
              //    using (var client = new HttpClient())
              //      {
              //          client.BaseAddress = new Uri(Settings.ApiAddress());
              //          client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
              //          client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

              //          HttpResponseMessage response = await client.GetAsync("api/GetAssetAttributes");
              //          if (response.IsSuccessStatusCode)
              //          {
              //              _list = await response.Content.ReadAsAsync < IEnumerable <AssetAttributeModel>>();
              //            result = _list.ToList();
              //              HttpContext.Current.Session["AssetAttributes"] = result;
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




                IList<AssetAttributeModel> result = HttpRuntime.Cache["AssetAttributes"] as IList<AssetAttributeModel>;// HttpContext.Current.Session["AssetAttributes"] as IList<AssetAttributeModel>;
                if (result == null || ShouldRefreshService.GetShouldRefreshAssetAttributes())
                {
                    if (result != null)
                        HttpRuntime.Cache.Remove("AssetAttributes");
                    var client = new HttpClient();
                    client.BaseAddress = new Uri(Settings.ApiAddress());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());
                    var request = client.GetAsync("api/GetAssetAttributes");
                    var jsonres = request.ContinueWith(http =>
                        http.Result.Content.ReadAsAsync<List<AssetAttributeModel>>());
                    result = jsonres.Unwrap().Result;
                    HttpRuntime.Cache.Insert("AssetAttributes", result);
                    ShouldRefreshService.SetShouldRefreshAssetAttributes(false);
                }

                return result;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public static async Task<ObjectResponse> CreateAssetAttribute(AssetAttributeModel mod)
        {
            ObjectResponse _resp = new ObjectResponse();
            try
            {

                AssetAttributeModel _added;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Settings.ApiAddress());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

                var response = await client.PutAsJsonAsync("api/AssetAttribute/add", mod).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    _added = JsonConvert.DeserializeObject<AssetAttributeModel>(await response.Content.ReadAsStringAsync());
                    _resp.Response = _added;
                    ShouldRefreshService.SetShouldRefreshAssetAttributes(true);
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

        public static async Task<ObjectResponse> UpdateAssetAttribute(AssetAttributeModel mod)
        {
            ObjectResponse _resp = new ObjectResponse();
            try
            {

                AssetAttributeModel _added;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Settings.ApiAddress());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

                var response = await client.PostAsJsonAsync("api/AssetAttribute/update", mod).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    _added = JsonConvert.DeserializeObject<AssetAttributeModel>(await response.Content.ReadAsStringAsync());
                    _resp.Response = _added;
                    ShouldRefreshService.SetShouldRefreshAssetAttributes(true);
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

        public static AssetAttributeModel OneAssetAttribute(Func<AssetAttributeModel, bool> predicate)
        {
            return ReadAssetAttributes().Result.FirstOrDefault(predicate);
        }

        public static AssetAttributeModel OneAssetAttribute(Guid id)
        {
            return ReadAssetAttributes().Result.Where(x=>x.AssetAttributeID==id).FirstOrDefault();
        }


        public static async Task<ObjectResponse> RemoveAssetAttribute(Guid id)
        {
            ObjectResponse _resp = new ObjectResponse();
            try
            {
                bool _deleted = false;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Settings.ApiAddress());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

                var response = await client.DeleteAsync("api/AssetAttribute/delete/" + id.ToString());

                if (response.IsSuccessStatusCode)
                {
                    _deleted = true;
                    ShouldRefreshService.SetShouldRefreshAssetAttributes(true);
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

