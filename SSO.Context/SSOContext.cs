using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace SSO.Context
{
    public class SSOContext:DbContext
    {
        public SSOContext(DbContextOptions option):base(option)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=PRCNMG1L0311;initial catalog=SSOContext;user Id=sa;password=Root@admin");
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<ApiResource> APIResources { get; set; }
        public DbSet<IdentityResource> ApiResource { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityResource>().Ignore(p => p.UserClaims).HasKey(p => p.Name);
            modelBuilder.Entity<ApiResource>().Ignore(p => p.UserClaims).HasKey(p => p.Name);
            modelBuilder.Entity<Scope>().Ignore(p => p.UserClaims).HasKey(p => p.Name);
            modelBuilder.Entity<Secret>().HasKey(p => p.Value);
            base.OnModelCreating(modelBuilder);
        }
    }


    public class Person
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public interface IPersonRepository
    {
        IEnumerable<Person> GetList();
    }


    public class PersonRepository : IPersonRepository
    {
        private readonly SSOContext Context;
        public PersonRepository(SSOContext context)
        {
            Context = context;
        }
        public IEnumerable<Person> GetList()
        {
            return Context.Persons.Where(p => true).AsEnumerable();
        }
    }
}
