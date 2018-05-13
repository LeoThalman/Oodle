using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oodle.Models.ViewModels
{
    public class CalendarVM
    {
        public List<Assignment> Assignments { get; set; }
        public List<Quizze> Quizzes { get; set; }
        public int UserID { get; set; }
    }
}