using Microsoft.EntityFrameworkCore;
using Orion_DataAcess.Configurations;
using Orion_DataAcess.Entities;
using System;

namespace Orion_DataAcess
{
    public class OrionContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ContractConfiguration());
            modelBuilder.ApplyConfiguration(new PackageConfiguration());
            modelBuilder.ApplyConfiguration(new ContractEditConfiguration());

            modelBuilder.Entity<ContractPackage>().HasKey(x => new { x.ContractId, x.PackageId });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-AUKHI58;Initial Catalog=Orion;Integrated Security=True");
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<ContractEdit> ContractEdits { get; set; }
        public DbSet<ContractPackage> contractPackages { get; set; }
        public DbSet<Package> Packages { get; set; }
    }
}
