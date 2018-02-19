namespace Oodle.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Grade
    {
        [Key]
        public int GradesID { get; set; }

        public int UsersID { get; set; }

        public int AssignmentID { get; set; }

        [StringLength(64)]
        public string Grader { get; set; }

        [StringLength(256)]
        public string Comment { get; set; }

        [Column("Grade")]
        public int Grade1 { get; set; }

        public virtual Assignment Assignment { get; set; }

        public virtual User User { get; set; }
    }
}
