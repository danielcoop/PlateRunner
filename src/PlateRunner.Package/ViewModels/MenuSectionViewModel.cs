namespace PlateRunner.Package.ViewModels;

/// <summary>
/// Represents a named group of menu items (e.g. "Starters", "Mains", "Desserts").
/// </summary>
public sealed class MenuSectionViewModel
{
    /// <summary>Display name of the section.</summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>Optional description shown beneath the section heading.</summary>
    public string? Description { get; init; }

    /// <summary>Ordered items within this section.</summary>
    public IReadOnlyList<MenuItemViewModel> Items { get; init; } = [];
}
