using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Oodle.Models
{
    public class QuizReview
    {
        public int Points { get; set; }

        public int StudentPoints { get; set; }

        [StringLength(512)]
        public string QuestionText { get; set; }

        [StringLength(512)]
        public string Answer1 { get; set; }

        [StringLength(512)]
        public string Answer2 { get; set; }

        [StringLength(512)]
        public string Answer3 { get; set; }

        [StringLength(512)]
        public string Answer4 { get; set; }

        public int CorrectAnswer { get; set; }

        public int StudentAnswer { get; set; }
        public int QuestionID { get; set; }
    }
}