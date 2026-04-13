using System.ComponentModel.DataAnnotations;
using ProcessDock.Domain.Enums;


namespace ProcessDock.Application.DTOs.WorkItems;

public class UpdateWorkItemRequest
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(500)]
    public string? Description { get; set; }

    [Required]
    public WorkItemStatus? Status { get; set; }
}
