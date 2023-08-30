using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Infastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;


namespace CleanArchitecture.Infastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfastructure(this IServiceCollection services)
    {
        services.AddScoped<IUnitofWork, UnitOfWork>();
        return  services;
    }
}
