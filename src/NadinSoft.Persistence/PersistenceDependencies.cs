using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NadinSoft.Application.Abstractions.Data;
using NadinSoft.Application.Feature.Data;
using NadinSoft.Domain.Abstractions.Persistence.Data;
using NadinSoft.Domain.Abstractions.Persistence.Repositories;
using NadinSoft.Persistence.Data;
using NadinSoft.Persistence.Profiles;
using NadinSoft.Persistence.Repositories;
using System.Reflection;

namespace NadinSoft.Persistence
{
    public static class PersistenceDependencies
    {
        public static IServiceCollection AddPersistenceDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("MSSqlServer"));
            });
            services.AddAutoMapper(Assembly.GetAssembly(typeof(IProfile)));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddSingleton<IDapperService, DapperService>();
            return services;
        }
    }
}
