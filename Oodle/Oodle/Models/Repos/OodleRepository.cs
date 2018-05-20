using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Oodle.Models.Repos
{
    public class OodleRepository : IOodleRepository
    {
        private Model1 db = new Model1();

//-----------------------Database Tables-------------------------------
        //These act as tables for the database
        public IEnumerable<User> Users
        {
            get { return db.Users; }
        }

        public IEnumerable<UserRoleClass> UserRoleClasses
        {
            get { return db.UserRoleClasses; }
        }

        public IEnumerable<Class> Classes
        {
            get { return db.Classes; }
        }

        public IEnumerable<ClassNotification> ClassNotifications
        {
            get { return db.ClassNotifications; }
        }

        public IEnumerable<HiddenNotification> HiddenNotifications
        {
            get { return db.HiddenNotifications; }
        }

        public IEnumerable<Assignment> Assignments
        {
            get { return db.Assignments; }
        }

        public IEnumerable<Document> Documents
        {
            get { return db.Documents; }
        }

        public IEnumerable<Quizze> Quizzes
        {
            get { return db.Quizzes; }
        }

        public IEnumerable<QuizQuestion> QuizQuestions
        {
            get { return db.QuizQuestions; }
        }

        public IEnumerable<MultChoiceAnswer> MultChoiceAnswers
        {
            get { return db.MultChoiceAnswers; }
        }

        public IEnumerable<Tasks> Tasks
        {
            get { return db.Tasks; }
        }

        public IEnumerable<Notes> Notes
        {
            get { return db.Notes; }
        }

        public IEnumerable<StudentQuizze> StudentQuizzes 
        {
            get { return db.StudentQuizzes; }
        }

        public IEnumerable<StudentAnswer> StudentAnswers
        {
            get { return db.StudentAnswers; }
        }


        //-----------------------Add and Remove Methods for the tables-------------------------------
        //Save Db Changes
        public void SaveChanges()
        {
            db.SaveChanges();
        }
        //Mark EntityState as Modified to save edits
        public void SetModified(object entity)
        {
            db.Entry(entity).State = EntityState.Modified;
        }

        public void RemoveNotif(ClassNotification notif)
        {
            db.ClassNotifications.Remove(notif);
        }

        public void AddNotif(ClassNotification notif)
        {
            db.ClassNotifications.Add(notif);
        }

        public void AddHiddenNotif(HiddenNotification hnotif)
        {
            db.HiddenNotifications.Add(hnotif);
        }

        public void RemoveHiddenNotif(HiddenNotification hnotif)
        {
            db.HiddenNotifications.Remove(hnotif);
        }


        public void RemoveURC(UserRoleClass urc)
        {
            db.UserRoleClasses.Remove(urc);
        }

        public void AddURC(UserRoleClass urc)
        {
            db.UserRoleClasses.Add(urc);
        }

        public void RemoveClass(Class c)
        {
            db.Classes.Remove(c);
        }

        public void AddAssignment(Assignment a)
        {
            db.Assignments.Add(a);
        }

        public void AddQuiz(Quizze q)
        {
            db.Quizzes.Add(q);
        }

        public void AddQuestion(QuizQuestion q)
        {
            db.QuizQuestions.Add(q);
        }

        public void AddAnswer(MultChoiceAnswer a)
        {
            db.MultChoiceAnswers.Add(a);
        }

        public void AddTask(Tasks t)
        {
            db.Tasks.Add(t);
        }

        public void RemoveTask(Tasks t)
        {
            db.Tasks.Remove(t);
        }

        public void AddNote(Notes n)
        {
            db.Notes.Add(n);
        }

        public void AddDocument(Document d)
        {
            db.Documents.Add(d);
        }

        public void AddStudentQuiz(StudentQuizze q)
        {
            db.StudentQuizzes.Add(q);
        }

        public void RemoveStudentQuiz(StudentQuizze q)
        {
            db.StudentQuizzes.Remove(q);
        }

        public void AddStudentAnswer(StudentAnswer a)
        {
            db.StudentAnswers.Add(a);
        }

        public void RemoveStudentAnswer(StudentAnswer a)
        {
            db.StudentAnswers.Remove(a);
        }
        public void RemoveQuiz(Quizze q)
        {
            db.Quizzes.Remove(q);
		}
        public void RemoveDocument(Document d)
        {
            db.Documents.Remove(d);
        }

        public void DeleteAssignment(Assignment a)
        {
            db.Assignments.Remove(a);
        }
    }
}