using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Oodle.Controllers
{
    public class StudentsController : Controller
    {
        // GET: Students
        public ActionResult Index()
        {
            return View("Index", "_StudentLayout");
        }

        public ActionResult Grade()
        {
            return View("Grade", "_StudentLayout");
        }

        public ActionResult Assignment()
        {
            return View("Assignment", "_StudentLayout");
        }

        public ActionResult Quiz()
        {
            return View("Quiz", "_StudentLayout");
        }

        public ActionResult Task()
        {
            return View("Task", "_StudentLayout");
        }

        public ActionResult Slack()
        {
            return View("Slack", "_StudentLayout");
        }
    }
}