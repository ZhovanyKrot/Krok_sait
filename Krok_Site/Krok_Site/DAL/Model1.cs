namespace Krok_Site.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=KrokConection")
        {
        }

        public virtual DbSet<Departments> Departments { get; set; }
        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<LiabilityStatistics> LiabilityStatistics { get; set; }
        public virtual DbSet<Professors> Professors { get; set; }
        public virtual DbSet<Responsebles> Responsebles { get; set; }
        public virtual DbSet<Students> Students { get; set; }
        public virtual DbSet<Subjects> Subjects { get; set; }
        public virtual DbSet<TimeTables> TimeTables { get; set; }
        public virtual DbSet<WorkPlan> WorkPlan { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Departments>()
                .HasMany(e => e.Groups)
                .WithOptional(e => e.Departments)
                .HasForeignKey(e => e.DepartmentId);

            modelBuilder.Entity<Departments>()
                .HasMany(e => e.LiabilityStatistics)
                .WithOptional(e => e.Departments)
                .HasForeignKey(e => e.DepartmentId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Departments>()
                .HasMany(e => e.Professors)
                .WithOptional(e => e.Departments)
                .HasForeignKey(e => e.DepartmentId);

            modelBuilder.Entity<Departments>()
                .HasMany(e => e.Subjects)
                .WithOptional(e => e.Departments)
                .HasForeignKey(e => e.DepartmentId);

            modelBuilder.Entity<Departments>()
                .HasMany(e => e.TimeTables)
                .WithOptional(e => e.Departments)
                .HasForeignKey(e => e.DepartmentId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Groups>()
                .HasMany(e => e.LiabilityStatistics)
                .WithOptional(e => e.Groups)
                .HasForeignKey(e => e.GroupId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Groups>()
                .HasMany(e => e.Students)
                .WithOptional(e => e.Groups)
                .HasForeignKey(e => e.GroupId);

            modelBuilder.Entity<Groups>()
                .HasMany(e => e.TimeTables)
                .WithOptional(e => e.Groups)
                .HasForeignKey(e => e.GroupId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Groups>()
                .HasMany(e => e.WorkPlan)
                .WithOptional(e => e.Groups)
                .HasForeignKey(e => e.GroupId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Professors>()
                .HasMany(e => e.TimeTables)
                .WithOptional(e => e.Professors)
                .HasForeignKey(e => e.ProfessorId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Responsebles>()
                .HasMany(e => e.LiabilityStatistics)
                .WithOptional(e => e.Responsebles)
                .HasForeignKey(e => e.ResponsebleId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Students>()
                .HasMany(e => e.LiabilityStatistics)
                .WithOptional(e => e.Students)
                .HasForeignKey(e => e.StudentId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Students>()
                .HasMany(e => e.Responsebles)
                .WithOptional(e => e.Students)
                .HasForeignKey(e => e.StudentId);

            modelBuilder.Entity<Students>()
                .HasMany(e => e.WorkPlan)
                .WithOptional(e => e.Students)
                .HasForeignKey(e => e.StudentId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Subjects>()
                .HasMany(e => e.Responsebles)
                .WithOptional(e => e.Subjects)
                .HasForeignKey(e => e.SubjectId);

            modelBuilder.Entity<Subjects>()
                .HasMany(e => e.TimeTables)
                .WithOptional(e => e.Subjects)
                .HasForeignKey(e => e.SubjectId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Subjects>()
                .HasMany(e => e.WorkPlan)
                .WithOptional(e => e.Subjects)
                .HasForeignKey(e => e.SubjectId);

            modelBuilder.Entity<WorkPlan>()
                .Property(e => e.Semester)
                .IsFixedLength();
        }
    }
}
