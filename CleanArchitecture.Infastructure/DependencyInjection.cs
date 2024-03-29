﻿using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Infastructure.Common;
using CleanArchitecture.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;



namespace CleanArchitecture.Infastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, Action<IDBConnectionModel> action)
    {
        services.Configure(action);
        services.AddScoped<IUnitofWork, UnitOfWork>();
        services.AddScoped<IDBConnectionFactory, DBConnectionFactory>();
        return  services;
    }
}
