namespace PlateRunner.Package.Constants;

/// <summary>
/// String keys that identify the built-in PlateRunner themes.
/// Use these constants wherever a theme key is required to avoid magic strings.
/// </summary>
public static class ThemeKeys
{
    public const string MinimalElegant = "minimal-elegant";
    public const string RetroDiner     = "retro-diner";
    public const string FiestaStreet   = "fiesta-street";

    /// <summary>
    /// The theme returned when no valid theme key is supplied.
    /// </summary>
    public const string Default = MinimalElegant;
}
