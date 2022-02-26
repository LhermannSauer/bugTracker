using BugTracker.Models.viewModels;

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

        [HttpPost("prioritize/{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> PrioritizeIssue(int id, PrioritizeFormViewModel model)
        {
            // get the issue with the ID 
            var issue = await _context.Issues.SingleAsync(i => i.Id == id);
            if (issue == null)
            {
                return NotFound();
            }

            //set the issue to the priorityid
            issue.PriorityId = model.PriorityId;

            //get the user
            var user = await _context.Users.SingleAsync(u => u.UserName == HttpContext.User.Identity.Name);

            // post an activity with the message as the description, getting user, and setting the message to "xxxx changed the priority to xxx"
            var activity = new Activity
            {
                DateCreated = DateTime.Now,
                Description = model.Description,
                IssueId = id,
                UserId = user.Id,
                UpdatedStatus = false,
                ReassignedIssue = false,
                ResolvedIssue = false,
            };
            await _context.Activities.AddAsync(activity);
            await _context.SaveChangesAsync();

            // return to the index of issues
            return Ok(activity);
        }
    }
}

