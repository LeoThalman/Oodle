using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;
using Oodle.Models;
using Oodle.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace Oodle.Controllers
{
    public class ClassController : Controller
    {
        private Model1 db = new Model1();
        private SlackController slack = new SlackController();


        // GET: Class
        [Authorize]
        public ActionResult Index(int classID)
        {
            var idid = User.Identity.GetUserId();

            User user = db.Users.Where(a => a.IdentityID == idid).FirstOrDefault();

            UserRoleClass urc = db.UserRoleClasses.Where(s => s.UsersID == user.UsersID && s.ClassID == classID).FirstOrDefault();

            if (urc == null)
            {
                return View(db.Classes.Where(i => i.ClassID == classID).FirstOrDefault());

            }

            else if (urc.RoleID == 0)
            {
                return RedirectToAction("Index", "Teachers", new { classId = classID });
            }
            else if (urc.RoleID == 1)
            {
                return RedirectToAction("Index", "Graders", new { classId = classID });
            }
            else if (urc.RoleID == 2)
            {
                return RedirectToAction("Index", "Students", new { classId = classID });
            }
            else
            {
                return RedirectToAction("Pending", new { classId = classID });
            }
        }

        [Authorize]
        public ActionResult Teacher(int classID)
        {
            var idid = User.Identity.GetUserId();

            User user = db.Users.Where(a => a.IdentityID == idid).FirstOrDefault();
            UserRoleClass urc = db.UserRoleClasses.Where(s => s.UsersID == user.UsersID && s.ClassID == classID).FirstOrDefault();

            if (urc == null || urc.RoleID != 0)
            {
                return RedirectToAction("Index", new { classId = classID });
            }

            var urcL = db.UserRoleClasses.Where(i => i.RoleID == 3 && i.ClassID == classID);
            var list = new List<int>();

            foreach (var i in urcL) 
            {
                list.Add(i.UsersID);
            }

            var request = db.Users.Where(i => list.Contains(i.UsersID)).ToList();

            var teacher = new TeacherVM(db.Classes.Where(i => i.ClassID == classID).FirstOrDefault(), request);
            
            return View(teacher);
        }

        [Authorize]
        public ActionResult Grader(int classID)
        {
            var idid = User.Identity.GetUserId();

            User user = db.Users.Where(a => a.IdentityID == idid).FirstOrDefault();
            UserRoleClass urc = db.UserRoleClasses.Where(s => s.UsersID == user.UsersID && s.ClassID == classID).FirstOrDefault();

            if (urc == null || urc.RoleID != 1)
            {
                return RedirectToAction("Index", new { classId = classID });
            }
            return View(db.Classes.Where(i => i.ClassID == classID).FirstOrDefault());
        }

        [Authorize]
        public ActionResult Student(int classID)
        {
            var idid = User.Identity.GetUserId();

            User user = db.Users.Where(a => a.IdentityID == idid).FirstOrDefault();
            UserRoleClass urc = db.UserRoleClasses.Where(s => s.UsersID == user.UsersID && s.ClassID == classID).FirstOrDefault();

            if (urc == null || urc.RoleID != 2)
            {
                return RedirectToAction("Index", new { classId = classID });
            }
            return View(db.Classes.Where(i => i.ClassID == classID).FirstOrDefault());
        }

        [Authorize]
        public ActionResult Pending(int classID)
        {
            var idid = User.Identity.GetUserId();

            User user = db.Users.Where(a => a.IdentityID == idid).FirstOrDefault();
            UserRoleClass urc = db.UserRoleClasses.Where(s => s.UsersID == user.UsersID && s.ClassID == classID).FirstOrDefault();

            if (urc == null || urc.RoleID != 3)
            {
                return RedirectToAction("Index", new { classId = classID });
            }
            return View(db.Classes.Where(i => i.ClassID == classID).FirstOrDefault());
        }

        public ActionResult List()
        {
            return View(db.Classes.ToList());
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        public ActionResult Accept(int classID, int userID)
        {
            db.UserRoleClasses.Where(i => i.UsersID == userID & i.ClassID == classID).ToList().ForEach(x => x.RoleID = 2);
            db.SaveChanges();

            //get user and class
            User user = db.Users.Where(i => i.UsersID == userID).FirstOrDefault();
            Class c = db.Classes.Where(i => i.ClassID == classID).FirstOrDefault();


            //check if there is a slack token, if not don't run slack methods
            if (slack.HasToken())
            {
                if (slack.IsOnSlack(user.Email) && !c.SlackName.Equals("%"))
                {
                    //Send request to slack for user to join the group
                    slack.JoinChannel(user.Email, c.Name);
                }
                else
                {
                    Debug.WriteLine("User not on Slack / No Slack Channel");
                }
            }
            return RedirectToAction("Teacher", new { classId = classID });
        }

        [Authorize]
        public ActionResult Reject(int classID, int userID)
        {
            db.UserRoleClasses.Remove(db.UserRoleClasses.Where(i => i.UsersID == userID & i.ClassID == classID).FirstOrDefault());
            db.SaveChanges();

            return RedirectToAction("Teacher", new { classId = classID });
        }


        [HttpPost]
        [Authorize]
        public ActionResult CreateClass()
        {
            var idid = User.Identity.GetUserId();
            User user = db.Users.Where(a => a.IdentityID == idid).FirstOrDefault();

            ViewBag.RequestMethod = "POST";

            string name = Request.Form["name"];
            string desc = Request.Form["description"];

            
            //get the value of slackChoice
            Boolean slackOption = Convert.ToBoolean(Request.Form["slackChoice"].ToString());
            Debug.WriteLine("Does user want a slack channel: " + slackOption);

            //slack channel name, if no channel/name is taken leave as "%"
            //otherwise gets renamed to the new slackchannel name
            string sName = "%";
            string tempName = Request.Form["slackName"];
            
            //check if there is a slack token, if not don't run slack methods
            if (slack.HasToken())
            {
                //if user does want a slack channel, check to see if their email is on the slack workspace
                //if so create a channel and put them in it, otherwise don't
                if (slackOption)
                {
                    if (slack.IsOnSlack(user.Email))
                    {
                        if (tempName.Equals(""))
                        {
                            //alter class name to match slack naming conventions
                            tempName = slack.ValidateSlackName(name);
                        }
                        //create a slack channel for this class
                        sName = slack.CreateChannel(tempName);
                        //join created slack channel
                        if (!sName.Equals("%"))
                        {
                            slack.JoinChannel(user.Email, sName);
                        }
                        else
                        {
                            Debug.WriteLine("Name already Taken/Invalid");
                        }

                    }
                }
            }


            var cl = new Class();

            cl.UsersID = user.UsersID;
            cl.Name = name;
            cl.Description = desc;
            cl.SlackName = sName;

            var urc = new UserRoleClass();


            db.Classes.Add(cl);
            db.SaveChanges();

            urc.UsersID = user.UsersID;
            urc.ClassID = cl.ClassID;
            urc.RoleID = 0;

            db.UserRoleClasses.Add(urc);
            db.SaveChanges();

            return RedirectToAction("List");

        }

        [Authorize]
        public ActionResult Join(int classID){
            var urc = new UserRoleClass();

            var idid = User.Identity.GetUserId();
            User user = db.Users.Where(a => a.IdentityID == idid).FirstOrDefault();

            urc.UsersID = user.UsersID;
            urc.ClassID = classID;
            urc.RoleID = 3;

            db.UserRoleClasses.Add(urc);
            db.SaveChanges();

            return RedirectToAction("Pending", new { classId = classID });
        }



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [Authorize]
        public ActionResult Edit(int classID)
        {
            ViewBag.id = classID;
            Class temp = db.Classes.Where(i => i.ClassID == classID).FirstOrDefault();
            return View(temp);
        }

        [Authorize]
        public ActionResult EditClass()
        {
            ViewBag.RequestMethod = "POST";

            string name = Request.Form["name"];
            string desc = Request.Form["description"];
            string notif = Request.Form["notification"];
            int classID = int.Parse(Request.Form["classID"]);

            Class hasSlack = db.Classes.Where(i => i.ClassID == classID).FirstOrDefault();
            if (!hasSlack.SlackName.Equals("%"))
            {
                    slack.SlackNotif(notif, hasSlack.SlackName);
            }

            AddNotification(notif, classID);

            db.Classes.Where(i => i.ClassID == classID).ToList().ForEach(x => x.Name = name);
            db.Classes.Where(i => i.ClassID == classID).ToList().ForEach(x => x.Description = desc);

            db.SaveChanges();

            return RedirectToAction("Teacher", new { classId = classID });
        }

        private void AddNotification(string notif, int classID)
        {
            ClassNotification cNotif = new ClassNotification();
            cNotif.Notification = notif;
            cNotif.TimePosted = DateTime.Now;
            cNotif.ClassID = classID;
            db.ClassNotifications.Add(cNotif);

            db.SaveChanges();


        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

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

            return RedirectToAction("List");
        }
    }
}