using SAMS.Model;
using SAMS.WebUI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SAMS.WebUI.Helpers
{
    public static class SessionData
    {
        //public static async Task<IList<OrganizationModel>> ReadOrganizations(bool refresh = false)
        //{
        //    try
        //    {
        //        IList<OrganizationModel> result = HttpContext.Current.Session["Organizations"] as IList<OrganizationModel>;
        //        if (result == null || refresh)
        //        {
        //            IEnumerable<OrganizationModel> _list = await OrganizationsService.ReadOrganizations().ConfigureAwait(false);  
        //            HttpContext.Current.Session["Organizations"] = result;
        //        }

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception(ex.Message);
        //    }
        //}
    }
}