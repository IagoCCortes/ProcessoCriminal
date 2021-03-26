using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProcedimentoCriminal.Core.Application.Interfaces;
using ProcedimentoCriminal.Core.Domain;
using ProcedimentoCriminal.Core.Domain.Interfaces;
using ProcedimentoCriminal.Investigacao.Infrastructure.Persistence;
using ProcedimentoCriminal.Investigacao.Infrastructure.Services;

namespace ProcedimentoCriminal.Investigacao.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("EfSettings").GetSection("ConnectionString").Value;
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(
                    connectionString,
                    ServerVersion.AutoDetect(connectionString),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IDomainEventService, DomainEventService>();
            services.AddTransient<IDateTime, DateTimeService>();
            return services;
        }
    }
}