using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using ProcessDock.Models;


namespace ProcessDock.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
    {
        
    }

    public DbSet<Workspace> Workspaces { get; set;}
    public DbSet<Project> Projects{ get; set;}

    
}
