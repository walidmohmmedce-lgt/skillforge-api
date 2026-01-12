namespace SkillForge.Api.Entities;

public class SkillDependency
{
    public int Id { get; set; }

    // The skill that is locked
    public int SkillId { get; set; }
    public Skill? Skill { get; set; }

    // The required skill
    public int RequiredSkillId { get; set; }
    public Skill? RequiredSkill { get; set; }
}
