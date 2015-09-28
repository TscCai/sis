using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Ncepu.GraduationProject.Sis.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        public int Port { get { return 4433; } }

    }
}