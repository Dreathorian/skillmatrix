using System.Diagnostics.CodeAnalysis;
using SkillMatrixAPI.Models;
using SkillMatrixAPITest.DI;

namespace SkillMatrixAPITest.Repositories;

public abstract class _Repository
{
    protected readonly IDatabase _db = SimpleDI.GetSingleton<IDatabase>();

    protected bool TryGetSkill(SkillCategory skillCategory, string skillName, [MaybeNullWhen(false)] out Skill skill)
    {
        skill = skillCategory.Skills.Values.FirstOrDefault(s => s.Name == skillName);
        return skill != null;
    }

    protected bool TryGetLanguage(int languageId, [MaybeNullWhen(false)] out Language language) =>
            _db.Languages.TryGetValue(languageId, out language);

    protected bool TryGetSkillCategory(int categoryId, [MaybeNullWhen(false)] out SkillCategory skillCategory) =>
            _db.Categories.TryGetValue(categoryId, out skillCategory);


}