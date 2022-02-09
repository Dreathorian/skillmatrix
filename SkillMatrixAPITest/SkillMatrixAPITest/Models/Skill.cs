namespace SkillMatrixAPI.Models;

public class Skill
{
    private static int counter;

    public int           Id { get; init; } = counter++;

    public string        Name;
    public SkillCategory Category;
}
