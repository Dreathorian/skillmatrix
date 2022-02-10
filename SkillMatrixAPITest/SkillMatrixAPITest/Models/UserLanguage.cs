namespace SkillMatrixAPI.Models;

public class UserLanguage
{

    private static Dictionary<(int, int), UserLanguage> languages = new Dictionary<(int, int), UserLanguage>();
    
    public static UserLanguage Get(int languageID, int level)
    {
        if (languages.TryGetValue((languageID, level), out var userLanguage)) return userLanguage;
        userLanguage = new UserLanguage(languageID, level);
        languages.Add((languageID, level), userLanguage);
        return userLanguage;
    }

    private UserLanguage(int languageId, int level)
    {
        LanguageId = languageId;
        Level = level;
    }

    public int LanguageId { get; init; }
    public int Level { get; set; }
}