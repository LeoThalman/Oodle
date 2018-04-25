﻿using System;
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
    }
}