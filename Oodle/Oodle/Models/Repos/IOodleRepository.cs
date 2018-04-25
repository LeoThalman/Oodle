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
        //These act as tables for the database
        IEnumerable<User> Users { get; }
        IEnumerable<UserRoleClass> UserRoleClasses { get; }
        IEnumerable<Class> Classes { get; }
        IEnumerable<ClassNotification> ClassNotifications { get; }
        IEnumerable<Assignment> Assignments { get; }
        IEnumerable<Document> Documents{ get; }
        IEnumerable<Quizze> Quizzes { get; }
        IEnumerable<QuizQuestion> QuizQuestions { get; }
        IEnumerable<MultChoiceAnswer> MultChoiceAnswers { get; }
        IEnumerable<Tasks> Tasks { get; }


        //Save db changes
        void SaveChanges();
        //Mark entry EntityState as modified to save edits
        void SetModified(object entity);

        //Various Add and Remove functions for the tables
        void RemoveNotif(ClassNotification notif);
        void AddNotif(ClassNotification notif);
        void RemoveURC(UserRoleClass urc);
        void AddURC(UserRoleClass urc);
        void RemoveClass(Class c);
        void AddAssignment(Assignment a);
        void AddQuiz(Quizze q);
        void AddQuestion(QuizQuestion q);
        void AddAnswer(MultChoiceAnswer a);
        void AddTask(Tasks t);
        void RemoveTask(Tasks t);
    }
}
