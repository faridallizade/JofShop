using JofShop.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JofShop.DAL
{
    public class AppDbcontext : IdentityDbContext<AppUser>
    {
        public AppDbcontext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Fruit> fruits { get; set; }
    }
}
