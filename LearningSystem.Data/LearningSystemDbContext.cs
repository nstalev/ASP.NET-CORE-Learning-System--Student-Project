﻿
namespace LearningSystem.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class LearningSystemDbContext : IdentityDbContext<User>
    {
        public LearningSystemDbContext(DbContextOptions<LearningSystemDbContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses  { get; set; }

        public DbSet<Article> Articles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
               .Entity<StudentCourse>()
               .HasKey(sc => new { sc.StudentId, sc.CourseId });

            builder
                .Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.Courses)
                .HasForeignKey(sc => sc.StudentId);

            builder
               .Entity<StudentCourse>()
               .HasOne(sc => sc.Course)
               .WithMany(c => c.Students)
               .HasForeignKey(sc => sc.CourseId);

            builder
                .Entity<Course>()
                .HasOne(c => c.Trainer)
                .WithMany(t => t.Trainings)
                .HasForeignKey(c => c.TrainerId);

            builder
                .Entity<Article>()
                .HasOne(a => a.Author)
                .WithMany(a => a.Articles)
                .HasForeignKey(a => a.AuthorId);
            //builder
            //    .Entity<StudentCourse>()
            //    .HasKey(sc => new { sc.StudentId, sc.CourseId });

            //builder
            //    .Entity<StudentCourse>()
            //    .HasOne(sc => sc.Student)
            //    .WithMany(c => c.Courses)
            //    .HasForeignKey(sc => sc.StudentId);

            //builder
            //    .Entity<StudentCourse>()
            //    .HasOne(sc => sc.Course)
            //    .WithMany(c => c.Students)
            //    .HasForeignKey(sc => sc.CourseId);


            //builder
            //    .Entity<Course>()
            //    .HasOne(c => c.Trainer)
            //    .WithMany(s => s.Tranings)
            //    .HasForeignKey(c => c.TrainerId);

            //builder
            //    .Entity<Article>()
            //    .HasOne(a => a.Author)
            //    .WithMany(ar => ar.Articles)
            //    .HasForeignKey(a => a.AuthorId);



            base.OnModelCreating(builder);
          
        }
    }
}
