using PlateRunner.Package.Models;

namespace PlateRunner.Package.Services.Interfaces;

/// <summary>
/// Resolves a <see cref="ThemeDefinition"/> from a theme key string.
/// Always returns a valid theme — falls back to the default when the key
/// is null, empty, or unrecognised.
/// </summary>
public interface IThemeResolver
{
    /// <summary>
    /// Returns the <see cref="ThemeDefinition"/> for <paramref name="themeKey"/>,
    /// or the default theme if the key is null, empty, or unknown.
    /// </summary>
    ThemeDefinition Resolve(string? themeKey);

    /// <summary>Returns all registered themes.</summary>
    IReadOnlyCollection<ThemeDefinition> GetAllThemes();
}
