namespace SkillMatrixAPI.Models;

public class Team
{
    public string      Name       { get; set; }
    public Department? Department { get; set; }

    public Dictionary<int, User> Users            { get; init; } = new();
    public Dictionary<int, int>  SkillCoverage    { get; init; } = new();
    public Dictionary<int, int>  LanguageCoverage { get; init; } = new();

    public Team(string name) => Name = name;

}