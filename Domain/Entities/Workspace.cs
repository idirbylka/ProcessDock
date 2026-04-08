using System.ComponentModel.DataAnnotations;

namespace ProcessDock.Domain.Entities;

public class Workspace
{
    public int Id { get; set; } 
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Description { get; set; } =string.Empty;
    public DateTime Created { get; set; } = DateTime.UtcNow; 
}
