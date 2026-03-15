namespace PlateRunner.Package.ViewModels;

/// <summary>
/// Holds the contact information for a hospitality business.
/// Individual properties are nullable — only set the fields that have been configured.
/// </summary>
public sealed class ContactDetailsViewModel
{
    /// <summary>Phone number as a display string (e.g. "020 7946 0001").</summary>
    public string? Phone { get; init; }

    /// <summary>Bookings or enquiries email address.</summary>
    public string? Email { get; init; }

    /// <summary>First line of the address.</summary>
    public string? AddressLine1 { get; init; }

    /// <summary>Second line of the address (optional).</summary>
    public string? AddressLine2 { get; init; }

    /// <summary>Town or city.</summary>
    public string? City { get; init; }

    /// <summary>Postcode or ZIP code.</summary>
    public string? Postcode { get; init; }

    /// <summary>Country. Null when not specified.</summary>
    public string? Country { get; init; }

    /// <summary>External website URL (including scheme, e.g. "https://example.com").</summary>
    public string? WebsiteUrl { get; init; }

    /// <summary>
    /// Returns <c>true</c> when at least one address field is populated,
    /// allowing views to conditionally render the address block.
    /// </summary>
    public bool HasAddress =>
        !string.IsNullOrWhiteSpace(AddressLine1) ||
        !string.IsNullOrWhiteSpace(City) ||
        !string.IsNullOrWhiteSpace(Postcode);
}
