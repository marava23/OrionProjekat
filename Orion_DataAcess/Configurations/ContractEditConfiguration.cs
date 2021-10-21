using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orion_DataAcess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orion_DataAcess.Configurations
{
    public class ContractEditConfiguration : IEntityTypeConfiguration<ContractEdit>
    {
        public void Configure(EntityTypeBuilder<ContractEdit> builder)
        {
            builder.Property(x => x.Date).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.TotalPrice).IsRequired();

            builder.HasOne(x => x.Contract)
                .WithMany(x => x.ContractEdits)
                .HasForeignKey(x => x.ContractId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
