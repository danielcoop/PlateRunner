using System.Globalization;
using PlateRunner.Package.Services.Interfaces;

namespace PlateRunner.Package.Services.Implementations;

/// <summary>
/// Formats a price using the current thread culture's currency settings.
/// Replace or decorate this via DI to customise currency symbol or formatting.
/// </summary>
public sealed class PriceFormatter : IPriceFormatter
{
    /// <inheritdoc />
    public string? Format(decimal? price)
    {
        if (price is null or < 0)
        {
            return null;
        }

        return price.Value.ToString("C", CultureInfo.CurrentCulture);
    }
}
