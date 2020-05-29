using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Entities;

namespace TP.Data.EntityTypeConfiguration
{
    public class InviteEntityTypeConfiguration : IEntityTypeConfiguration<Invite>
    {
        public void Configure(EntityTypeBuilder<Invite> builder)
        {
            builder.ToTable(nameof(Invite));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Email);
            builder.HasOne(x => x.Employee).WithMany(x => x.Invites).HasForeignKey(x => x.EmployeeId);
        }
    }
}
