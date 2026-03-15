using PlateRunner.Package.ViewModels;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace PlateRunner.Package.Services.Interfaces;

/// <summary>
/// Maps an Umbraco <see cref="IPublishedContent"/> microsite node into a
/// <see cref="MicrositeViewModel"/> ready for theme rendering.
/// </summary>
public interface IMicrositeMapper
{
    /// <summary>
    /// Maps <paramref name="content"/> to a <see cref="MicrositeViewModel"/>.
    /// </summary>
    /// <param name="content">
    /// A published content node whose content type alias is
    /// <c>microsite</c> (see <see cref="Constants.ContentTypeAliases.Microsite"/>).
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="content"/> is <c>null</c>.
    /// </exception>
    MicrositeViewModel Map(IPublishedContent content);
}
