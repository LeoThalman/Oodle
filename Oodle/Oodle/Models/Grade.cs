namespace Oodle.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Grade
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Grade()
        {
            Documents = new HashSet<Document>();
            StudentQuizzes = new HashSet<StudentQuizze>();
        }

        public int GradeID { get; set; }

        [Column("Grade")]
        public int? Grade1 { get; set; }

        public int? AssignmentID { get; set; }

        public int ClassID { get; set; }

        public int? QuizID { get; set; }

        public int GradeWeight { get; set; }

        public DateTime DateApplied { get; set; }

        public virtual Assignment Assignment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Document> Documents { get; set; }

        public virtual Quizze Quizze { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentQuizze> StudentQuizzes { get; set; }
    }
}
