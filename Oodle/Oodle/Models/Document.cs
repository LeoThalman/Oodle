namespace Oodle.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Document
    {
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Required]
        [StringLength(250)]
        public string ContentType { get; set; }

        [Required]
        public byte[] Data { get; set; }

        [Required]
        public int ClassID { get; set; }

        [Required]
        public int AssignmentID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public int Grade { get; set; }

        [Required]
        public DateTime? Date { get; set; }


        public virtual Assignment Assignment { get; set; }

        public virtual Class Class { get; set; }

        public virtual User User { get; set; }
    }
}
