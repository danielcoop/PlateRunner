namespace PlateRunner.Package.Constants;

/// <summary>
/// Umbraco property aliases for each PlateRunner content type.
/// Nested classes group aliases by the content type that owns them.
/// Keep in sync with the document types configured in the Umbraco back-office.
/// </summary>
public static class PropertyAliases
{
    /// <summary>Properties on the <c>microsite</c> document type.</summary>
    public static class Microsite
    {
        // Branding
        public const string BusinessName = "businessName";
        public const string Tagline      = "tagline";
        public const string Description  = "description";
        public const string Logo         = "logo";
        public const string HeroImage    = "heroImage";

        // Theme
        public const string Theme = "theme";

        // Contact details (flat properties on the microsite node)
        public const string ContactPhone        = "contactPhone";
        public const string ContactEmail        = "contactEmail";
        public const string ContactAddressLine1 = "contactAddressLine1";
        public const string ContactAddressLine2 = "contactAddressLine2";
        public const string ContactCity         = "contactCity";
        public const string ContactPostcode     = "contactPostcode";
        public const string ContactCountry      = "contactCountry";
        public const string ContactWebsiteUrl   = "contactWebsiteUrl";
    }

    /// <summary>Properties on the <c>menuSection</c> document type.</summary>
    public static class MenuSection
    {
        public const string Name        = "sectionName";
        public const string Description = "sectionDescription";
    }

    /// <summary>Properties on the <c>menuItem</c> document type.</summary>
    public static class MenuItem
    {
        public const string Name        = "itemName";
        public const string Description = "itemDescription";
        public const string Price       = "price";
        public const string IsAvailable = "isAvailable";
        /// <summary>
        /// Expects a Tags property editor (returns <c>IEnumerable&lt;string&gt;</c>).
        /// </summary>
        public const string DietaryTags = "dietaryTags";
        public const string Image       = "image";
    }

    /// <summary>Properties on the <c>announcement</c> document type.</summary>
    public static class Announcement
    {
        public const string Title              = "title";
        public const string Body               = "body";
        public const string CallToActionLabel  = "callToActionLabel";
        public const string CallToActionUrl    = "callToActionUrl";
        public const string IsVisible          = "isVisible";
    }

    /// <summary>Properties on the <c>openingHour</c> document type.</summary>
    public static class OpeningHour
    {
        /// <summary>Display label, e.g. "Monday" or "Mon–Fri".</summary>
        public const string Day       = "day";
        /// <summary>Text value, e.g. "09:00" or "9:00 AM".</summary>
        public const string OpenTime  = "openTime";
        /// <summary>Text value, e.g. "22:00" or "10:00 PM".</summary>
        public const string CloseTime = "closeTime";
        public const string IsClosed  = "isClosed";
        public const string Note      = "note";
    }

    /// <summary>Properties on the <c>socialLink</c> document type.</summary>
    public static class SocialLink
    {
        /// <summary>Lowercase platform slug, e.g. "instagram".</summary>
        public const string Platform     = "platform";
        public const string Url          = "url";
        public const string DisplayLabel = "displayLabel";
    }
}
