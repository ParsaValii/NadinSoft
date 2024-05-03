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
            optionsBuilder
                .UseSqlServer(
                    "Server=localhost;Database=NadinDB;Integrated Security=True;TrustServerCertificate=True;"
                );
        }
        public NadinDbContext(DbContextOptions options) : base(options) { }
        public NadinDbContext(DbContextOptions options, Action disposeAction) : this(options)
        {
            _disposeAction = disposeAction;
        }

        public DbSet<Product> Products { get; set; }

        public override void Dispose()
        {
            _disposeAction?.Invoke();
        }
    }
}