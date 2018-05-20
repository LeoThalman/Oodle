namespace Oodle.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Assignment> Assignments { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<ClassNotification> ClassNotifications { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<HiddenNotification> HiddenNotifications { get; set; }
        public virtual DbSet<MultChoiceAnswer> MultChoiceAnswers { get; set; }
        public virtual DbSet<Notes> Notes { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuizQuestion> QuizQuestions { get; set; }
        public virtual DbSet<Quizze> Quizzes { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<StudentAnswer> StudentAnswers { get; set; }
        public virtual DbSet<StudentQuizze> StudentQuizzes { get; set; }
        public virtual DbSet<Tasks> Tasks { get; set; }
        public virtual DbSet<UserRoleClass> UserRoleClasses { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.IdentityID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Assignment>()
                .HasMany(e => e.Documents)
                .WithRequired(e => e.Assignment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Assignment>()
                .HasMany(e => e.Grades)
                .WithRequired(e => e.Assignment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Assignment>()
                .HasMany(e => e.Questions)
                .WithRequired(e => e.Assignment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Class>()
                .HasMany(e => e.Documents)
                .WithRequired(e => e.Class)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Class>()
                .HasMany(e => e.Notes)
                .WithRequired(e => e.Class)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Class>()
                .HasMany(e => e.Tasks)
                .WithRequired(e => e.Class)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<QuizQuestion>()
                .HasMany(e => e.StudentAnswers)
                .WithRequired(e => e.QuizQuestion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Classes)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Documents)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Grades)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserRoleClasses)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
