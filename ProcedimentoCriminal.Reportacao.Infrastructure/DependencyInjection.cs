using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProcedimentoCriminal.Core.Application.Interfaces;
using ProcedimentoCriminal.Core.Domain;
using ProcedimentoCriminal.Reportacao.Application.Interfaces.ReadRepositories;
using ProcedimentoCriminal.Reportacao.Domain.Interfaces;
using ProcedimentoCriminal.Reportacao.Infrastructure.Persistence;
using ProcedimentoCriminal.Reportacao.Infrastructure.Persistence.Interfaces;
using ProcedimentoCriminal.Reportacao.Infrastructure.Persistence.ReadRepositories;
using ProcedimentoCriminal.Reportacao.Infrastructure.Persistence.WriteRepositories;
using ProcedimentoCriminal.Reportacao.Infrastructure.Services;

namespace ProcedimentoCriminal.Reportacao.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("DapperSettings").GetSection("ConnectionString").Value;
            services.AddSingleton<IDapperConnectionFactory>(new DapperConnectionFactory(connectionString));
            services.AddScoped<IOcorrenciaRepository>(provider => new OcorrenciaRepository(
                provider.GetRequiredService<IDapperConnectionFactory>(),
                provider.GetRequiredService<IDomainEventService>(),
                provider.GetRequiredService<ICurrentUserService>(),
                provider.GetRequiredService<IDateTime>()));
            services.AddScoped<IOcorrenciaReadRepository>(provider =>
                new OcorrenciaReadRepository(provider.GetRequiredService<IDapperConnectionFactory>()));
            services.AddScoped<IDomainEventService, DomainEventService>();
            services.AddTransient<IDateTime, DateTimeService>();
            return services;
        }
    }
}