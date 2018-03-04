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
    public class ClassController : Controller
    {
        private Model1 db = new Model1();

        //Slack token to allow creating and joining classes
        private string SlackToken = System.Web.Configuration.WebConfigurationManager.AppSettings["SlackToken"];


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

            else if (urc.RoleID == 0) {
                return RedirectToAction("Teacher", new { classId = classID });
            }
            else if (urc.RoleID == 1)
            {
                return RedirectToAction("Grader", new { classId = classID });
            }
            else if (urc.RoleID == 2)
            {
                return RedirectToAction("Student", new { classId = classID });
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



            if (IsOnSlack(user.Email) && !c.SlackName.Equals("%"))
            {
                //Send request to slack for user to join the group
                JoinChannel(user.Email, c.Name);
            }
            else
            {
                Debug.WriteLine("User not on Slack / No Slack Channel");
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
            //if user does want a slack channel, check to see if their email is on the slack workspace
            //if so create a channel and put them in it, otherwise don't
            if (slackOption)
            {
                if (IsOnSlack(user.Email))
                {
                    //create a slack channel for this class
                    sName = CreateChannel(name);
                    //join created slack channel
                    if (!sName.Equals("%")) {
                        JoinChannel(user.Email, sName);
                    }
                    else
                    {
                        Debug.WriteLine("Name already Taken/Invalid");
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

        /// <summary>
        /// Checks if user is on slack
        /// </summary>
        /// <param name="email">email of user to check</param>
        /// <returns>true if user is on slack, false if not</returns>
        private Boolean IsOnSlack (string email)
        {
            string surl = "https://slack.com/api/users.lookupByEmail?token=" + SlackToken + "&email=" + email + "&pretty=1";
            WebRequest request = WebRequest.Create(surl);
            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
            Stream dataStream = resp.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string slackData = reader.ReadToEnd();
            reader.Close();
            resp.Close();
            dataStream.Close();

            JObject userID = JObject.Parse(slackData);
            Boolean onSlack = Convert.ToBoolean(userID["ok"].ToString());
            Debug.WriteLine("User Email is On Slack: " + onSlack);
            return onSlack;
        }

        /// <summary>
        /// Sends an http reqeust to slack api to create a private channel in the slack work space
        /// </summary>
        /// <param name="className">Name of class/channel</param>
        [Authorize]
        private string CreateChannel(string className)
        {
            //url to query Slack to create a private channel. Slack Token authorizes method and identifies slack workspace.
            //className is the name of the private channel
            string surl = "https://slack.com/api/groups.create?token=" + SlackToken + "&name=" + className + "&pretty=1";
            //Send method request to Slack
            WebRequest request = WebRequest.Create(surl);
            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
            Stream dataStream = resp.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);

            //Convert Slack method response to readable string
            string slackData = reader.ReadToEnd();
            //Close open requests
            reader.Close();
            resp.Close();
            dataStream.Close();

            Debug.WriteLine(slackData);
            JObject channel = JObject.Parse(slackData);
            string name = "%";
            if (Convert.ToBoolean(channel["ok"].ToString()))
            {
                name = channel["group"]["name"].ToString();
            }

            return name;

        }

        /// <summary>
        /// Sends an http reqeust to slack api to join a private channel in the slack work space
        /// </summary>
        /// <param name="email">email of user trying to join a slack private channel</param>
        /// <param name="className">name of private to join</param>
        [Authorize]
        private void JoinChannel(string email, string className)
        {
            String cid = GetChannelId(className);
            String uid = GetSlackUserId(email);
            string qurl = "https://slack.com/api/groups.invite?token=" + SlackToken + "&channel=" + cid + "&user=" + uid + "&pretty=1";
            WebRequest request = WebRequest.Create(qurl);
            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
            Stream dataStream = resp.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string slackData = reader.ReadToEnd();
            reader.Close();
            resp.Close();
            dataStream.Close();
        }

        /// <summary>
        /// Sends an http reqeust to slack api to get a user ID based on email
        /// </summary>
        /// <param name="email">email of user</param>
        /// <returns>the slack user ID</returns>
        [Authorize]
        private string GetSlackUserId(string email)
        {
            string surl = "https://slack.com/api/users.lookupByEmail?token=" + SlackToken + "&email=" + email+ "&pretty=1";
            WebRequest request = WebRequest.Create(surl);
            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
            Stream dataStream = resp.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string slackData = reader.ReadToEnd();
            reader.Close();
            resp.Close();
            dataStream.Close();

            JObject userID = JObject.Parse(slackData);
            string id = "";
            id = userID["user"]["id"].ToString();
            return id;
        }

        /// <summary>
        /// Sends an http reqeust to slack api to get channel ID based on class name
        /// </summary>
        /// <param name="className">name of class/channel</param>
        /// <returns>ID of channel</returns>
        [Authorize]
        private string GetChannelId(string className)
        {

            string surl = "https://slack.com/api/groups.list?token=" + SlackToken + "&pretty=1";
            WebRequest request = WebRequest.Create(surl);
            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
            Stream dataStream = resp.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string slackData = reader.ReadToEnd();
            reader.Close();
            resp.Close();
            dataStream.Close();

            string id = "";
            JObject channels = JObject.Parse(slackData);
            IList<JToken> cData = channels["groups"].Children().ToList();
            foreach (JToken temp in cData)
            {
                ChannelID cTemp = temp.ToObject<ChannelID>();
                if (cTemp.name.Equals(className.ToLower()))
                {
                    id = cTemp.id;
                }
            }
            return id;
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
            return View();
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

            return RedirectToAction("Teacher", new { classId = classID });
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