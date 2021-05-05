using Microsoft.EntityFrameworkCore;
using Presenteie.Models;

namespace Presenteie
{
    public class PresenteieContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        
        public DbSet<List> Lists { get; set; }
        
        public DbSet<Item> Items { get; set; }
        public PresenteieContext(DbContextOptions options) : base(options)
        {
        }
    }
}