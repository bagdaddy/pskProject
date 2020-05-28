using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TP.Data.Entities;

namespace TP.Data.EntityTypeConfiguration
{
    public class GoalEntityTypeConfiguration : IEntityTypeConfiguration<Goal>
    {
        public void Configure(EntityTypeBuilder<Goal> builder)
        {
            builder.ToTable(nameof(Goal));
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Employee).WithMany(x => x.Goals).HasForeignKey(x => x.EmployeeId);
            builder.HasOne(x => x.Subject).WithMany(x => x.Goals).HasForeignKey(x => x.SubjectId);
        }
    }
}
