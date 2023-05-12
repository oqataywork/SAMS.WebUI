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
using SAMS.WebUI.Services.Partials;
//test 
namespace SAMS.WebUI.Services
{

	public static partial class PersonnelImagesService 
	{
        public static async Task<ObjectResponse> UpdateOrInsertPersonnelImage(PersonnelImageModel mod)
        {
            ObjectResponse _resp = new ObjectResponse();
            try
            {
                PersonnelImageModel exist = PersonnelImagesService.OnePersonnelImage(mod.PersonnelImageID);
                if (exist == null)
                    _resp = await PersonnelImagesService.CreatePersonnelImage(mod);
                else
                    _resp = await PersonnelImagesService.UpdatePersonnelImage(mod);

                ShouldRefreshService.SetShouldRefreshPersonnelImages(true);
                ShouldRefreshService.SetShouldRefreshPersonnels(true);

            }
            catch (Exception ex)
            {

                _resp.error = ex.Message;
            }
            return _resp;
        }
    }
}

