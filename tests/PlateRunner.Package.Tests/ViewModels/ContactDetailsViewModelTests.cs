using PlateRunner.Package.ViewModels;

namespace PlateRunner.Package.Tests.ViewModels;

public sealed class ContactDetailsViewModelTests
{
    // -----------------------------------------------------------------------
    // HasAddress — true when at least one address field is populated
    // -----------------------------------------------------------------------

    [Fact]
    public void HasAddress_WhenAddressLine1IsSet_ReturnsTrue()
    {
        var vm = new ContactDetailsViewModel { AddressLine1 = "12 High Street" };

        Assert.True(vm.HasAddress);
    }

    [Fact]
    public void HasAddress_WhenCityIsSet_ReturnsTrue()
    {
        var vm = new ContactDetailsViewModel { City = "London" };

        Assert.True(vm.HasAddress);
    }

    [Fact]
    public void HasAddress_WhenPostcodeIsSet_ReturnsTrue()
    {
        var vm = new ContactDetailsViewModel { Postcode = "SW1A 1AA" };

        Assert.True(vm.HasAddress);
    }

    [Fact]
    public void HasAddress_WhenNoAddressFieldIsSet_ReturnsFalse()
    {
        var vm = new ContactDetailsViewModel
        {
            Phone      = "020 7946 0001",
            Email      = "hello@example.com",
            WebsiteUrl = "https://example.com"
        };

        Assert.False(vm.HasAddress);
    }

    [Fact]
    public void HasAddress_WhenAllPropertiesAreNull_ReturnsFalse()
    {
        var vm = new ContactDetailsViewModel();

        Assert.False(vm.HasAddress);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void HasAddress_WhenAddressLine1IsEmptyOrWhiteSpace_ReturnsFalse(string value)
    {
        var vm = new ContactDetailsViewModel { AddressLine1 = value };

        Assert.False(vm.HasAddress);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void HasAddress_WhenCityIsEmptyOrWhiteSpace_ReturnsFalse(string value)
    {
        var vm = new ContactDetailsViewModel { City = value };

        Assert.False(vm.HasAddress);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void HasAddress_WhenPostcodeIsEmptyOrWhiteSpace_ReturnsFalse(string value)
    {
        var vm = new ContactDetailsViewModel { Postcode = value };

        Assert.False(vm.HasAddress);
    }

    [Fact]
    public void HasAddress_WhenAllAddressFieldsAreSet_ReturnsTrue()
    {
        var vm = new ContactDetailsViewModel
        {
            AddressLine1 = "12 High Street",
            AddressLine2 = "Flat 1",
            City         = "London",
            Postcode     = "SW1A 1AA",
            Country      = "United Kingdom"
        };

        Assert.True(vm.HasAddress);
    }

    // -----------------------------------------------------------------------
    // AddressLine2 and Country do not affect HasAddress on their own
    // -----------------------------------------------------------------------

    [Fact]
    public void HasAddress_WhenOnlyAddressLine2IsSet_ReturnsFalse()
    {
        var vm = new ContactDetailsViewModel { AddressLine2 = "Flat 1" };

        Assert.False(vm.HasAddress);
    }

    [Fact]
    public void HasAddress_WhenOnlyCountryIsSet_ReturnsFalse()
    {
        var vm = new ContactDetailsViewModel { Country = "United Kingdom" };

        Assert.False(vm.HasAddress);
    }
}
