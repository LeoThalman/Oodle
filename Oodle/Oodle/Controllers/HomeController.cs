﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Oodle.Models;
using Oodle.Models.ViewModels;
using Oodle.Models.Repos;

namespace Oodle.Controllers
{
    public class HomeController : Controller
    {
        private IOodleRepository db;

        public HomeController(IOodleRepository repo)
        {
            this.db = repo;
        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public class FormData
        {
            public string TextBoxStringData { get; set; }

            public int TextBoxIntData { get; set; }

        }

        public ActionResult Tools()
        {

            return View();
        }


        [HttpPost]
        public int Tools(int textBoxIntData = 5)
        {
            int rating = textBoxIntData;
            //if rating is less then 0 we will just consider it the lowest possible
            if (rating < 0)
            {
                rating = 0;
            } //if rating is above 5 then we will just make it max rating.
            else if (rating > 5)
            {
                rating = 5;
            }
            //if its between 0-5 then we will keep it as is
              

            System.Diagnostics.Debug.WriteLine("this should only ever be between 1 and 5" + rating);

            return rating;
        }

       

        public ActionResult UnderDevelopment()
        {

            return View();

        }

       


        //can remove, was just using to test that testing works and it does
        public string Capitalize(string str)
        {
            return char.ToUpper(str[0]) + str.Substring(1);

        }

        //Also a test method for Sam
        public string GetTimeOfDay(DateTime dateTime)
        {
            if (dateTime.Hour >= 0 && dateTime.Hour < 6)
            {
                return "Night";
            }
            if (dateTime.Hour >= 6 && dateTime.Hour < 12)
            {
                return "Morning";
            }
            if (dateTime.Hour >= 12 && dateTime.Hour < 18)
            {
                return "Noon";
            }
            return "Evening";
        }

        public List<int> GetClassIDs(int UserID)
        {
            List<int> ClassIDList = new List<int>();
            List<UserRoleClass> temp = db.UserRoleClasses.Where(u => u.UsersID == UserID).ToList();
            foreach(UserRoleClass c in temp)
            {
                ClassIDList.Add(c.ClassID);
            }

            return ClassIDList;
        }

        public ActionResult Calendar()
        {
            var id = User.Identity.GetUserId();
            
            User user = db.Users.Where(a => a.IdentityID == id).FirstOrDefault();
            
            List<int> ClassIDs = GetClassIDs(user.UsersID);
            CalendarVM cal = new CalendarVM();

            cal.UserID = user.UsersID;
            cal.Assignments = new List<Assignment>();
            cal.Quizzes = new List<Quizze>();
            List<Assignment> TempAssign = new List<Assignment>();
            List<Quizze> TempQuiz = new List<Quizze>();
            foreach (int ClassID in ClassIDs)
            {
                TempAssign = db.Assignments.Where(a => a.ClassID == ClassID).ToList();
                foreach (Assignment a in TempAssign)
                {
                    cal.Assignments.Add(a);
                }

                TempQuiz = db.Quizzes.Where(q => q.ClassID == ClassID).ToList();
                foreach (Quizze q in TempQuiz)
                {
                    cal.Quizzes.Add(q);
                }
            }
            return View("Calendar", "_CalendarLayout", cal);
        }
    }
}