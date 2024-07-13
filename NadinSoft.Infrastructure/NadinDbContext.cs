using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NadinSoft.Domain.Entities;

namespace NadinSoft.Infrastructure
{
    public class NadinDbContext : IdentityDbContext
    {
        public NadinDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.ProduceDate)
                .IsUnique();
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.ManufactureEmail)
                .IsUnique();
        }
    }
}