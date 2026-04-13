using ProcessDock.Domain.Enums;

namespace ProcessDock.Application.DTOs.Projects;

public class ProjectResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ProjectStatus Status { get; set; }
    public DateTime CreatedAtUtc { get; set; }
}
