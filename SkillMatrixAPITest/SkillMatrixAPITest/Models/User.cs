namespace SkillMatrixAPI.Models;

public class User
{
    private static int _counter;

    public User(string email, string password)
    {
        Email    = email;
        Password = password;
    }

    public int Id { get; init; } = _counter++;

    public string Email    { get; set; }
    public string Password { get; set; }

    public string? FirstName      { get; set; }
    public string? LastName       { get; set; }
    public string? ProfilePicture { get; set; }
    public string? Position       { get; set; }

    public Department? Department { get; set; }

    public Dictionary<int, UserLanguage> Languages { get; init; } = new();
    public Dictionary<int, UserSkill>    Skills    { get; init; } = new();
    public Dictionary<int, Team>         Teams     { get; init; } = new();

    public bool IsAdmin { get; set; }

}