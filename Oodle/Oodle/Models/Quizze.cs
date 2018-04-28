namespace Oodle.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Quizze
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Quizze()
        {
            QuizQuestions = new HashSet<QuizQuestion>();
        }

        [Key]
        public int QuizID { get; set; }

        [Required]
        [StringLength(256)]
        public string QuizName { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int ClassID { get; set; }

        public bool IsHidden { get; set; }

        public int? TotalPoints { get; set; }

        public virtual Class Class { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuizQuestion> QuizQuestions { get; set; }
    }
}
