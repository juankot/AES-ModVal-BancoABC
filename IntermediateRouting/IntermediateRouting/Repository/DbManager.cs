using Microsoft.EntityFrameworkCore;

namespace IntermediateRouting.Repository
{
    public class DbManager : DbContext
    {
        public DbManager(DbContextOptions<DbManager> outerConfig) : base(outerConfig)
        {
        }

        public DbSet<ConfigurationProviderService> Providers { get; set; }
    }
}
