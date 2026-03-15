using PlateRunner.Package.Constants;
using PlateRunner.Package.Models;
using static PlateRunner.Package.Constants.ThemeKeys;
using PlateRunner.Package.Services.Interfaces;

namespace PlateRunner.Package.Services.Implementations;

/// <summary>
/// Default implementation of <see cref="IThemeResolver"/>.
/// Holds an in-memory registry of the built-in themes and performs
/// case-insensitive key lookups with a safe fallback.
/// </summary>
public sealed class ThemeResolver : IThemeResolver
{
    // Static registry: themes are fixed at compile time for v1.
    // Extension point: a future IThemeProvider collection could replace this.
    private static readonly IReadOnlyDictionary<string, ThemeDefinition> Themes =
        new Dictionary<string, ThemeDefinition>(StringComparer.OrdinalIgnoreCase)
        {
            [ThemeKeys.MinimalElegant] = new ThemeDefinition
            {
                Key            = ThemeKeys.MinimalElegant,
                DisplayName    = "Minimal Elegant",
                ViewPath       = "~/Views/Themes/MinimalElegant/Index.cshtml",
                StylesheetPath = "/platerunner/css/themes/minimal-elegant.css"
            },
            [ThemeKeys.RetroDiner] = new ThemeDefinition
            {
                Key            = ThemeKeys.RetroDiner,
                DisplayName    = "Retro Diner",
                ViewPath       = "~/Views/Themes/RetroDiner/Index.cshtml",
                StylesheetPath = "/platerunner/css/themes/retro-diner.css"
            },
            [ThemeKeys.FiestaStreet] = new ThemeDefinition
            {
                Key            = ThemeKeys.FiestaStreet,
                DisplayName    = "Fiesta Street",
                ViewPath       = "~/Views/Themes/FiestaStreet/Index.cshtml",
                StylesheetPath = "/platerunner/css/themes/fiesta-street.css"
            }
        };

    /// <inheritdoc />
    public ThemeDefinition Resolve(string? themeKey)
    {
        if (string.IsNullOrWhiteSpace(themeKey) || !Themes.TryGetValue(themeKey, out var theme))
        {
            return Themes[ThemeKeys.Default];
        }

        return theme;
    }

    /// <inheritdoc />
    public IReadOnlyCollection<ThemeDefinition> GetAllThemes() =>
        Themes.Values.ToArray();
}
