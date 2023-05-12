using SAMS.Model;
using SAMS.WebUI.Models;
using SAMS.WebUI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using SAMS.Domain.Enums;
using SAMS.WebUI.Helpers;
using Kendo.Mvc.Extensions;
using System.Threading.Tasks;

namespace SAMS.WebUI.Controllers
{

[Attributes.IsAuthenticated(Order = 0), Attributes.AuthorizeUserType(Order = 1)]
	public partial class CharacteristicCategoriesController : BaseController
	{

        public ActionResult Index()
        {
            return View();
        }

        [AccessLevelFilter(Action = AccessLevelEnum.Read, TableName = "tbl_CharacteristicCategories")]
        public async Task<ActionResult> CharacteristicCategories_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                IEnumerable<CharacteristicCategoryModel> cats =await CharacteristicCategoriesService.ReadCharacteristicCategorys();

                DataSourceResult result = cats.ToDataSourceResult(request);
                var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(null);
            }
        }

   
        public CharacteristicCategoryModel GetCharacteristicCategoryByID(Guid id )
        {
            CharacteristicCategoryModel mod = null;
            try
            {
               mod = CharacteristicCategoriesService.OneCharacteristicCategory(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                
            }
            return mod;
        }


        public ActionResult GetCharacteristicCategoryReadForm( string id)
        {

            try
            {
                CharacteristicCategoryModel mod = GetCharacteristicCategoryByID(new Guid(id));
                if(mod!=null)
                {
                    ViewBag.CharacteristicCategoryID = mod.CharacteristicCategoryID;
                    return PartialView("CharacteristicCategoryReadFormPartial", mod);
                }
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult GetCharacteristicCategoryEditForm(string id)
        {

            try
            {
                CharacteristicCategoryModel mod = GetCharacteristicCategoryByID(new Guid(id));
                if (mod != null)
                {
                    ViewBag.CharacteristicCategoryID = mod.CharacteristicCategoryID;
                    return PartialView("CharacteristicCategoryEditFormPartial", mod);
                }
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }


        public ActionResult GetCharacteristicCategoryNewForm()
        {

            try
            {
                CharacteristicCategoryModel mod = new CharacteristicCategoryModel();
                if (mod != null)
                {
                    ViewBag.CharacteristicCategoryID = mod.CharacteristicCategoryID;
                    return PartialView("CharacteristicCategoryEditFormPartial", mod);
                }
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }



        [AcceptVerbs(HttpVerbs.Post)]
        [AccessLevelFilter(Action = AccessLevelEnum.Insert, TableName = "tbl_CharacteristicCategories")]
        public async Task<ActionResult> CharacteristicCategories_Create([DataSourceRequest] DataSourceRequest request, CharacteristicCategoryModel mod)
        {
            string _err = "";
            try
            {

                if (mod == null) _err = "CharacteristicCategory is null";
                if (mod.CharacteristicCategoryID != null && mod.CharacteristicCategoryID.ToString() != "00000000-0000-0000-0000-000000000000") _err = "CharacteristicCategory is not new";

                if (!ModelState.IsValid)
                {
                    var errMsg = ModelState.Values.Where(x => x.Errors.Count >= 1);
                    string errorstr = "";
                    foreach (var item in errMsg)
                    {
                        if (item.Errors[0].ErrorMessage != "The CharacteristicCategoryID field is required.")
                            errorstr += " " + item.Errors[0].ErrorMessage;
                    }


                    if (errorstr==null || errorstr.Trim() == "")
                        ModelState.Clear();

                }


                if (_err != "")
                    ModelState.AddModelError("exception", _err);

                if (ModelState.IsValid)
                {

                    ObjectResponse resp = await CharacteristicCategoriesService.CreateCharacteristicCategory(mod);
                    if (resp.Response != null)
                    {
                        mod = (CharacteristicCategoryModel)resp.Response;
                    }
                    else
                        ModelState.AddModelError("exception", resp.error);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("exception", ex.Message);
            }
            return Json(new[] { mod }.ToDataSourceResult(request, ModelState));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        [AccessLevelFilter(Action = AccessLevelEnum.Edit, TableName = "tbl_CharacteristicCategories")]
        public async Task<ActionResult> CharacteristicCategories_Update([DataSourceRequest] DataSourceRequest request, CharacteristicCategoryModel mod)
        {
            try
            {
                string _err = "";
                CharacteristicCategoryModel _updated;

                if (mod != null)
                {
                    if(!ModelState.IsValid)
                    {

                        if(ModelState["CreateDate"]!=null && ModelState["CreateDate"].Errors!=null)
                        {
                            var createdateerrors = ModelState["CreateDate"].Errors.ToList();
                            foreach (var error in createdateerrors)
                            {
                                ModelState["CreateDate"].Errors.Remove(error);             
                            }
                        }

                        if (ModelState["ChangeDate"] != null && ModelState["ChangeDate"].Errors != null)
                        {
                            var createdateerrors = ModelState["CreateDate"].Errors.ToList();
                            foreach (var error in createdateerrors)
                            {
                                ModelState["ChangeDate"].Errors.Remove(error);
                            }
                        }
                        var errMsg = ModelState.Where(x => x.Value.Errors.Count >= 1);
                        string _errors = "";
                        foreach (var item in errMsg)
                        {
                            if(!String.IsNullOrEmpty(item.Value.Errors[0].ErrorMessage))
                            {
                                _errors += ("\n" + item.Value.Errors[0].ErrorMessage);
                            }
                                
                        }
                            
                        if(!String.IsNullOrEmpty(_errors))
                            ModelState.AddModelError("exception", _errors);
                        else
                            ModelState.Clear();
                    }


                    if (ModelState.IsValid)
                    {
                        ObjectResponse resp = await CharacteristicCategoriesService.UpdateCharacteristicCategory(mod);
                        if (resp.Response != null)
                        {
                            _updated = (CharacteristicCategoryModel)resp.Response;
                            mod = _updated;
                        }
                        else
                            ModelState.AddModelError("exception", resp.error);
                    }
                    else
                    {
                        var errMsg = ModelState.Where(x => x.Value.Errors.Count >= 1);
                        string _errors = "";
                        foreach (var item in errMsg)
                            _errors += ("\n" + item.Value.Errors[0].ErrorMessage);

                        ModelState.AddModelError("exception", _errors);
                    }
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("exception", ex.Message);
            }
            return Json(new[] { mod }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [AccessLevelFilter(Action = AccessLevelEnum.Edit, TableName = "tbl_CharacteristicCategories")]
        public async Task<ActionResult> CharacteristicCategories_Create_or_Update([DataSourceRequest] DataSourceRequest request, CharacteristicCategoryModel mod)
        {
            try
            {
                string _err = "";
                CharacteristicCategoryModel _updated;

                if (mod != null)
                {
                    if (!ModelState.IsValid)
                    {

                        if (ModelState["CreateDate"] != null && ModelState["CreateDate"].Errors != null)
                        {
                            var createdateerrors = ModelState["CreateDate"].Errors.ToList();
                            foreach (var error in createdateerrors)
                            {
                                ModelState["CreateDate"].Errors.Remove(error);
                            }
                        }

                        if (ModelState["ChangeDate"] != null && ModelState["ChangeDate"].Errors != null)
                        {
                            var createdateerrors = ModelState["CreateDate"].Errors.ToList();
                            foreach (var error in createdateerrors)
                            {
                                ModelState["ChangeDate"].Errors.Remove(error);
                            }
                        }

                    }


                    if (ModelState.IsValid)
                    {
                        ObjectResponse resp;
                        if (mod.CharacteristicCategoryID==Guid.Empty)
                            resp = await CharacteristicCategoriesService.CreateCharacteristicCategory(mod);      
                        else
                            resp = await CharacteristicCategoriesService.UpdateCharacteristicCategory(mod);

                        if (resp.Response != null)
                            mod = (CharacteristicCategoryModel)resp.Response;
                        else
                            ModelState.AddModelError("exception", resp.error);
                    }
                    else
                    {
                        var errMsg = ModelState.Where(x => x.Value.Errors.Count >= 1);
                        string _errors = "";
                        foreach (var item in errMsg)
                            _errors += ("\n" + item.Value.Errors[0].ErrorMessage);

                        ModelState.AddModelError("exception", _errors);
                    }
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("exception", ex.Message);
            }
            return Json(new[] { mod }.ToDataSourceResult(request, ModelState));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        [AccessLevelFilter(Action = AccessLevelEnum.Delete, TableName = "tbl_CharacteristicCategories")]
        public async Task<ActionResult> CharacteristicCategories_Destroy([DataSourceRequest] DataSourceRequest request, CharacteristicCategoryModel mod)
        {
            try
            {
                string _err = "";
                
                ObjectResponse resp = await CharacteristicCategoriesService.RemoveCharacteristicCategory(mod.CharacteristicCategoryID);
                if (!(bool)resp.Response)
                {
                    ModelState.AddModelError("exception", resp.error);
                }     
                else
                {
                    ModelState.Clear();
                }
                        
                //}
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("exception", ex.Message);
            }
            return Json(new[] { mod }.ToDataSourceResult(request, ModelState));
        }
	}
}

