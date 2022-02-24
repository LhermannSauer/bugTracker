using BugTracker.Models.viewModels;

namespace BugTracker.Controllers
{
    [Authorize]
    public class IssuesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IssuesController(ApplicationDbContext context)
        {
            _context = context;
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }

        // GET: Issues
        public async Task<IActionResult> Index()
        {

            var issues = await _context.Issues
                            .Include(i => i.Status)
                            .Include(i => i.Priority)
                            .Include(i => i.Project)
                            .ToListAsync();

            return View(issues);
        }

        // GET: Issues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _context.Issues
                .Include(i => i.Project)
                .Include(i => i.Creator)
                .Include(i => i.Area)
                .Include(i => i.Priority)
                .Include(i => i.Status)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (issue == null)
            {
                return NotFound();
            }

            issue.Activities = _context.Activities.Include(a => a.User).Include(a => a.Status).Include(a => a.ReassignedTo.User).Where(a => a.IssueId == issue.Id).ToList();

            return View(issue);
        }

        public IActionResult IssueForm()
        {
            var areas = _context.Areas.ToList();
            var priorities = _context.Priorities.ToList();
            var projects = _context.Projects.ToList();

            var model = new IssuesFormViewModel();

            model.Areas = new SelectList(areas, "Id", "Name");
            model.Priorities = new SelectList(priorities, "Id", "Name");
            model.Projects = new SelectList(projects, "Id", "Name"); ;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(IssuesFormViewModel issueForm)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            // If id is 0, then it is a new issue
            if (issueForm.Id == 0)
            {
                // get data of the user that submitted the form.
                var userName = HttpContext.User.Identity.Name;
                var user = _context.Users.Single(u => u.UserName == userName);

                // Create the new object
                var issue = new Issue()
                {
                    Title = issueForm.Title,
                    Description = issueForm.Description,
                    PriorityId = issueForm.PriorityId,
                    AreaId = issueForm.AreaId,
                    ProjectId = issueForm.ProjectId,
                    StatusId = Status.Open,
                    CreatedDate = DateTime.Now,
                    CreatorId = user.Id,
                };

                // add it to the db
                await _context.Issues.AddAsync(issue);
            }
            else // The id already exists in db
            {
                //Get the db object
                var issueInDb = await _context.Issues.SingleAsync(i => i.Id == issueForm.Id);

                //update fields
                issueInDb.Title = issueForm.Title;
                issueInDb.Description = issueForm.Description;
                issueInDb.PriorityId = issueForm.PriorityId;
                issueInDb.AreaId = issueForm.AreaId;
                issueInDb.ProjectId = issueForm.ProjectId;
                // Set updated date to now
                issueInDb.UpdatedDate = DateTime.Now;
                ;

            }
            await _context.SaveChangesAsync();


            return RedirectToAction("Index", "Issues");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            IssuesFormViewModel viewModel;

            if (id != null)
            {
                if (!await _context.Issues.AnyAsync(i => i.Id == id))
                {
                    return NotFound();
                }
            }

            var areas = _context.Areas.ToList();
            var priorities = _context.Priorities.ToList();
            var projects = _context.Projects.ToList();

            if (id == null)
            {
                viewModel = new IssuesFormViewModel
                {
                    Id = 0,
                    Areas = new SelectList(areas, "Id", "Name"),
                    Priorities = new SelectList(priorities, "Id", "Name"),
                    Projects = new SelectList(projects, "Id", "Name"),
                }
                ;
            }
            else
            {
                var issue = await _context.Issues.SingleAsync(i => i.Id == id);

                viewModel = new IssuesFormViewModel
                {
                    Id = issue.Id,
                    Title = issue.Title,
                    Description = issue.Description,
                    PriorityId = issue.PriorityId,
                    AreaId = issue.AreaId,
                    ProjectId = issue.ProjectId,
                    Areas = new SelectList(areas, "Id", "Name"),
                    Priorities = new SelectList(priorities, "Id", "Name"),
                    Projects = new SelectList(projects, "Id", "Name"),
                };
            }

            return View("IssueForm", viewModel);
        }


        public async Task<IActionResult> Prioritize(int? id, int? priorityId)
        {
            var issue = await _context.Issues.FirstOrDefaultAsync(i => i.Id == id);
            issue.PriorityId = priorityId;

            _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Close(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _context.Issues
                .FirstOrDefaultAsync(m => m.Id == id);
            if (issue == null)
            {
                return NotFound();
            }

            issue.StatusId = Status.Closed;

            return RedirectToAction("Index");
        }



        private bool IssueExists(int id)
        {
            return _context.Issues.Any(e => e.Id == id);
        }
    }
}
