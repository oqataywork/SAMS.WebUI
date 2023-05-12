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

	public static partial class CountriesService 
	{

        public static async Task<IList<CountryModel>> ReadCountrys()
        {
            try
            {
              //  IList <CountryModel> result = HttpContext.Current.Session["Countrys"] as IList<CountryModel>;
  
              //if (result == null || refresh)
              //  {
              //      IEnumerable <CountryModel> _list = null;
  
              //    using (var client = new HttpClient())
              //      {
              //          client.BaseAddress = new Uri(Settings.ApiAddress());
              //          client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
              //          client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

              //          HttpResponseMessage response = await client.GetAsync("api/GetCountrys");
              //          if (response.IsSuccessStatusCode)
              //          {
              //              _list = await response.Content.ReadAsAsync < IEnumerable <CountryModel>>();
              //            result = _list.ToList();
              //              HttpContext.Current.Session["Countrys"] = result;
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




                IList<CountryModel> result = HttpRuntime.Cache["Countrys"] as IList<CountryModel>;// HttpContext.Current.Session["Countrys"] as IList<CountryModel>;
                if (result == null || ShouldRefreshService.GetShouldRefreshCountrys())
                {
                    if (result != null)
                        HttpRuntime.Cache.Remove("Countrys");
                    var client = new HttpClient();
                    client.BaseAddress = new Uri(Settings.ApiAddress());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());
                    var request = client.GetAsync("api/GetCountrys");
                    var jsonres = request.ContinueWith(http =>
                        http.Result.Content.ReadAsAsync<List<CountryModel>>());
                    result = jsonres.Unwrap().Result;
                    HttpRuntime.Cache.Insert("Countrys", result);
                    ShouldRefreshService.SetShouldRefreshCountrys(false);
                }

                return result;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public static async Task<ObjectResponse> CreateCountry(CountryModel mod)
        {
            ObjectResponse _resp = new ObjectResponse();
            try
            {

                CountryModel _added;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Settings.ApiAddress());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

                var response = await client.PutAsJsonAsync("api/Country/add", mod).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    _added = JsonConvert.DeserializeObject<CountryModel>(await response.Content.ReadAsStringAsync());
                    _resp.Response = _added;
                    ShouldRefreshService.SetShouldRefreshCountrys(true);
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

        public static async Task<ObjectResponse> UpdateCountry(CountryModel mod)
        {
            ObjectResponse _resp = new ObjectResponse();
            try
            {

                CountryModel _added;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Settings.ApiAddress());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

                var response = await client.PostAsJsonAsync("api/Country/update", mod).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    _added = JsonConvert.DeserializeObject<CountryModel>(await response.Content.ReadAsStringAsync());
                    _resp.Response = _added;
                    ShouldRefreshService.SetShouldRefreshCountrys(true);
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

        public static CountryModel OneCountry(Func<CountryModel, bool> predicate)
        {
            return ReadCountrys().Result.FirstOrDefault(predicate);
        }

        public static CountryModel OneCountry(Guid id)
        {
            return ReadCountrys().Result.Where(x=>x.CountryID==id).FirstOrDefault();
        }


        public static async Task<ObjectResponse> RemoveCountry(Guid id)
        {
            ObjectResponse _resp = new ObjectResponse();
            try
            {
                bool _deleted = false;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Settings.ApiAddress());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

                var response = await client.DeleteAsync("api/Country/delete/" + id.ToString());

                if (response.IsSuccessStatusCode)
                {
                    _deleted = true;
                    ShouldRefreshService.SetShouldRefreshCountrys(true);
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

