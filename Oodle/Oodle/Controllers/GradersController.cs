using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Oodle.Controllers
{
    public class GradersController : Controller
    {
        // GET: Graders
        public ActionResult Index()
        {
            return View("Index", "_GraderLayout");
        }

        public ActionResult ToDoList()
        {
            return View("ToDoList", "_GraderLayout");
        }

        public ActionResult GradeAssignment()
        {
            return View("GradeAssignment", "_GraderLayout");
        }

        public ActionResult GradeQuiz()
        {
            return View("GradeQuiz", "_GraderLayout");
        }

        public ActionResult GradeTask()
        {
            return View("GradeTask", "_GraderLayout");
        }
    }
}