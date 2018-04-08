namespace Oodle.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Classes = new HashSet<Class>();
            Documents = new HashSet<Document>();
            Grades = new HashSet<Grade>();
            UserRoleClasses = new HashSet<UserRoleClass>();
        }

        [Key]
        public int UsersID { get; set; }

        [Required]
        [StringLength(128)]
        public string IdentityID { get; set; }

        [StringLength(64)]
        public string FirstName { get; set; }

        [StringLength(128)]
        public string Lastname { get; set; }

        [Required]
        [StringLength(128)]
        public string Email { get; set; }

        public byte[] Icon { get; set; }

        [StringLength(512)]
        public string Bio { get; set; }

        [Required]
        [StringLength(128)]
        public string UserName { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Class> Classes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Document> Documents { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Grade> Grades { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserRoleClass> UserRoleClasses { get; set; }
    }
}
