namespace SkillMatrixAPI.Models;

public class Skill
{
    private static int counter;

    public Skill(string name, SkillCategory category)
    {
        Name     = name;
        Category = category;
    }

    public int           Id { get; init; } = counter++;

    public string        Name;
    public SkillCategory Category;
}
