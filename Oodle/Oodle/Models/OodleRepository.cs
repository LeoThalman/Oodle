using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Oodle.Models
{
    public class OodleRepository : IOodleRepository
    {
        private Model1 db = new Model1();
        public DbSet<User> Users { get; }
        public DbSet<UserRoleClass> UserRoleClasses { get; }
        public DbSet<Class> Classes { get; }

        public OodleRepository()
        {
            Users = db.Users;
            UserRoleClasses = db.UserRoleClasses;
            Classes = db.Classes;
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}