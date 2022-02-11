namespace SkillMatrixAPITest.Repositories;

public interface IUserRepository
{
    public bool AddSkill(int         userId, int skillId, int level);
    public bool AddOrUpdateSkill(int userId, int skillId, int level);
    public bool UpdateSkill(int      userId, int skillId, int level);
    public bool RemoveSkill(int      userId, int skillId);

    public bool AddLanguage(int         userId, int languageId, int level);
    public bool AddOrUpdateLanguage(int userId, int languageId, int level);
    public bool UpdateLanguage(int      userId, int languageId, int level);
    public bool RemoveLanguage(int      userId, int languageId);
}