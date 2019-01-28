using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Oodle.Models
{
    public class HideNotifList
    {
        public int NotifID { get; set; }

        [Display(Name = "Hide On Class Page")]
        public Boolean Hidden { get; set; }
        public int ClassID { get; set; }
        public string Notification { get; set; }
    }
}