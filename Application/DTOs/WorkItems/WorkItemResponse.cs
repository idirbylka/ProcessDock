using ProcessDock.Domain.Enums;

namespace ProcessDock.Application.DTOs.WorkItems;

public class WorkItemResponse
{
    public int Id { get; set;} 
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set;}
    public WorkItemStatus Status { get; set; }
    public DateTime CreatedAtUtc { get; set;}
    public int ProjectId { get; set; }
}
