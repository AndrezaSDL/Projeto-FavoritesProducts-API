using Microsoft.EntityFrameworkCore;
using FavoritesProductsAPI.Models;

namespace FavoritesProductsAPI.Data
{
    public class FavoritesProductsContext : DbContext
    {
        public FavoritesProductsContext(DbContextOptions<FavoritesProductsContext> opt) : base (opt){ }

        public DbSet<Client> Clients { get; set; }
        public DbSet<FavoriteProduct> FavoritesProducts { get; set;     }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<FavoriteProduct>().HasKey(t => new { t.ProductId, t.ClientId });
        }
    }
}
