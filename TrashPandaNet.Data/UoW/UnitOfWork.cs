using Microsoft.EntityFrameworkCore.Infrastructure;
using TrashPandaNet.Data.DataBase;
using TrashPandaNet.Data.Repositories;

namespace TrashPandaNet.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Dispose() { }

        public IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = true)
            where TEntity : class
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<Type, object>();
            }

            if (hasCustomRepository)
            {
                var customRepository = _appDbContext.GetService<IRepository<TEntity>>();

                if (customRepository != null)
                {
                    return customRepository;
                }
            }

            var type = typeof(TEntity);

            if (!_repositories.ContainsKey(type)) 
            {
                _repositories[type] = new BaseRepository<TEntity>(_appDbContext);
            }

            return (IRepository<TEntity>)_repositories[type];
        }

        public int SaveChanges(bool ensureAutoHistory = false)
        {
            throw new NotImplementedException();
        }
    }
}
