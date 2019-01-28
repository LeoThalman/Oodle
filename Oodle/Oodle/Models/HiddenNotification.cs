namespace Oodle.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HiddenNotification")]
    public partial class HiddenNotification
    {
        public int HiddenNotificationID { get; set; }

        public int ClassNotificationID { get; set; }

        public int ClassID { get; set; }

        public int UsersID { get; set; }

        public virtual ClassNotification ClassNotification { get; set; }

        public virtual User User { get; set; }
    }
}
