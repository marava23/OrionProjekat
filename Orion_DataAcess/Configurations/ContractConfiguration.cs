using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orion_DataAcess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orion_DataAcess.Configurations
{
    public class ContractConfiguration : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.Property(x => x.Date).IsRequired();
            builder.Property(x => x.Username).IsRequired();
            builder.Property(x => x.Duration).IsRequired();
            builder.Property(x => x.Discount).IsRequired(false);
            builder.Property(x => x.FreeMonths).IsRequired(false);
            builder.Property(x => x.Status).IsRequired();

            builder.HasMany(x => x.ContractEdits)
                .WithOne(x => x.Contract)
                .HasForeignKey(x => x.ContractId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.ContractPackages)
                .WithOne(x => x.Contract)
                .HasForeignKey(x => x.ContractId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
