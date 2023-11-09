using EcommerceApi.Core.Configuration;
using EcommerceApi.Core.Entities.Concrete;
using EcommerceApi.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.DataAccess.Concrete.EntityFramework
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DatabaseConfiguration.ConnectionString);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<WishList> WishList { get; set; }
        public DbSet<Spacification> Spacifications { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AppUserRole> AppUsersRoles { get; set; }
    }
}
