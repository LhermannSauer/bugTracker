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
        public async Task<IActionResult> GetIssues()
        {
            var issues = await _context.Issues
                            .Include(i => i.Status)
                            .Include(i => i.Area)
                            .Include(i => i.Priority)
                            .Include(i => i.Project)
                            .ToListAsync();

            return Ok(issues);

        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetIssue(int id)
        {
            var issue = await _context.Issues
                            .Include(i => i.Status)
                            .Include(i => i.Area)
                            .Include(i => i.Priority)
                            .Include(i => i.Project)
                            .SingleOrDefaultAsync(i => i.Id == id);

            if (issue == null)
            {
                return NotFound();
            }
            return Ok(issue);
        }


        // Issues should never be deletet just resolved or closed
        [HttpDelete("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> CloseIssue(int id)
        {
            var issue = await _context.Issues.SingleOrDefaultAsync(i => i.Id == id);

            if (issue == null)
            {
                return NotFound();
            }

            issue.StatusId = Status.Closed;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Issues");
        }


    }
}

