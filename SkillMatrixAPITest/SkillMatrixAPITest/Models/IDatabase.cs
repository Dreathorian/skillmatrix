namespace SkillMatrixAPI.Models;

public interface IDatabase
{
    Dictionary<int, Language>      Languages   { get; init; }
    Dictionary<int, SkillCategory> Categories  { get; init; }
    Dictionary<int, Department>    Departments { get; init; }
    Dictionary<int, Team>          Teams       { get; init; }
    Dictionary<int, User>          Users       { get; init; }
    Dictionary<int, Skill> Skills      { get; init; }
}