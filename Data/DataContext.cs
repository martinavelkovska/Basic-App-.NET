using Microsoft.EntityFrameworkCore;
using Yt_App.Entities;

namespace Yt_App.Data
{
    //to gete some data and store some data in database
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<SuperHero> SuperHeroes { get; set; } //name of the table that reperesent superhero entry
    }
}
