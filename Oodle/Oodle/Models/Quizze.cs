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
        [Display(Name = "Quiz Name")]
        public string QuizName { get; set; }

        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        [Display(Name = "End Time")]
        public DateTime EndTime { get; set; }

        public int ClassID { get; set; }

        [Display(Name = "Hide Quiz from Students")]
        public bool IsHidden { get; set; }

        public int? TotalPoints { get; set; }

        public virtual Class Class { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuizQuestion> QuizQuestions { get; set; }
    }
}
