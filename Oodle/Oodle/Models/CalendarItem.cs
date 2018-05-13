using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oodle.Models
{
    public class CalendarItem
    {
        public String Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Boolean IsAQuiz { get; set; }
    }
}