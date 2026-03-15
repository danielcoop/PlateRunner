using PlateRunner.Package.Constants;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.Serialization;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Strings;
using Umbraco.Cms.Infrastructure.Migrations;
using UmbracoConst = Umbraco.Cms.Core.Constants;

namespace PlateRunner.Package.Migrations;

/// <summary>
/// Initial migration that creates all six PlateRunner document types.
/// Safe to run against an existing database — skips silently when the
/// <c>microsite</c> content type is already present.
/// </summary>
public sealed class CreateContentTypesMigration : AsyncMigrationBase
{
    private readonly IContentTypeService              _contentTypeService;
    private readonly IDataTypeService                 _dataTypeService;
    private readonly IShortStringHelper               _shortStringHelper;
    private readonly PropertyEditorCollection         _propertyEditors;
    private readonly IConfigurationEditorJsonSerializer _configSerializer;

    public CreateContentTypesMigration(
        IMigrationContext                    context,
        IContentTypeService                  contentTypeService,
        IDataTypeService                     dataTypeService,
        IShortStringHelper                   shortStringHelper,
        PropertyEditorCollection             propertyEditors,
        IConfigurationEditorJsonSerializer   configSerializer)
        : base(context)
    {
        _contentTypeService = contentTypeService;
        _dataTypeService    = dataTypeService;
        _shortStringHelper  = shortStringHelper;
        _propertyEditors    = propertyEditors;
        _configSerializer   = configSerializer;
    }

    protected override async Task MigrateAsync()
    {
        // Idempotent: nothing to do if the main type already exists
        if (_contentTypeService.Get(ContentTypeAliases.Microsite) != null)
            return;

        // ── Resolve built-in data types ────────────────────────────────────────
        // Primary lookup by display name; fall back to editor-alias query for
        // installations where someone has renamed the built-in data types.
        var textstring  = (await ByNameAsync("Textstring")  ?? await ByAliasAsync(UmbracoConst.PropertyEditors.Aliases.TextBox))!;
        var textarea    = (await ByNameAsync("Textarea")    ?? await ByAliasAsync(UmbracoConst.PropertyEditors.Aliases.TextArea))!;
        var trueFalse   = (await ByNameAsync("True/false")  ?? await ByAliasAsync(UmbracoConst.PropertyEditors.Aliases.Boolean))!;
        var tags        = (await ByNameAsync("Tags")        ?? await ByAliasAsync(UmbracoConst.PropertyEditors.Aliases.Tags))!;
        var @decimal    = await GetOrCreateDecimalDataTypeAsync();

        // MediaPicker3 is optional — media properties are skipped when absent
        var mediaPicker = await ByAliasAsync("Umbraco.MediaPicker3");

        // ── Social Link ────────────────────────────────────────────────────────
        var socialLink = Build(ContentTypeAliases.SocialLink, "Social Link", "icon-share");
        Prop(socialLink, "content", "Content", "Platform",      PropertyAliases.SocialLink.Platform,     textstring);
        Prop(socialLink, "content", "Content", "URL",           PropertyAliases.SocialLink.Url,          textstring);
        Prop(socialLink, "content", "Content", "Display Label", PropertyAliases.SocialLink.DisplayLabel, textstring);
        await CreateAsync(socialLink);

        // ── Opening Hour ───────────────────────────────────────────────────────
        var openingHour = Build(ContentTypeAliases.OpeningHour, "Opening Hour", "icon-time");
        Prop(openingHour, "content", "Content", "Day",        PropertyAliases.OpeningHour.Day,       textstring);
        Prop(openingHour, "content", "Content", "Open Time",  PropertyAliases.OpeningHour.OpenTime,  textstring);
        Prop(openingHour, "content", "Content", "Close Time", PropertyAliases.OpeningHour.CloseTime, textstring);
        Prop(openingHour, "content", "Content", "Is Closed",  PropertyAliases.OpeningHour.IsClosed,  trueFalse);
        Prop(openingHour, "content", "Content", "Note",       PropertyAliases.OpeningHour.Note,      textstring);
        await CreateAsync(openingHour);

        // ── Announcement ───────────────────────────────────────────────────────
        var announcement = Build(ContentTypeAliases.Announcement, "Announcement", "icon-megaphone");
        Prop(announcement, "content", "Content", "Title",                PropertyAliases.Announcement.Title,             textstring);
        Prop(announcement, "content", "Content", "Body",                 PropertyAliases.Announcement.Body,              textarea);
        Prop(announcement, "content", "Content", "Call To Action Label", PropertyAliases.Announcement.CallToActionLabel, textstring);
        Prop(announcement, "content", "Content", "Call To Action URL",   PropertyAliases.Announcement.CallToActionUrl,   textstring);
        Prop(announcement, "content", "Content", "Is Visible",           PropertyAliases.Announcement.IsVisible,         trueFalse);
        await CreateAsync(announcement);

        // ── Menu Item ──────────────────────────────────────────────────────────
        var menuItem = Build(ContentTypeAliases.MenuItem, "Menu Item", "icon-cutlery");
        Prop(menuItem, "content", "Content", "Item Name",        PropertyAliases.MenuItem.Name,        textstring);
        Prop(menuItem, "content", "Content", "Item Description", PropertyAliases.MenuItem.Description, textarea);
        Prop(menuItem, "content", "Content", "Price",            PropertyAliases.MenuItem.Price,       @decimal);
        Prop(menuItem, "content", "Content", "Is Available",     PropertyAliases.MenuItem.IsAvailable, trueFalse);
        Prop(menuItem, "content", "Content", "Dietary Tags",     PropertyAliases.MenuItem.DietaryTags, tags);
        if (mediaPicker != null)
            Prop(menuItem, "content", "Content", "Image", PropertyAliases.MenuItem.Image, mediaPicker);
        await CreateAsync(menuItem);

        // ── Menu Section ───────────────────────────────────────────────────────
        var menuSection = Build(ContentTypeAliases.MenuSection, "Menu Section", "icon-list");
        menuSection.AllowedContentTypes =
        [
            new ContentTypeSort(menuItem.Key, 0, menuItem.Alias)
        ];
        Prop(menuSection, "content", "Content", "Section Name",        PropertyAliases.MenuSection.Name,        textstring);
        Prop(menuSection, "content", "Content", "Section Description", PropertyAliases.MenuSection.Description, textarea);
        await CreateAsync(menuSection);

        // ── Microsite ──────────────────────────────────────────────────────────
        var microsite = Build(ContentTypeAliases.Microsite, "Microsite", "icon-restaurant", allowedAsRoot: true);
        microsite.AllowedContentTypes =
        [
            new ContentTypeSort(menuSection.Key,  0, menuSection.Alias),
            new ContentTypeSort(announcement.Key, 1, announcement.Alias),
            new ContentTypeSort(openingHour.Key,  2, openingHour.Alias),
            new ContentTypeSort(socialLink.Key,   3, socialLink.Alias)
        ];
        Prop(microsite, "content", "Content", "Business Name", PropertyAliases.Microsite.BusinessName, textstring);
        Prop(microsite, "content", "Content", "Tagline",       PropertyAliases.Microsite.Tagline,      textstring);
        Prop(microsite, "content", "Content", "Description",   PropertyAliases.Microsite.Description,  textarea);
        if (mediaPicker != null)
        {
            Prop(microsite, "content", "Content", "Logo",       PropertyAliases.Microsite.Logo,      mediaPicker);
            Prop(microsite, "content", "Content", "Hero Image", PropertyAliases.Microsite.HeroImage, mediaPicker);
        }
        Prop(microsite, "content",  "Content",  "Theme",          PropertyAliases.Microsite.Theme,               textstring);
        Prop(microsite, "contact",  "Contact",  "Phone",          PropertyAliases.Microsite.ContactPhone,        textstring);
        Prop(microsite, "contact",  "Contact",  "Email",          PropertyAliases.Microsite.ContactEmail,        textstring);
        Prop(microsite, "contact",  "Contact",  "Address Line 1", PropertyAliases.Microsite.ContactAddressLine1, textstring);
        Prop(microsite, "contact",  "Contact",  "Address Line 2", PropertyAliases.Microsite.ContactAddressLine2, textstring);
        Prop(microsite, "contact",  "Contact",  "City",           PropertyAliases.Microsite.ContactCity,         textstring);
        Prop(microsite, "contact",  "Contact",  "Postcode",       PropertyAliases.Microsite.ContactPostcode,     textstring);
        Prop(microsite, "contact",  "Contact",  "Country",        PropertyAliases.Microsite.ContactCountry,      textstring);
        Prop(microsite, "contact",  "Contact",  "Website URL",    PropertyAliases.Microsite.ContactWebsiteUrl,   textstring);
        await CreateAsync(microsite);
    }

    // ── Helpers ────────────────────────────────────────────────────────────────

    /// <summary>
    /// Returns the first existing Decimal data type, or creates one when a fresh
    /// Umbraco install has none (Umbraco 17 ships the <c>Umbraco.Decimal</c> editor
    /// but does not pre-configure a data type for it).
    /// </summary>
    private async Task<IDataType> GetOrCreateDecimalDataTypeAsync()
    {
        var existing = await ByNameAsync("Decimal")
                    ?? await ByAliasAsync(UmbracoConst.PropertyEditors.Aliases.Decimal);
        if (existing is not null)
            return existing;

        if (!_propertyEditors.TryGet(UmbracoConst.PropertyEditors.Aliases.Decimal, out IDataEditor? editor))
            throw new InvalidOperationException(
                $"Property editor '{UmbracoConst.PropertyEditors.Aliases.Decimal}' is not registered. " +
                "Ensure Umbraco is installed correctly.");

        var dataType = new DataType(editor, _configSerializer) { Name = "Decimal" };
        var attempt  = await _dataTypeService.CreateAsync(dataType, UmbracoConst.Security.SuperUserKey);

        if (!attempt.Success)
            throw new InvalidOperationException(
                $"Failed to create Decimal data type. Status: {attempt.Status}");

        return attempt.Result!;
    }

    private ContentType Build(string alias, string name, string icon, bool allowedAsRoot = false)
        => new(_shortStringHelper, -1) { Name = name, Alias = alias, Icon = icon, IsElement = false, AllowedAsRoot = allowedAsRoot };

    private void Prop(ContentType ct, string groupAlias, string groupName, string label, string alias, IDataType dataType)
    {
        var prop = new PropertyType(_shortStringHelper, dataType) { Name = label, Alias = alias };
        ct.AddPropertyType(prop, groupAlias, groupName);
    }

    /// <summary>Persists a content type using the non-obsolete async API.</summary>
    private Task CreateAsync(IContentType ct) =>
        _contentTypeService.CreateAsync(ct, UmbracoConst.Security.SuperUserKey);

    /// <summary>Looks up a built-in data type by its display name.</summary>
    private Task<IDataType?> ByNameAsync(string name) =>
        _dataTypeService.GetAsync(name);

    /// <summary>Looks up the first data type registered for the given editor alias.</summary>
    private async Task<IDataType?> ByAliasAsync(string editorAlias) =>
        (await _dataTypeService.GetByEditorAliasAsync(editorAlias)).FirstOrDefault();
}
