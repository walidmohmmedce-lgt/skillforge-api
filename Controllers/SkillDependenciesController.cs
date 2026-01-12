using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillForge.Api.Data;
using SkillForge.Api.Entities;

namespace SkillForge.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SkillDependenciesController : ControllerBase
{
    private readonly AppDbContext _context;

    public SkillDependenciesController(AppDbContext context)
    {
        _context = context;
    }

    // POST: api/skilldependencies
    [HttpPost]
    public async Task<IActionResult> Create(SkillDependency dependency)
    {
        if (dependency.SkillId == dependency.RequiredSkillId)
            return BadRequest("A skill cannot depend on itself.");

        var skillExists = await _context.Skills.AnyAsync(s => s.Id == dependency.SkillId);
        var requiredExists = await _context.Skills.AnyAsync(s => s.Id == dependency.RequiredSkillId);

        if (!skillExists || !requiredExists)
            return BadRequest("One or both skills do not exist.");

        _context.SkillDependencies.Add(dependency);
        await _context.SaveChangesAsync();

        return Ok(dependency);
    }

    // GET: api/skilldependencies/by-skill/{skillId}
    [HttpGet("by-skill/{skillId}")]
    public async Task<IActionResult> GetDependencies(int skillId)
    {
        var deps = await _context.SkillDependencies
            .Where(d => d.SkillId == skillId)
            .ToListAsync();

        return Ok(deps);
    }
}
