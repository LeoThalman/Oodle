namespace Oodle.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MultChoiceAnswer
    {
        [Key]
        public int AnswerID { get; set; }

        public int QuestionID { get; set; }

        [Required]
        [StringLength(512)]
        public string Answer1 { get; set; }

        [StringLength(512)]
        public string Answer2 { get; set; }

        [StringLength(512)]
        public string Answer3 { get; set; }

        [StringLength(512)]
        public string Answer4 { get; set; }

        public int CorrectAnswer { get; set; }

        public virtual QuizQuestion QuizQuestion { get; set; }
    }
}
