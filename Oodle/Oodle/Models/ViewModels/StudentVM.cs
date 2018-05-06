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
        public List<Quizze> Quizzes { get; set; }
        public List<StudentQuizze> StudentQuizzes { get; set; }
        public List<StudentAnswer> StudentAnswers { get; set; }
        public List<QuizListQuiz> QuizListQuizzes { get; set; }
        public StudentQuizze StudentQuiz { get; set; }
        public List<QuizQuestion> questionList { get; set; }
        public List<MultChoiceAnswer> answerList { get; set; }

        public StudentVM(Class classP, List<User> userP)
        {
            cl = classP;
            users = userP;
        }

    }
}