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
using SAMS.Core.ViewModels;
using SAMS.Core.Helpers;

namespace SAMS.WebUI.Services.Partials
{
    public static class PartialService
    {
 
        public static async Task<IEnumerable<AssetCategoryViewModel>> GetCategoriesTree()
        {
            try
            {
                //if (HttpContext.Current.Session["GetCategoriesTree"] == null || _shouldRefreshGetCategoriesTree)
                //{
                //    IEnumerable<AssetCategoryViewModel> _list = null;
                //    using (var client = new HttpClient())
                //    {
                //        client.BaseAddress = new Uri(Settings.ApiAddress());
                //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //        client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

                //        HttpResponseMessage response = await client.GetAsync("api/GetAssetCategorysTree").ConfigureAwait(false);
                //        if (response.IsSuccessStatusCode)
                //        {
                //            _list = await response.Content.ReadAsAsync<IEnumerable<AssetCategoryViewModel>>();
                //            _shouldRefreshGetCategoriesTree = false;
                //        }
                //        else
                //            throw new Exception(response.ReasonPhrase);
                //    }
                //    HttpContext.Current.Session["GetCategoriesTree"] = _list;
                //}
                //return HttpContext.Current.Session["GetCategoriesTree"] as IEnumerable<AssetCategoryViewModel>;


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


                //IList<AssetCategoryModel> result = HttpRuntime.Cache["AssetCategorys"] as IList<AssetCategoryModel>;
                //if (result == null || refresh)
                //{
                //    if (result != null)
                //        HttpRuntime.Cache.Remove("AssetCategorys");
                //    var client = new HttpClient();
                //    client.BaseAddress = new Uri(Settings.ApiAddress());
                //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //    client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());
                //    var request = client.GetAsync("api/GetAssetCategorys");
                //    var jsonres = request.ContinueWith(http =>
                //        http.Result.Content.ReadAsAsync<List<AssetCategoryModel>>());
                //    result = jsonres.Unwrap().Result;
                //    HttpRuntime.Cache.Insert("AssetCategorys", result, null, DateTime.Now.AddHours(24), System.Web.Caching.Cache.NoSlidingExpiration);
                //}
                //return result;


                IList<AssetCategoryViewModel> result = HttpRuntime.Cache["GetCategoriesTree"] as IList<AssetCategoryViewModel>;
                if (result == null || ShouldRefreshService.GetShouldRefreshGetCategoriesTree())
                {
                    if (result != null)
                        HttpRuntime.Cache.Remove("GetCategoriesTree");
                    var client = new HttpClient();
                    client.BaseAddress = new Uri(Settings.ApiAddress());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());
                    var request = client.GetAsync("api/GetAssetCategorysTree");
                    var jsonres = request.ContinueWith(http =>
                        http.Result.Content.ReadAsAsync<List<AssetCategoryViewModel>>());
                    result = jsonres.Unwrap().Result;
                    HttpRuntime.Cache.Insert("GetCategoriesTree", result);
                    ShouldRefreshService.SetShouldRefreshGetCategoriesTree(false);
                }

                return result;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public static async Task<IEnumerable<IntKeyValue>> GetAssetCategoryTypesEnum()
        {
            try
            {
                //IEnumerable<IntKeyValue> _list = null;

                //if (HttpContext.Current.Session["GetAssetCategoryTypesEnum"] == null)
                //{
                //    using (var client = new HttpClient())
                //    {
                //        client.BaseAddress = new Uri(Settings.ApiAddress());
                //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //        client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

                //        HttpResponseMessage response = await client.GetAsync("api/AssetCategoryTypesEnum").ConfigureAwait(false);
                //        if (response.IsSuccessStatusCode)
                //        {
                //            _list = await response.Content.ReadAsAsync<IEnumerable<IntKeyValue>>();
                //        }
                //        else
                //            throw new Exception(response.ReasonPhrase);
                //    }
                //    HttpContext.Current.Session["GetAssetCategoryTypesEnum"] = _list;
                //}
                //return HttpContext.Current.Session["GetAssetCategoryTypesEnum"] as IEnumerable<IntKeyValue>;

                IList<IntKeyValue> result = HttpRuntime.Cache["GetCategoriesTree"] as IList<IntKeyValue>;
                if (result == null)
                {
                    var client = new HttpClient();
                    client.BaseAddress = new Uri(Settings.ApiAddress());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());
                    var request = client.GetAsync("api/AssetCategoryTypesEnum");
                    var jsonres = request.ContinueWith(http =>
                        http.Result.Content.ReadAsAsync<List<IntKeyValue>>());
                    result = jsonres.Unwrap().Result;
                    HttpRuntime.Cache.Insert("GetAssetCategoryTypesEnum", result);
                }

                return result;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        public static async Task<List<Guid>> GetAllDescendantAssetCategoriesByID(string id)
        {
            try
            {
                List<Guid> _list = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Settings.ApiAddress());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

                    HttpResponseMessage response = await client.GetAsync("api/GetAllDescndantAssetCategoriesGuids/?id=" + id).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        _list = await response.Content.ReadAsAsync<List<Guid>>();
                    }
                    else
                        throw new Exception(response.ReasonPhrase);
                }


                return _list;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public static async Task<List<Guid>> GetAllDescendantDepartmentsByID(string id)
        {
            try
            {
                List<Guid> _list = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Settings.ApiAddress());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

                    HttpResponseMessage response = await client.GetAsync("api/GetAllDescndantDepartmentsGuids/?id=" + id).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        _list = await response.Content.ReadAsAsync<List<Guid>>();
                    }
                    else
                        throw new Exception(response.ReasonPhrase);
                }


                return _list;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        private static IEnumerable<AssetCategoryModel> FindAllParents(List<AssetCategoryModel> all_data, AssetCategoryModel child)
        {
            var parent = all_data.FirstOrDefault(x => x.AssetCategoryID == child.ParentAssetCategoryID);

            if (parent == null)
                return Enumerable.Empty<AssetCategoryModel>();

            return new[] { parent }.Concat(FindAllParents(all_data, parent));
        }

        public static async Task<List<Guid>> GetAllParentAssetCategoriesByID(string id)
        {
            try
            {
                List<Guid> _list = null;
                if(HttpContext.Current.Session["SelectedAssetCategoryID"]!=null)
                {
                    string sessionid = HttpContext.Current.Session["SelectedAssetCategoryID"].ToString();
                    if(sessionid.ToLower()==id.ToLower())
                    {
                        _list = HttpContext.Current.Session["ParentAssetCategoriesByID"] as List<Guid>;
                        return _list;
                    }
                }
                HttpContext.Current.Session["SelectedAssetCategoryID"] = id;

                IEnumerable<AssetCategoryModel> cats = await AssetCategoriesService.ReadAssetCategorys();
                AssetCategoryModel current= cats.Where(x=>x.AssetCategoryID== new Guid(id)).FirstOrDefault();
                if (current == null) return null;
                _list = FindAllParents(cats.ToList(), current).Select(x => x.AssetCategoryID).ToList();
                _list.Add(current.AssetCategoryID);
                HttpContext.Current.Session["ParentAssetCategoriesByID"] = _list;

                return _list;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public static async Task<List<Guid>> GetAllParentAssetCategoriesByIDFromApi(string id)
        {
            try
            {
                List<Guid> _list = null;
                if (HttpContext.Current.Session["SelectedAssetCategoryID"] != null)
                {
                    string sessionid = HttpContext.Current.Session["SelectedAssetCategoryID"].ToString();
                    if (sessionid.ToLower() == id.ToLower())
                    {
                        _list = HttpContext.Current.Session["ParentAssetCategoriesByID"] as List<Guid>;
                        return _list;
                    }
                }
                HttpContext.Current.Session["SelectedAssetCategoryID"] = id;

                var client = new HttpClient();
                client.BaseAddress = new Uri(Settings.ApiAddress());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());
                var request = client.GetAsync("api/GetAllParentAssetCategoriesByID/?id=" + id);
                var jsonres = request.ContinueWith(http => http.Result.Content.ReadAsAsync<List<Guid>>());
                _list = jsonres.Unwrap().Result;
                HttpContext.Current.Session["ParentAssetCategoriesByID"] = _list;

                return _list;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}