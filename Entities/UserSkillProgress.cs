namespace SkillForge.Api.Entities;

public class UserSkillProgress
{
    public int Id { get; set; }

    public int SkillId { get; set; }
    public Skill? Skill { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime? CompletedAt { get; set; }
}
