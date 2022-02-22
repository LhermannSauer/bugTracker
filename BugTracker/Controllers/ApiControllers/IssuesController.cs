namespace BugTracker.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssuesController : ControllerBase
    {
        ApplicationDbContext _context;

        public IssuesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Issues()
        {
            var issues = await _context.Issues
                            .Include(i => i.Status)
                            .Include(i => i.Area)
                            .Include(i => i.Priority)
                            .Include(i => i.Project)
                            .ToListAsync();

            return Ok(issues);

        }

        [HttpPost("/Close/{id:int}")]
        public async Task<IActionResult> CloseIssue(int id)
        {
            var issue = await _context.Issues.SingleOrDefaultAsync(i => i.Id == id);

            if (issue == null)
            {
                return NotFound();
            }

            issue.StatusId = Status.Closed;
            await _context.SaveChangesAsync();

            return Ok(issue);
        }


    }
}

