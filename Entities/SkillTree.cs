namespace SkillForge.Api.Entities;

public class SkillTree
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<Skill> Skills { get; set; } = new();
}
