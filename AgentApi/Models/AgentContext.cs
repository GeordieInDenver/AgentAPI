using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AgentApi.Models
{
    public class AgentContext : DbContext
    {

        public AgentContext(DbContextOptions<AgentContext> options) : base(options)
        {
        }

        public DbSet<Agent> Agents { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<ContactPhone> Phones { get; set; }

        public DbSet<PersonName> Names { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactPhone>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();


            modelBuilder.Entity<PersonName>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            var splitStringConverter = new ValueConverter<List<string>, string>(v => string.Join(",", v), v => v.Split(new[] { ',' }).ToList());
            modelBuilder.Entity<Customer>().Property(p => p.Tags).HasConversion(splitStringConverter);
        }

    }
}
