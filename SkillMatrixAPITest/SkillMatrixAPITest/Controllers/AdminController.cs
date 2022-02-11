using SkillMatrixAPITest.DI;
using SkillMatrixAPITest.DTOs;
using SkillMatrixAPITest.Repositories;

namespace SkillMatrixAPITest.Controllers;

public class AdminController
{
    private readonly IAdminRepository _repo = SimpleDI.GetSingleton<IAdminRepository>();

    public int CreateUser(CreateUserDTO dto) => _repo.CreateUser(dto);

    public int CreateLanguage(string languageName) => _repo.CreateLanguage(languageName);

    public int CreateSkill(string skillName, int skillCategory) => _repo.CreateSkill(skillName, skillCategory);

    public int CreateSkillCategory(string categoryName) => _repo.CreateSkillCategory(categoryName);
}