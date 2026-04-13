using ProcessDock.Domain.Enums;

namespace ProcessDock.Domain.Entities;

public class WorkItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public WorkItemStatus Status { get; set; } = WorkItemStatus.Pending;
    public int ProjectId { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
