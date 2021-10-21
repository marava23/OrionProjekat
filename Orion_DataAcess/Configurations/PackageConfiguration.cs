using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orion_DataAcess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orion_DataAcess.Configurations
{
    public class PackageConfiguration : IEntityTypeConfiguration<Package>
    {
        public void Configure(EntityTypeBuilder<Package> builder)
        {
            builder.HasIndex(x => x.Name).IsUnique();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Price).IsRequired();

            builder.HasOne(x => x.Category)
                .WithMany(x => x.Packages)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.ContractPackages)
                .WithOne(x => x.Package)
                .HasForeignKey(x => x.PackageId);

        }
    }
}
