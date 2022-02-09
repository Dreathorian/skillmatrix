namespace SkillMatrixAPI.Models;

public class SkillCategory
{
    public SkillCategory(string name) => this.Name = name;

    public string      Name { get; set; }

    public List<Skill> Skills       { get; init; } = new();
}
