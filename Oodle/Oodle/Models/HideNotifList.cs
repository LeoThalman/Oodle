using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oodle.Models
{
    public class HideNotifList
    {
        public int NotifID { get; set; }
        public Boolean Hidden { get; set; }
        public int ClassID { get; set; }
        public string Notification { get; set; }
    }
}