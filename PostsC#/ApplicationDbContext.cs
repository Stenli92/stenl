using Microsoft.EntityFrameworkCore;

namespace PostsC_
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Post> repository { get; set; }

    }
}
