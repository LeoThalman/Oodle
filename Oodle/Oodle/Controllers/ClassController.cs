using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Oodle.Models.ViewModels;

namespace Oodle.Controllers
{
    public class ClassController : Controller
    {
        private UserRoleVM vm = new UserRoleVM();
        // GET: Class
        public ActionResult Index()
        {
            var idid = User.Identity.GetUserId();
            


            return View();
        }
    }
}