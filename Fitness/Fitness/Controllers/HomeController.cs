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
            return View();
        }

        public ActionResult About()
        {

            return View(db.Students.ToList());
        }

        public void ParserTest()
        {
            //Absolute path to test file            
            FileStream fitFile = new FileStream(Server.MapPath("~/App_Data/7CMA3933.FIT"), FileMode.Open);
            ParseFastFit(fitFile);
            fitFile.Close();
            FileStream fitFile2 = new FileStream(Server.MapPath("~/App_Data/81PD5011.FIT"), FileMode.Open);
            ParseFastFit(fitFile2);
            fitFile2.Close();
        }

        /// <summary>
        /// Parses  a .fit file and retrieves the data
        /// parses using FastFitParser
        /// </summary>
        public void ParseFastFit(FileStream fitFile)
        {
            //init parser and list of temp files
            FastParser parser = new FastParser(fitFile);
            if (parser.IsFileValid())
            {
                //init fields for data
                Double tD = 0;
                Double aS = 0;
                Double aC = 0;
                Double aHR = 0;
                Boolean sR = false;

                //go through DataRecord and retrieve relevant data
                foreach (DataRecord d in parser.GetDataRecords())
                {
                    if (d.GlobalMessageNumber == GlobalMessageDecls.Record)
                    {
                        if (d.TryGetField(RecordDef.Distance, out Double distance))
                        {
                            if (distance > tD)
                            {
                                tD = distance;
                            }
                        }
                        if (d.TryGetField(RecordDef.Speed, out double speed))
                        {
                            aS += speed;
                        }
                        if (d.TryGetField(RecordDef.Cadence, out double cadence))
                        {
                            aC += cadence;
                        }
                        if (d.TryGetField(RecordDef.HeartRate, out double heartRate))
                        {
                            aHR = heartRate;
                        }
                        //after first run through start finding the average of the data
                        if (sR)
                        {
                            aS = Math.Round(aS / 2, 2);
                            aC = Math.Round(aC / 2, 2);
                            aHR = Math.Round(aHR / 2, 2);
                        }
                        else
                        {
                            sR = true;
                        }
                    }
                }
                //convert speed and distance to meters and write to Debug.
                aS = Math.Round(aS /1000, 2);
                tD = Math.Round(tD / 1000, 2); ;
                Debug.WriteLine("Distance: " + tD + " m");
                Debug.WriteLine("Speed: " + aS + " m/s");
                Debug.WriteLine("Cadence: " + aC);
                Debug.WriteLine("Heart Rate: " + aHR);

                ViewBag.distanceRan = tD;
                ViewBag.runSpeed = aS;
                ViewBag.Candence = aC;
                ViewBag.heartRate = aHR;

      

                Console.WriteLine("Distance: " + tD + " m");
                Console.WriteLine("Speed: " + aS + " m/s");
                Console.WriteLine("Cadence: " + aC);
                Console.WriteLine("Heart Rate: " + aHR);

                fitFile.Close();
            }
            
        }





        [HttpGet]
        public ActionResult Upload()
        {
                return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    var path = Server.MapPath("~/UploadedFiles");

                    if (!System.IO.Directory.Exists(path))
                    {
                        System.IO.Directory.CreateDirectory(path);
                    }


                    if (System.IO.File.Exists(Path.Combine(Server.MapPath("~/UploadedFiles"), Path.GetFileName(file.FileName))))
                    {
                        System.IO.File.Delete(Path.Combine(Server.MapPath("~/UploadedFiles"), Path.GetFileName(file.FileName)));
                    }

                    if (Path.GetExtension(file.FileName) == ".fit" || Path.GetExtension(file.FileName) == ".FIT")
                        {
                            string _FileName = Path.GetFileName(file.FileName);
                            string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                            file.SaveAs(_path);
                            FileStream fitFile = new FileStream(Server.MapPath("~/UploadedFiles/" + _FileName), FileMode.Open);




                            ParseFastFit(fitFile);
                            ViewBag.Message = "File Uploaded Successfully.";
                        }
                        else
                        {
                            ViewBag.Message = "You can only upload .FIT files.";
                        }
                    }
            
            
            
                return View();
            }
            catch(Exception e)
            {
                ViewBag.Message = e;
                return View();
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}