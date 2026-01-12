using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillForge.Api.Data;
using SkillForge.Api.Entities;

namespace SkillForge.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProgressController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProgressController(AppDbContext context)
    {
        _context = context;
    }

    // POST: api/progress/complete/{skillId}
    [HttpPost("complete/{skillId}")]
    public async Task<IActionResult> CompleteSkill(int skillId)
    {
        var skillExists = await _context.Skills.AnyAsync(s => s.Id == skillId);
        if (!skillExists)
            return NotFound("Skill not found");

        var existing = await _context.UserSkillProgresses
            .FirstOrDefaultAsync(p => p.SkillId == skillId);

        if (existing != null)
        {
            existing.IsCompleted = true;
            existing.CompletedAt = DateTime.UtcNow;
        }
        else
        {
            _context.UserSkillProgresses.Add(new UserSkillProgress
            {
                SkillId = skillId,
                IsCompleted = true,
                CompletedAt = DateTime.UtcNow
            });
        }

        await _context.SaveChangesAsync();

        return Ok("Skill marked as completed");
    }

    // GET: api/progress
    // This is only for debugging/testing so we can see what's in the table
    [HttpGet]
    public async Task<IActionResult> GetAllProgress()
    {
        var progress = await _context.UserSkillProgresses.ToListAsync();
        return Ok(progress);
    }

    // DELETE: api/progress
// Only for testing to reset progress
[HttpDelete]
public async Task<IActionResult> ClearProgress()
{
    _context.UserSkillProgresses.RemoveRange(_context.UserSkillProgresses);
    await _context.SaveChangesAsync();

    return Ok("Progress cleared");
}

}
