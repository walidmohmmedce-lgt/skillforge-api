namespace SkillForge.Api.Entities;

public class Skill
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int SkillTreeId { get; set; }

    public SkillTree? SkillTree { get; set; }
}
