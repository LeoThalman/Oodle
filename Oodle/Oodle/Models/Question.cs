namespace Oodle.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Question
    {
        [Key]
        public int QuestionsID { get; set; }

        public int AssignmentID { get; set; }

        [Required]
        [StringLength(500)]
        public string Text { get; set; }

        public int Weight { get; set; }

        public int Answer { get; set; }

        public bool Flagged { get; set; }

        public virtual Assignment Assignment { get; set; }
    }
}
