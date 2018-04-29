using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Oodle.Models.Repos
{
    public class TestRepository : IOodleRepository
    {
        //Fake Tables for test Database
        public IEnumerable<User> Users { get; }
        public IEnumerable<UserRoleClass> UserRoleClasses { get; }
        public IEnumerable<Class> Classes { get; }
        public IEnumerable<ClassNotification> ClassNotifications { get; }
        public IEnumerable<Assignment> Assignments { get; }
        public IEnumerable<Document> Documents { get; }
        public IEnumerable<Quizze> Quizzes { get; set; }
        public IEnumerable<QuizQuestion> QuizQuestions { get; set; }
        public IEnumerable<MultChoiceAnswer> MultChoiceAnswers { get; set; }
        public IEnumerable<Tasks> Tasks { get; }
        public IEnumerable<Notes> Notes { get; }



        //No database so no need to save
        public void SaveChanges()
        {

        }

        //No database so no need to edit
        public void SetModified(object entity)
        {
           
        }

//--------------------Add and Remove methods for unit tests------------------
        //Convert IEnumerable into a list to add or remove from it
        //Haven't tested if the changes save or if we need to save them manually
        public void RemoveNotif(ClassNotification notif)
        {
            List<ClassNotification> temp = ClassNotifications.ToList();
            temp.Remove(notif);

        }

        public void AddNotif(ClassNotification notif)
        {
            List<ClassNotification> temp = ClassNotifications.ToList();
            temp.Add(notif);
        }

        public void RemoveURC(UserRoleClass urc)
        {
            List<UserRoleClass> temp = UserRoleClasses.ToList();
            temp.Remove(urc);
        }

        public void AddURC(UserRoleClass urc)
        {
            List<UserRoleClass> temp = UserRoleClasses.ToList();
            temp.Add(urc);
        }

        public void RemoveClass(Class c)
        {
            List<Class> temp = Classes.ToList();
            temp.Remove(c);
        }

        public void AddAssignment(Assignment a)
        {
            List<Assignment> temp = Assignments.ToList();
            temp.Add(a);
        }

        public void AddQuiz(Quizze q)
        {
            List<Quizze> temp =  Quizzes.ToList();
            temp.Add(q);
            Quizzes = temp;
        }

        public void AddQuestion(QuizQuestion q)
        {
            List<QuizQuestion> temp = QuizQuestions.ToList();
            temp.Add(q);
            QuizQuestions = temp;
        }

        public void AddAnswer(MultChoiceAnswer a)
        {
            List<MultChoiceAnswer> temp = MultChoiceAnswers.ToList();
            temp.Add(a);
            MultChoiceAnswers = temp;
        }

        public void AddTask(Tasks t)
        {
            List<Tasks> temp = Tasks.ToList();
            temp.Add(t);
        }

        public void RemoveTask(Tasks t)
        {
            List<Tasks> temp = Tasks.ToList();
            temp.Remove(t);
        }


        public void AddNote(Notes n)
        {
            List<Notes> temp = Notes.ToList();
            temp.Add(n);
        }
    }
}