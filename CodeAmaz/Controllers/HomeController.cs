using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeAmaz.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Home()
		{
            return View();
		}

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}