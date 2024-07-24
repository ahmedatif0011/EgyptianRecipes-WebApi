using App.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Persistence.UnitOfWork
{
    public class DbFactory : IDisposable
    {
        private bool _disposed;
        private  Func<dbContext> _instanceFunc;
        private dbContext _dbContext;
        public dbContext DbContext => _dbContext ?? (_dbContext = _instanceFunc.Invoke());

        public DbFactory(Func<dbContext> dbContextFactory,dbContext dbContext)
        {
            _instanceFunc = dbContextFactory;
            _dbContext = dbContext;
        }

        public void Dispose()
        {
            if (!_disposed && _dbContext != null)
            {
                _disposed = true;
               _dbContext.Dispose();
            }
        }
    }
}
