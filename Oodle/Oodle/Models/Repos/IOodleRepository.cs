using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;


namespace Oodle.Models.Repos
{
    public interface IOodleRepository
    {
        IEnumerable<User> Users { get; }
        IEnumerable<UserRoleClass> UserRoleClasses { get; }
        IEnumerable<Class> Classes { get; }
        IEnumerable<ClassNotification> ClassNotifications { get; }
        IEnumerable<Assignment> Assignments { get; }
        IEnumerable<Document> Documents{ get; }
        IEnumerable<Quizze> Quizzes { get; }
        IEnumerable<QuizQuestion> QuizQuestions { get; }
        IEnumerable<MultChoiceAnswer> MultChoiceAnswers { get; }


        void SaveChanges();
        void SetModified(object entity);


        void RemoveNotif(ClassNotification notif);
        void AddNotif(ClassNotification notif);
        void RemoveURC(UserRoleClass urc);
        void AddURC(UserRoleClass urc);
        void RemoveClass(Class c);
        void AddAssignment(Assignment a);
        void AddQuiz(Quizze q);
        void AddQuestion(QuizQuestion q);
        void AddAnswer(MultChoiceAnswer a);
    }
}
