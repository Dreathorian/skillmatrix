using System.Diagnostics.CodeAnalysis;
using SkillMatrixAPI.Models;
using SkillMatrixAPITest.DTOs;

namespace SkillMatrixAPITest.Repositories;

public class AdminRepository : _Repository, IAdminRepository
{

    public int CreateUser(CreateUserDTO dto) //todo add all user parameters or turn into DTO
    {
        if (!EmailValid(dto.Email) || !PasswordValid(dto.Password) ||
            !TryCreateUser(dto.Email, dto.Password, out var user)) return -1;

        user.FirstName = dto.FirstName;
        user.LastName  = dto.LastName;
        return user.Id;
    }

    public int CreateSkillCategory(string categoryName)
    {
        if (!TryCreateSkillCategory(categoryName, out var skillCategory)) return -1;
        return skillCategory.Id;
    }

    public int CreateSkill(string skillName, int skillCategory)
    {
        if (!TryCreateSkill(skillName, skillCategory, out var skill)) return -1;

        return skill.Id;
    }

    public int CreateLanguage(string languageName)
    {
        if (!TryCreateLanguage(languageName, out var language)) return -1;
        return language.Id;
    }


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

    private bool TryCreateLanguage(string languageName, [MaybeNullWhen(false)] out Language language)
    {
        if (TryGetLanguage(languageName, out language)) return false;

        language = new Language(languageName);
        _db.Languages.Add(language.Id, language);
        return true;
    }


    private bool TryCreateSkill(string skillName, int categoryId, [MaybeNullWhen(false)] out Skill skill)
    {
        skill = null;
        if (!TryGetSkillCategory(categoryId, out var skillCategory) ||
            TryGetSkill(skillCategory, skillName, out skill)) return false;

        skill = new Skill(skillName, skillCategory);
        skillCategory.Skills.Add(skill.Id, skill);
        _db.Skills.Add(skill.Id, skill);

        return true;
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
        category = _db.Categories.Values.FirstOrDefault(c =>
                string.Equals(c.Name, categoryName, StringComparison.CurrentCultureIgnoreCase));
        return category != null;
    }

    private bool TryGetLanguage(string languageName, [MaybeNullWhen(false)] out Language language)
    {
        language = _db.Languages.Values.FirstOrDefault(l => string.Equals(l.Name, languageName, StringComparison.CurrentCultureIgnoreCase));
        return language != null;
    }

    private bool PasswordValid(string password) => true;
    private bool EmailValid(string    email)    => true;

    private bool UserExists(string email) => _db.Users.Any(pair => pair.Value.Email == email);
}