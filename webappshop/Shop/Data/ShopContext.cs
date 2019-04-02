using Microsoft.EntityFrameworkCore;

namespace Shop.Models
{
    public class ShopContext : DbContext
    {
        public ShopContext (DbContextOptions<ShopContext> options)
            : base(options)
        { }

        public DbSet<Foodstuffs> Foodstuffs { get; set; }
    }
}
