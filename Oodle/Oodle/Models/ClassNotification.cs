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
        public int ClassNotificationID { get; set; }

        [Required]
        [StringLength(256)]
        public string Notification { get; set; }

        public DateTime TimePosted { get; set; }

        public int ClassID { get; set; }

        public virtual Class Class { get; set; }
    }
}
