namespace SkillMatrixAPI.Models;

public class UserLanguage
{

    private static readonly Dictionary<(Language, int), UserLanguage> Languages = new();
    
    public static UserLanguage Get(Language language, int level)
    {
        if (Languages.TryGetValue((language, level), out var userLanguage)) return userLanguage;
        userLanguage = new UserLanguage(language, level);
        Languages.Add((language, level), userLanguage);
        return userLanguage;
    }

    private UserLanguage(Language language, int level)
    {
        Language = language;
        Level = level;
    }

    public Language Language { get; init; }
    public int Level { get; set; }
}