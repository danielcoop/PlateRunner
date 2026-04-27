using System.Globalization;
using PlateRunner.Package.Services.Implementations;

namespace PlateRunner.Package.Tests.Services;

public sealed class PriceFormatterTests
{
    private readonly PriceFormatter _sut = new();

    // -----------------------------------------------------------------------
    // Null / negative — should return null
    // -----------------------------------------------------------------------

    [Fact]
    public void Format_NullPrice_ReturnsNull()
    {
        var result = _sut.Format(null);

        Assert.Null(result);
    }

    [Fact]
    public void Format_NegativePrice_ReturnsNull()
    {
        var result = _sut.Format(-0.01m);

        Assert.Null(result);
    }

    [Fact]
    public void Format_LargeNegativePrice_ReturnsNull()
    {
        var result = _sut.Format(-100m);

        Assert.Null(result);
    }

    // -----------------------------------------------------------------------
    // Zero — valid price that should be formatted
    // -----------------------------------------------------------------------

    [Fact]
    public void Format_Zero_ReturnsFormattedString()
    {
        var result = _sut.Format(0m);

        Assert.NotNull(result);
        Assert.False(string.IsNullOrWhiteSpace(result));
    }

    // -----------------------------------------------------------------------
    // Positive values — currency formatting
    // -----------------------------------------------------------------------

    [Theory]
    [InlineData(8.50)]
    [InlineData(1.00)]
    [InlineData(12.99)]
    [InlineData(100.00)]
    public void Format_PositivePrice_ReturnsNonNullString(decimal price)
    {
        var result = _sut.Format(price);

        Assert.NotNull(result);
        Assert.False(string.IsNullOrWhiteSpace(result));
    }

    [Fact]
    public void Format_PositivePrice_MatchesCurrentCultureCurrencyFormat()
    {
        const decimal price = 9.99m;

        var result = _sut.Format(price);

        var expected = price.ToString("C", CultureInfo.CurrentCulture);
        Assert.Equal(expected, result);
    }

    // -----------------------------------------------------------------------
    // Boundary: exactly 0 is valid, -0.01 is not
    // -----------------------------------------------------------------------

    [Theory]
    [InlineData(0.00, true)]
    [InlineData(0.01, true)]
    [InlineData(-0.01, false)]
    public void Format_BoundaryValues_NullBehaviourIsCorrect(decimal price, bool expectValue)
    {
        var result = _sut.Format(price);

        if (expectValue)
            Assert.NotNull(result);
        else
            Assert.Null(result);
    }
}
