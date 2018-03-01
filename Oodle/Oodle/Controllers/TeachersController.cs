using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Oodle.Controllers
{
    public class TeachersController : Controller
    {
        // GET: Teachers
        public ActionResult Index()
        {
            return View("Index", "_TeacherLayout");
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