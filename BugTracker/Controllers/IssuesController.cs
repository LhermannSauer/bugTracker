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

            issue.Activities = _context.Activities.Where(a => a.IssueId == issue.Id);

            return View(issue);
        }

        // GET: Issues/Create
        public IActionResult Create()
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

        // POST: Issues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IssuesFormViewModel issueForm)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var userName = HttpContext.User.Identity.Name;
            var user = _context.Users.Single(u => u.UserName == userName);

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

            await _context.Issues.AddAsync(issue);
            await _context.SaveChangesAsync();


            return RedirectToAction("Index", "Issues");
        }

        // GET: Issues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _context.Issues.FindAsync(id);
            if (issue == null)
            {
                return NotFound();
            }
            return View(issue);
        }

        // POST: Issues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Issues/Delete/5
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

        // POST: Issues/Delete/5
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
