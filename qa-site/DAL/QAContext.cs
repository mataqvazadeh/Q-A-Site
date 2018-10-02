namespace QASite
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;

    public partial class QAContext : IdentityDbContext<User>
    {
        public QAContext()
            : base("name=QAContext")
        {
        }

        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        //public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Vote> Votes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>()
                .HasMany(e => e.Answers)
                .WithRequired(e => e.Question)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Answers)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.AuthorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.AuthorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Questions)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.AuthorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Votes)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.VoterId)
                .WillCascadeOnDelete(false);
        }
    }
}
