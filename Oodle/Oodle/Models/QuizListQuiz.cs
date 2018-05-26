using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oodle.Models
{
    public class QuizListQuiz
    {
        public Quizze Quiz { get; set; }
        public Boolean Taken { get; set; }
        public StudentQuizze StudentQuiz { get; set; }
    }
}