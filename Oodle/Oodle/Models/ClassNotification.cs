namespace Oodle.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ClassNotification")]
    public partial class ClassNotification
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClassNotification()
        {
            HiddenNotifications = new HashSet<HiddenNotification>();
        }

        public int ClassNotificationID { get; set; }

        [Required]
        [StringLength(256)]
        public string Notification { get; set; }

        public DateTime TimePosted { get; set; }

        public int ClassID { get; set; }

        public virtual Class Class { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HiddenNotification> HiddenNotifications { get; set; }
    }
}
