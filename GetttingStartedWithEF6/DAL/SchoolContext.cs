﻿using GetttingStartedWithEF6.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace GetttingStartedWithEF6.DAL
{
    public class SchoolContext : DbContext
    {
        public SchoolContext() : base("SchoolContext") { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        public DbSet<Person> Person { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Course>()
            .HasMany(c => c.Instructors).WithMany(i => i.Courses)
            .Map(t => t.MapLeftKey("CourseID")
            .MapRightKey("InstructorID")
            .ToTable("CourseInstructor"));

            modelBuilder.Entity<Department>().MapToStoredProcedures();

            modelBuilder.Entity<Department>().Property(x => x.RowVersion).IsConcurrencyToken();
        }
    }
}