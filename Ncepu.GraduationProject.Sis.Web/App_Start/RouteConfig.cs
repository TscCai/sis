using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ncepu.GraduationProject.Sis.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //   name: "Admin",
            //   url: "admin/{controller}/{action}/{id}",
            //   defaults: new { controller = "User", action = "Index", id = UrlParameter.Optional },
            //    namespaces: new string[] { "Ncepu.GraduationProject.Sis.Web.Controllers.Admin" }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                //constraints: new { controller = "^((?:(?!(User)).)*)$" },
                //namespaces: new string[] { "Ncepu.GraduationProject.Sis.Web.Controllers" }
            );

        }
    }
}