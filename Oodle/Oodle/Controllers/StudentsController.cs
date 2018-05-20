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
using Oodle.Models.Repos;

namespace Oodle.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private IOodleRepository db;

        public StudentsController(IOodleRepository repo)
        {
            this.db = repo;
        }


        public ActionResult test(int classID)
        {
            var idid = User.Identity.GetUserId();

            User user = db.Users.Where(a => a.IdentityID == idid).FirstOrDefault();
            UserRoleClass urc = db.UserRoleClasses.Where(s => s.UsersID == user.UsersID && s.ClassID == classID).FirstOrDefault();

            if (urc == null || urc.RoleID != 2)
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


            var student = getTVM(classID);
            var classes = db.Classes.ToList(); // list of all classes



            ViewBag.classList = classes;
            foreach (var item in classes) // Loop through List with foreach
            {
                System.Diagnostics.Debug.WriteLine(item);
            }


            System.Diagnostics.Debug.WriteLine(classes);





            return View("Index", "_StudentLayout", student);
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult IndexPost(int classID)
        {
            if (test(classID) != null)
            {

                return test(classID);
            }


            var student = getTVM(classID);
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


            student.Notes = db.Notes.ToList();

            // return View("Notes", "_StudentLayout", student);
            return View("Index", "_StudentLayout", student);
        }






        public ActionResult Assignment(int classID)
        {
            if (test(classID) != null)
            {
                return test(classID);
            }

            var student = getTVM(classID);

            return View("Assignment", "_StudentLayout", student);
        }

        public ActionResult Quiz()
        {
            return View("Quiz", "_StudentLayout");
        }

        /*
         * Returns the actionresult for Tasks and directs you to the tasks view.
         */
        public ActionResult Tasks(int classID)
        {
            return View("Tasks", "_StudentLayout", getTVM(classID));
        }

        public ActionResult Slack()
        {
            return View("Slack", "_StudentLayout");
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
            //adds tasks to Teacher VM
            teacher.Tasks = db.Tasks.ToList();
            //adds notes to teacher VM
            teacher.Notes = db.Notes.ToList();

            return teacher;
        }

        ///////////////////////////////////////////////////////

        public ActionResult AssignmentTurnIn(int classID, int assignmentID)
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

            var student = new TeacherVM(db.Classes.Where(i => i.ClassID == classID).FirstOrDefault(), request);

            student.assignment = db.Assignments.Where(i => i.ClassID == classID && i.AssignmentID == assignmentID).ToList();

            var idid = User.Identity.GetUserId();
            int studentID = db.Users.Where(a => a.IdentityID == idid).FirstOrDefault().UsersID;


            student.documents = GetFiles(classID, assignmentID, studentID);

            return View("AssignmentTurnIn", "_StudentLayout", student);
        }

        [HttpPost]
        public ActionResult AssignmentTurnIn(HttpPostedFileBase postedFile, int classID, int assignmentID)
        {
            if (test(classID) != null)
            {
                return test(classID);
            }
            var idid = User.Identity.GetUserId();
            int studentID = db.Users.Where(a => a.IdentityID == idid).FirstOrDefault().UsersID;


            byte[] bytes;
            using (BinaryReader br = new BinaryReader(postedFile.InputStream))
            {
                bytes = br.ReadBytes(postedFile.ContentLength);
            }

            AssignmentTurnInHelper(postedFile, classID, assignmentID, studentID, bytes);
           

            return RedirectToAction("Grade", "Students", new { classId = classID });
        }

        public Boolean AssignmentTurnInHelper(HttpPostedFileBase postedFile, int classID, int assignmentID, int studentID, byte[] bytes)
        {
            var submitted = DateTime.Now;
            var urcL = db.UserRoleClasses.Where(i => i.RoleID == 3 && i.ClassID == classID);
            var list = new List<int>();

            foreach (var i in urcL)
            {
                list.Add(i.UsersID);
            }

            var date = DateTime.Now;
            var request = db.Users.Where(i => list.Contains(i.UsersID)).ToList();

            var student = new TeacherVM(db.Classes.Where(i => i.ClassID == classID).FirstOrDefault(), request);

            student.assignment = db.Assignments.Where(i => i.ClassID == classID && i.AssignmentID == assignmentID).ToList();
            Debug.WriteLine("test3");


            if (db.Documents.Where(i => i.ClassID == classID && i.AssignmentID == assignmentID && i.UserID == studentID).ToList().Count() == 0)
            {
                Debug.WriteLine("test");

                var doc = new Document();
                doc.Name = Path.GetFileName(postedFile.FileName);
                doc.ContentType = postedFile.ContentType;
                doc.Data = bytes;
                doc.ClassID = classID;
                doc.AssignmentID = assignmentID;
                doc.UserID = studentID;
                doc.submitted = submitted;
                doc.Grade = -1;
                doc.Date = date;

                db.AddDocument(doc);

                db.SaveChanges();
                student.documents = GetFiles(classID, assignmentID, studentID);

                return true;
            }
            else
            {
                Debug.WriteLine("test2");


                var change = db.Documents.Where(i => i.ClassID == classID && i.AssignmentID == assignmentID && i.UserID == studentID).ToList();
                change.ForEach(x => x.Name = Path.GetFileName(postedFile.FileName));
                change.ForEach(x => x.ContentType = postedFile.ContentType);
                change.ForEach(x => x.Data = bytes);
                change.ForEach(x => x.ClassID = classID);
                change.ForEach(x => x.AssignmentID = assignmentID);
                change.ForEach(x => x.UserID = studentID);
                change.ForEach(x => x.submitted = submitted);
                change.ForEach(x => x.Grade = -1);
                change.ForEach(x => x.Date = date);

                db.SaveChanges();
                student.documents = GetFiles(classID, assignmentID, studentID);
                return false;
            }
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

        public List<Document> GetFiles(int classID, int assignmentID, int studentID)
        {
            return db.Documents.Where(i => i.ClassID == classID && i.AssignmentID == assignmentID && i.UserID == studentID).ToList();
        }

        public ActionResult Grade(int classID)
        {
            if (test(classID) != null)
            {
                return test(classID);
            }
            var idid = User.Identity.GetUserId();

            int userId = db.Users.Where(a => a.IdentityID == idid).FirstOrDefault().UsersID;

            var teacher = getTVM(classID);

            //This will be used when I refactor this code in a later sprint.
            teacher.assignment = db.Assignments.Where(i => i.ClassID == classID).ToList();
            teacher.documents = db.Documents.Where(i => i.ClassID == classID && i.UserID == userId).ToList();

            List<Assignment> list2 = teacher.assignment;
            List<Document> list = teacher.documents;

            teacher.classGrade = new List<int>();
            teacher.classGrade.Add(GradeHelper(list, list2));

            return View("Grade", "_StudentLayout", teacher);
        }

        //This is the method I'm really testing.
        public int GradeHelper(List<Document> list, List<Assignment> list2)
        {
            int total = 0;
            int totalWeight = 0;

            foreach (Assignment assi in list2)
            {
                Document doc = list.Where(i => i.AssignmentID == assi.AssignmentID).FirstOrDefault();
                if (doc != null)
                {
                    if (doc.Grade != -1)
                    {
                        total = total + (doc.Grade * doc.Assignment.Weight);
                        totalWeight = totalWeight + doc.Assignment.Weight;
                    }
                }
                else if (DateTime.Compare(DateTime.Parse(assi.DueDate.ToString()), DateTime.Now) < 0)
                {
                    total = total + (0);
                    totalWeight = totalWeight + assi.Weight;
                }
            }
            if (totalWeight == 0)
            {
                return total = 0;
            }
            else
            {
                return total = total / totalWeight;
            }
        }

        [HttpPost]
        public ActionResult FakeGrade()
        {
            string id = Request.Form["classID"];
            int classID = int.Parse(id);

            var idid = User.Identity.GetUserId();

            int userId = db.Users.Where(a => a.IdentityID == idid).FirstOrDefault().UsersID;

            TeacherVM teacher = FakeGradeHelper(classID, userId, Request.Form);

            return View("Grade", "_StudentLayout", teacher);
        }


        public TeacherVM FakeGradeHelper(int classID, int userId, System.Collections.Specialized.NameValueCollection form)
        {
            var teacher = getTVM(classID);

            teacher.assignment = db.Assignments.Where(j => j.ClassID == classID).ToList();

            teacher.documents = db.Documents.Where(j => j.ClassID == classID && j.UserID == userId).ToList();
            var assis = db.Assignments.Where(j => j.ClassID == classID).ToList();

            int i = 0;
            string i2 = "1";
            int fTotal = 0;
            int fNumber = 0;
            teacher.fClassGrade = new List<int>();

            while (i2 != null && i2 != "")
            {
                i2 = form[i.ToString()];
                if (i2 != null)
                {
                    int i3 = Int32.Parse(i2);
                    fTotal = (i3 * assis[i].Weight) + fTotal;
                    teacher.fClassGrade.Add(i3);
                    fNumber = fNumber + assis[i].Weight;
                }
                i++;
            }

            if (fNumber == 0)
            {
                teacher.fakeTotal = 0;
            }
            else
            {
                teacher.fakeTotal = fTotal / fNumber;
            }

            teacher.classGrade = new List<int>();
            teacher.classGrade.Add(GradeHelper(teacher.documents, teacher.assignment));

            return teacher;
        }

        public ActionResult CreateNote(int classID)
        {
            var student = getTVM(classID);


            student.Notes = db.Notes.ToList();

            return View("CreateNote", "_StudentLayout", student);
        }

        public ActionResult Notes(int classID)
        {
            var student = getTVM(classID);

            //return View("Notes", "_StudentLayout", student);
            return View("Index", "_StudentLayout", student);

        }

        public StudentVM getSVM(int classID)
        {
            var urcL = db.UserRoleClasses.Where(i => i.RoleID == 3 && i.ClassID == classID);
            var list = new List<int>();

            foreach (var i in urcL)
            {
                list.Add(i.UsersID);
            }
            var request = db.Users.Where(i => list.Contains(i.UsersID)).ToList();

            StudentVM student = new StudentVM(db.Classes.Where(i => i.ClassID == classID).FirstOrDefault(), request);

            student.assignment = db.Assignments.Where(i => i.ClassID == classID).OrderBy(i => i.StartDate).ToList();

            var id = User.Identity.GetUserId();
            User user = db.Users.Where(a => a.IdentityID == id).FirstOrDefault();
            student.notifs = GetNotifs(user.UsersID, classID);
            //student.notifs = db.ClassNotifications.Where(i => i.ClassID == classID).OrderBy(i => i.TimePosted).ToList();
            //adds tasks to Teacher VM
            student.Tasks = db.Tasks.ToList();
            //adds notes to teacher VM
            student.Notes = db.Notes.ToList();

            return student;
        }

        public List<ClassNotification> GetNotifs (int UsersID, int classID)
        {
            List<HiddenNotification> HiddenNotifs = db.HiddenNotifications.Where(n => n.UsersID == UsersID && n.ClassID == classID).ToList();
            if (HiddenNotifs == null)
            {
                return db.ClassNotifications.Where(i => i.ClassID == classID).OrderBy(i => i.TimePosted).ToList();
            }
            else
            {
                List<ClassNotification> NotifList = db.ClassNotifications.Where(i => i.ClassID == classID).OrderBy(i => i.TimePosted).ToList();
                ClassNotification Temp = null;
                foreach (HiddenNotification h in HiddenNotifs)
                {
                    Temp = NotifList.FirstOrDefault(c => c.ClassNotificationID == h.ClassNotificationID);
                    if(Temp != null)
                    {
                        NotifList.Remove(Temp);
                        Temp = null;
                    }
                }
                return NotifList;                
            }
        }

        public ActionResult HideNotifs(int classID)
        {
            if (test(classID) != null)
            {
                return test(classID);
            }
            var id = User.Identity.GetUserId();
            int UsersID = db.Users.Where(a => a.IdentityID == id).FirstOrDefault().UsersID;
            StudentVM student = getSVM(classID);
            List<HiddenNotification> HList = db.HiddenNotifications.Where(n => n.UsersID == UsersID && n.ClassID == classID).ToList();
            List<ClassNotification> ClassNotifs = db.ClassNotifications.Where(n => n.ClassID == classID).ToList();
            student.HideNotifs = new List<HideNotifList>();
            HideNotifList Temp = null;
            foreach(ClassNotification c in ClassNotifs)
            {
                Temp = new HideNotifList();
                Temp.Notif = c;
                if(HList.Exists(h => h.ClassNotificationID == c.ClassNotificationID))
                {
                    Temp.Hidden = true;
                }
                else
                {
                    Temp.Hidden = false;
                }
                student.HideNotifs.Add(Temp);
            }

            return View("HideNotifs", "_StudentLayout", student);
        }

        public ActionResult SaveHideNotifs

        public ActionResult QuizList(int classID)
        {
            if (test(classID) != null)
            {
                return test(classID);
            }
            StudentVM student = getSVM(classID);
            student.Quizzes = db.Quizzes.Where(q => q.ClassID == classID).ToList();
            student.StudentQuizzes = db.StudentQuizzes.Where(q => q.Quizze.ClassID == classID).ToList();
            student.QuizListQuizzes = new List<QuizListQuiz>();
            QuizListQuiz temp = null;
            foreach (Quizze q in student.Quizzes)
            {
                temp = new QuizListQuiz();
                if(db.StudentQuizzes.Where(sq => sq.QuizID == q.QuizID).FirstOrDefault() != null)
                {
                    temp.Quiz = q;
                    temp.Taken = true;
                    student.QuizListQuizzes.Add(temp);
                }
                else
                {
                    if (q.IsHidden == false && q.StartTime.CompareTo(DateTime.Now) < 0 && q.EndTime.CompareTo(DateTime.Now) > 0)
                    {
                        temp.Quiz = q;
                        temp.Taken = false;
                        student.QuizListQuizzes.Add(temp);
                    }
                }
                
            }
            return View("QuizList", "_StudentLayout", student);
        }

        [HttpGet]
        public ActionResult TakeQuiz(int QuizID)
        {

            int classID = db.Quizzes.Where(q => q.QuizID == QuizID).FirstOrDefault().ClassID;
            if (test(classID) != null)
            {
                return test(classID);
            }
            StudentVM student = getSVM(classID);
            student.StudentQuiz = new StudentQuizze();
            student.StudentQuiz.QuizID = QuizID;
            student.questionList = db.QuizQuestions.Where(q => q.QuizID == QuizID).ToList();
            student.answerList = db.MultChoiceAnswers.Where(q => q.QuizQuestion.QuizID == QuizID).ToList();
            student.StudentAnswers = new List<StudentAnswer>();

            return View("TakeQuiz", "_StudentLayout", student);
        }

        [HttpPost]
        public ActionResult AnswerQuiz([Bind(Include = "QuestionID,AnswerNumber")] List<StudentAnswer> StudentAnswers, 
                                       [Bind(Include = "QuizID")] StudentQuizze StudentQuiz)
        {
            int QuizID = StudentQuiz.QuizID;
            int ClassID = db.Quizzes.Where(q=> q.QuizID == QuizID).FirstOrDefault().ClassID;
            StudentVM student = getSVM(ClassID);
            var IdentityID = User.Identity.GetUserId();                
            StudentQuiz.UserID = db.Users.Where(a => a.IdentityID == IdentityID).FirstOrDefault().UsersID;
            StudentQuiz.CanReview = true;
            db.AddStudentQuiz(StudentQuiz);
            db.SaveChanges();
            int PointTotal = 0;
            foreach (StudentAnswer a in StudentAnswers)
            {
                if (a.AnswerNumber == db.MultChoiceAnswers.Where(an => an.QuestionID == a.QuestionID).FirstOrDefault().CorrectAnswer)
                {
                    a.StudentPoints = db.QuizQuestions.Where(q => q.QuestionID == a.QuestionID).FirstOrDefault().Points;
                }
                else
                {
                    a.StudentPoints = 0;
                }
                PointTotal += a.StudentPoints;
                a.SQID = StudentQuiz.SQID;
                db.AddStudentAnswer(a);
                db.SaveChanges();
            }
            StudentQuiz.TotalPoints = PointTotal;
            db.SetModified(StudentQuiz);
            db.SaveChanges();
            return RedirectToAction("QuizList", "Students", new { classID = ClassID });

        }
    }
}