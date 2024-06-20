using Microsoft.EntityFrameworkCore;
using UdemyWebApi.Models.Domain;

namespace UdemyWebApi.Data
{
    public class DataBContext : DbContext
    {
        public DataBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
    }
}
   