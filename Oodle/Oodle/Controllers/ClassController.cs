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





        [HttpPost]
        [Authorize]
        public ActionResult CreateClass()
        {
            var idid = User.Identity.GetUserId();
            User user = db.Users.Where(a => a.IdentityID == idid).FirstOrDefault();

            ViewBag.RequestMethod = "POST";

            string name = Request.Form["name"];
            string desc = Request.Form["description"];

            var cl = new Class();

            cl.UsersID = user.UsersID;
            cl.Name = name;
            cl.Description = desc;



            db.Classes.Add(cl);
            db.SaveChanges();

            var urc = new UserRoleClass();


            urc.UsersID = user.UsersID;
            urc.ClassID = cl.ClassID;
            urc.RoleID = 0;

            db.UserRoleClasses.Add(urc);
            db.SaveChanges();

            //create a slack channel for this class
            //CreateChannel(name);
            //join created slack channel
            //JoinChannel(user.Email, name);
            return RedirectToAction("List");

        }

        /// <summary>
        /// Sends an http reqeust to slack api to create a private channel in the slack work space
        /// </summary>
        /// <param name="className">Name of class/channel</param>
        [Authorize]
        private void CreateChannel(string className)
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
    }
}