using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProcessDock.Data;
using ProcessDock.Models;

namespace ProcessDock.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkspacesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WorkspacesController(ApplicationDbContext context)
        {
            _context = context;
        }

        

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Workspace>>> GetWorkspacesAsync()
        {
            var workspacesList = await _context.Workspaces.ToListAsync();
        
            return Ok(workspacesList);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Workspace>> GetWorkspaceById(int id)
        {
            var workspace = await _context.Workspaces.FindAsync(id);

            if (workspace == null)
                return NotFound();

            return Ok(workspace);
            
        }

        [HttpPost]
        public async Task<ActionResult<Workspace>> AddWorkspace(Workspace workspace)
        {
            _context.Workspaces.Add(workspace);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetWorkspaceById), new { id = workspace.Id}, workspace);
        }

        

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateWorkspace(int id, Workspace workspaceToUpdate)
        {
            if (id != workspaceToUpdate.Id)
                return BadRequest("Id mismatch");
            
            var workspace = await _context.Workspaces.FindAsync(id);
            
            if(workspace == null)
                return NotFound();
            
            workspace.Name = workspaceToUpdate.Name;
            workspace.Description = workspaceToUpdate.Description;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteWorkspace(int id)
        {

            var workspace = await _context.Workspaces.FindAsync(id);

            if (workspace == null)
                return NotFound();

            _context.Workspaces.Remove(workspace);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}


