using PlateRunner.Package.ViewModels;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace PlateRunner.Package.Services.Interfaces;

/// <summary>
/// Retrieves published microsite content nodes from Umbraco for rendering.
/// Keeping queries here prevents controllers and views from depending
/// directly on Umbraco's content APIs.
/// </summary>
public interface IMicrositeQueryService
{
    /// <summary>
    /// Returns the microsite node for the given <paramref name="content"/> node,
    /// walking up the tree if <paramref name="content"/> is a child of a microsite.
    /// Returns <c>null</c> when no microsite ancestor is found.
    /// </summary>
    IPublishedContent? GetMicrositeContent(IPublishedContent content);

    /// <summary>
    /// Returns all published microsite nodes across the entire content tree.
    /// </summary>
    IReadOnlyList<IPublishedContent> GetAllMicrosites();
}
