namespace PlateRunner.Package.Models;

/// <summary>
/// Describes a single PlateRunner theme: its key, display name, and asset paths
/// used by the rendering pipeline to select the correct view and stylesheet.
/// </summary>
public sealed class ThemeDefinition
{
    /// <summary>The unique slug key for this theme, e.g. "minimal-elegant".</summary>
    public required string Key { get; init; }

    /// <summary>Human-readable name shown in the Umbraco back-office.</summary>
    public required string DisplayName { get; init; }

    /// <summary>
    /// Razor view path resolved at render time, e.g.
    /// "~/Views/Themes/MinimalElegant/Index.cshtml".
    /// </summary>
    public required string ViewPath { get; init; }

    /// <summary>
    /// Site-relative path to the theme stylesheet served from wwwroot, e.g.
    /// "/platerunner/css/themes/minimal-elegant.css".
    /// </summary>
    public required string StylesheetPath { get; init; }
}
