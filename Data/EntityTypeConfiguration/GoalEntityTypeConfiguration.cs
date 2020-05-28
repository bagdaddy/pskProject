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
        }
    }
}
