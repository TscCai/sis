using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Ncepu.GraduationProject.Sis.Web.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        public HomeController()
        {
            ViewBag.Port = Port;
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}
