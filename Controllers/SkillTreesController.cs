using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillForge.Api.Data;
using SkillForge.Api.Entities;

namespace SkillForge.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SkillTreesController : ControllerBase
{
    private readonly AppDbContext _context;

    public SkillTreesController(AppDbContext context)
    {
        _context = context;
    }

    // POST: api/skilltrees
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SkillTree skillTree)
    {
        _context.SkillTrees.Add(skillTree);
        await _context.SaveChangesAsync();

        return Ok(skillTree);
    }

    // GET: api/skilltrees
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var trees = await _context.SkillTrees.ToListAsync();
        return Ok(trees);
    }
}
