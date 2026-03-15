namespace PlateRunner.Package.ViewModels;

/// <summary>
/// Represents a single item on the menu, ready for display.
/// All formatting (e.g. price) is applied before this view model is created.
/// </summary>
public sealed class MenuItemViewModel
{
    /// <summary>Display name of the dish or item.</summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>Optional description of the item.</summary>
    public string? Description { get; init; }

    /// <summary>
    /// Pre-formatted price string (e.g. "£8.50"). Null when the item has no price,
    /// is free, or pricing is intentionally omitted.
    /// </summary>
    public string? FormattedPrice { get; init; }

    /// <summary>
    /// Whether this item is currently available for ordering.
    /// Items where <c>false</c> can still be displayed (e.g. shown as unavailable).
    /// </summary>
    public bool IsAvailable { get; init; } = true;

    /// <summary>
    /// Dietary or allergen tags (e.g. "Vegan", "Gluten-Free", "Contains Nuts").
    /// Empty when no tags have been set.
    /// </summary>
    public IReadOnlyList<string> DietaryTags { get; init; } = [];

    /// <summary>Site-relative or absolute URL of the item image. Null when not set.</summary>
    public string? ImageUrl { get; init; }
}
