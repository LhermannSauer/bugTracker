#nullable disable

namespace BugTracker.Controllers
{
    public class PrioritiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PrioritiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Priorities
        public async Task<IActionResult> Index()
        {
            return View(await _context.Priorities.ToListAsync());
        }

        // GET: Priorities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            Priority priority;
            if (id == null)
            {
                priority = new Priority();
                return View("PriorityForm", priority);
            }

            priority = await _context.Priorities.FindAsync(id);
            if (priority == null)
            {
                return NotFound();
            }
            return View("PriorityForm", priority);
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(int id, [Bind("Id,Name,RespondWithin,ResolveWithin")] Priority priority)
        {
            if (id == 0)
            {
                await _context.AddAsync(priority);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            if (id != priority.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(priority);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PriorityExists(priority.Id))
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
            return RedirectToAction("Index");
        }


        private bool PriorityExists(int id)
        {
            return _context.Priorities.Any(e => e.Id == id);
        }
    }
}
