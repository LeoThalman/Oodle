using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Oodle.Models;
using Oodle.Models.ViewModels;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace Oodle.Controllers
{
    public class StudentsController : Controller
    {
        // GET: Students


        private Model1 db = new Model1();

        [Authorize]
        public ActionResult Index(int classID)
        {
            var idid = User.Identity.GetUserId();

            User user = db.Users.Where(a => a.IdentityID == idid).FirstOrDefault();
            UserRoleClass urc = db.UserRoleClasses.Where(s => s.UsersID == user.UsersID && s.ClassID == classID).FirstOrDefault();

            if (urc == null || urc.RoleID != 2)
            {
                return RedirectToAction("Index", new { classId = classID });
            }
            return View("Grade", "_StudentLayout", db.Classes.Where(i => i.ClassID == classID).FirstOrDefault());
        }
        public ActionResult Grade()
        {
            return View("Grade", "_StudentLayout");
        }

        public ActionResult Assignment()
        {
            return View("Assignment", "_StudentLayout");
        }

        public ActionResult Quiz()
        {
            return View("Quiz", "_StudentLayout");
        }

        public ActionResult Task()
        {
            return View("Task", "_StudentLayout");
        }

        public ActionResult Slack()
        {
            return View("Slack", "_StudentLayout");
        }
    }
}