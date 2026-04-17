using ProcessDock.Domain.Enums;

namespace ProcessDock.Domain.Entities;

public class WorkSession
{
    public int Id { get; set; }
    public int WorkItemId { get; set; }
    public WorkItem WorkItem { get; set; } = null!;
    public DateTime StartedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime? EndedAtUtc { get; set; }
    public int DurationSeconds { get; set; } 
    public WorkSessionStatus Status { get; set; } = WorkSessionStatus.Running;
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public string? Notes { get; set; }
}
