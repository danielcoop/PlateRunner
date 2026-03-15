using Microsoft.Extensions.DependencyInjection;
using PlateRunner.Package.Services.Implementations;
using PlateRunner.Package.Services.Interfaces;
using Umbraco.Cms.Core.DependencyInjection;

namespace PlateRunner.Package.Extensions;

/// <summary>
/// <see cref="IServiceCollection"/> and <see cref="IUmbracoBuilder"/> extension
/// methods for registering PlateRunner services with the .NET DI container.
/// </summary>
public static class PlateRunnerServiceCollectionExtensions
{
    /// <summary>
    /// Registers all core PlateRunner services.
    /// </summary>
    /// <remarks>
    /// This overload works with the plain <see cref="IServiceCollection"/> and
    /// can be used from <c>Program.cs</c> or any test host.
    /// For Umbraco hosts prefer <see cref="AddPlateRunner(IUmbracoBuilder)"/> so
    /// Umbraco-specific dependencies are also wired correctly.
    /// </remarks>
    public static IServiceCollection AddPlateRunnerServices(this IServiceCollection services)
    {
        // ── Theme ─────────────────────────────────────────────────────────────
        // Singleton: theme registry is static for the lifetime of the app.
        services.AddSingleton<IThemeResolver, ThemeResolver>();

        // ── Formatting ────────────────────────────────────────────────────────
        // Singleton: stateless, thread-safe.
        services.AddSingleton<IPriceFormatter, PriceFormatter>();

        // ── Mapping ───────────────────────────────────────────────────────────
        // Transient: cheap to construct, no shared state.
        services.AddTransient<IMicrositeMapper, MicrositeMapper>();

        // ── Querying ──────────────────────────────────────────────────────────
        // Scoped: wraps IPublishedContentCache which is itself scoped per request.
        services.AddScoped<IMicrositeQueryService, MicrositeQueryService>();

        return services;
    }

    /// <summary>
    /// Registers all PlateRunner services via the Umbraco builder.
    /// Use this overload from an Umbraco <see cref="Umbraco.Cms.Core.Composing.IComposer"/>.
    /// </summary>
    public static IUmbracoBuilder AddPlateRunner(this IUmbracoBuilder builder)
    {
        builder.Services.AddPlateRunnerServices();
        return builder;
    }
}
