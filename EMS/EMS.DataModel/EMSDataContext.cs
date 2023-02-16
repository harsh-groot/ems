using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace EMS.DataModel
{
    public class EMSDataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public EMSDataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // in memory database used for simplicity, change to a real db for production applications
            options.UseInMemoryDatabase("EMSDb");
        }

        public DbSet<Employees> Employees { get; set; }
    }

}