using Microsoft.EntityFrameworkCore;

namespace mimiAPI.Models
{
    public class MimicContext : DbContext
    {
        public MimicContext(DbContextOptions<MimicContext> options) : base(options)
        {
        }

        public DbSet<Palavra> Palavras { get; set; }
        
    }
}