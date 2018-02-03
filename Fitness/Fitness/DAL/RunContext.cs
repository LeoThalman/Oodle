namespace Fitness.DAL
{
    using System;
    using Fitness.Models;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class RunContext : DbContext
    {
        public RunContext()
            : base("name=RunContext")
        {
        }

        public virtual DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
