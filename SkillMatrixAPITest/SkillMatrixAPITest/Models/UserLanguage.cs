namespace SkillMatrixAPI.Models;

public class UserLanguage
{
    public UserLanguage(int languageId, int level)
    {
        LanguageId = languageId;
        Level = level;
    }

    public int LanguageId { get; init; }
    public int Level { get; set; }
}