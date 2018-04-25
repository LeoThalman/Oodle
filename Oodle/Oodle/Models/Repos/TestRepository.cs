﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace Oodle.Models.Repos
{
    public class TestRepository : IOodleRepository
    {
        public IEnumerable<User> Users { get; }
        public IEnumerable<UserRoleClass> UserRoleClasses { get; }
        public IEnumerable<Class> Classes { get; }

        public IEnumerable<ClassNotification> ClassNotifications { get; }

        public IEnumerable<Assignment> Assignments { get; }
        public IEnumerable<Document> Documents { get; }

        public IEnumerable<Quizze> Quizzes { get; }

        public IEnumerable<QuizQuestion> QuizQuestions { get; }
        public IEnumerable<MultChoiceAnswer> MultChoiceAnswers { get; }

        public void SaveChanges()
        {

        }

        public void SetModified(object entity)
        {
           
        }


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
            temp.Remove(a);
        }

        public void AddQuiz(Quizze q)
        {
            List<Quizze> temp =  Quizzes.ToList();
            temp.Remove(q);
        }

        public void AddQuestion(QuizQuestion q)
        {
            List<QuizQuestion> temp = QuizQuestions.ToList();
            temp.Remove(q);
        }

        public void AddAnswer(MultChoiceAnswer a)
        {
            List<MultChoiceAnswer> temp = MultChoiceAnswers.ToList();
            temp.Remove(a);
        }
    }
}