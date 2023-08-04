using Microsoft.EntityFrameworkCore;

namespace Davies_Forum_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public Microsoft.EntityFrameworkCore.DbSet<Forum> ForumEntries { get; set; }
    }
}
