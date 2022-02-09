namespace SkillMatrixAPI.Models;

public class Language
{
    private static int _counter;
    public Language(string name) => Name = name;

    public int Id { get; init; } = _counter++;
    public string Name { get; set; }
}


