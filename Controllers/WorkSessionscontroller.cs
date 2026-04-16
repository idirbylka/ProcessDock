using Microsoft.AspNetCore.Mvc;
using ProcessDock.Application.DTOs.WorkSessions;
using ProcessDock.Application.Services;

namespace ProcessDock.Controllers;

[ApiController]
[Route("api/workitems/{workItemId}/sessions")]
public class WorkSessionsController : ControllerBase
{
    private readonly IWorkSessionService _workSessionService;

    public WorkSessionsController(IWorkSessionService workSessionService)
    {
        _workSessionService = workSessionService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<WorkSessionResponse>>> GetWorkSessions(int workItemId)
    {
        var sessions = await _workSessionService.GetWorkSessionsAsync(workItemId);

        if (sessions is null)
        {
            return NotFound();
        }

        return Ok(sessions);
    }

    [HttpGet("{sessionId}")]
    public async Task<ActionResult<WorkSessionResponse>> GetWorkSessionById(int workItemId, int sessionId)
    {
        var session = await _workSessionService.GetWorkSessionByIdAsync(workItemId, sessionId);

        if (session is null)
        {
            return NotFound();
        }

        return Ok(session);
    }

    [HttpPost("start")]
    public async Task<ActionResult<WorkSessionResponse>> StartWorkSession(int workItemId)
    {
        try
        {
            var session = await _workSessionService.StartWorkSessionAsync(workItemId);

            if (session is null)
            {
                return NotFound();
            }

            return CreatedAtAction(
                nameof(GetWorkSessionById),
                new { workItemId, sessionId = session.Id },
                session);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{sessionId}/stop")]
    public async Task<ActionResult<WorkSessionResponse>> StopWorkSession(
        int workItemId,
        int sessionId,
        StopWorkSessionRequest request)
    {
        try
        {
            var session = await _workSessionService.StopWorkSessionAsync(workItemId, sessionId, request);

            if (session is null)
            {
                return NotFound();
            }

            return Ok(session);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}