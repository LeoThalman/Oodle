namespace Oodle.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class StudentQuizze
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StudentQuizze()
        {
            StudentAnswers = new HashSet<StudentAnswer>();
        }

        [Key]
        public int SQID { get; set; }

        public int QuizID { get; set; }

        public int UserID { get; set; }

        public int TotalPoints { get; set; }

        public bool CanReview { get; set; }

        public virtual Quizze Quizze { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentAnswer> StudentAnswers { get; set; }
    }
}
