using ProcessDock.Application.DTOs.WorkSessions;

namespace ProcessDock.Application.Services;

public interface IWorkSessionService
{
    Task<IEnumerable<WorkSessionResponse>?> GetWorkSessionsAsync(int workItemId);
    Task<WorkSessionResponse?> GetWorkSessionByIdAsync(int workItemId, int sessionId);
    Task<WorkSessionResponse?> StartWorkSessionAsync(int workItemId);
    Task<WorkSessionResponse?> StopWorkSessionAsync(
        int workItemId,
        int sessionId,
        StopWorkSessionRequest request);

}
