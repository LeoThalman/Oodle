using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Oodle.Models
{
    interface IOodleRepository
    {
        DbSet<User> Users { get; }
        DbSet<UserRoleClass> UserRoleClasses { get; }
        DbSet<Class> Classes { get; }

        void SaveChanges();
    }
}
