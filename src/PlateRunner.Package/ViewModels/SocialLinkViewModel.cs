namespace PlateRunner.Package.ViewModels;

/// <summary>
/// Represents a single social media or external link for the business.
/// </summary>
public sealed class SocialLinkViewModel
{
    /// <summary>
    /// Platform identifier (e.g. "instagram", "facebook", "tiktok", "twitter").
    /// Lowercase — can be used to select a platform icon in the view.
    /// </summary>
    public string Platform { get; init; } = string.Empty;

    /// <summary>Full URL of the social profile or page.</summary>
    public string Url { get; init; } = string.Empty;

    /// <summary>
    /// Human-readable label for the link (e.g. "@myrestaurant").
    /// Falls back to <see cref="Platform"/> when not provided.
    /// </summary>
    public string DisplayLabel { get; init; } = string.Empty;
}
