public class User
{
    public List<Skill> Skills { get; set; } = new();
    public List<Language> Languages { get; set; } = new();

    public void AddSkill(string skillName, int level) => Skills.Add(new Skill(skillName, level));

    public bool RemoveSkill(string skillName) => Skills.TryGetAndDo(s => s.Name == skillName, s => Skills.Remove(s));

    public bool UpdateSkill(string skillName, int level) => 
        Skills.TryGetAndDo(s => s.Name == skillName, s => s.Level = level);

    public void AddLanguage(string languageName, int level) => Languages.Add(new Language(languageName, level));

    public bool RemoveLanguage(string languageName) => 
        Languages.TryGetAndDo(l => l.Name == languageName, l => Languages.Remove(l));

    public bool UpdateLanguage(string languageName, int level) => 
        Languages.TryGetAndDo(l => l.Name == languageName, l => l.Level = level);
}