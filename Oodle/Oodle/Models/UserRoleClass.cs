namespace Oodle.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserRoleClass")]
    public partial class UserRoleClass
    {
        public int UserRoleClassID { get; set; }

        public int UsersID { get; set; }

        public int RoleID { get; set; }

        public int ClassID { get; set; }

        public virtual Class Class { get; set; }

        public virtual Role Role { get; set; }

        public virtual User User { get; set; }
    }
}
