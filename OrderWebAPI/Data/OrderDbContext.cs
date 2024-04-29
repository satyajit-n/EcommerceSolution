using Microsoft.EntityFrameworkCore;
using OrderWebAPI.Models.Domains;

namespace OrderWebAPI.Data
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }

        public DbSet<ORDER> ORDERS { get; set; }
    }
}
