using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using SAMS.Model;
using SAMS.WebUI.Helpers;
using SAMS.WebUI.Services;
using SAMS.WebUI.Services.Partials;

namespace SAMS.WebUI.Controllers
{
    public class SAMSWebApiController : ApiController
    {
        // GET api/<controller>
        [Route("api/get")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        [Route("api/addAssetType")]
        public async Task<IHttpActionResult> AddAssetType(AssetTypeModel mod)
        {
            try
            {
                AssetTypeModel res=null;
                ObjectResponse resp = await AssetTypesService.CreateAssetType(mod);
                if (resp.Response != null)
                {
                    res = (AssetTypeModel)resp.Response;
                    return Ok(res);
                }
                return Content(HttpStatusCode.BadRequest, "Error when add asset type");
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, e.Message);
            }
        }


        [HttpPost]
        [Route("api/addAsset")]
        public async Task<IHttpActionResult> AddAsset(AssetModel mod, string token)
        {
            //try
            //{
            //    AssetModel res = null;
            //    ObjectResponse resp = await AssetsService.CreateAsset(mod);
            //    if (resp.Response != null)
            //    {
            //        res = (AssetModel)resp.Response;
            //        return Ok(res);
            //    }
            //    return Content(HttpStatusCode.BadRequest, "Error when add asset");
            //}
            //catch (Exception e)
            //{
            //    return Content(HttpStatusCode.BadRequest, e.Message);
            //}

            try
            {

                AssetModel _added;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Settings.ApiAddress());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("SAMSUA", token);

                var response = await client.PutAsJsonAsync("api/Asset/add", mod).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    _added = JsonConvert.DeserializeObject<AssetModel>(await response.Content.ReadAsStringAsync());
                    ShouldRefreshService.SetShouldRefreshAssets(true);
                    return Ok(_added);
                }
                else
                {
                    return Content(HttpStatusCode.BadRequest, "Error when add asset");
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }

        }


        [HttpPost]
        [Route("api/updateAsset")]
        public async Task<IHttpActionResult> UpdateAsset(AssetModel mod, string token)
        {
  
            try
            {

                AssetModel _added;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Settings.ApiAddress());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("SAMSUA", token);

                var response = await client.PostAsJsonAsync("api/Asset/update", mod).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    _added = JsonConvert.DeserializeObject<AssetModel>(await response.Content.ReadAsStringAsync());
                    ShouldRefreshService.SetShouldRefreshAssets(true);
                    return Ok(_added);
                }
                else
                {
                    return Content(HttpStatusCode.BadRequest, "Error when add asset");
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }

        }
    }
}