namespace SkillMatrixAPI.Models;

public class Department
{
    public Department(string name) => Name = name;

    public string Name { get; set; }
    public Dictionary<int, User> Users { get; init; } = new();
    public Dictionary<int, Team> Teams { get; init; } = new();

}