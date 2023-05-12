using SAMS.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAMS.WebUI.Controllers
{
    public class SharedController : BaseController
    {
        // GET: Shared
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult PartialMainMenu()
        {
            List<MainMenu> mod = new List<MainMenu>();
            ////mod.Add(new MainMenu { MenuName = "mnuMainPage", Text = "Главная страница", Action="Index", Controller="Home" });
            //mod.Add(new MainMenu { MenuName = "mnu1", Text = "Служба Движения", Action = "Index", Controller = "TrafficService" });
            //mod.Add(new MainMenu { MenuName = "mnu2", Text = "Служба ремонта", Action = "Index", Controller = "Home" });
            //mod.Add(new MainMenu { MenuName = "mnu3", Text = "Склад", Action = "Index", Controller = "Home" });
            //mod.Add(new MainMenu { MenuName = "mnu4", Text = "BPM", Action = "Index", Controller = "Home" });
            //mod.Add(new MainMenu { MenuName = "mnu5", Text = "НСИ", Action = "Index", Controller = "Home" });
            //mod.Add(new MainMenu { MenuName = "mnu6", Text = "Телеметрия", Action = "Index", Controller = "Home" });
            //mod.Add(new MainMenu { MenuName = "mnu7", Text = "Финансы", Action = "Index", Controller = "Home" });
            //mod.Add(new MainMenu { MenuName = "mnu8", Text = "Отчеты", Action = "Index", Controller = "Home" });
            //mod.Add(new MainMenu { MenuName = "mnu9", Text = "Администрирование", Action = "Index", Controller = "Home" });
            //string avatar = "<div class='avatar k-avatar k-avatar-solid-primary k-avatar-solid k-avatar-md k-rounded-full' data-role='avatar'><span class='k-avatar-image'><img id='avatar' src='https://demos.telerik.com/kendo-ui/content/web/Customers/PICCO.jpg'/><span/></div>";
            //mod.Add(new MainMenu { MenuName = "mnu10", Text = avatar, Action = "Index", Controller = "Home", Encoded=false });


            //mod.Add(new MainMenu { MenuName = "mnuMainPage", Text = "Главная страница", Action="Index", Controller="Home" });
            mod.Add(new MainMenu { MenuName = "Desktop", Text = "Задачи", Encoded = false, Class = "mmenu-bpm" });
            mod.Add(new MainMenu { MenuName = "Directories", Text = "Номенклатура", Encoded = false, Class = "mmenu-directory" });
            mod.Add(new MainMenu { MenuName = "MetroRun", Text = "Подвижной состав", Encoded = false, Class = "mmenu-metrorun" });
            mod.Add(new MainMenu { MenuName = "TrafficServ", Text = "Служба Движения", Encoded = false, Class= "mmenu-traffics" });
            mod.Add(new MainMenu { MenuName = "RepairServ", Text = "Служба ремонта", Encoded = false, Class = "mmenu-repair" });
            mod.Add(new MainMenu { MenuName = "mnu3", Text = "Склад", Encoded = false, Class = "mmenu-stock" });
            
            mod.Add(new MainMenu { MenuName = "Telemetry", Text = "Телеметрия", Encoded = false, Class = "mmenu-telemetry" });
            mod.Add(new MainMenu { MenuName = "mnu7", Text = "Финансы", Encoded = false, Class = "mmenu-fin" });
            mod.Add(new MainMenu { MenuName = "mnu8", Text = "Отчеты", Encoded = false, Class = "mmenu-reports" });
            mod.Add(new MainMenu { MenuName = "OrganisationMenu", Text = "Организационная структура", Encoded = false, Class = "mmenu-orgchart" });
            mod.Add(new MainMenu { MenuName = "AdminMenu", Text = "Администрирование", Encoded = false, Class = "mmenu-admin" });
            //string avatar = "<div class='avatar k-avatar k-avatar-solid-primary k-avatar-solid k-avatar-md k-rounded-full' data-role='avatar'><span class='k-avatar-image'><img id='avatar' src='https://demos.telerik.com/kendo-ui/content/web/Customers/PICCO.jpg'/><span/></div>";
            //mod.Add(new MainMenu { MenuName = "mnu10", Text = avatar, Encoded = false });

            return PartialView(mod);
        }
    }
}