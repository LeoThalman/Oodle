using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oodle.Models.ViewModels
{
    public class TeacherVM
    {
        public Class cl { get; set; }
        public List<Assignment> assignment { get; set; }
        public List<User> users { get; set; }
        public List<Document> documents {get; set;}
        public List<ClassNotification> notifs { get; set; }
        public List<int> classGrade { get; set; }

        public TeacherVM(Class classP, List<User> userP)
        {
            cl = classP;
            users = userP;
        }
    }
}