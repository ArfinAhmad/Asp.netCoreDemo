using Asp.netCoreDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace Asp.netCoreDemo.DataContext
{ 
    public class DapperContext : DbContext

    {
      

        public DapperContext(DbContextOptions<DapperContext> options)
            : base(options)
        {
        }
        public DbSet<Customer> customer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //  base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>().HasKey(s => s.CustomerId);
        }

    }
}
