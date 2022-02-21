using BugTracker.Models.viewModels;

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

        // Based on the issueId you will get the form to add an activity
        // only creator and developers can post, other should request access
        [HttpGet]
        public async Task<IActionResult> Edit(int issueId)
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

            issue.Activities = _context.Activities.Where(a => a.IssueId == issue.Id);

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
            if (!isDeveloper && !isCreator)
            {
                return Unauthorized("Please request access to this issue");
            }

            var priorities = await _context.Priorities.ToListAsync();
            var reassignToList = await _context.Developers.Include(d => d.User).ToListAsync();
            var statuses = await _context.Statuses.ToListAsync();


            var viewModel = new AcitivityFormViewModel
            {
                Priorities = new SelectList(priorities, "Id", "Name"),
                Statuses = new SelectList(statuses, "Id", "Name"),
                ReassignToList = reassignToList,
                IsDeveloper = isDeveloper,
                IssueId = issue.Id,
                Issue = issue,
            };

            Console.WriteLine(viewModel.ReassignToList);

            return View("ActivityForm", viewModel);
        }



    }
}
