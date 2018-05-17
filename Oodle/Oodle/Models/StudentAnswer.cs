namespace Oodle.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class StudentAnswer
    {
        [Key]
        public int SQAID { get; set; }

        public int SQID { get; set; }

        public int QuestionID { get; set; }

        public int AnswerNumber { get; set; }

        public int StudentPoints { get; set; }

        public virtual QuizQuestion QuizQuestion { get; set; }

        public virtual StudentQuizze StudentQuizze { get; set; }
    }
}
