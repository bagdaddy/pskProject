using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Entities;
using TP.Data.EntityTypeConfiguration;

namespace TP.Data.Contexts
{
    public class SubjectContext : DbContext
    {
        public DbSet<Subject> Subjects { get; set; }
        public SubjectContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new SubjectEntityTypeConfiguration());
        }

    }
}
