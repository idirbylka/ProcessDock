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
            Status = Project.Status.ToString(),
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

        var response = new ProjectResponse
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            Status = project.Status.ToString(),
            CreatedAtUtc = project.CreatedAtUtc
        };

        return Ok(response);
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

        var response = new ProjectResponse
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            Status = project.Status.ToString(),
            CreatedAtUtc = project.CreatedAtUtc
        };

        return CreatedAtAction(nameof(GetProjectById), new { id = project.Id }, response);
    }

}
