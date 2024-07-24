using App.Domain.Entities;
using App.Infrastructure.Interfaces.Context;
using App.Infrastructure.Persistence.Seed;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace App.Infrastructure.DBContext
{
    public class dbContext : DbContext, IApplicationSqlDbContext
    {
        #region DBSet
        public DbSet<Branches> Branches;
        #endregion


        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConfiguration _configuration;

        public dbContext(DbContextOptions<dbContext> options, IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(options)
        {
            this.httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IApplicationSqlDbContext).Assembly);

        }
        public IDbConnection Connection => throw new NotImplementedException();
    }
}
