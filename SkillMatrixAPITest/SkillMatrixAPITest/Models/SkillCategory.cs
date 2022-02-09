namespace SkillMatrixAPI.Models;

public class SkillCategory
{
    private static int _counter;

    public SkillCategory(string name) => this.Name = name;

    public int    Id   { get; init; } = _counter++;
    public string Name { get; set; }

    public Dictionary<int, Skill> Skills       { get; init; } = new();
}
