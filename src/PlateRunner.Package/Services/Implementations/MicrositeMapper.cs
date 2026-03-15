using PlateRunner.Package.Constants;
using PlateRunner.Package.Services.Interfaces;
using PlateRunner.Package.ViewModels;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace PlateRunner.Package.Services.Implementations;

/// <summary>
/// Maps a published <c>microsite</c> content node and its children into a
/// <see cref="MicrositeViewModel"/>.
///
/// Content structure expected under the microsite node:
/// <list type="bullet">
///   <item>Children of type <c>announcement</c></item>
///   <item>Children of type <c>menuSection</c>, each with children of type <c>menuItem</c></item>
///   <item>Children of type <c>openingHour</c></item>
///   <item>Children of type <c>socialLink</c></item>
///   <item>Contact details as flat properties on the microsite node itself</item>
/// </list>
/// </summary>
public sealed class MicrositeMapper : IMicrositeMapper
{
    private readonly IThemeResolver _themeResolver;
    private readonly IPriceFormatter _priceFormatter;

    public MicrositeMapper(IThemeResolver themeResolver, IPriceFormatter priceFormatter)
    {
        _themeResolver = themeResolver;
        _priceFormatter = priceFormatter;
    }

    /// <inheritdoc />
    public MicrositeViewModel Map(IPublishedContent content)
    {
        ArgumentNullException.ThrowIfNull(content);

        var themeKey = content.Value<string>(PropertyAliases.Microsite.Theme);

        return new MicrositeViewModel
        {
            BusinessName   = content.Value<string>(PropertyAliases.Microsite.BusinessName) ?? string.Empty,
            Tagline        = content.Value<string>(PropertyAliases.Microsite.Tagline) ?? string.Empty,
            Description    = content.Value<string>(PropertyAliases.Microsite.Description) ?? string.Empty,
            LogoUrl        = GetMediaSrc(content, PropertyAliases.Microsite.Logo),
            HeroImageUrl   = GetMediaSrc(content, PropertyAliases.Microsite.HeroImage),
            Theme          = _themeResolver.Resolve(themeKey),
            Announcements  = MapAnnouncements(content),
            MenuSections   = MapMenuSections(content),
            OpeningHours   = MapOpeningHours(content),
            ContactDetails = MapContactDetails(content),
            SocialLinks    = MapSocialLinks(content)
        };
    }

    // -------------------------------------------------------------------------
    // Announcements
    // -------------------------------------------------------------------------

    private static IReadOnlyList<AnnouncementViewModel> MapAnnouncements(IPublishedContent content)
        => ChildrenOfType(content, ContentTypeAliases.Announcement)
            .Select(MapAnnouncement)
            .ToArray();

    private static AnnouncementViewModel MapAnnouncement(IPublishedContent node)
        => new()
        {
            Title             = node.Value<string>(PropertyAliases.Announcement.Title) ?? string.Empty,
            Body              = node.Value<string>(PropertyAliases.Announcement.Body),
            CallToActionLabel = node.Value<string>(PropertyAliases.Announcement.CallToActionLabel),
            CallToActionUrl   = node.Value<string>(PropertyAliases.Announcement.CallToActionUrl),
            IsVisible         = node.Value<bool>(PropertyAliases.Announcement.IsVisible, defaultValue: true)
        };

    // -------------------------------------------------------------------------
    // Menu
    // -------------------------------------------------------------------------

    private IReadOnlyList<MenuSectionViewModel> MapMenuSections(IPublishedContent content)
        => ChildrenOfType(content, ContentTypeAliases.MenuSection)
            .Select(MapMenuSection)
            .ToArray();

    private MenuSectionViewModel MapMenuSection(IPublishedContent node)
        => new()
        {
            Name        = node.Value<string>(PropertyAliases.MenuSection.Name) ?? string.Empty,
            Description = node.Value<string>(PropertyAliases.MenuSection.Description),
            Items       = MapMenuItems(node)
        };

    private IReadOnlyList<MenuItemViewModel> MapMenuItems(IPublishedContent section)
        => ChildrenOfType(section, ContentTypeAliases.MenuItem)
            .Select(MapMenuItem)
            .ToArray();

    private MenuItemViewModel MapMenuItem(IPublishedContent node)
    {
        var rawPrice = node.Value<decimal?>(PropertyAliases.MenuItem.Price);

        return new MenuItemViewModel
        {
            Name           = node.Value<string>(PropertyAliases.MenuItem.Name) ?? string.Empty,
            Description    = node.Value<string>(PropertyAliases.MenuItem.Description),
            FormattedPrice = _priceFormatter.Format(rawPrice),
            IsAvailable    = node.Value<bool>(PropertyAliases.MenuItem.IsAvailable, defaultValue: true),
            DietaryTags    = MapDietaryTags(node),
            ImageUrl       = GetMediaSrc(node, PropertyAliases.MenuItem.Image)
        };
    }

    private static IReadOnlyList<string> MapDietaryTags(IPublishedContent node)
    {
        // Tags property editor returns IEnumerable<string>
        var tags = node.Value<IEnumerable<string>>(PropertyAliases.MenuItem.DietaryTags);
        if (tags is not null)
        {
            return tags.ToArray();
        }

        // Fallback: comma-separated text string
        var raw = node.Value<string>(PropertyAliases.MenuItem.DietaryTags);
        if (string.IsNullOrWhiteSpace(raw))
        {
            return [];
        }

        return raw.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    }

    // -------------------------------------------------------------------------
    // Opening hours
    // -------------------------------------------------------------------------

    private static IReadOnlyList<OpeningHourViewModel> MapOpeningHours(IPublishedContent content)
        => ChildrenOfType(content, ContentTypeAliases.OpeningHour)
            .Select(MapOpeningHour)
            .ToArray();

    private static OpeningHourViewModel MapOpeningHour(IPublishedContent node)
    {
        var isClosed = node.Value<bool>(PropertyAliases.OpeningHour.IsClosed);

        return new OpeningHourViewModel
        {
            Day       = node.Value<string>(PropertyAliases.OpeningHour.Day) ?? string.Empty,
            OpenTime  = isClosed ? null : node.Value<string>(PropertyAliases.OpeningHour.OpenTime),
            CloseTime = isClosed ? null : node.Value<string>(PropertyAliases.OpeningHour.CloseTime),
            IsClosed  = isClosed,
            Note      = node.Value<string>(PropertyAliases.OpeningHour.Note)
        };
    }

    // -------------------------------------------------------------------------
    // Contact details
    // -------------------------------------------------------------------------

    private static ContactDetailsViewModel? MapContactDetails(IPublishedContent content)
    {
        var phone        = content.Value<string>(PropertyAliases.Microsite.ContactPhone);
        var email        = content.Value<string>(PropertyAliases.Microsite.ContactEmail);
        var addressLine1 = content.Value<string>(PropertyAliases.Microsite.ContactAddressLine1);
        var addressLine2 = content.Value<string>(PropertyAliases.Microsite.ContactAddressLine2);
        var city         = content.Value<string>(PropertyAliases.Microsite.ContactCity);
        var postcode     = content.Value<string>(PropertyAliases.Microsite.ContactPostcode);
        var country      = content.Value<string>(PropertyAliases.Microsite.ContactCountry);
        var websiteUrl   = content.Value<string>(PropertyAliases.Microsite.ContactWebsiteUrl);

        // Return null when no contact info has been configured at all
        var hasAnyValue =
            !string.IsNullOrWhiteSpace(phone)        ||
            !string.IsNullOrWhiteSpace(email)        ||
            !string.IsNullOrWhiteSpace(addressLine1) ||
            !string.IsNullOrWhiteSpace(city)         ||
            !string.IsNullOrWhiteSpace(postcode)     ||
            !string.IsNullOrWhiteSpace(websiteUrl);

        if (!hasAnyValue)
        {
            return null;
        }

        return new ContactDetailsViewModel
        {
            Phone        = NullIfEmpty(phone),
            Email        = NullIfEmpty(email),
            AddressLine1 = NullIfEmpty(addressLine1),
            AddressLine2 = NullIfEmpty(addressLine2),
            City         = NullIfEmpty(city),
            Postcode     = NullIfEmpty(postcode),
            Country      = NullIfEmpty(country),
            WebsiteUrl   = NullIfEmpty(websiteUrl)
        };
    }

    // -------------------------------------------------------------------------
    // Social links
    // -------------------------------------------------------------------------

    private static IReadOnlyList<SocialLinkViewModel> MapSocialLinks(IPublishedContent content)
        => ChildrenOfType(content, ContentTypeAliases.SocialLink)
            .Select(MapSocialLink)
            .ToArray();

    private static SocialLinkViewModel MapSocialLink(IPublishedContent node)
    {
        var platform = node.Value<string>(PropertyAliases.SocialLink.Platform) ?? string.Empty;
        var label    = node.Value<string>(PropertyAliases.SocialLink.DisplayLabel);

        return new SocialLinkViewModel
        {
            Platform     = platform.ToLowerInvariant(),
            Url          = node.Value<string>(PropertyAliases.SocialLink.Url) ?? string.Empty,
            DisplayLabel = NullIfEmpty(label) ?? platform
        };
    }

    // -------------------------------------------------------------------------
    // Private helpers
    // -------------------------------------------------------------------------

    /// <summary>
    /// Returns published children of <paramref name="content"/> whose content
    /// type alias matches <paramref name="typeAlias"/> (case-insensitive).
    /// </summary>
    private static IEnumerable<IPublishedContent> ChildrenOfType(
        IPublishedContent content,
        string typeAlias)
        => content.Children()
               .Where(c => c.ContentType.Alias.Equals(
                   typeAlias, StringComparison.OrdinalIgnoreCase));

    /// <summary>
    /// Returns the URL of a media picker value.
    /// Returns <c>null</c> when the property is not set.
    /// </summary>
    private static string? GetMediaSrc(IPublishedContent content, string alias)
        => content.Value<IPublishedContent>(alias)?.Url();

    /// <summary>Returns <c>null</c> for null, empty, or whitespace-only strings.</summary>
    private static string? NullIfEmpty(string? value)
        => string.IsNullOrWhiteSpace(value) ? null : value;
}
