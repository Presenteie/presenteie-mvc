using Microsoft.EntityFrameworkCore;
using Presenteie.Models;

namespace Presenteie
{
    public class PresenteieContext: DbContext
    {
        public DbSet<User> Users { get; set; }

        public PresenteieContext(DbContextOptions options) : base(options)
        {
        }
    }
}