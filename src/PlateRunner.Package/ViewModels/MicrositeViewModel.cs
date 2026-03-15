using PlateRunner.Package.Models;

namespace PlateRunner.Package.ViewModels;

/// <summary>
/// Top-level view model for a PlateRunner microsite page.
/// Passed from the controller/mapper into the theme view.
/// </summary>
public sealed class MicrositeViewModel
{
    /// <summary>Trading name of the business.</summary>
    public string BusinessName { get; init; } = string.Empty;

    /// <summary>Short tagline shown beneath the business name.</summary>
    public string Tagline { get; init; } = string.Empty;

    /// <summary>Longer description or about text. May be HTML.</summary>
    public string Description { get; init; } = string.Empty;

    /// <summary>Site-relative or absolute URL of the business logo. Null if not set.</summary>
    public string? LogoUrl { get; init; }

    /// <summary>Site-relative or absolute URL of the hero/header image. Null if not set.</summary>
    public string? HeroImageUrl { get; init; }

    /// <summary>Resolved theme for this microsite. Never null — defaults to Minimal Elegant.</summary>
    public required ThemeDefinition Theme { get; init; }

    /// <summary>Ordered list of menu sections. Empty when no menu has been configured.</summary>
    public IReadOnlyList<MenuSectionViewModel> MenuSections { get; init; } = [];

    /// <summary>Active announcements or specials. Empty when none are configured.</summary>
    public IReadOnlyList<AnnouncementViewModel> Announcements { get; init; } = [];

    /// <summary>Opening hours by day. Empty when not configured.</summary>
    public IReadOnlyList<OpeningHourViewModel> OpeningHours { get; init; } = [];

    /// <summary>Contact details for the business. Null when not configured.</summary>
    public ContactDetailsViewModel? ContactDetails { get; init; }

    /// <summary>Social media links. Empty when none are configured.</summary>
    public IReadOnlyList<SocialLinkViewModel> SocialLinks { get; init; } = [];
}
