using System.Diagnostics.CodeAnalysis;
using SkillMatrixAPI.Models;

namespace SkillMatrixAPITest.Repositories;

public class UserRepository : _Repository, IUserRepository
{
#region Skill

    public bool AddSkill(int userId, int skillId, int level)
    {
        if (!TryGetUser(userId, out var user) || !TryGetSkill(skillId, out var skill) ||
            user.Skills.ContainsKey(skillId))
            return false;

        user.Skills[skillId] = UserSkill.Get(skill, level);
        //todo need to update team coverage
        return true;
    }

    public bool AddOrUpdateSkill(int userId, int skillId, int level)
    {
        if (!LevelInRange(level) || !TryGetSkill(skillId, out var skill) ||
            !TryGetUser(userId, out var user)) return false;

        user.Skills[skillId] = UserSkill.Get(skill, level);
        //todo need to update team coverage
        return true;
    }

    public bool UpdateSkill(int userId, int skillId, int level)
    {
        if (!TryGetUser(userId, out var user) || !TryGetSkill(skillId, out _) ||
            !TryGetUserSkill(user, skillId, out var userSkill)) return false;

        userSkill.Level = level;
        //todo need to update team coverage
        return true;
    }

    public bool RemoveSkill(int userId, int skillId)
    {
        return TryGetUser(userId, out var user) && user.Skills.Remove(skillId);
        //todo need to update team coverage
    }

#endregion

#region Language

    public bool AddLanguage(int userId, int languageId, int level)
    {
        if (!LevelInRange(level)                          || !TryGetUser(userId, out var user) ||
            !TryGetLanguage(languageId, out var language) ||
            UserKnowsLanguage(user, languageId)) return false;
        user.Languages[languageId] = UserLanguage.Get(language, level);
        //todo need to update team coverage
        return true;
    }

    public bool AddOrUpdateLanguage(int userId, int languageId, int level)
    {
        if (!LevelInRange(level) || !TryGetUser(userId, out var user) ||
            !TryGetLanguage(languageId, out var language)) return false;

        user.Languages[languageId] = UserLanguage.Get(language, level);
        //todo need to update team coverage
        return true;
    }

    public bool UpdateLanguage(int userId, int languageId, int level)
    {
        if (!TryGetUser(userId, out var user) || !TryGetLanguage(languageId, out _) ||
            !TryGetUserLanguage(user, languageId, out var userLanguage)) return false;

        userLanguage.Level = level;
        //todo need to update team coverage
        return true;
    }

    public bool RemoveLanguage(int userId, int languageId)
    {
        return TryGetUser(userId, out var user) && user.Languages.Remove(languageId);
        //todo need to update team coverage
    }

#endregion

#region Helpers

    private bool LevelInRange(int level) => level is > 0 and < 4;

    private bool TryGetUser(int userId, [MaybeNullWhen(false)] out User user) =>
            _db.Users.TryGetValue(userId, out user);

    private bool UserKnowsLanguage(User user, int languageId) => user.Languages.ContainsKey(languageId);

    private bool TryGetUserSkill(User user, int skillId, [MaybeNullWhen(false)] out UserSkill userSkill) =>
            user.Skills.TryGetValue(skillId, out userSkill);

    private bool TryGetUserLanguage(User user, int languageId, [MaybeNullWhen(false)] out UserLanguage userLanguage) =>
            user.Languages.TryGetValue(languageId, out userLanguage);

    private bool TryGetSkill(int skillId, [MaybeNullWhen(false)] out Skill skill) =>
            _db.Skills.TryGetValue(skillId, out skill);

    private bool TryGetLanguage(int languageId, [MaybeNullWhen(false)] out Language language) =>
            _db.Languages.TryGetValue(languageId, out language);

#endregion
}