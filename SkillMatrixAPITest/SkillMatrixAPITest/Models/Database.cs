namespace SkillMatrixAPI.Models;

public class Database : IDatabase
{
    public Dictionary<int, Language>      Languages   { get; init; } = new();
    public Dictionary<int, SkillCategory> Categories  { get; init; } = new();
    public Dictionary<int, Department>    Departments { get; init; } = new();
    public Dictionary<int, Team>          Teams       { get; init; } = new();
    public Dictionary<int, User>          Users       { get; init; } = new();
    public Dictionary<int, Skill>         Skills      { get; init; } = new();
}