using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillForge.Api.Data;
using SkillForge.Api.Entities;

namespace SkillForge.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SkillsController : ControllerBase
{
    private readonly AppDbContext _context;

    public SkillsController(AppDbContext context)
    {
        _context = context;
    }

    // POST: api/skills
    [HttpPost]
    public async Task<IActionResult> Create(Skill skill)
    {
        var treeExists = await _context.SkillTrees.AnyAsync(t => t.Id == skill.SkillTreeId);
        if (!treeExists)
            return BadRequest("SkillTree does not exist.");

        _context.Skills.Add(skill);
        await _context.SaveChangesAsync();

        return Ok(skill);
    }

    // GET: api/skills/by-tree/{treeId}
    [HttpGet("by-tree/{treeId}")]
    public async Task<IActionResult> GetByTree(int treeId)
    {
        var skills = await _context.Skills
            .Where(s => s.SkillTreeId == treeId)
            .ToListAsync();

        return Ok(skills);
    }

    // ✅ FIXED LOGIC HERE
    // GET: api/skills/available/{treeId}
    [HttpGet("available/{treeId}")]
    public async Task<IActionResult> GetAvailableSkills(int treeId)
    {
        // 1. Get all skills in this tree
        var skills = await _context.Skills
            .Where(s => s.SkillTreeId == treeId)
            .ToListAsync();

        // 2. Get completed skills
        var completedSkillIds = await _context.UserSkillProgresses
            .Where(p => p.IsCompleted)
            .Select(p => p.SkillId)
            .ToListAsync();

        // 3. Get all dependencies at once
        var dependencies = await _context.SkillDependencies.ToListAsync();

        var availableSkills = new List<Skill>();

        foreach (var skill in skills)
        {
            var prerequisites = dependencies
                .Where(d => d.SkillId == skill.Id)
                .Select(d => d.RequiredSkillId)
                .ToList();

            // No prerequisites → available
            if (!prerequisites.Any())
            {
                availableSkills.Add(skill);
                continue;
            }

            // All prerequisites completed → available
            if (prerequisites.All(p => completedSkillIds.Contains(p)))
            {
                availableSkills.Add(skill);
            }
        }

        return Ok(availableSkills);
    }

    // DELETE: api/skills/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var skill = await _context.Skills.FindAsync(id);
        if (skill == null)
            return NotFound();

        _context.Skills.Remove(skill);
        await _context.SaveChangesAsync();

        return Ok("Skill deleted");
    }
}
