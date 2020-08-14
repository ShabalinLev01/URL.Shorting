using Microsoft.EntityFrameworkCore;

namespace URL.Shorting.Models
{
    public class UrlContext : DbContext
    {
        public DbSet<Urls> UrlTable { get; set; }
        
        public UrlContext(DbContextOptions<UrlContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}