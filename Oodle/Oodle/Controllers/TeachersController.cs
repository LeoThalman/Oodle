using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Oodle.Models;
using Oodle.Models.ViewModels;
using Oodle.Models.Repos;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;
using Oodle.Utility;
using System.Text.RegularExpressions;

namespace Oodle.Controllers
{
    [Authorize]
    public class TeachersController : Controller 
    {
        //Slack access
        private SlackManager slack = new SlackManager();


        //Regular database
        //private Model1 db = new Model1();
        //Repo for mocking database
        private IOodleRepository db;

        public TeachersController(IOodleRepository repo)
        {
            this.db = repo;
        }

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

        [HttpPost]
        [ActionName("Index")]
        public ActionResult IndexPost(int classID)
        {
            if (test(classID) != null)
            {

                return test(classID);
            }


            var teacher = getTVM(classID);
            var classes = db.Classes.ToList(); // list of all classes



            ViewBag.classList = classes;
            foreach (var item in classes) // Loop through List with foreach
            {
                System.Diagnostics.Debug.WriteLine(item);
            }


           System.Diagnostics.Debug.WriteLine(classes);


            ViewBag.RequestMethod = "POST";

            string desc = Request.Form["description"];
            string id = Request.Form["classID"];


            if (test(classID) != null)
            {
                return test(classID);
            }
            var note = new Notes();

            note.NotesID = db.Tasks.Count() + 1;
            note.Description = desc;
            note.ClassID = classID;


            db.AddNote(note);
            db.SaveChanges();


            teacher.Notes = db.Notes.ToList();

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
            if (slack.HasToken() && !c.SlackName.Equals("%") && slack.IsOnSlack(user.Email))
            {
                slack.JoinChannel(user.Email, c.SlackName);
            }
            return RedirectToAction("Index", new { classId = classID });
        }

        public ActionResult Reject(int classID, int userID)
        {
            if (test(classID) != null)
            {
                return test(classID);
            }


            db.RemoveURC(db.UserRoleClasses.Where(i => i.UsersID == userID & i.ClassID == classID).FirstOrDefault());
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
            int classID = int.Parse(Request.Form["classID"]);

            if (test(classID) != null)
            {
                return test(classID);
            }

            db.Classes.Where(i => i.ClassID == classID).ToList().ForEach(x => x.Name = name);
            db.Classes.Where(i => i.ClassID == classID).ToList().ForEach(x => x.Description = desc);

            db.SaveChanges();

            return RedirectToAction("Index", new { classId = classID });
        }

        /// <summary>
        /// Loads the add notification view
        /// </summary>
        /// <param name="classID">ID of Class</param>
        /// <returns>The Add Notification View</returns>
        public ActionResult AddNotifToClass(int classID)
        {
            if (test(classID) != null)
            {
                return test(classID);
            }

            ViewBag.id = classID;

            var teacher = getTVM(classID);

            return View("AddNotifToClass", "_TeacherLayout", teacher);
        }

        /// <summary>
        /// Adds the slack notification to the database and slack
        /// </summary>
        /// <param name="notif">notification text</param>
        /// <param name="classID">ID of Class</param>
        /// <returns>true if added to database successfully, false if not</returns>
        public Boolean AddNotifToDB(string notif, int classID)
        {
            Boolean rtn = false;
            Class hasSlack = db.Classes.Where(i => i.ClassID == classID).FirstOrDefault();


            if (!(string.IsNullOrEmpty(notif)))
            {
                if (!hasSlack.SlackName.Equals("%"))
                {
                    slack.SlackNotif(notif, hasSlack.SlackName);
                }
                AddNotification(notif, classID);
                rtn = true;
            }
            return rtn;
        }

        /// <summary>
        /// Gets the Notification data from the view, and saves it in the database
        /// </summary>
        /// <returns>Index page of class</returns>
        public ActionResult SaveNotif()
        {
            ViewBag.RequestMethod = "POST";
            string notif = Request.Form["notification"];
            int classID = int.Parse(Request.Form["classID"]);

            AddNotifToDB(notif, classID);

            return RedirectToAction("Index", new { classId = classID });
        }

        /// <summary>
        /// Loads the Remove Notification View to verify removing Notification
        /// </summary>
        /// <param name="classID">ID of Class</param>
        /// <param name="notifID">ID of Notification</param>
        /// <returns>RemoveNotification View if successful, otherwise redirects to class index page</returns>
        public ActionResult RemoveNotif(int classID, int notifID)
        {
            if (test(classID) != null)
            {
                return test(classID);
            }

            ClassNotification cNotif = db.ClassNotifications.Where(n => n.ClassID == classID 
                                                                && n.ClassNotificationID == notifID).FirstOrDefault();
            if(cNotif == null)
            {
                return RedirectToAction("Index", new { classId = classID });
            }

            var teacher = getTVM(classID);
            teacher.rNotif = cNotif;

            return View("RemoveNotif", "_TeacherLayout", teacher);
        }

        /// <summary>
        /// Remove the notification from the database
        /// </summary>
        /// <param name="classID">ID of Class</param>
        /// <param name="notifID">ID of Notification</param>
        /// <returns>The Index page for the class</returns>
        public ActionResult RemoveNotification(int classID, int notifID)
        {
            
            if (test(classID) != null)
            {
                return test(classID);
            }
            List<HiddenNotification> HiddenNotifs = db.HiddenNotifications.Where(h => h.ClassNotificationID == notifID).ToList();
            ClassNotification notif = db.ClassNotifications.Where(n => n.ClassID == classID
                                                    && n.ClassNotificationID == notifID).FirstOrDefault();
            //remove any hidden notifications for this notifications
            foreach (HiddenNotification h in HiddenNotifs)
            {
                db.RemoveHiddenNotif(h);
            }
            db.SaveChanges();
            db.RemoveNotif(notif);
            db.SaveChanges();

            return RedirectToAction("Index", new { classId = classID });
        }

        /// <summary>
        /// Add Notification to the Database and to slack channel if available
        /// </summary>
        /// <param name="notif">text of notification</param>
        /// <param name="classID">ID of Class to add notification to</param>
        private void AddNotification(string notif, int classID)
        {
            ClassNotification cNotif = new ClassNotification();
            cNotif.Notification = notif;
            cNotif.TimePosted = DateTime.Now;
            cNotif.ClassID = classID;
            db.AddNotif(cNotif);
            db.SaveChanges();           
        }

        public ActionResult Delete(int classID)
        {
            if (test(classID) != null)
            {
                return test(classID);
            }


            var list = db.UserRoleClasses.Where(i => i.ClassID == classID);
            Class hasSlack = db.Classes.Where(i => i.ClassID == classID).FirstOrDefault();
            foreach (var i in list)
            {
                db.RemoveURC(i);
            }

            //If class has a slack channel, delete it
            if (!hasSlack.SlackName.Equals("%"))
            {
                slack.DeleteChannel(hasSlack.SlackName);
            }

            db.RemoveClass(db.Classes.Where(i => i.ClassID == classID).FirstOrDefault());

            db.SaveChanges();


            return RedirectToAction("List", "Class");
        }


        public ActionResult MakeGrade()
        {
            return View("MakeGrade", "_TeacherLayout");
        }

        /*
         * Method returns the ViewRoster Html object and loads in 3 db models into the view.
         * 
         */
        public ActionResult ViewRoster(int classID)
        {
            var classes = db.Classes.ToList(); // list of all classes
            var user = db.Users.ToList(); // list of all users
            var roles = db.UserRoleClasses.ToList(); // List of all Roles


            ViewBag.name = User.Identity.GetUserName(); // testing viewbag output

            var urcL = db.UserRoleClasses.Where(i => i.RoleID == 0);
            var list = new List<int>();
            foreach (var i in urcL)
            {
                list.Add(i.UsersID);
            }
            var request = db.Users.Where(i => list.Contains(i.UsersID)).ToList();
            var request2 = db.Users.Where(i => list.Contains(i.UsersID)).ToList();

            var teacher = new TeacherVM(classes, user, roles);  // New TeacherVM using the list of classes and user

            teacher.cl = db.Classes.Where(i => i.ClassID == classID).FirstOrDefault();

            return View("ViewRoster", "_TeacherLayout", teacher);

        }


        public ActionResult removeStudent()
        {
            ViewBag.requestMethod = "POST";

            string id = Request.Form["classID"];
            string student = Request.Form["studentName"];
            string currentClass = Request.Form["currentClass"];

            int classID = int.Parse(id);
            int stuID = int.Parse(student);

            foreach (var x in db.UserRoleClasses.Where(i => i.UsersID == stuID && i.ClassID == classID))
            {

                //db.UserRoleClasses.Remove(x);
                db.RemoveURC(x);
            }

            db.SaveChanges();

            var teacher = getTVM(classID);
            teacher.Tasks = db.Tasks.ToList();

            return ViewRoster(int.Parse(currentClass));
            //return View("ViewRoster", "_TeacherLayout", teacher);
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

            teacher.notifs = db.ClassNotifications.Where(i => i.ClassID == classID).OrderBy(i => i.TimePosted).ToList();

            teacher.Tasks = db.Tasks.ToList();

            teacher.Notes = db.Notes.ToList();

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
            string weight = Request.Form["weight"];
            Boolean addNotif = Convert.ToBoolean(Request.Form["addNotif"].ToString());


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
            assi.Weight = int.Parse(weight);

            db.AddAssignment(assi);

            db.SaveChanges();


            var gradable = new Grade()
            {
                ClassID = int.Parse(id),
                DateApplied = DateTime.Parse(dueDate),
                GradeWeight = int.Parse(weight),
                AssignmentID = db.Assignments.Last().AssignmentID
            };

            db.AddGrade(gradable);
            db.SaveChanges();


            var teacher = getTVM(classID);
            if (addNotif)
            {
                AddNotifToDB("A new assignment has been added. Name: " + assi.Name + ". Opens: " + assi.StartDate + ". Due: " + assi.DueDate , assi.ClassID );
            }
            

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
            string weight = Request.Form["weight"];


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
            db.Assignments.Where(i => i.ClassID == classID && i.AssignmentID == assignmentID).ToList().ForEach(x => x.Weight = int.Parse(weight));

            db.SaveChanges();

            var teacher = getTVM(classID);

            return View("Assignment", "_TeacherLayout", teacher);
        }



        public ActionResult DeleteAssignmentAction()
        {
            ViewBag.RequestMethod = "POST";

            string assiID = Request.Form["assignmentID"];
            string id = Request.Form["classID"];

            int assignmentID = int.Parse(assiID);
            int classID = int.Parse(id);

            if (test(classID) != null)
            {
                return test(classID);
            }

            var assi = db.Assignments.Where(i => i.ClassID == classID && i.AssignmentID == assignmentID).FirstOrDefault();

            db.DeleteAssignment(assi);

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

            return View("SubmissionView", "_TeacherLayout", teacher);
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
      
        public ActionResult Grade(int classID, int documentID, int assignmentID)
        {
            var teacher = getTVM(classID);
            teacher.documents = db.Documents.Where(i => i.Id == documentID).ToList();
            int test = db.Documents.Where(i => i.Id == documentID).FirstOrDefault().UserID;
            teacher.users = db.Users.Where(i => i.UsersID == test).ToList();

            teacher.assignment = db.Assignments.Where(i => i.AssignmentID == assignmentID).ToList();

            return View("MakeGrade", "_TeacherLayout", teacher);
        }

        [HttpPost]
        public ActionResult SubmitGrade(int classID, int documentID, int assignmentID)
        {
            var grade = Int32.Parse(Request.Form["grade"]);

            var teacher = getTVM(classID);
            teacher.documents = db.Documents.Where(i => i.Id == documentID).ToList();
            int test = db.Documents.Where(i => i.Id == documentID).FirstOrDefault().UserID;
            teacher.users = db.Users.Where(i => i.UsersID == test).ToList();
            teacher.assignment = db.Assignments.Where(i => i.AssignmentID == assignmentID).ToList();

            db.Documents.Where(i => i.Id == documentID).ToList().ForEach(x => x.Grade = grade);

            db.SaveChanges();
            //marker
            return RedirectToAction("SubmissionView", "Teachers", new { ClassID = classID, assignmentID = assignmentID });
        }

        public ActionResult GradeList(int classID)
        {
            var teacher = getTVM(classID);
            var tmp = db.UserRoleClasses.Where(i => i.ClassID == classID && i.RoleID == 2).ToList(); //Gets all the students in the class.
            List<User> list = new List<User>();
            List<int> classGrades = new List<int>();
            teacher.perUser = new List<UserVMish>();
            List<Grade> gradables = db.Grades.Where(g => g.ClassID == classID).ToList();


            foreach (var i in tmp)
            {
                list.Add(db.Users.Where(l => l.UsersID == i.UsersID).FirstOrDefault());

                var UserVMish = new UserVMish();
                UserVMish.stat = new List<bool>();
                UserVMish.Late = new List<TimeSpan>();
                //var submissions = db.Documents.Where(l => l.ClassID == classID && l.UserID == i.UsersID).ToList();


                int total = 0;
                int divisor = 0;

                UserVMish.Grade = new List<Models.Grade>();

                foreach(var l in gradables)
                {
                    Document d;
                    StudentQuizze q;

                    if (l.Assignment != null)
                    {
                        if (db.Documents.Where(k => k.GradeID == l.GradeID && k.UserID == i.UsersID).FirstOrDefault() != null)
                        {
                            d = db.Documents.Where(k => k.GradeID == l.GradeID && k.UserID == i.UsersID).FirstOrDefault();
                            if (d.Grade != -1)
                            {
                                var contribution = l.Assignment.Weight * d.Grade;
                                total = contribution + total;
                                divisor = l.Assignment.Weight + divisor;
                            }

                            var submittedDate = d.Date;
                            var dueDate = l.Assignment.DueDate.Value;

                            Late(submittedDate, dueDate, UserVMish);
                        }
                        else if (DateTime.Compare(DateTime.Parse(l.DateApplied.ToString()), DateTime.Now) < 0)
                        {
                            total = total + 0;
                            divisor = divisor + l.Assignment.Weight;
                        }
                    }

                    if (l.Quizze != null)
                    {
                        q = db.StudentQuizzes.Where(k => k.QuizID == l.QuizID && k.UserID == i.UsersID).FirstOrDefault();

                        if (q != null)
                        {
                            int contribution = l.Quizze.GradeWeight * (q.TotalPoints / q.Quizze.TotalPoints ?? default(int));
                            total = (contribution*100) + total;
                            divisor = l.Quizze.GradeWeight + divisor;

                            var submittedDate = q.Quizze.EndTime;
                            var dueDate = l.Quizze.EndTime;

                            Late(submittedDate, dueDate, UserVMish);
                        }
                        else if (DateTime.Compare(DateTime.Parse(l.DateApplied.ToString()), DateTime.Now) < 0)
                        {
                            total = total + (0);
                            divisor = divisor + l.Quizze.GradeWeight;
                        }
                    }

                    UserVMish.Grade.Add(l);
                }
                teacher.perUser.Add(UserVMish);

                if (divisor != 0)
                {
                    classGrades.Add(total / divisor);
                }
                else
                {
                    classGrades.Add(0);
                }
            }//////



            teacher.users = list;
            teacher.StudentQuizze = db.StudentQuizzes.ToList();
            teacher.documents = db.Documents.Where(i => i.ClassID == classID).ToList();
            teacher.classGrade = classGrades;
            teacher.assignment = db.Assignments.Where(i => i.ClassID == classID).ToList();

            return View("Grades", "_TeacherLayout", teacher);
        }

        public void Late(DateTime submittedDate, DateTime dueDate, UserVMish userVMish)
        {
            if (0 <= DateTime.Compare(submittedDate, dueDate))
            {
                userVMish.stat.Add(false);
                userVMish.Late.Add(submittedDate.Subtract(dueDate));
            }
            else
            {
                userVMish.stat.Add(true);
                userVMish.Late.Add(dueDate.Subtract(submittedDate));
            }
        }

        /// <summary>
        /// Loads QuizList View, showing a list of all Quizzes for the class
        /// </summary>
        /// <param name="ClassID">ID of Class</param>
        /// <returns>The QuizList View for the specified Class</returns>
        public ActionResult QuizList(int ClassID)
        {
            if (test(ClassID) != null)
            { 
                return test(ClassID);
            }

            TeacherVM teacher = getTVM(ClassID);
            teacher.quizzes = db.Quizzes.Where(i => i.ClassID == ClassID).ToList();
            return View("QuizList", "_TeacherLayout", teacher);
        }
        
        /// <summary>
        /// Loads the Edit Quiz view, allowing teacher to Edit the Quiz
        /// </summary>
        /// <param name="QuizID">ID of Quiz to Edit</param>
        /// <param name="ClassID">ID of class</param>
        /// <returns>The EditQuiz View if successful, otherwise redirects to class index view</returns>
        [HttpGet]
        public ActionResult EditQuiz(int QuizID, int ClassID)
        {
            if (test(ClassID) != null)
            {
                return test(ClassID);
            }
            Quizze quiz = db.Quizzes.Where(q => q.QuizID == QuizID).FirstOrDefault();
            if (quiz == null)
            {
                return RedirectToAction("Index", "Class", new { classId = ClassID });
            }
            else if (quiz.ClassID == ClassID)
            {
                TeacherVM teacher = getTVM(ClassID);
                teacher.quiz = quiz;
                return View("EditQuiz", "_TeacherLayout", teacher);
            }
            else
            {
                return RedirectToAction("Index", "Class", new { classId = ClassID });
            }
        }

        /// <summary>
        /// Saves the edits for the quiz method
        /// </summary>
        /// <param name="Quiz">Edited Quiz</param>
        /// <returns>The ViewQuiz View if saved successfully, otherwise reloads EditQuiz View</returns>
        [HttpPost]
        public ActionResult EditQuiz(Quizze Quiz)
        {
            if (test(Quiz.ClassID) != null)
            {
                return test(Quiz.ClassID);
            }
            TeacherVM teacher = getTVM(Quiz.ClassID);
            teacher.quiz = Quiz;
            if (ModelState.IsValid && CheckQuizTime(Quiz))
            {

                db.SetModified(Quiz);
                db.SaveChanges();

                //STILL MORE TO BE DONE HERE!


                return RedirectToAction("ViewQuiz", "Teachers", new { QuizID = Quiz.QuizID, ClassID = Quiz.ClassID });
            }
            else
            {
                return View("EditQuiz", "_TeacherLayout", teacher);
            }
        }
        /// <summary>
        /// Method for Testing Moq
        /// </summary>
        /// <returns>List of Answers</returns>
        public List<MultChoiceAnswer> TestMoq()
        {
            return db.MultChoiceAnswers.ToList();
        }

        /// <summary>
        /// Method for Testing Moq
        /// </summary>
        /// <returns>List of tasks</returns>
        public List<Tasks> TestMoqTasks()
        {
            return db.Tasks.ToList();
        }

        /// <summary>
        /// Method for Testing Moq
        /// </summary>
        /// <returns>List of notes</returns>
        public List<Notes> TestMoqNotes()
        {
            return db.Notes.ToList();
        }


        /// <summary>
        /// Loads the View Quiz View, Allowing teacher to access
        /// more detailed information about the quiz
        /// </summary>
        /// <param name="QuizID">ID of Quiz</param>
        /// <param name="ClassID">ID of Class</param>
        /// <returns>The ViewQuiz View if successful, otherwise redirects to class index</returns>
        [HttpGet]
        public ActionResult ViewQuiz(int QuizID, int ClassID)
        {
            if (test(ClassID) != null)
            {
                return test(ClassID);
            }

            Quizze quiz = db.Quizzes.Where(q => q.QuizID == QuizID).FirstOrDefault();

            
            if (CheckQuizClassID(quiz,ClassID))
            {
                TeacherVM teacher = getTVM(ClassID);
                teacher.quiz = quiz;
                teacher.questionList = db.QuizQuestions.Where(q => q.QuizID == quiz.QuizID).ToList();
                teacher.answerList = db.MultChoiceAnswers.Where(a => a.QuizQuestion.QuizID == QuizID).ToList();
                if (db.StudentQuizzes.Where(q => q.QuizID == QuizID).FirstOrDefault() != null)
                {
                    teacher.Locked = true;
                }
                else
                {
                    teacher.Locked = false;
                }
                return View("ViewQuiz", "_TeacherLayout", teacher);
            }
                return RedirectToAction("Index", "Class", new { classId = ClassID });
        }


        /// <summary>
        /// makes sure Quiz ClassID matches the ClassID that was passed in
        /// </summary>
        /// <param name="quiz">Quiz to check</param>
        /// <param name="ClassID">ID of class to match to</param>
        /// <returns>true if Class IDs match, false if not</returns>
        public Boolean CheckQuizClassID(Quizze quiz, int ClassID)
        {
            Boolean rtn = false;
            if (quiz != null && quiz.ClassID == ClassID)
                rtn = true;
            return rtn;
        }

        /// <summary>
        /// Loads the createQuiz View
        /// </summary>
        /// <param name="ClassID">ID Of Class</param>
        /// <returns>The CreateQuiz View</returns>
        [HttpGet]
        public ActionResult CreateQuiz(int ClassID)
        {
            if (test(ClassID) != null)
            {
                return test(ClassID);
            }
            TeacherVM teacher = getTVM(ClassID);
            return View("CreateQuiz", "_TeacherLayout", teacher);
        }

        /// <summary>
        /// Gets the Quiz data from the View and 
        /// checks to make sure it is correct before adding to the database
        /// </summary>
        /// <param name="Quiz">The Quiz Data</param>
        /// <returns>The Quiz List view if successful, otherwise reloads the createquiz view</returns>
        [HttpPost]
        public ActionResult CreateQuiz([Bind(Include = "QuizName,ClassID,StartTime,EndTime,IsHidden,GradeWeight,CanReview")] Quizze Quiz)
        {
            if (test(Quiz.ClassID) != null)
            {
                return test(Quiz.ClassID);
            }

            TeacherVM teacher = getTVM(Quiz.ClassID);
            teacher.quiz = Quiz;
            if (AddQuizToDB(Quiz))
            {
                var gradable = new Grade()
                {
                    DateApplied = Quiz.EndTime,
                    GradeWeight = Quiz.GradeWeight,
                    ClassID = Quiz.ClassID,
                    QuizID = db.Quizzes.Last().QuizID
                };

                db.AddGrade(gradable);
                db.SaveChanges();


                return RedirectToAction("QuizList", "Teachers", new { classId = Quiz.ClassID });
            }
            else
            {
                return View("CreateQuiz", "_TeacherLayout", teacher);
            }
        }

        /// <summary>
        /// Adds the quiz to the Database
        /// </summary>
        /// <param name="Quiz">Quiz to add</param>
        /// <returns>true if added successfully, false if any information was incorrect or Quiz is null</returns>
        public Boolean AddQuizToDB(Quizze Quiz)
        {
            Boolean rtn = false;
            if (Quiz != null)
            {
                if (ModelState.IsValid)
                {
                    if (CheckQuizTime(Quiz)) {
                    
                    db.AddQuiz(Quiz);
                    db.SaveChanges();
                    rtn = true;
                    }
                }
            }
            return rtn;
        }

        /// <summary>
        /// Loads the Delete Quiz view, to verify deleting the quiz
        /// </summary>
        /// <param name="ClassID">ID of Class</param>
        /// <param name="QuizID">ID of Quiz to Delete</param>
        /// <returns>The Delete Quiz View</returns>
        [HttpGet]
        public ActionResult RemoveQuiz(int ClassID, int QuizID)
        {
            if (test(ClassID) != null)
            {
                return test(ClassID);
            }

            Quizze Quiz = db.Quizzes.Where(q => q.QuizID == QuizID).FirstOrDefault();
            if (Quiz == null)
            {
                return RedirectToAction("Index", new { classId = ClassID });
            }

            var teacher = getTVM(ClassID);
            teacher.quiz = Quiz;

            return View("RemoveQuiz", "_TeacherLayout", teacher);
        }

        /// <summary>
        /// Gets all Grade objects with a matching QuizID to QuizID and deletes them
        /// </summary>
        /// <param name="QuizID">ID of Quiz to match for Grade objects</param>
        /// <param name="ClassID">ID of Class</param>
        /// <returns>True if grades deleted successfully</returns>
        public Boolean RemoveQuizGrades(int QuizID, int ClassID)
        {
            Boolean rtn = true;

            List<Grade> TempGrades = db.Grades.Where(g => g.QuizID == QuizID).ToList();
            foreach(Grade g in TempGrades)
            {
                db.RemoveGrade(g);
            }
            db.SaveChanges();
            return rtn;
        }


        /// <summary>
        /// Gets all StudentQuizzes with a matching QuizID to QuizID and deletes them
        /// </summary>
        /// <param name="QuizID">ID of Quiz to match for StudentQuizzes</param>
        /// <param name="ClassID">ID of Class</param>
        /// <returns>True if quizzes deleted successfully</returns>
        public Boolean RemoveStudentQuizzes(int QuizID, int ClassID)
        {
            Boolean rtn = true;

            List<StudentQuizze> TempQuizzes = db.StudentQuizzes.Where(q => q.QuizID == QuizID).ToList();
            foreach (StudentQuizze q in TempQuizzes)
            {
                db.RemoveStudentQuiz(q);
            }
            db.SaveChanges();
            return rtn;
        }

        /// <summary>
        /// Deletes the Quiz and loads the QuizList view
        /// </summary>
        /// <param name="QuizID">ID of Quiz</param>
        /// <param name="ClassID">ID of Class</param>
        /// <returns>The Quiz List View</returns>
        public ActionResult DeleteQuiz(int QuizID, int ClassID)
        {
            Quizze Quiz = db.Quizzes.Where(q => q.QuizID == QuizID).FirstOrDefault();
            if (test(Quiz.ClassID) != null)
            {
                return test(Quiz.ClassID);
            }
            RemoveStudentQuizzes(QuizID, ClassID);
            RemoveQuizGrades(QuizID, ClassID);
            

            db.RemoveQuiz(Quiz);
            db.SaveChanges();

            return RedirectToAction("QuizList", "Teachers", new { ClassID = ClassID });
        }

        /// <summary>
        /// Loads the edit question view, allowing teacher to edit the fields
        /// for both Question and Answer
        /// </summary>
        /// <param name="question">Question to edit</param>
        /// <param name="answer">Answer to edit</param>
        /// <returns>loads the edit question view</returns>
        [HttpGet]
        public ActionResult EditQuestion(int QuestionID, int ClassID, int QuizID)
        {
            if (db.StudentQuizzes.Where(q => q.QuizID == QuizID).FirstOrDefault() != null)
            {
                return RedirectToAction("ViewQuiz", "Teachers", new { QuizID = QuizID, ClassID = ClassID });
            }
            if (test(ClassID) != null)
            {
                return test(ClassID);
            }
            TeacherVM teacher = getTVM(ClassID);
            teacher.quiz = db.Quizzes.Where(q => q.QuizID == QuizID).FirstOrDefault();
            teacher.question = db.QuizQuestions.Where(q => q.QuestionID == QuestionID).FirstOrDefault();
            teacher.answer = db.MultChoiceAnswers.Where(a => a.QuestionID == QuestionID).FirstOrDefault();
            return View("EditQuestion", "_TeacherLayout", teacher);
        }

        /// <summary>
        /// Saves the edits for the Question and Answer objects.
        /// </summary>
        /// <param name="question">Edited Question</param>
        /// <param name="answer">Edited Answer</param>
        /// <returns>view quiz view if edits saved successfully, otherwise reloads edit question view</returns>
        [HttpPost]
        public ActionResult EditQuestion(QuizQuestion question, MultChoiceAnswer answer)
        {
            if (db.StudentQuizzes.Where(q => q.QuizID == question.QuizID).FirstOrDefault() != null)
            {
                Quizze temp = db.Quizzes.Where(q => q.QuizID == question.QuizID).FirstOrDefault();
                return RedirectToAction("ViewQuiz", "Teachers", new { QuizID = question.QuizID, ClassID = temp.ClassID });
            }
            Quizze Quiz = db.Quizzes.Where(q => q.QuizID == question.QuizID).FirstOrDefault();
            TeacherVM teacher = getTVM(Quiz.ClassID);
            teacher.quiz = Quiz;
            teacher.question = question;
            teacher.answer = answer;
            if (test(teacher.quiz.ClassID) != null)
            {
                return test(teacher.quiz.ClassID);
            }
            if (ModelState.IsValid && CheckCorrectAnswerNotNull(answer))
            {
                answer = ShortenAnswer(answer);
                db.SetModified(answer);
                db.SetModified(question);
                db.SaveChanges();
                SetPointTotal(question.QuizID);

                return RedirectToAction("ViewQuiz", "Teachers", new { QuizID = teacher.quiz.QuizID, ClassID = teacher.quiz.ClassID });
            }
            else
            {
                return View("EditQuestion", "_TeacherLayout", teacher);
            }
        }

        /// <summary>
        /// Loads the Remove Question view to insure that teacher wants to
        /// delete the quiz question
        /// </summary>
        /// <param name="QuestionID">ID of question to delete</param>
        /// <param name="ClassID">ID of Class</param>
        /// <param name="QuizID">ID of Quiz</param>
        /// <returns></returns>
        public ActionResult RemoveQuestion(int QuestionID, int ClassID, int QuizID)
        {
            if (db.StudentQuizzes.Where(q => q.QuizID == QuizID).FirstOrDefault() != null)
            {
                return RedirectToAction("ViewQuiz", "Teachers", new { QuizID = QuizID, ClassID = ClassID });
            }
            if (test(ClassID) != null)
            {
                return test(ClassID);
            }
            TeacherVM teacher = getTVM(ClassID);
            teacher.quiz = db.Quizzes.Where(q => q.QuizID == QuizID).FirstOrDefault();
            teacher.question = db.QuizQuestions.Where(q => q.QuestionID == QuestionID).FirstOrDefault();

            return View("RemoveQuestion", "_TeacherLayout", teacher);
        }

        /// <summary>
        /// Deletes the question from the Quiz
        /// </summary>
        /// <param name="QuestionID">ID of question to delete</param>
        /// <param name="ClassID">ID of Class</param>
        /// <param name="QuizID">ID of Quiz</param>
        /// <returns></returns>
        public ActionResult DeleteQuizQuestion(int QuestionID, int ClassID, int QuizID)
        {
            if(db.StudentQuizzes.Where(q=> q.QuizID == QuizID).FirstOrDefault() != null)
            {
                return RedirectToAction("ViewQuiz", "Teachers", new { QuizID = QuizID, ClassID = ClassID });
            }
            QuizQuestion Question = db.QuizQuestions.Where(q => q.QuestionID == QuestionID).FirstOrDefault();
            if (test(ClassID) != null)
            {
                return test(ClassID);
            }


            db.RemoveQuestion(Question);
            db.SaveChanges();

            SetPointTotal(Question.QuizID);
            return RedirectToAction("ViewQuiz", "Teachers", new { QuizID = QuizID, ClassID = ClassID });
        }

        /// <summary>
        /// Checks to make sure Quiz start and end times are 
        /// correct. Start before end and end after start.
        /// </summary>
        /// <param name="Quiz">Quiz to check</param>
        /// <returns>true if the time is correct, false if not</returns>
        public Boolean CheckQuizTime (Quizze Quiz)
        {
            Boolean rtn = false;
            if (Quiz.StartTime.CompareTo(Quiz.EndTime) < 0 )
                rtn = true;
            else
                ModelState.AddModelError("Quiz.StartTime", "Start Time must be before End Time");
            return rtn;
        }

        /// <summary>
        /// Load the AddQuestion view
        /// </summary>
        /// <param name="QuizID">ID of Quiz to add Question to</param>
        /// <param name="ClassID">ID of Class for Quiz</param>
        /// <returns>The Add Question View</returns>
        public ActionResult AddQuestion(int QuizID, int ClassID)
        {
            if (test(ClassID) != null)
            {
                return test(ClassID);
            }
            TeacherVM teacher = getTVM(ClassID);
            teacher.quiz = db.Quizzes.Where(q => q.QuizID == QuizID).FirstOrDefault();
            teacher.answer = new MultChoiceAnswer();
            teacher.answer.QuestionID = -1;
            teacher.question = new QuizQuestion();
            teacher.question.QuizID = teacher.quiz.QuizID;
            return View("AddQuestion", "_TeacherLayout", teacher);
        }

        /// <summary>
        /// Adds the Question and Answer to the db
        /// </summary>
        /// <param name="question">Question to added</param>
        /// <param name="answer">Answer to add</param>
        /// <returns>returns true if added correctly, returns false if any information was incorrect</returns>
        public Boolean AddQuestionToDB(QuizQuestion question, MultChoiceAnswer answer)
        {
            Boolean rtn = false;
            if (question == null || answer == null)
                return rtn;
            if (ModelState.IsValid &&  CheckCorrectAnswerNotNull(answer))
            {
                answer = ShortenAnswer(answer);
                db.AddQuestion(question);
                db.SaveChanges();
                answer.QuestionID = question.QuestionID;
                db.AddAnswer(answer);
                db.SaveChanges();
                rtn = true;
            }
            return rtn;
        }

        /// <summary>
        /// Checks to make sure that the corresponding Answer for CorrectAnswer is filled out
        /// </summary>
        /// <param name="a">answer to check</param>
        /// <returns>true if correct answer is filled out, false if not</returns>
        public Boolean CheckCorrectAnswerNotNull(MultChoiceAnswer a)
        {
            Boolean rtn = false;
            if ((a.CorrectAnswer == 1 && !String.IsNullOrWhiteSpace(a.Answer1)) ||
                (a.CorrectAnswer == 2 && !String.IsNullOrWhiteSpace(a.Answer2)) ||
                (a.CorrectAnswer == 3 && !String.IsNullOrWhiteSpace(a.Answer3)) ||
                (a.CorrectAnswer == 4 && !String.IsNullOrWhiteSpace(a.Answer4)))
                rtn = true;
            else
                ModelState.AddModelError("answer.CorrectAnswer", "Corresponding answer field is empty, " +
                                        "Please fill it out or choose a new answer");
            return rtn;
        }
        
        /// <summary>
        /// Concatenates answers, i.e. if only Answer1 and Answer4 will filled out
        /// moves Answer4 to Answer2, and changes the correct answer appropriately.
        /// </summary>
        /// <param name="answer">The answer to concatenate</param>
        /// <returns>The newly shortened answer</returns>
        public MultChoiceAnswer ShortenAnswer(MultChoiceAnswer answer)
        {
            if (!String.IsNullOrWhiteSpace(answer.Answer4) && String.IsNullOrWhiteSpace(answer.Answer3))
            {
                answer.Answer3 = answer.Answer4;
                answer.Answer4 = null;
                if (answer.CorrectAnswer == 4)
                    answer.CorrectAnswer = 3;
            }

            if (!String.IsNullOrWhiteSpace(answer.Answer3) && String.IsNullOrWhiteSpace(answer.Answer2))
            {
                answer.Answer2 = answer.Answer3;
                answer.Answer3 = null;
                if (answer.CorrectAnswer == 3)
                    answer.CorrectAnswer = 2;
            }
            return answer;
        }

        /// <summary>
        /// Adds the question to the question table and the
        /// answer to the answer table, returns the Add Question view with
        /// the parameter data if anything was incorrect, otherwise returns the AddQuestion View
        /// with empty fields, for another question
        /// </summary>
        /// <param name="question">Quiz Question to be added to the QuizQuestions table</param>
        /// <param name="answer">Answer to be added to the MultchoiceAnswer table</param>
        /// <returns>The AddQuestion View if successful, or the AddQuestion view with old data if any information was incorrect</returns>
        public ActionResult AddAnother([Bind(Include = "QuizID,Points,QuestionText")] QuizQuestion question,
                                       [Bind(Include = "Answer1,Answer2,Answer3,Answer4,CorrectAnswer")] MultChoiceAnswer answer)
        {
            Quizze temp = db.Quizzes.Where(q => q.QuizID == question.QuizID).FirstOrDefault();
            int ClassID = temp.ClassID;
            if (test(ClassID) != null)
            {
                return test(ClassID);
            }
            TeacherVM teacher = getTVM(ClassID);
            teacher.quiz = temp;
            teacher.question = question;
            teacher.answer = answer;

            if(AddQuestionToDB(question, answer))
            {
                SetPointTotal(question.QuizID);
                return RedirectToAction("AddQuestion", "Teachers", new { QuizID = question.QuizID, ClassID = temp.ClassID });
            }

            
            return View("AddQuestion", "_TeacherLayout", teacher);
        }

        /// <summary>
        /// Adds the question to the question table and the
        /// answer to the answer table, returns the Add Question view
        /// if any fields were filled out incorrectly, otherwise redirects to the view
        /// quiz page
        /// </summary>
        /// <param name="question">Quiz Question to be added to the QuizQuestions table</param>
        /// <param name="answer">Answer to be added to the MultchoiceAnswer table</param>
        /// <returns>The View Quiz View if successful, or the AddQuestion view if any information was incorrect</returns>
        public ActionResult SaveQuestion([Bind(Include = "QuizID,Points,QuestionText")] QuizQuestion question,
                                       [Bind(Include = "Answer1,Answer2,Answer3,Answer4,CorrectAnswer")] MultChoiceAnswer answer)
        {
            Quizze temp = db.Quizzes.Where(q => q.QuizID == question.QuizID).FirstOrDefault();
            int ClassID = temp.ClassID;
            if (test(ClassID) != null)
            {
                return test(ClassID);
            }
            TeacherVM teacher = getTVM(ClassID);
            teacher.quiz = temp;
            teacher.question = question;
            teacher.answer = answer;
            if (AddQuestionToDB(question, answer))
            {
                SetPointTotal(question.QuizID);
                return RedirectToAction("ViewQuiz", "Teachers", new { QuizID = question.QuizID, ClassID = temp.ClassID });
            }

            
            return View("AddQuestion", "_TeacherLayout", teacher);
        }

        /// <summary>
        /// Update the quiz with the new point total, called whenever
        /// questions are added or edited.
        /// </summary>
        /// <param name="QuizID">ID of Quiz to update</param>
        /// <returns>true if Quiz ID is valid and was successfully updated</returns>
        public Boolean SetPointTotal(int QuizID)
        {
            Boolean rtn = true;
            if (QuizID <= 0)
                rtn = false;
            else
            {
                int TempTotal = 0;
                List<QuizQuestion> QuestionList = db.QuizQuestions.Where(q => q.QuizID == QuizID).ToList();
                foreach (QuizQuestion Quiz in QuestionList)
                {
                    TempTotal += Quiz.Points;
                }
                Quizze ChangedQuiz = db.Quizzes.Where(q => q.QuizID == QuizID).FirstOrDefault();
                ChangedQuiz.TotalPoints = TempTotal;
                db.SetModified(ChangedQuiz);
                db.SaveChanges();
            }
            return rtn;
        }

        public ActionResult CreateTask(int classID)
        {
            var teacher = getTVM(classID);

            teacher.Tasks = db.Tasks.ToList();

            return View("CreateTask", "_TeacherLayout", teacher);
        }


        public ActionResult CreateTasksEntry(HttpPostedFileBase postedFile)
        {
            ViewBag.RequestMethod = "POST";

            string desc = Request.Form["description"];
            string id = Request.Form["classID"];
            string startDate = Request.Form["startDate"];
            string dueDate = Request.Form["dueDate"];
            string vString = Request.Form["video"];
            
            vString = vString.Replace("watch?v=", "embed/");
            var tsk = new Tasks();

            if (postedFile != null)
            {
                byte[] bytes;
                using (BinaryReader br = new BinaryReader(postedFile.InputStream))
                {
                    bytes = br.ReadBytes(postedFile.ContentLength);


                    tsk.ContentType = postedFile.ContentType;
                    tsk.Data = bytes;
                    tsk.Name = Path.GetFileName(postedFile.FileName);


                }
            }




            desc = "v::" + vString + "::v" + desc;

            int classID = int.Parse(id);

            if (test(classID) != null)
            {
                return test(classID);
            }

            tsk.TasksID = db.Tasks.Count() + 1;
            tsk.Description = desc;
            tsk.ClassID = classID;
            tsk.StartDate = DateTime.Parse(startDate);
            tsk.DueDate = DateTime.Parse(dueDate);

            db.AddTask(tsk);
            db.SaveChanges();

            var teacher = getTVM(classID);

            teacher.Tasks = db.Tasks.ToList();

            return View("Tasks", "_TeacherLayout", teacher);
        }


        /*
         * Returns a view for editTasks
         */
        public ActionResult EditTasks(int classID, int tasksID)
        {

            var urcL = db.UserRoleClasses.Where(i => i.RoleID == 3 && i.ClassID == classID);
            var list = new List<int>();

            foreach (var i in urcL)
            {
                list.Add(i.UsersID);
            }
            var request = db.Users.Where(i => list.Contains(i.UsersID)).ToList();

            var teacher = new TeacherVM(db.Classes.Where(i => i.ClassID == classID).FirstOrDefault(), request);

            teacher.Tasks = db.Tasks.Where(i => i.ClassID == classID && i.TasksID == tasksID).ToList();

            return View("EditTasks", "_TeacherLayout", teacher);

        }

        public ActionResult EditTaskAction(HttpPostedFileBase postedFile)
        {
            ViewBag.RequestMethod = "POST";

            string desc = Request.Form["description"];
            string id = Request.Form["classID"];
            string startDate = Request.Form["startDate"];
            string dueDate = Request.Form["dueDate"];
            string TaskID = Request.Form["TaskID"];
            string delItem = Request.Form["Delete"];
            string vString = Request.Form["video"];
            vString = vString.Replace("watch?v=", "embed/");


            int TasksID = int.Parse(TaskID);
            int classID = int.Parse(id);

            if (postedFile != null)
            {
                byte[] bytes;
                using (BinaryReader br = new BinaryReader(postedFile.InputStream))
                {
                    bytes = br.ReadBytes(postedFile.ContentLength);

                    db.Tasks.Where(i => i.ClassID == classID && i.TasksID == TasksID).ToList().ForEach(x => x.ContentType = postedFile.ContentType);
                    db.Tasks.Where(i => i.ClassID == classID && i.TasksID == TasksID).ToList().ForEach(x => x.Data = bytes);
                    db.Tasks.Where(i => i.ClassID == classID && i.TasksID == TasksID).ToList().ForEach(x => x.Name = Path.GetFileName(postedFile.FileName));
                }
            }




            if (delItem == "True")
            {
                foreach (var x in db.Tasks.Where(i => i.TasksID == TasksID))
                {
                    db.RemoveTask(x);
                }

            }
            else
            {
                desc = "v::" + vString + "::v" + desc;
                db.Tasks.Where(i => i.ClassID == classID && i.TasksID == TasksID).ToList().ForEach(x => x.Description = desc);
                db.Tasks.Where(i => i.ClassID == classID && i.TasksID == TasksID).ToList().ForEach(x => x.StartDate = DateTime.Parse(startDate));
                db.Tasks.Where(i => i.ClassID == classID && i.TasksID == TasksID).ToList().ForEach(x => x.DueDate = DateTime.Parse(dueDate));
            }

            db.SaveChanges();

            var teacher = getTVM(classID);
            teacher.Tasks = db.Tasks.ToList();

            return View("Tasks", "_TeacherLayout", teacher);
        }


        /*
         * Method for grabbing the Teacher View Model for tasks
         */
        public ActionResult Tasks(int classID)
        {
            var teacher = getTVM(classID);

            return View("Tasks", "_TeacherLayout", teacher);

        }


        [HttpPost]
        public FileResult DownloadTask(int? fileId)
        {
            byte[] bytes;
            string fileName, contentType;
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT Name, Data, ContentType FROM Tasks WHERE TasksID=@Id";
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

    }
}




