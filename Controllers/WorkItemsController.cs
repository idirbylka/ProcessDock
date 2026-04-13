using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProcessDock.Application.DTOs.WorkItems;
using ProcessDock.Infrastructure.Data;
using ProcessDock.Domain.Entities;
using ProcessDock.Domain.Enums;



namespace ProcessDock.Controllers;

[ApiController]
[Route("api/projects/{projectId}/workitems")]
public class WorkItemsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public WorkItemsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<WorkItemResponse>>> GetWorkItems(int projectId)
    {
        var projectExists = await _context.Projects
        .AnyAsync(project => project.Id == projectId);

        if (!projectExists)
        {
            return NotFound();
        }

        var workItem = await _context.WorkItems
        .Where(workItem => workItem.ProjectId == projectId)
        .Select(WorkItem => new WorkItemResponse
        {
            Id = WorkItem.Id,
            Name = WorkItem.Name,
            Description = WorkItem.Description,
            Status = WorkItem.Status,
            CreatedAtUtc = WorkItem.CreatedAtUtc,
            ProjectId = WorkItem.ProjectId
        }).ToListAsync();

        return Ok(workItem);
    }

    [HttpGet("{workItemId}")]
    public async Task<ActionResult<WorkItemResponse>> GetWorkItemById(int projectId, int workItemId)
    {
        var workItem = await _context.WorkItems
        .FirstOrDefaultAsync(workItem =>
            workItem.Id == workItemId && 
            workItem.ProjectId == projectId);

        if (workItem is null)
        {
            return NotFound();
        }

        return Ok(ToWorkItemResponse(workItem));
    }
    [HttpPost]
    public async Task<ActionResult<WorkItemResponse>> CreateWorkItem(int projectId, CreateWorkItemRequest request)
    {
        var projectExists = await _context.Projects
            .AnyAsync(project => project.Id == projectId);

        if (!projectExists)
        {
            return NotFound();
        }

        var workItem = new WorkItem
        {
            Name = request.Name,
            Description = request.Description,
            Status = WorkItemStatus.Pending,
            ProjectId = projectId,
            CreatedAtUtc = DateTime.UtcNow
        };

        _context.WorkItems.Add(workItem);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetWorkItemById),
            new { projectId, workItemId = workItem.Id },
            ToWorkItemResponse(workItem));
    }


    [HttpPut("{workItemId}")]
    public async Task<ActionResult<WorkItemResponse>> UpdateWorkItem(
        int projectId, 
        int workItemId, 
        UpdateWorkItemRequest request )
    {
        var workItem = await _context.WorkItems
        .FirstOrDefaultAsync(workItem => 
            workItem.Id == workItemId &&
            workItem.ProjectId == projectId);

        if (workItem is null)
        {
            return NotFound();
        }
            

        workItem.Name = request.Name;
        workItem.Description = request.Description;
        workItem.Status = request.Status!.Value;

        await _context.SaveChangesAsync();

        return Ok(ToWorkItemResponse(workItem));
    }

    [HttpDelete("{workItemId}")]
    public async Task<ActionResult> DeleteWorkItem(int projectId, int workItemId)
    {
        var workItem = await _context.WorkItems
            .FirstOrDefaultAsync(workItem =>
                workItem.Id == workItemId &&
                workItem.ProjectId == projectId);

        if (workItem is null)
        {
            return NotFound();
        }
        
        _context.WorkItems.Remove(workItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }


    /// <summary>
    /// Maps a WorkItem entity to a WorkItemResponse DTO.
    /// Centralizes mapping logic to avoid duplication across actions.
    /// </summary>
    private static WorkItemResponse ToWorkItemResponse(WorkItem workItem)
    {
        return new WorkItemResponse
        {
            Id = workItem.Id,
            Name = workItem.Name,
            Description = workItem.Description,
            Status = workItem.Status,
            CreatedAtUtc = workItem.CreatedAtUtc,
            ProjectId = workItem.ProjectId
        };
    }

    
}
