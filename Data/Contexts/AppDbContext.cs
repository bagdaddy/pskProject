﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Entities;
using TP.Data.EntityTypeConfiguration;

namespace TP.Data.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public AppDbContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new SubjectEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeEntityTypeConfiguration());

            modelBuilder.Entity<EmployeeSubject>()
                .HasKey(t => new { t.EmployeeId, t.SubjectId });

            modelBuilder.Entity<EmployeeSubject>()
                .HasOne(es => es.Employee)
                .WithMany(e => e.LearnedSubjects)
                .HasForeignKey(es => es.EmployeeId);
            modelBuilder.Entity<EmployeeSubject>()
                .HasOne(es => es.Subject)
                .WithMany(s => s.EmployeesWhoLearnedIt)
                .HasForeignKey(es => es.SubjectId);
        }

    }
}