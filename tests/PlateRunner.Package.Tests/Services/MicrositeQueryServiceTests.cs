using NSubstitute;
using PlateRunner.Package.Constants;
using PlateRunner.Package.Services.Implementations;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace PlateRunner.Package.Tests.Services;

public sealed class MicrositeQueryServiceTests
{
    private readonly IPublishedContentQuery _contentQuery = Substitute.For<IPublishedContentQuery>();
    private readonly MicrositeQueryService _sut;

    public MicrositeQueryServiceTests()
    {
        _sut = new MicrositeQueryService(_contentQuery);
    }

    // -----------------------------------------------------------------------
    // GetMicrositeContent — content is itself a microsite
    // -----------------------------------------------------------------------

    [Fact]
    public void GetMicrositeContent_WhenContentIsMicrosite_ReturnsSameNode()
    {
        var content = BuildContent(ContentTypeAliases.Microsite);

        var result = _sut.GetMicrositeContent(content);

        Assert.Same(content, result);
    }

    // -----------------------------------------------------------------------
    // GetMicrositeContent — null argument guard
    // -----------------------------------------------------------------------

    [Fact]
    public void GetMicrositeContent_NullContent_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => _sut.GetMicrositeContent(null!));
    }

    // -----------------------------------------------------------------------
    // GetAllMicrosites — empty content tree
    // -----------------------------------------------------------------------

    [Fact]
    public void GetAllMicrosites_WhenNoRootContent_ReturnsEmptyList()
    {
        _contentQuery.ContentAtRoot().Returns([]);

        var result = _sut.GetAllMicrosites();

        Assert.Empty(result);
    }

    // -----------------------------------------------------------------------
    // Private helpers
    // -----------------------------------------------------------------------

    /// <summary>
    /// Builds a minimal NSubstitute mock of <see cref="IPublishedContent"/> with the
    /// specified content type alias.
    /// </summary>
    private static IPublishedContent BuildContent(string typeAlias)
    {
        var contentType = Substitute.For<IPublishedContentType>();
        contentType.Alias.Returns(typeAlias);

        var content = Substitute.For<IPublishedContent>();
        content.ContentType.Returns(contentType);

        return content;
    }
}
