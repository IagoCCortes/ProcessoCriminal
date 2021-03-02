using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProcedimentoCriminal.Core.Application.Interfaces;
using ProcedimentoCriminal.Core.Domain;
using ProcedimentoCriminal.Reportacao.Domain.Interfaces;
using ProcedimentoCriminal.Reportacao.Infrastructure.Persistence;
using ProcedimentoCriminal.Reportacao.Infrastructure.Services;

namespace ProcedimentoCriminal.Reportacao.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("DapperSettings").GetSection("ConnectionString").Value;
            var connectionFactory = new DapperConnectionFactory(connectionString);
            services.AddScoped<IOcorrenciaRepository>(provider => new OcorrenciaRepository(connectionFactory, provider.GetRequiredService<IDomainEventService>()));
            services.AddScoped<IDomainEventService, DomainEventService>();
            services.AddTransient<IDateTime, DateTimeService>();
            return services;
        }
    }
}