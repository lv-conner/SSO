using Microsoft.EntityFrameworkCore;
using SSO.Domain;
using System;

namespace SSO.ApplicatonContext
{
    public class OidcContext:DbContext
    {
        public OidcContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<OidcClient> Cliens { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
