using System;
using Fitness.DAL;
using Fitness.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fitness.Controllers
{
    public class HomeController : Controller
    {
        //initialize db connection
        private RunContext db = new RunContext();

        public ActionResult Index()
        {
            //pass a list of students to the view
            return View(db.Students.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}