using PlateRunner.Package.Constants;
using PlateRunner.Package.Services.Interfaces;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace PlateRunner.Package.Services.Implementations;

/// <summary>
/// Queries the Umbraco published content cache for microsite nodes.
/// </summary>
public sealed class MicrositeQueryService : IMicrositeQueryService
{
    private readonly IPublishedContentQuery _contentQuery;

    public MicrositeQueryService(IPublishedContentQuery contentQuery)
    {
        _contentQuery = contentQuery;
    }

    /// <inheritdoc />
    public IPublishedContent? GetMicrositeContent(IPublishedContent content)
    {
        ArgumentNullException.ThrowIfNull(content);

        // If this node is itself a microsite, return it directly
        if (IsMicrosite(content))
        {
            return content;
        }

        // Walk ancestors to find the nearest microsite node
        return content.AncestorsOrSelf()
                      .FirstOrDefault(IsMicrosite);
    }

    /// <inheritdoc />
    public IReadOnlyList<IPublishedContent> GetAllMicrosites()
        => _contentQuery
               .ContentAtRoot()
               .SelectMany(root => root.DescendantsOrSelf())
               .Where(IsMicrosite)
               .ToArray();

    // -------------------------------------------------------------------------
    // Private helpers
    // -------------------------------------------------------------------------

    private static bool IsMicrosite(IPublishedContent content)
        => content.ContentType.Alias.Equals(
               ContentTypeAliases.Microsite,
               StringComparison.OrdinalIgnoreCase);
}
