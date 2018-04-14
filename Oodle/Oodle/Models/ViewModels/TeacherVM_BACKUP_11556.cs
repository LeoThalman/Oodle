using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Oodle.Models.ViewModels
{
    public class TeacherVM
    {
        public Class cl { get; set; }
        public List<Class> classList{ get; set;}
        public List<Assignment> assignment { get; set; }
        public List<User> users { get; set; }
        public List<Document> documents {get; set;}
        public List<ClassNotification> notifs { get; set; }
<<<<<<< HEAD
        public List<int> classGrade { get; set; }
=======
        public List<UserRoleClass> roles { get; set; }
>>>>>>> teacherRoster

        public TeacherVM(Class classP, List<User> userP)
        {
            cl = classP;
            users = userP;
        }

        public TeacherVM(List<Class> classP, List<User> userP, List<UserRoleClass> rolesP)
        {
            classList = classP;
            users = userP;
            roles = rolesP;
        }
    }
}