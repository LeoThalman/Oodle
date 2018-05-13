using System;
using System.ComponentModel.DataAnnotations;

namespace Oodle.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Assignment")]
    public partial class Assignment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Assignment()
        {
            Documents = new HashSet<Document>();
            Grades = new HashSet<Grade>();
            Questions = new HashSet<Question>();
        }

        public int AssignmentID { get; set; }

        public int ClassID { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [StringLength(512)]
        public string Description { get; set; }

        public DateTime? StartDate { get; set; }

        [MyDate(ErrorMessage = "Invalid date")]
        public DateTime? DueDate { get; set; }

        public int Weight { get; set; }

        public virtual Class Class { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Document> Documents { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Grade> Grades { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Question> Questions { get; set; }
    }
}

public class MyDateAttribute : ValidationAttribute
{
    public override bool IsValid(object value)// Return a boolean value: true == IsValid, false != IsValid
    {
        DateTime d = Convert.ToDateTime(value);
        return d >= DateTime.Now; //Dates Greater than or equal to today are valid (true)
    }
}