using ProcessDock.Domain.Enums;


namespace ProcessDock.Domain.Entities;

public class Project
{
    public int Id { get; set;}
    public string Name { get; set;} = string.Empty;
    public string Description { get; set;} = string.Empty;
    public ProjectStatus Status { get; set;} = ProjectStatus.Draft;
    public DateTime CreatedAtUtc { get; set;} = DateTime.UtcNow;
    public ICollection<WorkItem> WorkItems{ get; set; } = new List<WorkItem>();
}
