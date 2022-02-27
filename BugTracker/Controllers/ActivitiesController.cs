namespace BugTracker.Controllers
{
    [Authorize]
    public class ActivitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActivitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }

        // Based on the issueId you will get the form to add an activity
        // only creator, admin, and developers can post, other should request access
        [HttpGet]
        public async Task<IActionResult> Edit(int issueId, bool partialView = false)
        {
            var isCreator = false;
            var isDeveloper = false;
            // get the issue
            var issue = await _context.Issues
                            .Include(i => i.Area)
                            .Include(i => i.AssignedTo)
                            .Include(i => i.Creator)
                            .Include(i => i.Status)
                            .Include(i => i.Priority)
                            .Include(i => i.Project)
                            .SingleAsync(i => i.Id == issueId);

            issue.Activities = _context.Activities.ToList().Where((a => a.IssueId == issue.Id));

            //Get the user ID for permits
            var userId = _context.Users.Single(u => u.UserName == HttpContext.User.Identity.Name).Id;

            // Check if id exists in Developers Table
            isDeveloper = await _context.Developers.AnyAsync(d => d.User.Id == userId);

            // If not a developer, check if it is the creator of the issue
            if (!isDeveloper)
            {
                isCreator = issue.CreatorId == userId;
            }

            // if not a dev or the creator, not authorized to add. Until implement issue sharing
            if (!isDeveloper && !isCreator && !(HttpContext.User.IsInRole("CanPostInAllIssues")))
            {
                return Unauthorized("Please request access to this issue");
            }

            var priorities = await _context.Priorities.ToListAsync();
            var reassignToList = await _context.Developers.Include(d => d.User).ToListAsync();
            var statuses = await _context.Statuses.ToListAsync();


            var viewModel = new ActivityFormViewModel
            {
                Priorities = new SelectList(priorities, "Id", "Name"),
                Statuses = new SelectList(statuses, "Id", "Name"),
                ReassignToList = reassignToList,
                IsDeveloper = isDeveloper,
                IssueId = issue.Id,
                Issue = issue,
            };

            return View("ActivityForm", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Save(ActivityFormViewModel activity)
        {
            if (!ModelState.IsValid)
            {
                var priorities = await _context.Priorities.ToListAsync();
                var reassignToList = await _context.Developers.Include(d => d.User).ToListAsync();
                var statuses = await _context.Statuses.ToListAsync();
                var issue = await _context.Issues
                                .Include(i => i.Area)
                                .Include(i => i.AssignedTo)
                                .Include(i => i.Creator)
                                .Include(i => i.Status)
                                .Include(i => i.Priority)
                                .Include(i => i.Project)
                                .SingleAsync(i => i.Id == activity.IssueId);
                issue.Activities = _context.Activities.ToList().Where((a => a.IssueId == issue.Id));


                var viewModel = new ActivityFormViewModel
                {
                    Priorities = new SelectList(priorities, "Id", "Name"),
                    Statuses = new SelectList(statuses, "Id", "Name"),
                    ReassignToList = reassignToList,
                    ReassignToId = activity.ReassignToId,
                    ReassignedIssue = activity.ReassignedIssue,
                    IsDeveloper = activity.IsDeveloper,
                    IssueId = activity.IssueId,
                    Issue = issue
                };
                return View("ActivityForm", viewModel);

            }
            // Get the user object that submitted the form
            var user = await _context.Users.SingleAsync(u => u.UserName == HttpContext.User.Identity.Name);

            activity.UserId = user.Id;

            // get referenced issue
            var issueInDb = _context.Issues.SingleOrDefault(i => i.Id == activity.IssueId);
            if (issueInDb == null)
            {
                return BadRequest();
            }

            // Create the new activity and map the data from the form
            var newActivity = new Activity
            {
                Description = activity.Description,
                IssueId = activity.IssueId,
                UserId = activity.UserId,
                UpdatedStatus = activity.UpdatedStatus,
                StatusId = activity.StatusId,
                ReassignedIssue = activity.ReassignedIssue,
                DateCreated = DateTime.Now,
                ResolvedIssue = activity.StatusId == Status.Resolved,
            };



            // If the activity updated the status, change it in the issue
            if (newActivity.UpdatedStatus.Value)
            {
                issueInDb.StatusId = activity.StatusId;
            }
            // If the reassigned the status, update it in the issue
            if (newActivity.ReassignedIssue.Value)
            {
                issueInDb.AssignedToId = activity.ReassignToId;
                newActivity.ReassignedToId = activity.ReassignToId;
            }

            // in any case, save the date the issue was updated
            issueInDb.UpdatedDate = DateTime.Now;

            await _context.Activities.AddAsync(newActivity);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Issues", new { id = issueInDb.Id });
        }

        // static method used by others controllers in order to add activities based on their methods' requirements. 
        // For instance, when you send a close request to an issue, an activity should be added in order to show who and when did that.
        public async void AddActivityAsync(Issue issue,
                                        string userId,
                                        string message,
                                        int? statusId,
                                        bool updatedStatus = false)
        {

            var newActivity = new Activity
            {
                DateCreated = DateTime.Now,
                Description = message,
                IssueId = issue.Id,
                UserId = userId,
                UpdatedStatus = updatedStatus,
                ReassignedIssue = false,
                ResolvedIssue = false,
            };

            newActivity.StatusId = updatedStatus ? statusId.Value : issue.StatusId;


            await _context.Activities.AddAsync(newActivity);
        }

    }
}
