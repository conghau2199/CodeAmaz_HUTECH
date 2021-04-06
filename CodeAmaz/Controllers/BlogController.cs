using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeAmaz.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        public ActionResult Home()
        {
            return View();
        }
    }
}