using App.Infrastructure.Interfaces.Context;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Persistence.Seed
{
    public class OnModelCreatingService
    {
        public OnModelCreatingService( )
        {
        }
        public void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IApplicationSqlDbContext).Assembly);


        }
    }
}
