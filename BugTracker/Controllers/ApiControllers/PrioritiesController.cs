namespace BugTracker.Controllers.ApiControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrioritiesController : ControllerBase
    {
        ApplicationDbContext _context;

        public PrioritiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetPriorities()
        {
            var priorities = await _context.Priorities.ToListAsync();

            return Ok(priorities);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePriority(int id)
        {
            var priority = await _context.Priorities.SingleOrDefaultAsync(x => x.Id == id);
            if (priority == null)
            {
                return NotFound();
            }

            _context.Priorities.Remove(priority);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}