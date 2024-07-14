using Microsoft.Extensions.DependencyInjection;
using TrashPandaNet.Data.Repositories;
using TrashPandaNet.Data.UoW;

namespace TrashPandaNet.Data.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection AddCustomRepository<TEntity, IRepository>(this IServiceCollection services)
            where TEntity : class
            where IRepository : class, IRepository<TEntity>
        {
            services.AddScoped<IRepository<TEntity>, IRepository>();

            return services;
        }
    }
}
