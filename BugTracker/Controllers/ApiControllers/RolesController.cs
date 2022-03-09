namespace BugTracker.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {

        private RoleManager<IdentityRole> roleManager;
        private UserManager<AppUser> userManager;
        public RolesController(RoleManager<IdentityRole> roleMgr, UserManager<AppUser> userMgr)
        {
            roleManager = roleMgr;
            userManager = userMgr;
        }

        [Route("assignList")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AssignList([FromBody] AssignListViewModel model)
        {
            //receive a list of ids and add them to a specific role

            // get the users
            var usersToAdd = await userManager.Users.Where(u => model.UsersToAdd.Contains(u.Id)).ToListAsync();

            // iterate and add to role
            foreach (var user in usersToAdd)
                await userManager.AddToRoleAsync(user, model.RoleName);


            return RedirectToAction("Index");
        }

        [Route("unassignList")]
        [HttpPost]
        public async Task<IActionResult> UnassignList([FromBody] AssignListViewModel model)
        {
            var usersToAdd = await userManager.Users.Where(u => model.UsersToAdd.Contains(u.Id)).ToListAsync();

            // iterate and add to role
            foreach (var user in usersToAdd)
                await userManager.RemoveFromRoleAsync(user, model.RoleName);


            return RedirectToAction("Index");
        }
    }
}
