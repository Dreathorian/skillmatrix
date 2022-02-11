namespace SkillMatrixAPI.Models;

public class UserSkill
{
    private static readonly Dictionary<(Skill, int), UserSkill> Skills = new Dictionary<(Skill, int), UserSkill>();
    
    public static UserSkill Get(Skill skill, int level)
    {
        if (Skills.TryGetValue((skill, level), out var userSkill)) return userSkill;
        userSkill = new UserSkill(skill, level);
        Skills.Add((skill, level), userSkill);
        return userSkill;
    }

    private UserSkill(Skill skill, int level)
    {
        Skill = skill;
        Level = level;
    }

    public Skill Skill { get; init; }
    public int   Level { get; set; }
}