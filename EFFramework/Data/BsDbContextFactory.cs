using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFFramework.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EFFramework.Data
{
    public class BsDbContextFactory : IDesignTimeDbContextFactory<BsDbContext>
    {
        public BsDbContext CreateDbContext(string[]? args = null)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BsDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost,1433;Initial Catalog=BookStoreDB;User ID=sa;Password=SuperSecret7!;TrustServerCertificate=True;");

            return new BsDbContext(optionsBuilder.Options);
        }
    }
}
