using ProcessDock.Domain.Enums;

namespace ProcessDock.Application.DTOs.WorkSessions;

public class WorkSessionResponse
{
    public int Id { get; set; }

    public int WorkItemId { get; set; }

    public DateTime StartedAtUtc { get; set; }
    public DateTime? EndedAtUtc { get; set; }

    public int DurationSeconds { get; set; }

    public WorkSessionStatus Status { get; set; }

    public DateTime CreatedAtUtc { get; set; }
    public string? Notes { get; set; }
}
