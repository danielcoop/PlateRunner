namespace PlateRunner.Package.ViewModels;

/// <summary>
/// Represents the opening hours for a single day of the week.
/// </summary>
public sealed class OpeningHourViewModel
{
    /// <summary>Display label for the day (e.g. "Monday", "Mon–Fri").</summary>
    public string Day { get; init; } = string.Empty;

    /// <summary>
    /// Formatted opening time (e.g. "9:00 AM", "09:00").
    /// Null when the business is closed on this day.
    /// </summary>
    public string? OpenTime { get; init; }

    /// <summary>
    /// Formatted closing time (e.g. "10:00 PM", "22:00").
    /// Null when the business is closed on this day.
    /// </summary>
    public string? CloseTime { get; init; }

    /// <summary>When <c>true</c> the business is closed; <see cref="OpenTime"/> and <see cref="CloseTime"/> will be null.</summary>
    public bool IsClosed { get; init; }

    /// <summary>Optional note for the day (e.g. "Kitchen closes at 9pm", "Bank Holiday hours").</summary>
    public string? Note { get; init; }
}
