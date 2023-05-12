using SAMS.Core.Helpers;
using SAMS.Domain.HelperModels;
using SAMS.Model;
using SAMS.WebUI.Helpers;
using SAMS.WebUI.Services;
using SAMS.WebUI.Services.Partials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SAMS.WebUI.Controllers
{
    public class BaseController : Controller
    {
        //[Inject]
        //public readonly IUnitOfWork unitOfWork;


        //public BaseController(IUnitOfWork unitOfWork) => this.unitOfWork = unitOfWork;
        protected int _CurrentPage = 1;
        protected Guid? _UserGuid = null;
        protected Guid? _OperatorUserGuid = null;
        protected Guid? _OperatorDeskGuid = null;
        protected string _CurrentOrganisationLogo = null;

        protected UserSession _currentUser = null;



        protected override void OnActionExecuting(ActionExecutingContext ctx)
        {
            base.OnActionExecuting(ctx);

            var controllerName = ctx.RouteData.Values["controller"];
            var actionName = ctx.RouteData.Values["action"];
            if (actionName.ToString().ToLower() == "login" && controllerName.ToString().ToLower() == "accounts")
            {
                Console.WriteLine("login");
            }
            else
            {
                if (CurrentUser == null)
                    ctx.Result = new RedirectResult(Url.Action("Login", "Accounts"));
                else
                {
                    if (controllerName.ToString() == "Departments" && (actionName.ToString() == "GetDepartmentEditForm" || actionName.ToString() == "GetDepartmentNewForm"))
                    {
                        if (ViewData["organizationslist"] == null)
                        {
                            PopulateOrganizationsList();
                        }
                        if (ViewData["locationslist"] == null)
                        {
                            PopulateLocationsList();
                        }

                        if (ViewData["personslist"] == null)
                        {
                            PopulatePersonsList();
                        }

                        //if (ViewData["personslist"] == null)
                        //{
                        //    PopulatePersonsList();
                        //}
                    }

                    if (controllerName.ToString() == "Assets")
                    {
                        if (actionName.ToString() == "GetAssetNewForm" || actionName.ToString() == "GetAssetEditForm")
                        {
                            int permis = CurrentUser.UserModel.Role.TblAssets;
                            ViewData["AssetAttributeMapPermission"] = permis;
                            ViewData["AssetCharacteristicPermission"] = permis;
                            ViewData["AssetPermission"] = permis;
                            ViewData["AssetOperationIndicatorsMapPermission"] = permis;
                            ViewData["AssetServiceIntervalPermission"] = permis;
                            ViewData["AssetDocumentPermission"] = permis;

                        }
                        else
                        {
                            ViewData["AssetAttributeMapPermission"] = 2;
                            ViewData["AssetCharacteristicPermission"] = 2;
                            ViewData["AssetPermission"] = 2;
                            ViewData["AssetOperationIndicatorsMapPermission"] = 2;
                            ViewData["AssetServiceIntervalPermission"] = 2;
                            ViewData["AssetDocumentPermission"] = 2;
                        }

                    }


                    if (controllerName.ToString() == "AssetTypes" && (actionName.ToString() == "GetAssetTypeNewForm" || actionName.ToString() == "GetAssetTypeEditForm"))
                    {
                        if (ViewData["schedullertypes_list"] == null)
                        {
                            PopulateScheduleTypes();
                        }

                        if (ViewData["assetcategorieslist"] == null)
                        {
                            PopulateAssetCategoriesist();
                        }
                    }

                    if (controllerName.ToString() == "Roles" && (actionName.ToString() == "GetRoleNewForm" || actionName.ToString() == "GetRoleEditForm"))
                    {
                        if (ViewData["organizationslist"] == null)
                        {
                            PopulateOrganizationsList();
                        }
                    }


                    if (controllerName.ToString() == "Personnels" && (actionName.ToString() == "GetPersonnelNewForm" || actionName.ToString() == "GetPersonnelEditForm"))
                    {
                        if (ViewData["organizationslist"] == null)
                        {
                            PopulateOrganizationsList();
                        }
                        if (ViewData["positionslist"] == null)
                        {
                            PopulatePositionsList();
                        }
                        if (ViewData["departmentslist"] == null)
                        {
                            PopulateDepartmentsList();
                        }
                    }

                    if (controllerName.ToString() == "Assets" && (actionName.ToString() == "GetAssetNewForm" || actionName.ToString() == "GetAssetEditForm"))
                    {
                        if (ViewData["departmentslist"] == null)
                        {
                            PopulateDepartmentsList();
                        }
                    }

                    if (controllerName.ToString() == "AssetAttributes")
                    {
                        if (ViewData["assetattributescategories"] == null)
                        {
                            PopulateAttributesCategoriesList();
                        }

                        if (ViewData["asset_attribute_type_enumslist"] == null)
                        {
                            PopulateAssetAttributeTypeEnumList();
                        }
                    }
                }
            }
            
        }

        private async Task PopulateAssetCategoriesist()
        {
            try
            {
                List<AssetCategoryModel> _list = await AssetCategoriesService.ReadAssetCategorys() as List<AssetCategoryModel>;
                var cats = _list.Select(c => new { AssetCategoryID = c.AssetCategoryID, AssetCategoryName = c.AssetCategoryName }).ToList();
                ViewData["assetcategorieslist"] = cats;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task PopulateScheduleTypes()
        {
            try
            {
                var cats = (await ScheduleTypesService.ReadScheduleTypes()).Select(c => new { ScheduleTypeID = c.ScheduleTypeID, ScheduleTypeName = c.ScheduleTypeName }).ToList();
                ViewData["schedullertypes_list"] = cats;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        protected override void OnException(ExceptionContext filterContext)
        {
            Exception ex = filterContext.Exception;
            filterContext.ExceptionHandled = true;
            var controllerName = filterContext.RouteData.Values["controller"];
            var actionName = filterContext.RouteData.Values["action"];
            var model = new HandleErrorInfo(filterContext.Exception, controllerName.ToString(), actionName.ToString());

            filterContext.Result = new ViewResult()
            {
                ViewName = "Error",
                ViewData = new ViewDataDictionary(model)
            };
            //throw new Exception(ex.Message);
        }

        private async Task PopulatePositionsList()
        {
            try
            {
                IEnumerable<PositionModel> cats = await PositionsService.ReadPositions();
                ViewData["positionslist"] = cats.Where(x => !x.Deactivate).Select(c => new { PositionID = c.PositionID, PositionName = c.PositionName });
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task PopulateDepartmentsList()
        {
            try
            {
                IEnumerable<DepartmentModel> cats =await  DepartmentsService.ReadDepartments();
                ViewData["departmentslist"] = cats.Where(x => !x.Deactivate).Select(c => new { DepartmentID = c.DepartmentID, DepartmentName = c.DepartmentName });
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task PopulateAttributesCategoriesList()
        {
            try
            {
                IEnumerable<AssetAttributeCategoryModel> cats = await AssetAttributeCategoriesService.ReadAssetAttributeCategorys();
                ViewData["assetattributescategories"] = cats.Where(x => !x.Deactivate).Select(c => new { AssetAttributeCategoryID = c.AssetAttributeCategoryID, AssetAttributeCategoryName = c.AssetAttributeCategoryName });
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private async Task PopulateOrganizationsList()
        {
            try
            {
                IEnumerable<OrganizationModel> cats = await OrganizationsService.ReadOrganizations();
                ViewData["organizationslist"] = cats.Where(x => !x.Deactivate).Select(c => new { OrganizationID = c.OrganizationID, OrganizationName = c.OrganizationName });
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task PopulateAssetAttributeTypeEnumList()
        {
            try
            {
                IEnumerable<IntKeyValue> cats = await PartialService.GetAssetCategoryTypesEnum();
                ViewData["asset_attribute_type_enumslist"] = cats;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private async Task PopulateLocationsList()
        {
            try
            {
                IEnumerable<LocationModel> cats = await LocationsService.ReadLocations();
                ViewData["locationslist"] = cats.Where(x => !x.Deactivate).Select(c => new { LocationID = c.LocationID, LocationName = c.LocationName });
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task PopulatePersonsList()
        {
            try
            {
                IEnumerable<PersonnelModel> cats = await PersonnelsService.ReadPersonnels();
                ViewData["personslist"] = cats.Where(x => !x.Deactivate).Select(c => new { PersonnelID = c.PersonnelID, PeronFullName = String.Format("{0} {1} {2}", c.PersonnelFirstName, c.PersonnelLastName, c.PersonnelMiddleName) });
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //
        public BaseController()
        {
            try
            {


                _currentUser = UserSession.GetLoginInfo;
                var request = System.Web.HttpContext.Current.Request;
                //try
                //{

                //}
                //catch (Exception ex)
                //{
                //    var controllerName = request.RequestContext.RouteData.Values["controller"];
                //    var actionName = request.RequestContext.RouteData.Values["action"];
                //    var model = new HandleErrorInfo(ex, controllerName.ToString(), actionName.ToString());

                //    request.RequestContext.RouteData = new ViewResult()
                //    {
                //        ViewName = "Error",
                //        ViewData = new ViewDataDictionary(model)
                //    };
                //}


            
                if (HttpContext != null && HttpContext.Session != null)
                {
                    if ( HttpContext.Session["UserSession"] != null)
                    {
                        var userSession = (UserSession)HttpContext.Session["UserSession"];
                        _UserGuid = userSession.UserGuid;
                    }


                }
                else
                {
                    
                }


                //if (_currentUser != null && ViewBag.OrganisationList==null)
                //{
                //    List<OrganizationModel> orgs= OrganizationsService.ReadOrganizations().Result as List<OrganizationModel>;
                //    ViewBag.OrganisationList = orgs;
                //    var currentOrganisationLogoBytes = orgs.Where(x => x.OrganizationID == _currentUser.CurrentOrganisationGuid).Select(x => x.OrgLogoBase64).FirstOrDefault();
                //    if (currentOrganisationLogoBytes != null && currentOrganisationLogoBytes.Count() != 0)
                //    {
                //        _CurrentOrganisationLogo = currentOrganisationLogoBytes.ToString(); //Convert.ToBase64String(currentOrganisationLogoBytes);
                //        ViewBag.CurrentOrganisationLogo = _CurrentOrganisationLogo;
                //    }
                //}
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public UserSession CurrentUser
        {
            get
            {
                return _currentUser;
            }
        }
    }
}