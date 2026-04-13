using System.ComponentModel.DataAnnotations;

namespace ProcessDock.Application.DTOs.WorkItems;

public class CreateWorkItemRequest
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(500)]
    public string? Description { get; set;}
}
