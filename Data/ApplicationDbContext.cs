using Microsoft.EntityFrameworkCore;
using ProcessDock.Models;


namespace ProcessDock.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
    {
        
    }

    public DbSet<Workspace> Workspaces { get; set;}

    
}
