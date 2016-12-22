using Microsoft.EntityFrameworkCore;
using SharpSQlite.Model;

namespace SharpSQlite
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=MyDatabase.db");
        }
    }
}