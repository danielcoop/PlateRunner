namespace PlateRunner.Package.Services.Interfaces;

/// <summary>
/// Formats a raw price value into a localised display string.
/// Keeping formatting logic here prevents it leaking into mappers or views.
/// </summary>
public interface IPriceFormatter
{
    /// <summary>
    /// Returns a formatted price string (e.g. "£8.50"), or <c>null</c> when
    /// <paramref name="price"/> is <c>null</c> or negative.
    /// </summary>
    string? Format(decimal? price);
}
