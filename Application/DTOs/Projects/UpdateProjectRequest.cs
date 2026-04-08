using System.ComponentModel.DataAnnotations;

namespace ProcessDock.Application.DTOs.Projects;

public class UpdateProjectRequest
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    [Required]
    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;
    [Required]
    public string Status { get; set; } = string.Empty;
}
