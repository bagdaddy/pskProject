using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TP.Data.Entities;

namespace TP.Data.EntityTypeConfiguration
{
    public class DayEntityTypeConfiguration : IEntityTypeConfiguration<Day>
    {
        public void Configure(EntityTypeBuilder<Day> builder)
        {
            builder.ToTable(nameof(Day));
            builder.HasKey(x => x.Id);
        }
    }
}
