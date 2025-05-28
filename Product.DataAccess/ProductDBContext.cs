using Microsoft.EntityFrameworkCore;
using Product.DataAccess.Models;

namespace Product.DataAccess
{
    public class ProductDBContext : DbContext
    {
        public DbSet<MyProduct> MyProducts { get; set; }

        public ProductDBContext(DbContextOptions<ProductDBContext> options) : base (options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
