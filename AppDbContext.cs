using Microsoft.EntityFrameworkCore;
using UrlShortener.Models;

namespace UrlShortener
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public virtual DbSet<UrlManagement> Urls { get; set; }
    }
}
