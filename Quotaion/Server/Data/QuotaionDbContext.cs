using Microsoft.EntityFrameworkCore;
using Quotaion.Shared.Models;

namespace Quotaion.Server.Data
{
    public class QuotaionDbContext:DbContext
    {
        public QuotaionDbContext(DbContextOptions<QuotaionDbContext> options) : base(options)
        {
            
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OfferNumber> OfferNumbers { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<LogClass> LogClasses { get; set; }
    }
}
