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
            // set its status to closed
            issue.StatusId = Status.Closed;

            //get the usser that sent the request.
            var user = await _context.Users.SingleAsync(u => u.UserName == HttpContext.User.Identity.Name);

            // Post an activity in the issue. 
            PostActivity(issue, user.Id, "", Status.Closed, true);

            //save changes
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
            PostActivity(issue, user.Id, model.Description, issue.StatusId);

            await _context.SaveChangesAsync();

            // return to the index of issues
            return Ok(issue);
        }

        public void PostActivity(Issue issue,
                                        string userId,
                                        string message,
                                        int? statusId,
                                        bool updatedStatus = false)
        {
            var activitiesController = new ActivitiesController(_context);

            activitiesController.AddActivityAsync(issue, userId, message, statusId, updatedStatus);
        }

        [HttpPost("addParticipant")]
        [AllowAnonymous]
        public async Task<IActionResult> AddParticipant([FromBody] AddParticipantModel participant)
        {
            if (participant == null)
                return BadRequest();

            var user = await _context.Users.SingleAsync(u => u.UserName.Equals(participant.UserName));
            var userIsParticipant = await _context.IssueParticipants.AnyAsync(p => p.ParticipantsId == user.Id &&
                                                                                p.IssueId == participant.IssueId);

            if (userIsParticipant)
            {
                return BadRequest("The user is already a participant");
            }

            var newParticipant = new IssueParticipant
            {
                IssueId = participant.IssueId,
                ParticipantsId = user.Id
            };

            await _context.IssueParticipants.AddAsync(newParticipant);
            _context.SaveChanges();

            return Ok(newParticipant);
        }
    }
}

