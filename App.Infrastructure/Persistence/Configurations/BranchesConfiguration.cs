using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Persistence.Configurations
{
    public class BranchesConfiguration : IEntityTypeConfiguration<Branches>
    {
        public void Configure(EntityTypeBuilder<Branches> builder)
        {
            builder.ToTable(nameof(Branches));
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Title).HasMaxLength(200);
            builder.Property(c => c.ManagerName).HasMaxLength(250);
        }
    }
}
