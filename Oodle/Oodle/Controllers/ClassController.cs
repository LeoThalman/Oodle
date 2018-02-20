using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Oodle.Models;

namespace Oodle.Controllers
{
    public class ClassController : Controller
    {
        private Model1 db = new Model1();


        // GET: Class
        public ActionResult Index(int classID)
        {
            var idid = User.Identity.GetUserId();

            User user = db.Users.Where(a => a.IdentityID == idid).FirstOrDefault();

            UserRoleClass urc = db.UserRoleClasses.Where(s => s.UsersID == user.UsersID && s.ClassID == classID).FirstOrDefault();


            if (urc.RoleID == 0) {
                RedirectToAction("Teacher", classID);
            }
            else if (urc.RoleID == 1)
            {
                RedirectToAction("Grader", classID);
            }
            else if (urc.RoleID == 2)
            {
                RedirectToAction("Student", classID);
            }
            else if (urc.RoleID == 3)
            {
                RedirectToAction("Pending", classID);
            }
            return View();
        }

        public ActionResult Teacher(int classID)
        {
            var idid = User.Identity.GetUserId();

            User user = db.Users.Where(a => a.IdentityID == idid).FirstOrDefault();
            UserRoleClass urc = db.UserRoleClasses.Where(s => s.UsersID == user.UsersID && s.ClassID == classID).FirstOrDefault();

            if (urc.RoleID != 0)
            {
                RedirectToAction("Index", classID);
            }
            return View();
        }

        public ActionResult Grader(int classID)
        {
            var idid = User.Identity.GetUserId();

            User user = db.Users.Where(a => a.IdentityID == idid).FirstOrDefault();
            UserRoleClass urc = db.UserRoleClasses.Where(s => s.UsersID == user.UsersID && s.ClassID == classID).FirstOrDefault();

            if (urc.RoleID != 1)
            {
                RedirectToAction("Index", classID);
            }
            return View();
        }

        public ActionResult Student(int classID)
        {
            var idid = User.Identity.GetUserId();

            User user = db.Users.Where(a => a.IdentityID == idid).FirstOrDefault();
            UserRoleClass urc = db.UserRoleClasses.Where(s => s.UsersID == user.UsersID && s.ClassID == classID).FirstOrDefault();

            if (urc.RoleID != 2)
            {
                RedirectToAction("Index", classID);
            }
            return View();
        }

        public ActionResult Pending(int classID)
        {
            var idid = User.Identity.GetUserId();

            User user = db.Users.Where(a => a.IdentityID == idid).FirstOrDefault();
            UserRoleClass urc = db.UserRoleClasses.Where(s => s.UsersID == user.UsersID && s.ClassID == classID).FirstOrDefault();

            if (urc.RoleID != 3)
            {
                RedirectToAction("Index", classID);
            }
            return View();
        }

        public ActionResult List()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        public void CreateClass()
        {
            var idid = User.Identity.GetUserId();
            User user = db.Users.Where(a => a.IdentityID == idid).FirstOrDefault();





            //db.Classes.Add(c);
        }
    }
}