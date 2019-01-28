using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Oodle.Controllers
{
    public class ErrorController : Controller
    {

        public ActionResult ErrorPage()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}