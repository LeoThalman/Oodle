using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Oodle.Controllers
{
    public class HomeController : Controller
    {
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

        public ActionResult Calendar()
        {
            
            return View("Calendar", "_CalendarLayout");
        }




    }
}