using System.ComponentModel.DataAnnotations;


namespace ProcessDock.Application.DTOs.WorkSessions;

public class StopWorkSessionRequest
{
    [MaxLength(500)]
    public string? Notes { get; set; }
}
