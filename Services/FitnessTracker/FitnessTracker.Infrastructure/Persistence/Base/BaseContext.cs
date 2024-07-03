using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection;

namespace FitnessTracker.Infrastructure.Persistence.Base
{
    public class BaseContext<TDbContext> : DbContext where TDbContext : DbContext
    {
               protected IDbContextTransaction _currentTransaction;
        public BaseContext(DbContextOptions<TDbContext> options): base(options) { }
       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public DatabaseFacade ContextDatabase => Database;

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            int result = await base.SaveChangesAsync(cancellationToken);

            return true;
        }       
    }
}
