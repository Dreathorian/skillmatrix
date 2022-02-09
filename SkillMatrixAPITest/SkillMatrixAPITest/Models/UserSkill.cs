namespace SkillMatrixAPI.Models;

public class UserSkill
{
    public UserSkill(int skillId, int level)
    {
        SkillId = skillId;
        Level = level;
    }

    public int SkillId { get; init; }
    public int Level { get; set; }
}
