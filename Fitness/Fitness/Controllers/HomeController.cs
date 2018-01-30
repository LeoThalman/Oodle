using System;
using Fitness.DAL;
using Fitness.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using FastFitParser.Core;
using System.Web.Mvc;
using System.Diagnostics;

namespace Fitness.Controllers
{
    public class HomeController : Controller
    {
        //initialize db connection
        private RunContext db = new RunContext();

        public ActionResult Index()
        {
            //pass a list of students to the view
            //ParseFit("");
            return View(db.Students.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            FileStream fitFile = new FileStream("C:/Users/pocke/Desktop/school-work/cs46X/cs461/fit-test/7CMA3933.FIT", FileMode.Open);
            FastParser parser = new FastParser(fitFile);
            List<DataRecord> temp = null;
            if (parser.IsFileValid())
            {
                temp = parser.GetDataRecords().ToList();
                double recordCount = 0;
                double tDistance = 0;
                double tSpeed = 0;
                double tCadence = 0;
                double tHeartRate = 0;
                foreach(DataRecord d in temp)
                {
                    recordCount+= 1;
                    if (d.TryGetField(RecordDef.Distance, out double distance))
                    {
                        if (distance > tDistance)
                        {
                            tDistance = distance;
                        }
                    }
                    if (d.TryGetField(RecordDef.Speed, out double speed))
                    {
                        tSpeed += speed;
                    }
                    if (d.TryGetField(RecordDef.Cadence, out double cadence))
                    {
                        tCadence += cadence;
                    }
                    if (d.TryGetField(RecordDef.HeartRate, out double heartRate))
                    {
                        tHeartRate += heartRate;
                    }
                }
                tHeartRate = tHeartRate / recordCount;
                tSpeed = tSpeed / recordCount;
                tCadence = tCadence / recordCount;
                Debug.WriteLine("Distance: " + tDistance);
                Debug.WriteLine("Speed: " + tSpeed);
                Debug.WriteLine("Cadence: " + tCadence);
                Debug.WriteLine("Heart Rate: " + tHeartRate);
            }

            return View();
        }

        /*
        public void ParseFit(String fName)
        {
            FileStream fitFile = new FileStream(fName, FileMode.Open);
            FastParser parser = new FastParser(fitFile);
            if(parser.IsFileValid())
            {
                
            }
            
        }
        */

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}