using System.Diagnostics.CodeAnalysis;

namespace SkillMatrixAPI.Models;

public class UserController
{
    private readonly Database _db = new();


    public int CreateUser(string email, string password, string? firstName = null, string? lastName = null)//todo add all user parameters or turn into DTO
    {
        if (!EmailValid(email) || !PasswordValid(password) || !TryCreateUser(email, password, out var user)) return -1; 

        user.FirstName = firstName;
        user.LastName  = lastName;
        return user.Id;
    }

    //todo: move to AdminController
    public int CreateLanguage(string languageName)
    {
        if (!TryCreateLanguage(languageName, out var language)) return -1;
        return language.Id;
    }

    private bool TryCreateLanguage(string languageName, [MaybeNullWhen(false)] out Language language)
    {
        if (TryGetLanguage(languageName, out language)) return false;

        language = new Language(languageName);
        _db.Languages.Add(language.Id, language);
        return true;
    }

    private bool TryGetLanguage(string languageName, [MaybeNullWhen(false)] out Language language)
    {
        language = _db.Languages.Values.FirstOrDefault(c => string.Equals(c.Name, languageName, StringComparison.CurrentCultureIgnoreCase));
        return language != null;
    }

    //todo: move to AdminController
    public int CreateSkill(string skillName, int skillCategory)
    {
        if (!TryCreateSkill(skillName, skillCategory, out var skill)) return -1;

        return skill.Id;
    }

    private bool TryCreateSkill(string skillName, int categoryId, [MaybeNullWhen(false)] out Skill skill)
    {
        skill = null;
        if (!TryGetSkillCategory(categoryId, out var skillCategory) || TryGetSkill(skillCategory, skillName, out skill)) return false;

        skill = new Skill(skillName, skillCategory);
        skillCategory.Skills.Add(skill.Id, skill);
        _db.Skills.Add(skill.Id,skill);

        return true;
    }

    private bool TryGetSkill(SkillCategory skillCategory, string skillName, [MaybeNullWhen(false)] out Skill skill)
    {
        skill = skillCategory.Skills.Values.FirstOrDefault(s => s.Name == skillName);
        return skill != null;
    }

    private bool TryGetSkillCategory(int categoryId, [MaybeNullWhen(false)] out SkillCategory skillCategory) => _db.Categories.TryGetValue(categoryId, out skillCategory);

    //todo: move to AdminController
    public int CreateSkillCategory(string categoryName)
    {
        if (!TryCreateSkillCategory(categoryName, out var skillCategory)) return -1;
        return skillCategory.Id;
    }

    private bool TryCreateSkillCategory(string categoryName, [MaybeNullWhen(false)] out SkillCategory skillCategory)
    {
        if (TryGetSkillCategory(categoryName, out skillCategory)) return false;
        skillCategory = new SkillCategory(categoryName);
        _db.Categories.Add(skillCategory.Id, skillCategory);
        return true;
    }

    private bool TryGetSkillCategory(string categoryName, [MaybeNullWhen(false)] out SkillCategory category)
    {
        category = _db.Categories.Values.FirstOrDefault(c => string.Equals(c.Name, categoryName, StringComparison.CurrentCultureIgnoreCase));
        return category != null;
    }


    public bool AddOrUpdateSkill(int userId, int skillId, int level)
    {
        if (!LevelInRange(level) || !SkillIdExists(skillId) || !TryGetUser(userId, out var user) ) return false; // logical Or (|) could help identify invalid request causes but be more resource intensive

        user.Skills[skillId] = new UserSkill(skillId, level);
        //todo need to update team coverage
        return true;
    }

    public bool AddSkill(int userId, int skillId, int level)
    {
        if (!TryGetUser(userId, out var user) || !SkillIdExists(skillId) || user.Skills.ContainsKey(skillId)) return false;

        user.Skills[skillId] = new UserSkill(skillId, level);
        //todo need to update team coverage
        return true;
    }

    public bool RemoveSkill(int userId, int skillId)
    {
        return TryGetUser(userId, out var user) && user.Skills.Remove(skillId);
        //todo need to update team coverage
    }

    public bool UpdateSkill(int userId, int skillId, int level)
    {
        if (!TryGetUser(userId, out var user) || !SkillIdExists(skillId) || !TryGetUserSkill(user, skillId, out var userSkill)) return false;

        userSkill.Level = level;
        //todo need to update team coverage
        return true;
    }


    public bool AddLanguage(int userId, int languageId, int level)
    {
        if (!LevelInRange(level) || !TryGetUser(userId, out var user) || !LanguageIdExists(languageId) ||
            UserKnowsLanguage(user, languageId)) return false;
        user.Languages[languageId] = new UserLanguage(languageId, level);
        //todo need to update team coverage
        return true;

    }

    public bool AddOrUpdateLanguage(int userId, int languageId, int level)
    {
        if (!LevelInRange(level) || !TryGetUser(userId, out var user) || !LanguageIdExists(languageId)) return false;

        user.Languages[languageId] = new UserLanguage(languageId, level);
        //todo need to update team coverage
        return true;
    }

    public bool RemoveLanguage(int userId, int languageId)
    {
        return TryGetUser(userId, out var user) && user.Languages.Remove(languageId);
        //todo need to update team coverage
    }

    public bool UpdateLanguage(int userId, int languageId, int level)
    {
        if (!TryGetUser(userId, out var user) || !LanguageIdExists(languageId) || !TryGetUserLanguage(user, languageId, out var userLanguage)) return false;

        userLanguage.Level = level;
        //todo need to update team coverage
        return true;
    }

    private bool PasswordValid(string password) => true;
    private bool EmailValid(string email) => true;

    private bool UserExists(string    email)      => _db.Users.Any(pair => pair.Value.Email == email);
    private bool SkillIdExists(int    skillId)    => _db.Skills.ContainsKey(skillId);
    private bool LanguageIdExists(int languageId) => _db.Languages.ContainsKey(languageId);

    private bool LevelInRange(int level) => level is > 0 and < 4;

    private bool TryCreateUser(string email, string password, [MaybeNullWhen(false)] out User user)
    {
        if (UserExists(email))
        {
            user = null;
            return false;
        }
        user = new User(email, password);
        _db.Users.Add(user.Id, user);
        return true;
    }
    private bool TryGetUser(int userId, [MaybeNullWhen(false)] out User user) => _db.Users.TryGetValue(userId, out user);

    private bool UserKnowsLanguage(User user, int languageId) => user.Languages.ContainsKey(languageId);
    private bool UserKnowsSkill(User user, int skillId) => user.Skills.ContainsKey(skillId);

    private bool TryGetUserSkill(User user, int skillId, [MaybeNullWhen(false)] out UserSkill userSkill) => user.Skills.TryGetValue(skillId, out userSkill);
    private bool TryGetUserLanguage(User user, int languageId, [MaybeNullWhen(false)] out UserLanguage userLanguage) => user.Languages.TryGetValue(languageId, out userLanguage);

    private bool TryGetSkill(int    skillId,    [MaybeNullWhen(false)] out Skill    skill)    => _db.Skills.TryGetValue(skillId, out skill);
    private bool TryGetLanguage(int languageId, [MaybeNullWhen(false)] out Language language) => _db.Languages.TryGetValue(languageId, out language);

}