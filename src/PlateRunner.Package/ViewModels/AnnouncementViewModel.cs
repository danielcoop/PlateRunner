namespace PlateRunner.Package.ViewModels;

/// <summary>
/// Represents a featured announcement or special that can be displayed
/// prominently on the microsite (e.g. "Kitchen closed Monday", "Happy Hour 5–7pm").
/// </summary>
public sealed class AnnouncementViewModel
{
    /// <summary>Short headline for the announcement.</summary>
    public string Title { get; init; } = string.Empty;

    /// <summary>Body text of the announcement. May be HTML.</summary>
    public string? Body { get; init; }

    /// <summary>
    /// Optional call-to-action label (e.g. "Book now", "Find out more").
    /// Null when no CTA has been configured.
    /// </summary>
    public string? CallToActionLabel { get; init; }

    /// <summary>
    /// URL for the call-to-action. Null when no CTA has been configured.
    /// </summary>
    public string? CallToActionUrl { get; init; }

    /// <summary>
    /// Whether this announcement should be displayed. Allows the editor to
    /// toggle visibility without deleting the content.
    /// </summary>
    public bool IsVisible { get; init; } = true;

    /// <summary>Returns <c>true</c> when a call-to-action has been fully configured.</summary>
    public bool HasCallToAction =>
        !string.IsNullOrWhiteSpace(CallToActionLabel) &&
        !string.IsNullOrWhiteSpace(CallToActionUrl);
}
