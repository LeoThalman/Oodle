namespace Oodle.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class QuizQuestion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QuizQuestion()
        {
            MultChoiceAnswers = new HashSet<MultChoiceAnswer>();
            StudentAnswers = new HashSet<StudentAnswer>();
        }

        [Key]
        public int QuestionID { get; set; }

        public int QuizID { get; set; }

        public int TypeOfQuestion { get; set; }

        public int Points { get; set; }

        [Required]
        [StringLength(512)]
        public string QuestionText { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MultChoiceAnswer> MultChoiceAnswers { get; set; }

        public virtual Quizze Quizze { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentAnswer> StudentAnswers { get; set; }
    }
}
