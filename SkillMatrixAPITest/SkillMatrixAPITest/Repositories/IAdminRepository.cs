using SkillMatrixAPITest.DTOs;

namespace SkillMatrixAPITest.Repositories;

public interface IAdminRepository
{
    public int CreateUser(CreateUserDTO   dto);
    public int CreateLanguage(string      languageName);
    public int CreateSkill(string         skillName, int skillCategory);
    public int CreateSkillCategory(string categoryName);
}