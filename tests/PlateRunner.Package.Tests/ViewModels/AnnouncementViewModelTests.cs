using PlateRunner.Package.ViewModels;

namespace PlateRunner.Package.Tests.ViewModels;

public sealed class AnnouncementViewModelTests
{
    // -----------------------------------------------------------------------
    // HasCallToAction
    // -----------------------------------------------------------------------

    [Fact]
    public void HasCallToAction_WhenBothLabelAndUrlAreSet_ReturnsTrue()
    {
        var vm = new AnnouncementViewModel
        {
            CallToActionLabel = "Book now",
            CallToActionUrl   = "https://example.com/book"
        };

        Assert.True(vm.HasCallToAction);
    }

    [Fact]
    public void HasCallToAction_WhenLabelIsNull_ReturnsFalse()
    {
        var vm = new AnnouncementViewModel
        {
            CallToActionLabel = null,
            CallToActionUrl   = "https://example.com/book"
        };

        Assert.False(vm.HasCallToAction);
    }

    [Fact]
    public void HasCallToAction_WhenUrlIsNull_ReturnsFalse()
    {
        var vm = new AnnouncementViewModel
        {
            CallToActionLabel = "Book now",
            CallToActionUrl   = null
        };

        Assert.False(vm.HasCallToAction);
    }

    [Fact]
    public void HasCallToAction_WhenBothAreNull_ReturnsFalse()
    {
        var vm = new AnnouncementViewModel
        {
            CallToActionLabel = null,
            CallToActionUrl   = null
        };

        Assert.False(vm.HasCallToAction);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void HasCallToAction_WhenLabelIsEmptyOrWhiteSpace_ReturnsFalse(string label)
    {
        var vm = new AnnouncementViewModel
        {
            CallToActionLabel = label,
            CallToActionUrl   = "https://example.com/book"
        };

        Assert.False(vm.HasCallToAction);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void HasCallToAction_WhenUrlIsEmptyOrWhiteSpace_ReturnsFalse(string url)
    {
        var vm = new AnnouncementViewModel
        {
            CallToActionLabel = "Book now",
            CallToActionUrl   = url
        };

        Assert.False(vm.HasCallToAction);
    }

    // -----------------------------------------------------------------------
    // Default values
    // -----------------------------------------------------------------------

    [Fact]
    public void IsVisible_DefaultsToTrue()
    {
        var vm = new AnnouncementViewModel();

        Assert.True(vm.IsVisible);
    }

    [Fact]
    public void Title_DefaultsToEmptyString()
    {
        var vm = new AnnouncementViewModel();

        Assert.Equal(string.Empty, vm.Title);
    }
}
