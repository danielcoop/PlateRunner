using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;

namespace UmbracoSampleSite.Seeding;

/// <summary>
/// Registers the <see cref="DemoContentSeeder"/> with Umbraco's DI container
/// and hooks into the <see cref="UmbracoApplicationStartingNotification"/> to
/// run the seed on first boot.
/// </summary>
public sealed class DemoSiteComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.Services.AddTransient<DemoContentSeeder>();
        builder.AddNotificationAsyncHandler<UmbracoApplicationStartingNotification, DemoSiteStartingHandler>();
    }
}

/// <summary>
/// Async notification handler that triggers demo content seeding once Umbraco
/// finishes starting up.
/// </summary>
internal sealed class DemoSiteStartingHandler
    : INotificationAsyncHandler<UmbracoApplicationStartingNotification>
{
    private readonly DemoContentSeeder _seeder;

    public DemoSiteStartingHandler(DemoContentSeeder seeder) => _seeder = seeder;

    public Task HandleAsync(UmbracoApplicationStartingNotification notification, CancellationToken cancellationToken)
        => _seeder.SeedAsync();
}
