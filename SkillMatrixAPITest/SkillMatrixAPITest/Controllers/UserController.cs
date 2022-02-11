using SkillMatrixAPITest.DI;
using SkillMatrixAPITest.Repositories;


namespace SkillMatrixAPITest.Controllers;

public class UserController
{
    private readonly IUserRepository _repo = SimpleDI.GetSingleton<IUserRepository>();

    public bool AddOrUpdateSkill(int userId, int skillId, int level) => _repo.AddOrUpdateSkill(userId, skillId, level);
    public bool AddSkill(int         userId, int skillId, int level) => _repo.AddSkill(userId, skillId, level);
    public bool RemoveSkill(int      userId, int skillId)            => _repo.RemoveSkill(userId, skillId);
    public bool UpdateSkill(int      userId, int skillId, int level) => _repo.UpdateSkill(userId, skillId, level);


    public bool AddLanguage(int userId, int languageId, int level) => _repo.AddLanguage(userId, languageId, level);
    public bool AddOrUpdateLanguage(int userId, int languageId, int level) => _repo.AddOrUpdateLanguage(userId, languageId, level);
    public bool RemoveLanguage(int userId, int languageId) => _repo.RemoveLanguage(userId, languageId);
    public bool UpdateLanguage(int userId, int languageId, int level) => _repo.UpdateLanguage(userId, languageId, level);
}