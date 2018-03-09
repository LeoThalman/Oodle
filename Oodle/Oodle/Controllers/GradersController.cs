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
    public class GradersController : Controller
    {
        // GET: Graders
        private Model1 db = new Model1();

        [Authorize]
        public ActionResult index(int classID)
        {
            var idid = User.Identity.GetUserId();

            User user = db.Users.Where(a => a.IdentityID == idid).FirstOrDefault();
            UserRoleClass urc = db.UserRoleClasses.Where(s => s.UsersID == user.UsersID && s.ClassID == classID).FirstOrDefault();

            if (urc == null || urc.RoleID != 1)
            {
                return RedirectToAction("Index", "Class", new { classId = classID });
            }
            return View("index", "_GraderLayout", db.Classes.Where(i => i.ClassID == classID).FirstOrDefault());
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