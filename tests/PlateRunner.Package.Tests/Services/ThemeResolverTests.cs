using PlateRunner.Package.Constants;
using PlateRunner.Package.Services.Implementations;

namespace PlateRunner.Package.Tests.Services;

public sealed class ThemeResolverTests
{
    private readonly ThemeResolver _sut = new();

    // -----------------------------------------------------------------------
    // Resolve — known keys
    // -----------------------------------------------------------------------

    [Theory]
    [InlineData(ThemeKeys.MinimalElegant)]
    [InlineData(ThemeKeys.RetroDiner)]
    [InlineData(ThemeKeys.FiestaStreet)]
    public void Resolve_KnownKey_ReturnsMatchingTheme(string key)
    {
        var theme = _sut.Resolve(key);

        Assert.Equal(key, theme.Key);
    }

    [Theory]
    [InlineData(ThemeKeys.MinimalElegant, "Minimal Elegant")]
    [InlineData(ThemeKeys.RetroDiner,     "Retro Diner")]
    [InlineData(ThemeKeys.FiestaStreet,   "Fiesta Street")]
    public void Resolve_KnownKey_HasExpectedDisplayName(string key, string expectedDisplayName)
    {
        var theme = _sut.Resolve(key);

        Assert.Equal(expectedDisplayName, theme.DisplayName);
    }

    [Theory]
    [InlineData(ThemeKeys.MinimalElegant)]
    [InlineData(ThemeKeys.RetroDiner)]
    [InlineData(ThemeKeys.FiestaStreet)]
    public void Resolve_KnownKey_ViewPathAndStylesheetPathAreNotEmpty(string key)
    {
        var theme = _sut.Resolve(key);

        Assert.False(string.IsNullOrWhiteSpace(theme.ViewPath));
        Assert.False(string.IsNullOrWhiteSpace(theme.StylesheetPath));
    }

    // -----------------------------------------------------------------------
    // Resolve — case insensitivity
    // -----------------------------------------------------------------------

    [Theory]
    [InlineData("Minimal-Elegant")]
    [InlineData("MINIMAL-ELEGANT")]
    [InlineData("minimal-ELEGANT")]
    public void Resolve_KeyWithDifferentCasing_ReturnsMinimalElegantTheme(string key)
    {
        var theme = _sut.Resolve(key);

        Assert.Equal(ThemeKeys.MinimalElegant, theme.Key);
    }

    // -----------------------------------------------------------------------
    // Resolve — safe fallback
    // -----------------------------------------------------------------------

    [Fact]
    public void Resolve_NullKey_ReturnsDefaultTheme()
    {
        var theme = _sut.Resolve(null);

        Assert.Equal(ThemeKeys.Default, theme.Key);
    }

    [Fact]
    public void Resolve_EmptyStringKey_ReturnsDefaultTheme()
    {
        var theme = _sut.Resolve(string.Empty);

        Assert.Equal(ThemeKeys.Default, theme.Key);
    }

    [Fact]
    public void Resolve_WhiteSpaceKey_ReturnsDefaultTheme()
    {
        var theme = _sut.Resolve("   ");

        Assert.Equal(ThemeKeys.Default, theme.Key);
    }

    [Fact]
    public void Resolve_UnknownKey_ReturnsDefaultTheme()
    {
        var theme = _sut.Resolve("does-not-exist");

        Assert.Equal(ThemeKeys.Default, theme.Key);
    }

    // -----------------------------------------------------------------------
    // GetAllThemes
    // -----------------------------------------------------------------------

    [Fact]
    public void GetAllThemes_ReturnsAllThreeBuiltInThemes()
    {
        var themes = _sut.GetAllThemes();

        Assert.Equal(3, themes.Count);
    }

    [Fact]
    public void GetAllThemes_ContainsExpectedKeys()
    {
        var keys = _sut.GetAllThemes().Select(t => t.Key).ToHashSet();

        Assert.Contains(ThemeKeys.MinimalElegant, keys);
        Assert.Contains(ThemeKeys.RetroDiner, keys);
        Assert.Contains(ThemeKeys.FiestaStreet, keys);
    }

    [Fact]
    public void GetAllThemes_EachThemeHasNonEmptyPaths()
    {
        var themes = _sut.GetAllThemes();

        foreach (var theme in themes)
        {
            Assert.False(string.IsNullOrWhiteSpace(theme.ViewPath),
                $"{theme.Key} has empty ViewPath");
            Assert.False(string.IsNullOrWhiteSpace(theme.StylesheetPath),
                $"{theme.Key} has empty StylesheetPath");
        }
    }

    // -----------------------------------------------------------------------
    // DefaultKey constant
    // -----------------------------------------------------------------------

    [Fact]
    public void ThemeKeys_Default_IsMinimalElegant()
    {
        Assert.Equal(ThemeKeys.MinimalElegant, ThemeKeys.Default);
    }
}
