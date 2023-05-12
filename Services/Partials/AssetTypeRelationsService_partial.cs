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
using SAMS.Context;
using SAMS.Core.ViewModels;
using SAMS.WebUI.Services.Partials;
//test 
namespace SAMS.WebUI.Services
{

	public static partial class AssetTypeRelationsService 
	{
        //
        public static async Task<ObjectResponse> UpdateUnitInAssetTypeRelation(AssetTypeViewModel mod)
        {
            ObjectResponse _resp = new ObjectResponse();
            try
            {

                AssetTypeRelationModel _added;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Settings.ApiAddress());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

                var response = client.PostAsJsonAsync("api/UpdateUnitInAssetTypeRelation", mod).Result;

                if (response.IsSuccessStatusCode)
                {
                    _added = JsonConvert.DeserializeObject<AssetTypeRelationModel>(await response.Content.ReadAsStringAsync());
                    _resp.Response = _added;
                    //ReadAssetTypeRelations(true);
                    ShouldRefreshService.SetShouldRefreshAssetTypeRelations(true);
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


        public static async Task<ObjectResponse> UpdateOrInsertAssetTypeImage(AssetTypeImageModel mod)
        {
            ObjectResponse _resp = new ObjectResponse();
            try
            {
                AssetTypeImageModel exist = AssetTypeImagesService.OneAssetTypeImage(mod.AssetTypeImageID);
                if (exist == null)
                    _resp = await AssetTypeImagesService.CreateAssetTypeImage(mod);
                else
                    _resp = await AssetTypeImagesService.UpdateAssetTypeImage(mod);

                ShouldRefreshService.SetShouldRefreshAssetTypeImages(true);
                ShouldRefreshService.SetShouldRefreshAssetTypes(true);

            }
            catch (Exception ex)
            {

                _resp.error = ex.Message;
            }
            return _resp;
        }


        public static async Task<ObjectResponse> AddNewAssetTypeRelation(AssetTypeViewModel mod)
        {
            ObjectResponse _resp = new ObjectResponse();
            try
            {

                AssetTypeRelationModel _added;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Settings.ApiAddress());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

                var response = client.PostAsJsonAsync("api/AddNewAssetTypeRelation", mod).Result;

                if (response.IsSuccessStatusCode)
                {
                    _added = JsonConvert.DeserializeObject<AssetTypeRelationModel>(await response.Content.ReadAsStringAsync());
                    _resp.Response = _added;
                    ShouldRefreshService.SetShouldRefreshAssetTypeRelations(true);
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

        public static IEnumerable<AssetTypeViewModel> GetAllAsssetTypeChildrenByID(Guid id)
        {
            try
            {

                    IEnumerable<AssetTypeViewModel> _list = null;

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(Settings.ApiAddress());
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

                        HttpResponseMessage response = client.GetAsync("api/GetAllAsssetTypeChildrenByID?id=" + id).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            _list = response.Content.ReadAsAsync<IEnumerable<AssetTypeViewModel>>().Result;
                            return _list;

                        }
                        else
                            throw new Exception(response.ReasonPhrase);
                    }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


        }


        public static async Task<ObjectResponse> CloneAssetType(Guid assetTypeID, string newAssetTypeName)
        {
            ObjectResponse _resp = new ObjectResponse();
            try
            {
                AssetTypeViewModel mod = new AssetTypeViewModel();
                mod.AssetTypeID = assetTypeID;
                mod.AssetTypeName = newAssetTypeName;
                AssetTypeRelationModel _added;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Settings.ApiAddress());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

                var response = client.PostAsJsonAsync("api/CloneAssetType", mod).Result;

                if (response.IsSuccessStatusCode)
                {
                    _resp.Response = true;
                    ShouldRefreshService.SetShouldRefreshAssetTypes(true);
                    ShouldRefreshService.SetShouldRefreshAssetTypeRelations(true);
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
    }
}

