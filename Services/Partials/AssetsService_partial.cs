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
using SAMS.Core.ViewModels;
using SAMS.Entities;
using SAMS.WebUI.Services.Partials;

namespace SAMS.WebUI.Services
{

	public static partial class AssetsService 
	{

        public static async Task<IEnumerable<AssetTypeConnetAssetViewModel>> GetAssetsTreeWithAssetTypes(Guid assetID)
        {
            List<AssetModel> assets = (await ReadAssets()).ToList();
            AssetModel asset = assets.FirstOrDefault(x => x.AssetID == assetID);
            if (asset == null) return null;
            List<AssetTypeViewModel> initlist = AssetTypeRelationsService.GetAllAsssetTypeChildrenByID(asset.AssetTypeID).ToList();
            List<AssetTypeConnetAssetViewModel> list = initlist.Cast<AssetTypeConnetAssetViewModel>().ToList();
            AssetTypeConnetAssetViewModel root = list.Where(x => x.AssetTypeID == asset.AssetTypeID && x.ParentAssetTypeID==null).FirstOrDefault();

            if (root == null)
            {
                return null;
            }

            root.Asset = asset;
            BuildAssetTypeConnetAssetViewModelChilds(assets, ref list, asset.AssetTypeID);
            return list;
        }


        //private void BuildAssetTypeConnetAssetViewModelTree(List<AssetModel> assets, IEnumerable<AssetTypeConnetAssetViewModel> asstypes,  AssetModel rootasset)
        //{
        //    AssetTypeConnetAssetViewModel rootasstype = asstypes.FirstOrDefault(x => x.AssetTypeID == rootasset.AssetTypeID && x.ParentAssetTypeID==null);

        //    if (rootasstype == null)
        //    {
        //        return ;
        //    }

        //    rootasstype.Asset = rootasset;
        //    List<AssetTypeConnetAssetViewModel> childrenTypes =
        //        asstypes.Where(x => x.ParentAssetTypeID == rootasstype.AssetTypeID).ToList();

        //}

        private static void BuildAssetTypeConnetAssetViewModelChilds( List<AssetModel> assets,ref List<AssetTypeConnetAssetViewModel> asstypes, Guid parentAssType)
        {
            List<AssetTypeConnetAssetViewModel> children = asstypes.Where(x => x.ParentAssetTypeID == parentAssType).ToList();
            foreach (AssetTypeConnetAssetViewModel item in children)
            {
                AssetModel asset = assets.FirstOrDefault(x => x.AssetTypeID == item.AssetTypeID);
                item.Asset = asset;
                List<Guid> emplacementsGuids = AssetEmplacementsService.ReadAssetEmplacements().Result.Where(x=>x.EmplacementID==asset.AssetID).Select(z=>z.AssetID).ToList();
                List<AssetModel> childrenassets = assets.Where(x => emplacementsGuids.Contains(x.AssetID)).ToList();
                //List<AssetTypeConnetAssetViewModel> childrenasstypes = asstypes.Where(x => x.ParentAssetTypeID == item.AssetTypeID).ToList();
                BuildAssetTypeConnetAssetViewModelChilds(childrenassets, ref asstypes, (Guid)item.AssetTypeID);
            }

        }


        public static async  Task<IEnumerable<AssetTypeConnetAssetViewModel>> GetAsssetTreeByID(Guid id)
        {
            try
            {

                IEnumerable<AssetTypeConnetAssetViewModel> _list = null;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Settings.ApiAddress());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

                    HttpResponseMessage response = await client.GetAsync("api/GetAsssetTreeByID?id=" + id);
                    if (response.IsSuccessStatusCode)
                    {
                        _list = await response.Content.ReadAsAsync<IEnumerable<AssetTypeConnetAssetViewModel>>();
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

        public static async Task<IEnumerable<AssetViewModel>> GetNotAssignedAssets(Guid assetID)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Settings.ApiAddress());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("SAMSUA", Settings.Token());

                    HttpResponseMessage response = await client.GetAsync("api/GetNotAssignedAssets?assetID=" + assetID);
                    if (response.IsSuccessStatusCode)
                    {
                        IEnumerable<AssetViewModel> res = await response.Content.ReadAsAsync<IEnumerable<AssetViewModel>>();
                        //List<AssetViewModel> reslist = res.ToList();
                        //_list = reslist.ConvertAll(instance => (AssetTreeViewModel)instance);
   
                        //reslist.Cast<AssetTreeViewModel>().ToList();
                        return res;

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

        public static async Task<ObjectResponse> RemoveAssetEmplacemnt(Guid assetID, Guid assetTypeRelationID)
        {
            ObjectResponse _resp = new ObjectResponse();
            try
            {
                AssetEmplacementModel  assetEmplacement = (await AssetEmplacementsService.ReadAssetEmplacements())
                    .Where(x => x.AssetID == assetID && x.AssetTypeRelationID == assetTypeRelationID).FirstOrDefault();

                if (assetEmplacement == null)
                {
                    _resp.Response = false;
                    _resp.error = "The emplacement not found";
                }

                return await AssetEmplacementsService.RemoveAssetEmplacement(assetEmplacement.AssetEmplacementID);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


        }

    }
}

