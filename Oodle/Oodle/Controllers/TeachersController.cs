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
    public class TeachersController : Controller
    {
        // GET: Teachers
        private Model1 db = new Model1();

        [Authorize]
        public ActionResult Index(int classID)
        {
            var idid = User.Identity.GetUserId();

            User user = db.Users.Where(a => a.IdentityID == idid).FirstOrDefault();
            UserRoleClass urc = db.UserRoleClasses.Where(s => s.UsersID == user.UsersID && s.ClassID == classID).FirstOrDefault();

            if (urc == null || urc.RoleID != 0)
            {
                return RedirectToAction("Index", "Class", new { classId = classID });
            }

            var urcL = db.UserRoleClasses.Where(i => i.RoleID == 3 && i.ClassID == classID);
            var list = new List<int>();

            foreach (var i in urcL)
            {
                list.Add(i.UsersID);
            }

            var request = db.Users.Where(i => list.Contains(i.UsersID)).ToList();

            var teacher = new TeacherVM(db.Classes.Where(i => i.ClassID == classID).FirstOrDefault(), request);

            return View("index", "_TeacherLayout", teacher);
        }


        [Authorize]
        public ActionResult Accept(int classID, int userID)
        {
            db.UserRoleClasses.Where(i => i.UsersID == userID & i.ClassID == classID).ToList().ForEach(x => x.RoleID = 2);
            db.SaveChanges();

            //get user and class
            User user = db.Users.Where(i => i.UsersID == userID).FirstOrDefault();
            Class c = db.Classes.Where(i => i.ClassID == classID).FirstOrDefault();
            //Send request to slack for user to join the group
            //JoinChannel(user.Email, c.Name);

            return RedirectToAction("Index", new { classId = classID });
        }


        [Authorize]
        public ActionResult Reject(int classID, int userID)
        {
            db.UserRoleClasses.Remove(db.UserRoleClasses.Where(i => i.UsersID == userID & i.ClassID == classID).FirstOrDefault());
            db.SaveChanges();

            return RedirectToAction("Index", new { classId = classID });
        }


        [Authorize]
        public ActionResult Edit(int classID)
        {
            ViewBag.id = classID;
            return View("Edit", "_TeacherLayout");
        }

        [Authorize]
        public ActionResult EditClass()
        {
            ViewBag.RequestMethod = "POST";

            string name = Request.Form["name"];
            string desc = Request.Form["description"];
            int classID = int.Parse(Request.Form["classID"]);


            db.Classes.Where(i => i.ClassID == classID).ToList().ForEach(x => x.Name = name);
            db.Classes.Where(i => i.ClassID == classID).ToList().ForEach(x => x.Description = desc);

            db.SaveChanges();

            return RedirectToAction("Index", new { classId = classID });
        }


        [Authorize]
        public ActionResult Delete(int classID)
        {
            var list = db.UserRoleClasses.Where(i => i.ClassID == classID);
            foreach (var i in list)
            {
                db.UserRoleClasses.Remove(i);
            }

            db.Classes.Remove(db.Classes.Where(i => i.ClassID == classID).FirstOrDefault());

            db.SaveChanges();

            return RedirectToAction("List", "Class");
        }










        public ActionResult MakeGrade()
        {
            return View("MakeGrade", "_TeacherLayout");
        }

        public ActionResult ViewRoster()
        {
            return View("ViewRoster", "_TeacherLayout");
        }

        public ActionResult CreateAssignment()
        {
            return View("CreateAssignment", "_TeacherLayout");
        }

        public ActionResult CreateQuiz()
        {
            return View("CreateQuiz", "_TeacherLayout");
        }

        public ActionResult CreateTask()
        {
            return View("CreateTask", "_TeacherLayout");
        }

        public ActionResult CreateSlack()
        {
            return View("CreateSlack", "_TeacherLayout");
        }
    }
}