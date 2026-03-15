using Umbraco.Cms.Core.Packaging;

namespace PlateRunner.Package.Migrations;

/// <summary>
/// Defines the PlateRunner package migration plan.
/// Implements <see cref="PackageMigrationPlan"/> which is <c>IDiscoverable</c>,
/// so Umbraco auto-discovers this plan on startup and runs any pending migrations.
/// No component or manual registration is required.
/// </summary>
public sealed class PlateRunnerMigrationPlan : PackageMigrationPlan
{
    // The base constructor calls From(string.Empty) then DefinePlan() automatically.
    public PlateRunnerMigrationPlan() : base("PlateRunner") { }

    protected override void DefinePlan()
    {
        To<CreateContentTypesMigration>("1.0.0");
    }
}
