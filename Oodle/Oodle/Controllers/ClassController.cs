using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;
using Oodle.Models;
using Oodle.Utility;
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
        //Regular database
        private Model1 db = new Model1();
        //Repo for mocking database
        //private OodleRepository db = new OodleRepository();
        private SlackManager slack = new SlackManager();


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
            else
            {
                var id = urc.RoleID;

                if (id == 0)
                {
                    return RedirectToAction("Index", "Teachers", new { classId = classID });
                }
                else if (id == 1)
                {
                    return RedirectToAction("Index", "Graders", new { classId = classID });
                }
                else if (id == 2)
                {
                    return RedirectToAction("Index", "Students", new { classId = classID });
                }
                else
                {
                    return RedirectToAction("Pending", new { classId = classID });
                }
            }
        }
        
        public string roleFromID(int roleID)
        {
            if (roleID == 0)
            {
                return "teacher";
            }
            else if (roleID == 1)
            {
                return "grader";
            }
            else if (roleID == 2)
            {
                return "student";
            }
            else if (roleID == 3)
            {
                return "pending";
            }
            else
            {
                return "No Valid Role";
            }
        }


        public ActionResult List()
        {
            return View(db.Classes.ToList());
        }


        public List<Class> Filter(String s, List<Class> list)
        {
            string math = Request.Form[s];

            if (string.IsNullOrEmpty(math))
            {
                return list.Where(i => i.Subject != s).ToList();
            }

            return list;
        }

        public List<Class> FilterRole(int id, String s, List<Class> list)
        {
            string student = Request.Form[s];

            if (string.IsNullOrEmpty(student))
            {
                var test = db.UserRoleClasses.Where(j => j.UsersID == id && j.RoleID == 2).ToList();

                List<int> aClass = new List<int>();

                foreach (var i in test)
                {
                    aClass.Add(i.ClassID);
                }

                return list.Where(i => !aClass.Contains(i.ClassID)).ToList();
            }
            return list;
        }


        public List<Class> Sort(List<Class> list)
        {
            string sort = Request.Form["sort"];

            if (sort == null)
            {
            }
            else if (sort == "name")
            {
                return list.OrderBy(i => i.Name).ToList();
            }
            else if (sort == "mostRecent")
            {
                list.Reverse();
                return list;
            }
            return list;
        }



        private List<String> topics = new List<string>()
        {
            "math",
            "english",
            "cs",
            "art"
        };

        private List<String> roles = new List<string>()
        {
            "student",
            "grader",
            "teacher", 
            "pend"
        };





        public ActionResult FilterList()
        {
            var classes = db.Classes.ToList();

            foreach (string s in topics)
            {
                classes = Filter(s, classes);
            }

            var idid = User.Identity.GetUserId();

            int id = db.Users.Where(a => a.IdentityID == idid).FirstOrDefault().UsersID;

            {
                var test = db.UserRoleClasses.Where(j => j.UsersID == id).ToList();

                List<int> aClass = new List<int>();

                foreach (var i in test)
                {
                    aClass.Add(i.ClassID);
                }

                classes = classes.Where(i => aClass.Contains(i.ClassID)).ToList();
            }

            foreach (string s in roles)
            {
                classes = FilterRole(id, s, classes);
            }

            classes = Sort(classes);

            return View("List", classes);
        }

        public ActionResult FilterListAll()
        {
            var classes = db.Classes.ToList();

            foreach (string s in topics)
            {
                classes = Filter(s, classes);
            }

            classes = Sort(classes);

            return View("List", classes);
        }

        [Authorize]
        public ActionResult Join(int classID)
        {
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

        [Authorize]
        public ActionResult Create()
        {
            return View();
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
            string sub = Request.Form["subject"];


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
            cl.Subject = sub;

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
    }
}