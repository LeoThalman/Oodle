﻿using System;
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
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Oodle.Controllers
{
    [Authorize]
    public class TeachersController : Controller
    {
        //Slack tokens
        private string SlackToken = System.Web.Configuration.WebConfigurationManager.AppSettings["SlackToken"];
        private string SlackBot = System.Web.Configuration.WebConfigurationManager.AppSettings["SlackBot"];

        // GET: Teachers
        private Model1 db = new Model1();

        public ActionResult test(int classID)
        {
            var idid = User.Identity.GetUserId();

            User user = db.Users.Where(a => a.IdentityID == idid).FirstOrDefault();
            UserRoleClass urc = db.UserRoleClasses.Where(s => s.UsersID == user.UsersID && s.ClassID == classID).FirstOrDefault();

            if (urc == null || urc.RoleID != 0)
            {
                return RedirectToAction("Index", "Class", new { classId = classID });
            }
            return null;
        }


        public ActionResult Index(int classID)
        {
            if (test(classID) != null)
            {
                return test(classID);
            }
            var teacher = getTVM(classID);

            return View("index", "_TeacherLayout", teacher);
        }

        public ActionResult Accept(int classID, int userID)
        {
            if (test(classID) != null)
            {
                return test(classID);
            }


            db.UserRoleClasses.Where(i => i.UsersID == userID & i.ClassID == classID).ToList().ForEach(x => x.RoleID = 2);
            db.SaveChanges();

            //get user and class
            User user = db.Users.Where(i => i.UsersID == userID).FirstOrDefault();
            Class c = db.Classes.Where(i => i.ClassID == classID).FirstOrDefault();
            //Send request to slack for user to join the group
            //JoinChannel(user.Email, c.Name);

            return RedirectToAction("Index", new { classId = classID });
        }

        public ActionResult Reject(int classID, int userID)
        {
            if (test(classID) != null)
            {
                return test(classID);
            }


            db.UserRoleClasses.Remove(db.UserRoleClasses.Where(i => i.UsersID == userID & i.ClassID == classID).FirstOrDefault());
            db.SaveChanges();

            return RedirectToAction("Index", new { classId = classID });
        }

        public ActionResult Edit(int classID)
        {
            if (test(classID) != null)
            {
                return test(classID);
            }


            ViewBag.id = classID;

            var teacher = getTVM(classID);

            return View("Edit", "_TeacherLayout", teacher);
        }

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
                if (!notif.Equals(hasSlack.Notification))
                {
                    SlackNotif(notif, hasSlack.SlackName);
                }
            }

            if (test(classID) != null)
            {
                return test(classID);
            }



            db.Classes.Where(i => i.ClassID == classID).ToList().ForEach(x => x.Name = name);
            db.Classes.Where(i => i.ClassID == classID).ToList().ForEach(x => x.Description = desc);
            db.Classes.Where(i => i.ClassID == classID).ToList().ForEach(x => x.Notification = notif);

            db.SaveChanges();

            return RedirectToAction("Index", new { classId = classID });
        }

        private string GetSlackBotID()
        {
            string surl = "https://slack.com/api/users.list?token=" + SlackToken + "&pretty=1";
            WebRequest request = WebRequest.Create(surl);
            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
            Stream dataStream = resp.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string slackData = reader.ReadToEnd();
            reader.Close();
            resp.Close();
            dataStream.Close();

            string id = "";
            JObject users = JObject.Parse(slackData);
            IList<JToken> cData = users["members"].Children().ToList();
            foreach (JToken temp in cData)
            {
                SlackBot bTemp = temp.ToObject<SlackBot>();
                if (Convert.ToBoolean(bTemp.is_bot))
                {
                    id = bTemp.id;
                    Debug.WriteLine(id);
                }
            }
            return id;
        }

        private void SlackNotif(string notif, string sName)
        {
            string cID = GetChannelId(sName);
            notif = Regex.Replace(notif, @"[\s]+", "%20");

            string surl = "https://slack.com/api/chat.postMessage?token=" + SlackBot + "&channel=" + cID + "&as_user=true" + "&text=" + notif + "&pretty=1";
            //Send method request to Slack
            WebRequest request = WebRequest.Create(surl);
            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
            Stream dataStream = resp.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);

            //Convert Slack method response to readable string
            string slackData = reader.ReadToEnd();
            Debug.WriteLine(slackData);
            //Close open requests
            reader.Close();
            resp.Close();
            dataStream.Close();
            JObject message = JObject.Parse(slackData);
            PinMessage(message["ts"].ToString(), message["channel"].ToString());
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

        private void PinMessage(string time, string channel)
        {
            string surl = "https://slack.com/api/pins.add?token=" + SlackBot + "&channel=" + channel + "&timestamp=" + time + "&pretty=1";
            //Send method request to Slack
            WebRequest request = WebRequest.Create(surl);
            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
            Stream dataStream = resp.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string slackData = reader.ReadToEnd();
            Debug.WriteLine(slackData);
            //Close open requests
            reader.Close();
            resp.Close();
            dataStream.Close();
        }

        public ActionResult Delete(int classID)
        {
            if (test(classID) != null)
            {
                return test(classID);
            }


            var list = db.UserRoleClasses.Where(i => i.ClassID == classID);
            foreach (var i in list)
            {
                db.UserRoleClasses.Remove(i);
            }

            classID = 1;

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

        public ActionResult Assignment(int classID)
        {
            var teacher = getTVM(classID);

            return View("Assignment", "_TeacherLayout", teacher);
        }


        public TeacherVM getTVM(int classID)
        {
            var urcL = db.UserRoleClasses.Where(i => i.RoleID == 3 && i.ClassID == classID);
            var list = new List<int>();

            foreach (var i in urcL)
            {
                list.Add(i.UsersID);
            }
            var request = db.Users.Where(i => list.Contains(i.UsersID)).ToList();

            var teacher = new TeacherVM(db.Classes.Where(i => i.ClassID == classID).FirstOrDefault(), request);

            teacher.assignment = db.Assignments.Where(i => i.ClassID == classID).OrderBy(i => i.StartDate).ToList();

            return teacher;
        }



        public ActionResult CreateAssignmentAction()
        {
            ViewBag.RequestMethod = "POST";

            string name = Request.Form["name"];
            string desc = Request.Form["description"];
            string id = Request.Form["classID"];
            string startDate = Request.Form["startDate"];
            string dueDate = Request.Form["dueDate"];

            int classID = int.Parse(id);

            if (test(classID) != null)
            {
                return test(classID);
            }


            var assi = new Assignment();

            assi.Name = name;
            assi.Description = desc;
            assi.ClassID = classID;
            assi.StartDate = DateTime.Parse(startDate);
            assi.DueDate = DateTime.Parse(dueDate);


            db.Assignments.Add(assi);
            db.SaveChanges();

            var teacher = getTVM(classID);

            return View("Assignment", "_TeacherLayout", teacher);
        }


        public ActionResult EditAssignment(int classID, int assignmentID)
        {
            if (test(classID) != null)
            {
                return test(classID);
            }


            var urcL = db.UserRoleClasses.Where(i => i.RoleID == 3 && i.ClassID == classID);
            var list = new List<int>();

            foreach (var i in urcL)
            {
                list.Add(i.UsersID);
            }
            var request = db.Users.Where(i => list.Contains(i.UsersID)).ToList();

            var teacher = new TeacherVM(db.Classes.Where(i => i.ClassID == classID).FirstOrDefault(), request);

            teacher.assignment = db.Assignments.Where(i => i.ClassID == classID && i.AssignmentID == assignmentID).ToList();

            return View("EditAssignment", "_TeacherLayout", teacher);
        }

        public ActionResult EditAssignmentAction()
        {
            ViewBag.RequestMethod = "POST";

            string name = Request.Form["name"];
            string desc = Request.Form["description"];
            string id = Request.Form["classID"];
            string startDate = Request.Form["startDate"];
            string dueDate = Request.Form["dueDate"];
            string assiID = Request.Form["assignmentID"];

            int assignmentID = int.Parse(assiID);
            int classID = int.Parse(id);

            if (test(classID) != null)
            {
                return test(classID);
            }


            db.Assignments.Where(i => i.ClassID == classID && i.AssignmentID == assignmentID).ToList().ForEach(x => x.Name = name);
            db.Assignments.Where(i => i.ClassID == classID && i.AssignmentID == assignmentID).ToList().ForEach(x => x.Description = desc);
            db.Assignments.Where(i => i.ClassID == classID && i.AssignmentID == assignmentID).ToList().ForEach(x => x.StartDate = DateTime.Parse(startDate));
            db.Assignments.Where(i => i.ClassID == classID && i.AssignmentID == assignmentID).ToList().ForEach(x => x.DueDate = DateTime.Parse(dueDate));

            db.SaveChanges();

            var teacher = getTVM(classID);

            return View("Assignment", "_TeacherLayout", teacher);
        }


        public ActionResult CreateAssignment(int classID)
        {
            var teacher = getTVM(classID);


            return View("CreateAssignment", teacher);
        }



        ////////////////////////////////////////////////////////////////


        public ActionResult SubmissionView(int classID, int assignmentID)
        {
            if (test(classID) != null)
            {
                return test(classID);
            }


            var urcL = db.UserRoleClasses.Where(i => i.RoleID == 3 && i.ClassID == classID);
            var list = new List<int>();

            foreach (var i in urcL)
            {
                list.Add(i.UsersID);
            }
            var request = db.Users.Where(i => list.Contains(i.UsersID)).ToList();

            var teacher = new TeacherVM(db.Classes.Where(i => i.ClassID == classID).FirstOrDefault(), request);

            teacher.assignment = db.Assignments.Where(i => i.ClassID == classID && i.AssignmentID == assignmentID).ToList();

            teacher.documents = GetFiles(classID, assignmentID);

            foreach (var s in teacher.documents)
            {
                Debug.WriteLine(s.UserID);
            }

            var students = db.Users.ToList();

            students = students.Where(e => teacher.documents.Select(i => i.UserID).Contains(e.UsersID)).ToList();



            foreach (var s in students)
            {
                Debug.WriteLine(s.UsersID);
            }

            teacher.users = students;

            return View(teacher);
        }
        

        [HttpPost]
        public FileResult DownloadFile(int? fileId)
        {
            byte[] bytes;
            string fileName, contentType;
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT Name, Data, ContentType FROM Documents WHERE Id=@Id";
                    cmd.Parameters.AddWithValue("@Id", fileId);
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["Data"];
                        contentType = sdr["ContentType"].ToString();
                        fileName = sdr["Name"].ToString();
                    }
                    con.Close();
                }
            }

            return File(bytes, contentType, fileName);
        }


        private static List<Document> GetFiles(int classID, int assignmentID)
        {
            List<Document> files = new List<Document>();
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "SELECT Id, Name, UserID FROM Documents WHERE ClassID=@classID AND AssignmentID=@assignmentID";
                    cmd.Parameters.AddWithValue("@classID", classID);
                    cmd.Parameters.AddWithValue("@assignmentID", assignmentID);

                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            files.Add(new Document
                            {
                                Id = Convert.ToInt32(sdr["Id"]),
                                Name = sdr["Name"].ToString(),
                                UserID = Convert.ToInt32(sdr["UserID"])
                            });
                        }
                    }
                    con.Close();
                }
            }
            return files;
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