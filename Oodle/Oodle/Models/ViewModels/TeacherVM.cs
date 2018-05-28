using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Oodle.Models.ViewModels
{
    public class TeacherVM : ITeacherVM
    {
        public Class cl { get; set; }
        public List<Class> classList{ get; set;}
        public List<Assignment> assignment { get; set; }
        public List<User> users { get; set; }
        public List<Document> documents {get; set;}
        public List<ClassNotification> notifs { get; set; }
        public ClassNotification rNotif { get; set; }
        public List<Quizze> quizzes { get; set; }
        public List<int> classGrade { get; set; } 
        public List<UserRoleClass> roles { get; set; }
        public List<int> fClassGrade { get; set; }
        public int fakeTotal { get; set; }
        public Quizze quiz { get; set; }
        public QuizQuestion question { get; set; }
        public MultChoiceAnswer answer { get; set; }
        public Boolean Locked { get; set; }
        public List<QuizQuestion> questionList { get; set; }
        public List<MultChoiceAnswer> answerList { get; set; }
        public List<Tasks> Tasks { get; set; }
        public List<Notes> Notes { get; set; }
        public List<UserVMish> perUser { get; set; }
        public List<StudentQuizze> StudentQuizze { get; set; }

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