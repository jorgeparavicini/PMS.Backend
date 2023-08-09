using Detached.Mappers.EntityFramework;
using Detached.Mappers.HotChocolate;
using FluentValidation;
using HotChocolate.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PMS.Backend.Core.Database;
using PMS.Backend.Features;
using PMS.Backend.Features.GraphQL;

namespace PMS.Backend.Service.Extensions;

/// <summary>
///    Extension methods for <see cref="IServiceCollection" />.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     Adds the input validators for the graph Ql endpoints.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to add the services to.</param>
    /// <returns>The <see cref="IServiceCollection" /> so that additional calls can be chained.</returns>
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(Registrar).Assembly);
        return services;
    }

    /// <summary>
    ///     Adds and configures the database context to the service collection.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to add the services to.</param>
    /// <param name="configuration">The <see cref="IConfiguration" /> instance that holds the configuration data.</param>
    /// <returns>The <see cref="IServiceCollection" /> so that additional calls can be chained.</returns>
    public static IServiceCollection AddEfCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPooledDbContextFactory<PmsDbContext>(options =>
        {
            string connectionString = configuration.GetConnectionString("PMS")!;

            options.UseSqlServer(
                    connectionString,
                    serverOptions => serverOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
                .UseDetached()
                .UseMapping(cfg => { cfg.Default(mappingOptions => { mappingOptions.WithHotChocolate(); }); });
        });
        return services;
    }

    /// <summary>
    ///     Adds and configures Auto Mapper to the service collection.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to add the services to.</param>
    /// <returns>The <see cref="IServiceCollection" /> so that additional calls can be chained.</returns>
    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Registrar).Assembly);
        return services;
    }

    /// <summary>
    ///     Adds and configures CORS to the service collection.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to add the services to.</param>
    /// <returns>The <see cref="IServiceCollection" /> so that additional calls can be chained.</returns>
    public static IServiceCollection AddApplicationCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyOrigin();
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
            });
        });

        return services;
    }

    /// <summary>
    ///     Adds and configures the graph Ql endpoints to the service collection.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to add the services to.</param>
    /// <param name="environment">The <see cref="IHostEnvironment" /> instance that holds the environment data.</param>
    /// <returns>The <see cref="IServiceCollection" /> so that additional calls can be chained.</returns>
    public static IServiceCollection AddGraphQl(this IServiceCollection services, IHostEnvironment environment)
    {
        services.AddGraphQLServer()
            .AddMutationType<Mutation>()
            .AddQueryType<Query>()
            .AddGraphQlFeatures()
            .RegisterDbContext<PmsDbContext>(DbContextKind.Pooled)
            .AddFiltering()
            .AddSorting()
            .AddProjections()
            .AddFairyBread()
            .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = environment.IsDevelopment());

        return services;
    }
}
