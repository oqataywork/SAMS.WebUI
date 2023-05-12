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

	public static partial class MeasurementUnitTypesService 
	{

        public static async Task<IList<MeasurementUnitTypeModel>> ReadMeasurementUnitTypes()
        {
            try
            {
              //  IList <MeasurementUnitTypeModel> result = HttpContext.Current.Session["MeasurementUnitTypes"] as IList<MeasurementUnitTypeModel>;
  
              //if (result == null || refresh)
              //  {
              //      IEnumerable <MeasurementUnitTypeModel> _list = null;
  
              //    using (var client = new HttpClient())
              //      {
              //          client.BaseAddress = new Uri(Settings.ApiAddress());
              //          client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
              //          client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

              //          HttpResponseMessage response = await client.GetAsync("api/GetMeasurementUnitTypes");
              //          if (response.IsSuccessStatusCode)
              //          {
              //              _list = await response.Content.ReadAsAsync < IEnumerable <MeasurementUnitTypeModel>>();
              //            result = _list.ToList();
              //              HttpContext.Current.Session["MeasurementUnitTypes"] = result;
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




                IList<MeasurementUnitTypeModel> result = HttpRuntime.Cache["MeasurementUnitTypes"] as IList<MeasurementUnitTypeModel>;// HttpContext.Current.Session["MeasurementUnitTypes"] as IList<MeasurementUnitTypeModel>;
                if (result == null || ShouldRefreshService.GetShouldRefreshMeasurementUnitTypes())
                {
                    if (result != null)
                        HttpRuntime.Cache.Remove("MeasurementUnitTypes");
                    var client = new HttpClient();
                    client.BaseAddress = new Uri(Settings.ApiAddress());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());
                    var request = client.GetAsync("api/GetMeasurementUnitTypes");
                    var jsonres = request.ContinueWith(http =>
                        http.Result.Content.ReadAsAsync<List<MeasurementUnitTypeModel>>());
                    result = jsonres.Unwrap().Result;
                    HttpRuntime.Cache.Insert("MeasurementUnitTypes", result);
                    ShouldRefreshService.SetShouldRefreshMeasurementUnitTypes(false);
                }

                return result;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public static async Task<ObjectResponse> CreateMeasurementUnitType(MeasurementUnitTypeModel mod)
        {
            ObjectResponse _resp = new ObjectResponse();
            try
            {

                MeasurementUnitTypeModel _added;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Settings.ApiAddress());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

                var response = await client.PutAsJsonAsync("api/MeasurementUnitType/add", mod).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    _added = JsonConvert.DeserializeObject<MeasurementUnitTypeModel>(await response.Content.ReadAsStringAsync());
                    _resp.Response = _added;
                    ShouldRefreshService.SetShouldRefreshMeasurementUnitTypes(true);
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

        public static async Task<ObjectResponse> UpdateMeasurementUnitType(MeasurementUnitTypeModel mod)
        {
            ObjectResponse _resp = new ObjectResponse();
            try
            {

                MeasurementUnitTypeModel _added;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Settings.ApiAddress());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

                var response = await client.PostAsJsonAsync("api/MeasurementUnitType/update", mod).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    _added = JsonConvert.DeserializeObject<MeasurementUnitTypeModel>(await response.Content.ReadAsStringAsync());
                    _resp.Response = _added;
                    ShouldRefreshService.SetShouldRefreshMeasurementUnitTypes(true);
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

        public static MeasurementUnitTypeModel OneMeasurementUnitType(Func<MeasurementUnitTypeModel, bool> predicate)
        {
            return ReadMeasurementUnitTypes().Result.FirstOrDefault(predicate);
        }

        public static MeasurementUnitTypeModel OneMeasurementUnitType(Guid id)
        {
            return ReadMeasurementUnitTypes().Result.Where(x=>x.MeasurementUnitTypeID==id).FirstOrDefault();
        }


        public static async Task<ObjectResponse> RemoveMeasurementUnitType(Guid id)
        {
            ObjectResponse _resp = new ObjectResponse();
            try
            {
                bool _deleted = false;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Settings.ApiAddress());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

                var response = await client.DeleteAsync("api/MeasurementUnitType/delete/" + id.ToString());

                if (response.IsSuccessStatusCode)
                {
                    _deleted = true;
                    ShouldRefreshService.SetShouldRefreshMeasurementUnitTypes(true);
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

