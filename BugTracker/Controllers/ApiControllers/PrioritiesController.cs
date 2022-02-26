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

        public async Task<IActionResult> GetPriorities()
        {
            var priorities = _context.Priorities.ToListAsync();

            return Ok(priorities);
        }

    }
}