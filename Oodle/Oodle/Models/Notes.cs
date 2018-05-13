namespace Oodle.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Notes
    {
        [Key]
        public int NotesID { get; set; }

        public int ClassID { get; set; }

        [StringLength(512)]
        public string Description { get; set; }

        public virtual Class Class { get; set; }
    }
}
