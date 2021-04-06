using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeAmaz.Controllers
{
    public class LessonController : Controller
    {
        // GET: Lesson
        public ActionResult LessonDetails()
        {
            return View();
        }
    }
}