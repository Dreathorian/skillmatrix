namespace SkillMatrixAPI.Models;

public class UserController
{
    private readonly Database _db = new();


    public bool CreateUser(string email, string password, string? firstName = null, string? lastName = null)//todo add all user parameters or turn into DTO
    {
        if (!EmailValid(email) || !PasswordValid(password) || !TryCreateUser(email, password, out var user)) return false;

        user.FirstName = firstName;
        user.LastName  = lastName;
        return true;
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
        if (!TryGetUser(userId, out var user) || !SkillIdExists(skillId) || TryGetUserSkill(user, skillId, out var userSkill)) return false;

        userSkill.Level = level;
        //todo need to update team coverage
        return true;
    }


    public bool AddLanguage(int userId, int languageId, int level)
    {
        if (!LevelInRange(level) || !TryGetUser(userId, out var user) || !LanguageIdExists(languageId) || !UserKnowsLanguage(user, languageId)) return false;

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
        if (!TryGetUser(userId, out var user) || !LanguageIdExists(languageId) || TryGetUserLanguage(user, languageId, out var userLanguage)) return false;

        userLanguage.Level = level;
        //todo need to update team coverage
        return true;
    }

    private bool PasswordValid(string password) => true;
    private bool EmailValid(string email) => true;

    private bool UserExists(string email) => _db.Users.Any(pair => pair.Value.Email == email);

    private bool UserKnowsLanguage(User user, int languageId) => user.Languages.ContainsKey(languageId);
    private bool UserKnowsSkill(User user, int skillId) => user.Skills.ContainsKey(skillId);

    private bool SkillIdExists(int skillId) => _db.Skills.ContainsKey(skillId);
    private bool LanguageIdExists(int languageId) => _db.Languages.ContainsKey(languageId);
    private bool LevelInRange(int level) => level is > 0 and < 4;

    private bool TryCreateUser(string email, string password, out User user)
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
    private bool TryGetUser(int userId, out User user) => _db.Users.TryGetValue(userId, out user);
    private bool TryGetUserSkill(User user, int skillId, out UserSkill userSkill) => user.Skills.TryGetValue(skillId, out userSkill);
    private bool TryGetUserLanguage(User user, int languageId, out UserLanguage userLanguage) => user.Languages.TryGetValue(languageId, out userLanguage);

    private bool TryGetSkill(int skillId, out Skill skill) => _db.Skills.TryGetValue(skillId, out skill);
    private bool TryGetLanguage(int languageId, out Language language) => _db.Languages.TryGetValue(languageId, out language);
}