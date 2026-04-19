using ProcessDock.Domain.Enums;

namespace ProcessDock.Domain.Entities;

public class WorkSession
{
    public int Id { get; private set; }
    public int WorkItemId { get; private set; }
    public WorkItem WorkItem { get; private set; } = null!;
    public DateTime StartedAtUtc { get; private set; } = DateTime.UtcNow;
    public DateTime? EndedAtUtc { get; private set; }
    public int DurationSeconds { get; private set; } 
    public WorkSessionStatus Status { get; private set; } = WorkSessionStatus.Running;
    public DateTime CreatedAtUtc { get; private set; } = DateTime.UtcNow;
    public string? Notes { get; private set; }

    private WorkSession()
    {
    }

    public WorkSession(int workItemId)
    {
        WorkItemId = workItemId;
        StartedAtUtc = DateTime.UtcNow;
        Status = WorkSessionStatus.Running;
        DurationSeconds = 0;
        CreatedAtUtc = DateTime.UtcNow;
    }

    public void Stop(string? notes)
    {
        if (Status != WorkSessionStatus.Running)
        {
            throw new InvalidOperationException("This work session is not currently running.");
        }

        var endedAtUtc = DateTime.UtcNow;
        var duration = endedAtUtc - StartedAtUtc;

        EndedAtUtc = endedAtUtc;
        DurationSeconds = (int)duration.TotalSeconds;
        Status = WorkSessionStatus.Stopped;
        Notes = notes;

    }
}
