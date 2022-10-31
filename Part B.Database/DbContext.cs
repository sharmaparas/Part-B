using Microsoft.EntityFrameworkCore;
using Part_B.Database.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Part_B.Database
{
    public class PartBDbContext : DbContext
    {
        public DbSet<Glossary> Glossary { get; set; }

        public string DbPath { get; }

        public PartBDbContext(DbContextOptions<PartBDbContext> options)
        : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Glossary>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Glossary>()
              .HasIndex(x => x.Term);
        }
    }
}

