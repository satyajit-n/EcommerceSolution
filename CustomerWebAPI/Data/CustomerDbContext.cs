using CustomerWebAPI.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace CustomerWebAPI.Data
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options) { }

        public DbSet<CUSTOMER> CUSTOMERS { get; set; }
    }
}
