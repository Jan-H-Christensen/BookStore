using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFFramework.Model;
using Microsoft.EntityFrameworkCore;

namespace EFFramework.Data
{
    public class BsDbContext : DbContext
    {
        public BsDbContext(DbContextOptions<BsDbContext> options) : base(options) { }
        public BsDbContext() { }

        public virtual DbSet<Author>? usersTables { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}