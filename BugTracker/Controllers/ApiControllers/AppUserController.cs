namespace BugTracker.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private UserManager<AppUser> userManager;
        public AppUserController(RoleManager<IdentityRole> roleMgr, UserManager<AppUser> userMgr)
        {
            userManager = userMgr;
        }


        public async Task<IActionResult> GetUsers([FromQuery] string? filter)
        {
            var users = await userManager.Users.ToListAsync();

            if (filter != null)
                users = users.Where(u =>
                        (
                            u.FirstName.ToLower().Contains(filter) ||
                            u.UserName.ToLower().Contains(filter) ||
                            u.Email.ToLower().Contains(filter) ||
                            u.LastName.ToLower().Contains(filter)

                        )).ToList();


            if (users == null)
                return NoContent();

            return Ok(users);
        }

    }
}
