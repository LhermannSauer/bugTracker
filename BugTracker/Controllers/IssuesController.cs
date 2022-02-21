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

            issue.Activities = _context.Activities.Where(a => a.IssueId == issue.Id).ToList();

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
                var issueInDb = await _context.Issues.FindAsync(issueForm.Id);

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

        public ActionResult Create()
        {
            var areas = _context.Areas.ToList();
            var priorities = _context.Priorities.ToList();
            var projects = _context.Projects.ToList();
            var newIssue = new IssuesFormViewModel
            {
                Id = 0,
                Areas = new SelectList(areas, "Id", "Name"),
                Priorities = new SelectList(priorities, "Id", "Name"),
                Projects = new SelectList(projects, "Id", "Name"),
            };

            return View("IssueForm", newIssue);
        }

        // GET: Issues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _context.Issues.SingleOrDefaultAsync(i => i.Id == id);
            var areas = _context.Areas.ToList();
            var priorities = _context.Priorities.ToList();
            var projects = _context.Projects.ToList();

            var viewModel = new IssuesFormViewModel
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


            if (issue == null)
            {
                return NotFound();
            }

            return View("IssueForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,CreatedDate,UpdatedDate,ResolvedDate")] Issue issue)
        {
            if (id != issue.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(issue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssueExists(issue.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(issue);
        }

        public async Task<IActionResult> Prioritize(int? id, int? priorityId)
        {
            var issue = await _context.Issues.FirstOrDefaultAsync(i => i.Id == id);
            issue.PriorityId = priorityId;

            _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
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

            return View(issue);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var issue = await _context.Issues.FindAsync(id);
            _context.Issues.Remove(issue);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IssueExists(int id)
        {
            return _context.Issues.Any(e => e.Id == id);
        }
    }
}
