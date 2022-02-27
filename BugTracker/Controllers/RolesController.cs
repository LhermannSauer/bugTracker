namespace BugTracker.Controllers
{
    public class RolesController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        public RolesController(RoleManager<IdentityRole> roleMgr)
        {
            roleManager = roleMgr;
        }

        public ViewResult Index() => View(roleManager.Roles);

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }

        [HttpGet]
        public IActionResult Edit(string? id)
        {
            var viewModel = new RoleFormViewModel();

            if (id == null)
                return View("RoleForm", viewModel);

            var role = roleManager.Roles.Where(r => r.Id == id).FirstOrDefault();

            if (role == null)
                return NotFound();

            viewModel.Name = role.Name;
            viewModel.Id = role.Id;
            return View("RoleForm", viewModel);

        }


        [HttpPost]
        public async Task<IActionResult> Save(RoleFormViewModel viewModel)
        {
            // post from an update form
            if (viewModel.Id != null)
            {
                //get the role from the DB
                var role = await roleManager.FindByIdAsync(viewModel.Id);

                if (role == null)
                {
                    return NotFound();
                }

                // set the new name
                await roleManager.SetRoleNameAsync(role, viewModel.Name);
                // this has to be updated, may be the equivalent to _context.saveChanges()
                IdentityResult _result = await roleManager.UpdateAsync(role);

                if (_result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(_result);
            }

            // Here it is a new Role.

            IdentityResult result = await roleManager.CreateAsync(new IdentityRole(viewModel.Name));

            if (result.Succeeded)
                return RedirectToAction("Index");
            else
                Errors(result);

            return View("RoleForm", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("Index");

                else
                    Errors(result);
            }
            else
                ModelState.AddModelError("", "No role found");
            return View("Index", roleManager.Roles);
        }
    }
}
