using PlateRunner.Package.Extensions;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;

namespace PlateRunner.Package.Composers;

/// <summary>
/// Umbraco composer that bootstraps the PlateRunner package into the
/// host application's DI container.
/// Runs automatically when the package assembly is loaded — no manual
/// registration required in the host site.
/// The <see cref="Migrations.PlateRunnerMigrationPlan"/> is discovered
/// automatically by Umbraco via <c>IDiscoverable</c> and run on startup.
/// </summary>
public sealed class PlateRunnerComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.AddPlateRunner();
    }
}
