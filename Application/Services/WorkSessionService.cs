using Microsoft.EntityFrameworkCore;
using ProcessDock.Application.DTOs.WorkSessions;
using ProcessDock.Domain.Entities;
using ProcessDock.Domain.Enums;
using ProcessDock.Infrastructure.Data;

namespace ProcessDock.Application.Services;

public class WorkSessionService : IWorkSessionService
{
    private readonly ApplicationDbContext _context;

    public WorkSessionService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<WorkSessionResponse>?> GetWorkSessionsAsync(int workItemId)
    {
        var workItemExists = await _context.WorkItems
            .AnyAsync(workItem => workItem.Id == workItemId);

        if (!workItemExists)
        {
            return null;
        }

        return await _context.WorkSessions
            .Where(session => session.WorkItemId == workItemId)
            .Select(session => new WorkSessionResponse
            {
                Id = session.Id,
                WorkItemId = session.WorkItemId,
                StartedAtUtc = session.StartedAtUtc,
                EndedAtUtc = session.EndedAtUtc,
                DurationSeconds = session.DurationSeconds,
                Status = session.Status,
                Notes = session.Notes,
                CreatedAtUtc = session.CreatedAtUtc
            })
            .ToListAsync();
    }

    public async Task<WorkSessionResponse?> GetWorkSessionByIdAsync(int workItemId, int sessionId)
    {
        var session = await _context.WorkSessions
            .FirstOrDefaultAsync(session =>
                session.Id == sessionId &&
                session.WorkItemId == workItemId);

        if (session is null)
        {
            return null;
        }

        return ToWorkSessionResponse(session);
    }

    public async Task<WorkSessionResponse?> StartWorkSessionAsync(int workItemId)
    {
        var workItemExists = await _context.WorkItems
            .AnyAsync(workItem => workItem.Id == workItemId);

        if (!workItemExists)
        {
            return null;
        }

        var openSessionExists = await _context.WorkSessions
            .AnyAsync(session =>
                session.WorkItemId == workItemId &&
                session.Status == WorkSessionStatus.Running);

        if (openSessionExists)
        {
            throw new InvalidOperationException("This work item already has an active work session.");
        }

        var session = new WorkSession
        {
            WorkItemId = workItemId,
            StartedAtUtc = DateTime.UtcNow,
            Status = WorkSessionStatus.Running,
            DurationSeconds = 0,
            CreatedAtUtc = DateTime.UtcNow
        };

        _context.WorkSessions.Add(session);
        await _context.SaveChangesAsync();

        return ToWorkSessionResponse(session);
    }

    public async Task<WorkSessionResponse?> StopWorkSessionAsync(
        int workItemId,
        int sessionId,
        StopWorkSessionRequest request)
    {
        var session = await _context.WorkSessions
            .FirstOrDefaultAsync(session =>
                session.Id == sessionId &&
                session.WorkItemId == workItemId);

        if (session is null)
        {
            return null;
        }

        if (session.Status != WorkSessionStatus.Running)
        {
            throw new InvalidOperationException("This work session is not currently running.");
        }

        var endedAtUtc = DateTime.UtcNow;
        var duration = endedAtUtc - session.StartedAtUtc;

        session.EndedAtUtc = endedAtUtc;
        session.DurationSeconds = (int)duration.TotalSeconds;
        session.Status = WorkSessionStatus.Stopped;
        session.Notes = request.Notes;

        await _context.SaveChangesAsync();

        return ToWorkSessionResponse(session);
    }

    /// <summary>
    /// Maps a WorkSession entity to a WorkSessionResponse DTO.
    /// Centralizes mapping logic to avoid duplication across actions.
    /// </summary>
    private static WorkSessionResponse ToWorkSessionResponse(WorkSession session)
    {
        return new WorkSessionResponse
        {
            Id = session.Id,
            WorkItemId = session.WorkItemId,
            StartedAtUtc = session.StartedAtUtc,
            EndedAtUtc = session.EndedAtUtc,
            DurationSeconds = session.DurationSeconds,
            Status = session.Status,
            Notes = session.Notes,
            CreatedAtUtc = session.CreatedAtUtc
        };
    }
}