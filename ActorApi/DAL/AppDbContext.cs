using ActorApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ActorApi.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Actor> Actors { get; set; }
    }
}
