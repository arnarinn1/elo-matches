using Microsoft.EntityFrameworkCore;

namespace EloMatches.Query.Persistence
{
    public class EloMatchesQueryContext : DbContext
    {
        public EloMatchesQueryContext(DbContextOptions<EloMatchesQueryContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}