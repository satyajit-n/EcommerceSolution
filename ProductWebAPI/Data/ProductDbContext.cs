using Microsoft.EntityFrameworkCore;
using ProductWebAPI.Models.Domains;

namespace ProductWebAPI.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }

        public DbSet<PRODUCT> PRODUCTS { get; set; }
    }
}
