using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Oodle.Models.ViewModels
{
    public class UserVMish
    {
        public List<TimeSpan> Late { get; set; }
        public List<bool> stat { get; set; }
        public List<Document> submissions { get; set; }
        public List<Assignment> assi { get; set; }
    }
}