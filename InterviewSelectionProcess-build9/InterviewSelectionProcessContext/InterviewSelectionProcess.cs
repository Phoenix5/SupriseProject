namespace Interview.Domain.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Interview.Domain.Model;

    public partial class InterviewSelectionProcessContext : DbContext
    {
        public InterviewSelectionProcessContext()
            : base("name=InterviewSelectionProcess")
        {
        }

        public virtual DbSet<Candidate> Candidates { get; set; }
        public virtual DbSet<CandidateStatu> CandidateStatus { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<InterviewSchedule> InterviewSchedules { get; set; }
        public virtual DbSet<InterviewStatu> InterviewStatus { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<SelectionStatu> SelectionStatus { get; set; }
        public virtual DbSet<Technology> Technologies { get; set; }
        public virtual DbSet<TechStack> TechStacks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Candidate>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Candidate>()
                .Property(e => e.MiddleName)
                .IsUnicode(false);

            modelBuilder.Entity<Candidate>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Candidate>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Candidate>()
                .Property(e => e.MobileNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Candidate>()
                .Property(e => e.EmailAddress)
                .IsUnicode(false);

            modelBuilder.Entity<Candidate>()
                .HasMany(e => e.CandidateStatus)
                .WithRequired(e => e.Candidate)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Candidate>()
                .HasMany(e => e.InterviewSchedules)
                .WithRequired(e => e.Candidate)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.EmailID)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Role)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.InterviewSchedules)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<InterviewSchedule>()
                .Property(e => e.Note)
                .IsUnicode(false);

            modelBuilder.Entity<InterviewStatu>()
                .Property(e => e.InterviewStatusName)
                .IsUnicode(false);

            modelBuilder.Entity<InterviewStatu>()
                .HasMany(e => e.CandidateStatus)
                .WithRequired(e => e.InterviewStatu)
                .HasForeignKey(e => e.InterviewStatus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Post>()
                .Property(e => e.PostName)
                .IsUnicode(false);

            modelBuilder.Entity<Post>()
                .HasMany(e => e.Candidates)
                .WithRequired(e => e.Post)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SelectionStatu>()
                .Property(e => e.SelectionStatusName)
                .IsUnicode(false);

            modelBuilder.Entity<SelectionStatu>()
                .HasMany(e => e.CandidateStatus)
                .WithRequired(e => e.SelectionStatu)
                .HasForeignKey(e => e.SelectionStatus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Technology>()
                .Property(e => e.Technology1)
                .IsUnicode(false);

            modelBuilder.Entity<Technology>()
                .HasMany(e => e.Candidates)
                .WithRequired(e => e.Technology)
                .HasForeignKey(e => e.TechnologiesWorkedOnID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TechStack>()
                .Property(e => e.CandidateID)
                .IsUnicode(false);
        }
    }
}
