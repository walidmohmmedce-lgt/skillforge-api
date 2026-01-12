using Microsoft.EntityFrameworkCore;
using SkillForge.Api.Entities;

namespace SkillForge.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<SkillTree> SkillTrees => Set<SkillTree>();
    public DbSet<Skill> Skills => Set<Skill>();
    public DbSet<SkillDependency> SkillDependencies => Set<SkillDependency>();
    public DbSet<UserSkillProgress> UserSkillProgresses => Set<UserSkillProgress>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SkillDependency>()
            .HasOne(sd => sd.Skill)
            .WithMany()
            .HasForeignKey(sd => sd.SkillId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<SkillDependency>()
            .HasOne(sd => sd.RequiredSkill)
            .WithMany()
            .HasForeignKey(sd => sd.RequiredSkillId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
