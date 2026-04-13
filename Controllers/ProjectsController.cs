using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProcessDock.Application.DTOs.Projects;
using ProcessDock.Domain.Entities;
using ProcessDock.Domain.Enums;
using ProcessDock.Infrastructure.Data;


namespace ProcessDock.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProjectsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectResponse>>> GetProjects()
    {
        var projects = await _context.Projects.Select(Project => new ProjectResponse
        {
            Id = Project.Id,
            Name = Project.Name,
            Description = Project.Description,
            Status = Project.Status,
            CreatedAtUtc = Project.CreatedAtUtc
        }).ToListAsync();

        return Ok(projects);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectResponse>> GetProjectById(int id)
    {
        var project = await _context.Projects.FindAsync(id);

        if (project is null)
        {
            return NotFound();
        }

        return Ok(ToProjectResponse(project));
    }

    [HttpPost]
    public async Task<ActionResult<ProjectResponse>> CreateProject(CreateProjectRequest request)
    {
        var project = new Project
        {
            Name = request.Name,
            Description = request.Description,
            Status = ProjectStatus.Draft,
            CreatedAtUtc = DateTime.UtcNow
        };

        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProjectById), new { id = project.Id }, ToProjectResponse(project));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProjectResponse>> UpdateProject(int id, UpdateProjectRequest request )
    {
        var project = await _context.Projects.FindAsync(id);

        if (project is null)
            return NotFound();

        project.Name = request.Name;
        project.Description = request.Description;
        project.Status = request.Status!.Value;

        await _context.SaveChangesAsync();

        return Ok(ToProjectResponse(project));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProject(int id)
    {
        var project = await _context.Projects.FindAsync(id);

        if (project is null)
           return NotFound();
        
        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Maps a Project entity to a ProjectResponse DTO.
    /// Centralizes mapping logic to avoid duplication across actions.
    /// </summary>
    private static ProjectResponse ToProjectResponse (Project project)
    {
        return new ProjectResponse
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            Status = project.Status,
            CreatedAtUtc = project.CreatedAtUtc
        };
    }

}
