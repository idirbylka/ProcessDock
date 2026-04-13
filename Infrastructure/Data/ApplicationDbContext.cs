using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using ProcessDock.Domain.Entities;


namespace ProcessDock.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
    {
        
    }

    public DbSet<Workspace> Workspaces { get; set;}
    public DbSet<Project> Projects { get; set; }
    public DbSet<WorkItem> WorkItems { get; set; }

    
}
