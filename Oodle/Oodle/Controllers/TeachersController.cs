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
            //slack.JoinChannel(user.Email, c.Name);

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

        public ActionResult SaveNotif()
        {
            ViewBag.RequestMethod = "POST";
            string notif = Request.Form["notification"];
            int classID = int.Parse(Request.Form["classID"]);

            if (test(classID) != null)
            {
                return test(classID);
            } 

            Class hasSlack = db.Classes.Where(i => i.ClassID == classID).FirstOrDefault();


            if (!(string.IsNullOrEmpty(notif)))
            {
                if (!hasSlack.SlackName.Equals("%"))
                {
                    slack.SlackNotif(notif, hasSlack.SlackName);
                }
                AddNotification(notif, classID);
            }


            return RedirectToAction("Index", new { classId = classID });
        }

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

        public ActionResult RemoveNotification(int classID, int notifID)
        {
            
            if (test(classID) != null)
            {
                return test(classID);
            }

            ClassNotification notif = db.ClassNotifications.Where(n => n.ClassID == classID
                                                    && n.ClassNotificationID == notifID).FirstOrDefault();
            db.RemoveNotif(notif);
            db.SaveChanges();

            return RedirectToAction("Index", new { classId = classID });
        }

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

            //classID = 1;
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
        public ActionResult ViewRoster()
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
            return View("ViewRoster", teacher);
            
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
      
        public ActionResult Grade(int classID, int documentID, int assignmentID)
        {
            var teacher = getTVM(classID);
            teacher.documents = db.Documents.Where(i => i.Id == documentID).ToList();
            teacher.users = db.Users.Where(i => i.UsersID == db.Documents.Where(j => j.Id == documentID).FirstOrDefault().UserID).ToList();
            teacher.assignment = db.Assignments.Where(i => i.AssignmentID == assignmentID).ToList();

            return View("MakeGrade", "_TeacherLayout", teacher);
        }

        [HttpPost]
        public ActionResult SubmitGrade(int classID, int documentID, int assignmentID)
        {
            var grade = Int32.Parse(Request.Form["grade"]);

            var teacher = getTVM(classID);
            teacher.documents = db.Documents.Where(i => i.Id == documentID).ToList();
            teacher.users = db.Users.Where(i => i.UsersID == db.Documents.Where(j => j.Id == documentID).FirstOrDefault().UserID).ToList();
            teacher.assignment = db.Assignments.Where(i => i.AssignmentID == assignmentID).ToList();

            db.Documents.Where(i => i.Id == documentID).ToList().ForEach(x => x.Grade = grade);

            db.SaveChanges();

            return View("MakeGrade", "_TeacherLayout", teacher);
        }

        public ActionResult GradeList(int classID)
        {
            var teacher = getTVM(classID);
            var tmp = db.UserRoleClasses.Where(i => i.ClassID == classID && i.RoleID == 2).ToList(); //Gets all the students in the class.

            List<User> list = new List<User>();
            List<int> classGrades = new List<int>();

            foreach(var i in tmp)
            {
                list.Add(db.Users.Where(l => l.UsersID == i.UsersID).FirstOrDefault());

                var submissions = db.Documents.Where(l => l.ClassID == classID && l.UserID == i.UsersID).ToList();

                int total = 0;
                int divisor = 0;
                foreach(var l in submissions)
                {
                    if (l.Grade != -1)
                    {
                        var contribution = l.Assignment.Weight * l.Grade;
                        total = contribution + total;
                        divisor = l.Assignment.Weight + divisor;
                    }
                }
                if (divisor != 0)
                {
                    classGrades.Add(total / divisor);
                }
                else
                {
                    classGrades.Add(0);
                }
            }

            teacher.documents = db.Documents.Where(i => i.ClassID == classID).ToList();
            teacher.classGrade = classGrades;
            teacher.users = list;
            teacher.assignment = db.Assignments.Where(i => i.ClassID == classID).ToList();

            return View("Grades", "_TeacherLayout", teacher);
        }

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

        [HttpPost]
        public ActionResult EditQuiz(Quizze Quiz)
        {
            if (test(Quiz.ClassID) != null)
            {
                return test(Quiz.ClassID);
            }
            if (ModelState.IsValid)
            {

                db.SetModified(Quiz);
                db.SaveChanges();
                return RedirectToAction("Index", "Class", new { classId = Quiz.ClassID });
            }
            else
            {
                return RedirectToAction("EduitQuiz", "Teachers", new {QuizID = Quiz.QuizID, ClassID = Quiz.ClassID });
            }
        }

        public List<Quizze> TestMoq()
        {
            return db.Quizzes.ToList();
        }

        [HttpGet]
        public ActionResult ViewQuiz(int QuizID, int ClassID)
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
                teacher.questionList = db.QuizQuestions.Where(q => q.QuizID == quiz.QuizID).ToList();
                teacher.answerList = db.MultChoiceAnswers.Where(a => a.QuizQuestion.QuizID == QuizID).ToList();
                return View("ViewQuiz", "_TeacherLayout", teacher);
            }
            else
            {
                return RedirectToAction("Index", "Class", new { classId = ClassID });
            }
        }

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

        [HttpPost]
        public ActionResult CreateQuiz([Bind(Include = "QuizName,ClassID,StartTime,EndTime,IsHidden")] Quizze Quiz)
        {
            if (test(Quiz.ClassID) != null)
            {
                return test(Quiz.ClassID);
            }
            if (ModelState.IsValid)
            {
                db.AddQuiz(Quiz);
                db.SaveChanges();
                return RedirectToAction("Index", "Class", new { classId = Quiz.ClassID });
            }
            else
            {
                return RedirectToAction("CreateQuiz", "Teachers", new { ClassID = Quiz.ClassID });
            }
        }

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


        public Boolean AddQuestionToDB(QuizQuestion question, MultChoiceAnswer answer)
        {
            Boolean rtn = false;

            if (ModelState.IsValid)
            {
                db.AddQuestion(question);
                db.SaveChanges();
                answer.QuestionID = question.QuestionID;
                db.AddAnswer(answer);
                db.SaveChanges();
                rtn = true;
            }

            return rtn;
        }

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
                return RedirectToAction("AddQuestion", "Teachers", new { QuizID = question.QuizID, ClassID = temp.ClassID });
            }
            
            return View("AddQuestion", "_TeacherLayout", teacher);
        }

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
                return RedirectToAction("ViewQuiz", "Teachers", new { QuizID = question.QuizID, ClassID = temp.ClassID });
            }


            return View("AddQuestion", "_TeacherLayout", teacher);
        }

        public ActionResult CreateTask(int classID)
        {
            var teacher = getTVM(classID);

            teacher.Tasks = db.Tasks.ToList();

            return View("CreateTask", "_TeacherLayout", teacher);
        }


        public ActionResult CreateTasksEntry()
        {
            ViewBag.RequestMethod = "POST";

            string desc = Request.Form["description"];
            string id = Request.Form["classID"];
            string startDate = Request.Form["startDate"];
            string dueDate = Request.Form["dueDate"];

            int classID = int.Parse(id);

            if (test(classID) != null)
            {
                return test(classID);
            }
            var tsk = new Tasks();

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


        /*
         * Method that pulls in data and then makes a decision to either delete those fields from the database, or change them.
         */
        public ActionResult EditTaskAction()
        {
            ViewBag.RequestMethod = "POST";

            string desc = Request.Form["description"];
            string id = Request.Form["classID"];
            string startDate = Request.Form["startDate"];
            string dueDate = Request.Form["dueDate"];
            string TaskID = Request.Form["TaskID"];
            string delItem = Request.Form["Delete"];

            int TasksID = int.Parse(TaskID);
            int classID = int.Parse(id);

            //If delete is checked, delete task from database.
            if (delItem == "True")
            {
                foreach (var x in db.Tasks.Where(i => i.TasksID == TasksID))
                {
                    db.RemoveTask(x);
                }

            }

            // save fields to selected database.
            else
            {

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

    }
}