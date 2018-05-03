using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oodle.Models.ViewModels
{
    public class StudentVM : ITeacherVM
    {
        public Class cl { get; set; }
        public List<Class> classList { get; set; }
        public List<User> users { get; set; }
        public List<Assignment> assignment { get; set; }
        public List<ClassNotification> notifs { get; set; }
        public List<Tasks> Tasks { get; set; }
        public List<Notes> Notes { get; set; }

        public StudentVM(Class classP, List<User> userP)
        {
            cl = classP;
            users = userP;
        }

    }
}