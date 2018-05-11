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

            var urcL = db.UserRoleClasses.Where(i => i.RoleID == 3 && i.ClassID == classID);
            var list = new List<int>();

            foreach (var i in urcL)
            {
                list.Add(i.UsersID);
            }
            var request = db.Users.Where(i => list.Contains(i.UsersID)).ToList();

            var student = new TeacherVM(db.Classes.Where(i => i.ClassID == classID).FirstOrDefault(), request);

            student.assignment = db.Assignments.Where(i => i.ClassID == classID && i.AssignmentID == assignmentID).ToList();

            byte[] bytes;
            using (BinaryReader br = new BinaryReader(postedFile.InputStream))
            {
                bytes = br.ReadBytes(postedFile.ContentLength);
            }

            var submitted = DateTime.Now;

            var date = DateTime.Now;

            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            if (db.Documents.Where(i => i.ClassID == classID && i.AssignmentID == assignmentID && i.UserID == studentID) == null)
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string query = "INSERT INTO Documents VALUES (@Name, @ContentType, @Data, @Submitted, @ClassID, @AssignmentID, @userID, @grade, @Date)";
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@Name", Path.GetFileName(postedFile.FileName));
                        cmd.Parameters.AddWithValue("@ContentType", postedFile.ContentType);
                        cmd.Parameters.AddWithValue("@Data", bytes);
                        cmd.Parameters.AddWithValue("@ClassID", classID);
                        cmd.Parameters.AddWithValue("@AssignmentID", assignmentID);
                        cmd.Parameters.AddWithValue("@userID", studentID);
                        cmd.Parameters.AddWithValue("@Submitted", submitted);
                        cmd.Parameters.AddWithValue("@grade", -1);
                        cmd.Parameters.AddWithValue("@Date", date);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            else
            {
                var change = db.Documents.Where(i => i.ClassID == classID && i.AssignmentID == assignmentID && i.UserID == studentID).ToList();
                change.ForEach(x => x.Name = Path.GetFileName(postedFile.FileName));
                change.ForEach(x => x.ContentType = postedFile.ContentType);
                change.ForEach(x => x.Data = bytes);
                change.ForEach(x => x.ClassID = classID);
                change.ForEach(x => x.AssignmentID = assignmentID);
                change.ForEach(x => x.UserID = studentID);
                change.ForEach(x => x.submitted =submitted);
                change.ForEach(x => x.Grade = -1);
                change.ForEach(x => x.Date = date);


                db.SaveChanges();

            }
            student.documents = GetFiles(classID, assignmentID, studentID);

            return RedirectToAction("Grade", "Students", new { classId = classID });
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

        private static List<Document> GetFiles(int classID, int assignmentID, int studentID)
        {
            List<Document> files = new List<Document>();
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "SELECT Id, Name FROM Documents WHERE ClassID=@classID AND AssignmentID=@assignmentID AND userID=@userID ";
                    cmd.Parameters.AddWithValue("@classID", classID);
                    cmd.Parameters.AddWithValue("@assignmentID", assignmentID);
                    cmd.Parameters.AddWithValue("@userID", studentID);
                    
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            files.Add(new Document
                            {
                                Id = Convert.ToInt32(sdr["Id"]),
                                Name = sdr["Name"].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }
            return files;
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
                Document doc = db.Documents.Where(i => i.AssignmentID == assi.AssignmentID).FirstOrDefault();
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
                i2 =  form[i.ToString()];
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
    }
}