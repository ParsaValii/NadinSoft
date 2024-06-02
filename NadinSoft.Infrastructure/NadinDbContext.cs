using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NadinSoft.Domain.Entities;

namespace NadinSoft.Infrastructure
{
    public class NadinDbContext : IdentityDbContext
    {
        readonly Action _disposeAction;
        public NadinDbContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // for Test , You Should Comment This Part
            optionsBuilder
                .UseSqlServer(
                    "Server=localhost;Database=NadinDB;Integrated Security=True;TrustServerCertificate=True;"
                );
            // for Test , You Should Comment This Part
        }
        public NadinDbContext(DbContextOptions options) : base(options) { }
        public NadinDbContext(DbContextOptions options, Action disposeAction) : this(options)
        {
            _disposeAction = disposeAction;
        }

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

        public override void Dispose()
        {
            _disposeAction?.Invoke();
        }
    }
}