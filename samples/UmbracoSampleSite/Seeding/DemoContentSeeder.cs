using PlateRunner.Package.Constants;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace UmbracoSampleSite.Seeding;

/// <summary>
/// Seeds three demo microsite content nodes on first boot.
/// Document types are created by the PlateRunner package migration
/// (<see cref="PlateRunner.Package.Migrations.CreateContentTypesMigration"/>)
/// which runs before this seeder via the component lifecycle.
/// </summary>
public sealed class DemoContentSeeder
{
    private readonly IContentService            _contentService;
    private readonly ILogger<DemoContentSeeder> _logger;

    public DemoContentSeeder(
        IContentService            contentService,
        ILogger<DemoContentSeeder> logger)
    {
        _contentService = contentService;
        _logger         = logger;
    }

    public Task SeedAsync()
    {
        if (_contentService.GetRootContent()
                           .Any(c => c.ContentType.Alias == ContentTypeAliases.Microsite))
        {
            _logger.LogInformation("PlateRunner demo content already seeded — skipping.");
            return Task.CompletedTask;
        }

        _logger.LogInformation("Seeding PlateRunner demo content…");

        try
        {
            SeedGlassTable();
            SeedPattysDiner();
            SeedElFuego();

            _logger.LogInformation("PlateRunner demo content seeded successfully.");
        }
        catch (Exception ex)
        {
            // Seeding is best-effort. Log and continue — a failure here must not
            // prevent the application from starting (e.g. when the migration
            // hasn't run yet or content types are unavailable).
            _logger.LogError(ex, "Failed to seed PlateRunner demo content.");
        }

        return Task.CompletedTask;
    }

    // -------------------------------------------------------------------------
    // Demo content — The Glass Table (Minimal Elegant)
    // -------------------------------------------------------------------------

    private void SeedGlassTable()
    {
        var microsite = Publish("The Glass Table", ContentTypeAliases.Microsite, -1, c =>
        {
            c.SetValue("businessName",        "The Glass Table");
            c.SetValue("tagline",             "Modern European dining");
            c.SetValue("description",         "An intimate dining room where seasonal ingredients meet quiet elegance. Tasting menus change weekly.");
            c.SetValue("theme",               "minimal-elegant");
            c.SetValue("contactPhone",        "+44 20 7946 0123");
            c.SetValue("contactEmail",        "reservations@glasstable.example");
            c.SetValue("contactAddressLine1", "12 Aldwych Lane");
            c.SetValue("contactCity",         "London");
            c.SetValue("contactPostcode",     "WC2B 4EX");
            c.SetValue("contactCountry",      "United Kingdom");
        });

        Publish("Chef's Table Available", ContentTypeAliases.Announcement, microsite.Id, c =>
        {
            c.SetValue("title",             "Chef's Table Available");
            c.SetValue("body",              "Join us for an exclusive four-course Chef's Table experience every Friday evening. Limited to 8 guests.");
            c.SetValue("callToActionLabel", "Book now");
            c.SetValue("callToActionUrl",   "#contact");
            c.SetValue("isVisible",         "1");
        });

        var starters = Publish("Starters", ContentTypeAliases.MenuSection, microsite.Id,
            c => c.SetValue("sectionName", "Starters"));
        MenuItem(starters.Id, "Burrata & Heritage Tomato", "Buffalo burrata, heirloom tomato, basil oil",          12.50m, "vegetarian,gluten-free");
        MenuItem(starters.Id, "Smoked Salmon Blinis",      "Hand-rolled blinis, crème fraîche, capers",            14.00m, "");
        MenuItem(starters.Id, "Charred Courgette Soup",    "Seasonal courgette, toasted pine nuts, aged parmesan",  9.50m, "vegetarian");

        var mains = Publish("Mains", ContentTypeAliases.MenuSection, microsite.Id,
            c => c.SetValue("sectionName", "Mains"));
        MenuItem(mains.Id, "Pan-Seared Halibut",    "Pea purée, brown butter, crispy shallots",   32.00m, "gluten-free");
        MenuItem(mains.Id, "Duck Breast",           "Confit leg, cherry jus, pommes dauphine",    28.00m, "");
        MenuItem(mains.Id, "Wild Mushroom Risotto", "Porcini, truffle oil, aged Parmesan",        22.00m, "vegetarian,gluten-free");

        var desserts = Publish("Desserts", ContentTypeAliases.MenuSection, microsite.Id,
            c => c.SetValue("sectionName", "Desserts"));
        MenuItem(desserts.Id, "Dark Chocolate Delice", "Salted caramel, cocoa nib tuile", 9.00m, "vegetarian");
        MenuItem(desserts.Id, "Lemon Posset",          "Shortbread, raspberry coulis",    8.50m, "vegetarian");

        foreach (var (day, open, close, closed) in new[]
        {
            ("Monday",   "",      "",      true),
            ("Tue–Thu",  "18:00", "22:00", false),
            ("Friday",   "12:00", "23:00", false),
            ("Saturday", "12:00", "23:00", false),
            ("Sunday",   "12:00", "21:00", false)
        })
        {
            Publish(day, ContentTypeAliases.OpeningHour, microsite.Id, c =>
            {
                c.SetValue("day",       day);
                c.SetValue("openTime",  open);
                c.SetValue("closeTime", close);
                c.SetValue("isClosed",  closed ? "1" : "0");
            });
        }

        Publish("Instagram", ContentTypeAliases.SocialLink, microsite.Id, c =>
        {
            c.SetValue("platform", "instagram");
            c.SetValue("url",      "https://instagram.com/glasstable");
        });
    }

    // -------------------------------------------------------------------------
    // Demo content — Patty's Original Diner (Retro Diner)
    // -------------------------------------------------------------------------

    private void SeedPattysDiner()
    {
        var microsite = Publish("Patty's Original Diner", ContentTypeAliases.Microsite, -1, c =>
        {
            c.SetValue("businessName",        "Patty's Original Diner");
            c.SetValue("tagline",             "Est. 1962 · Real burgers, real shakes");
            c.SetValue("description",         "Three generations of the Patty family serving the best smash burgers, crinkle fries, and hand-spun milkshakes in town.");
            c.SetValue("theme",               "retro-diner");
            c.SetValue("contactPhone",        "555-0182");
            c.SetValue("contactEmail",        "hello@pattys.example");
            c.SetValue("contactAddressLine1", "88 Maple Drive");
            c.SetValue("contactCity",         "Springfield");
        });

        Publish("National Burger Day Special", ContentTypeAliases.Announcement, microsite.Id, c =>
        {
            c.SetValue("title",     "🍔 Buy One Get One Free!");
            c.SetValue("body",      "Celebrate National Burger Day with us. Any burger, any day this week!");
            c.SetValue("isVisible", "1");
        });

        var burgers = Publish("Burgers", ContentTypeAliases.MenuSection, microsite.Id,
            c => c.SetValue("sectionName", "Burgers"));
        MenuItem(burgers.Id, "The Original Patty", "Double smash, American cheese, pickles, Patty sauce", 11.50m, "");
        MenuItem(burgers.Id, "Smoky BBQ Stack",    "Triple patty, crispy bacon, BBQ sauce, coleslaw",    14.50m, "");
        MenuItem(burgers.Id, "The Veggie Stack",   "Portobello mushroom, smashed avo, smoked cheddar",   11.00m, "vegetarian");
        MenuItem(burgers.Id, "Chicken Ranch",      "Crispy buttermilk chicken, ranch dressing, lettuce", 12.00m, "");

        var sides = Publish("Sides", ContentTypeAliases.MenuSection, microsite.Id,
            c => c.SetValue("sectionName", "Sides"));
        MenuItem(sides.Id, "Crinkle Fries",        "Seasoned, double-fried",              4.00m, "vegan");
        MenuItem(sides.Id, "Onion Rings",          "Beer-battered rings, chipotle dip",   4.50m, "vegetarian");
        MenuItem(sides.Id, "Mac 'n' Cheese Bites", "Deep-fried mac, jalapeño jam",        5.50m, "vegetarian");
        MenuItem(sides.Id, "Side Salad",           "House greens, buttermilk dressing",   3.50m, "vegetarian");

        var shakes = Publish("Shakes & Drinks", ContentTypeAliases.MenuSection, microsite.Id,
            c => c.SetValue("sectionName", "Shakes & Drinks"));
        MenuItem(shakes.Id, "Hand-Spun Vanilla Shake",       "Full-cream milk, real vanilla",        6.00m, "vegetarian");
        MenuItem(shakes.Id, "Chocolate Peanut Butter Shake", "Reese's pieces, whipped cream",        7.00m, "vegetarian");
        MenuItem(shakes.Id, "Strawberry Shake",              "Fresh strawberry, housemade syrup",    6.00m, "vegetarian");
        MenuItem(shakes.Id, "Classic Root Beer Float",       "Vintage root beer, vanilla ice cream", 5.50m, "vegetarian");

        foreach (var (day, open, close) in new[]
        {
            ("Mon–Fri",  "11:00", "22:00"),
            ("Saturday", "10:00", "23:00"),
            ("Sunday",   "10:00", "21:00")
        })
        {
            Publish(day, ContentTypeAliases.OpeningHour, microsite.Id, c =>
            {
                c.SetValue("day",       day);
                c.SetValue("openTime",  open);
                c.SetValue("closeTime", close);
                c.SetValue("isClosed",  "0");
            });
        }
    }

    // -------------------------------------------------------------------------
    // Demo content — El Fuego Taqueria (Fiesta Street)
    // -------------------------------------------------------------------------

    private void SeedElFuego()
    {
        var microsite = Publish("El Fuego Taqueria", ContentTypeAliases.Microsite, -1, c =>
        {
            c.SetValue("businessName",        "El Fuego Taqueria");
            c.SetValue("tagline",             "🌮 Authentic street tacos, every day");
            c.SetValue("description",         "Family recipes from Oaxaca. Made fresh, served fast, eaten loud. Find us at the Market Square Thursday to Sunday.");
            c.SetValue("theme",               "fiesta-street");
            c.SetValue("contactPhone",        "555-0247");
            c.SetValue("contactEmail",        "hola@elfuego.example");
            c.SetValue("contactAddressLine1", "Market Square");
            c.SetValue("contactCity",         "Austin");
        });

        Publish("New: Birria Tacos", ContentTypeAliases.Announcement, microsite.Id, c =>
        {
            c.SetValue("title",             "🔥 Birria Tacos Are Here!");
            c.SetValue("body",              "Slow-cooked beef birria with consommé for dipping. Limited daily — arrive early!");
            c.SetValue("callToActionLabel", "See the menu");
            c.SetValue("callToActionUrl",   "#menu");
            c.SetValue("isVisible",         "1");
        });

        var tacos = Publish("Tacos", ContentTypeAliases.MenuSection, microsite.Id,
            c => c.SetValue("sectionName", "🌮 Tacos"));
        MenuItem(tacos.Id, "Al Pastor",       "Spit-roasted pork, pineapple, cilantro, onion",   4.50m, "gluten-free");
        MenuItem(tacos.Id, "Birria",          "Slow-cooked beef, consommé dip, oaxacan cheese",   5.50m, "gluten-free");
        MenuItem(tacos.Id, "Grilled Fish",    "Baja battered fish, cabbage slaw, chipotle mayo",  5.00m, "");
        MenuItem(tacos.Id, "Veggie Esquites", "Corn, cotija, lime, chilli",                       4.00m, "vegetarian,gluten-free");
        MenuItem(tacos.Id, "Mushroom Tinga",  "Chipotle mushrooms, black beans, avocado crema",   4.50m, "vegan,gluten-free");

        var burritos = Publish("Burritos", ContentTypeAliases.MenuSection, microsite.Id,
            c => c.SetValue("sectionName", "🌯 Burritos"));
        MenuItem(burritos.Id, "El Clásico",     "Rice, beans, chicken tinga, salsa roja",               9.50m, "");
        MenuItem(burritos.Id, "El Grande",      "Double filling, guac, pico de gallo, sour cream",      12.00m, "");
        MenuItem(burritos.Id, "Veggie Supreme", "Black beans, roasted peppers, avocado, chipotle salsa", 9.00m, "vegetarian");

        var drinks = Publish("Drinks", ContentTypeAliases.MenuSection, microsite.Id,
            c => c.SetValue("sectionName", "🥤 Drinks"));
        MenuItem(drinks.Id, "Horchata",     "House-made cinnamon rice water", 3.50m, "vegan");
        MenuItem(drinks.Id, "Jamaica",      "Hibiscus iced tea, lime",        3.00m, "vegan");
        MenuItem(drinks.Id, "Mexican Coke", "Original glass bottle",          3.00m, "vegan");

        foreach (var (day, open, close, closed, note) in new[]
        {
            ("Monday",    "",      "",      true,  "Closed"),
            ("Tuesday",   "",      "",      true,  "Closed"),
            ("Wednesday", "",      "",      true,  "Closed"),
            ("Thursday",  "12:00", "20:00", false, "Market Square"),
            ("Friday",    "12:00", "21:00", false, "Market Square"),
            ("Saturday",  "11:00", "22:00", false, "Market Square"),
            ("Sunday",    "11:00", "19:00", false, "Market Square")
        })
        {
            Publish(day, ContentTypeAliases.OpeningHour, microsite.Id, c =>
            {
                c.SetValue("day",       day);
                c.SetValue("openTime",  open);
                c.SetValue("closeTime", close);
                c.SetValue("isClosed",  closed ? "1" : "0");
                c.SetValue("note",      note);
            });
        }

        Publish("Instagram", ContentTypeAliases.SocialLink, microsite.Id, c =>
        {
            c.SetValue("platform", "instagram");
            c.SetValue("url",      "https://instagram.com/elfuegotaqueria");
        });
        Publish("TikTok", ContentTypeAliases.SocialLink, microsite.Id, c =>
        {
            c.SetValue("platform", "tiktok");
            c.SetValue("url",      "https://tiktok.com/@elfuego");
        });
    }

    // -------------------------------------------------------------------------
    // Helpers
    // -------------------------------------------------------------------------

    private void MenuItem(int parentId, string name, string description, decimal price, string tags)
    {
        Publish(name, ContentTypeAliases.MenuItem, parentId, c =>
        {
            c.SetValue("itemName",        name);
            c.SetValue("itemDescription", description);
            c.SetValue("price",           price.ToString("F2"));
            c.SetValue("isAvailable",     "1");
            if (!string.IsNullOrWhiteSpace(tags))
            {
                // Tags property editor stores values as a JSON array.
                var tagList = tags.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                c.SetValue("dietaryTags", System.Text.Json.JsonSerializer.Serialize(tagList));
            }
        });
    }

    private IContent Publish(string name, string contentTypeAlias, int parentId, Action<IContent> configure)
    {
        var content = _contentService.Create(name, parentId, contentTypeAlias);
        configure(content);
        _contentService.Save(content);
        _contentService.Publish(content, Array.Empty<string>());
        return content;
    }
}
