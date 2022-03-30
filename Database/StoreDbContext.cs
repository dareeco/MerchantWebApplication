using MerchantWebApplication.Models;
using Microsoft.EntityFrameworkCore;
namespace MerchantWebApplication.Database
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) :base(options)
        {

        }
        public DbSet<Store> Stores{ get; set; }
    }
}
