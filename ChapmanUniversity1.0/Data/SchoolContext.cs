using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChapmanUniversity1._0.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ChapmanUniversity1._0.Data
{
    public class SchoolContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentSemesterEnrollment> StudentCourseEnrollments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Faculty> FacultyMembers { get; set; }
        public DbSet<Semester> Semesters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("Courses");
            modelBuilder.Entity<StudentSemesterEnrollment>().ToTable("StudentEnrollments");
            modelBuilder.Entity<Student>().ToTable("Students");
            modelBuilder.Entity<Faculty>().ToTable("FacultyMembers");
            modelBuilder.Entity<Semester>().ToTable("Semesters");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ChapmanUniversityDb", providerOptions => providerOptions.CommandTimeout(60))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking); 
        }

    }
}