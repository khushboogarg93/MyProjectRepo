using AMVACChemical.Entities;
using AMVACChemical.Entities.TrackAbout;
using AMVACChemical.Entities.TrackAbout.Identity;
using AMVACChemical.ViewModels.TrackAbout.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AMVACChemical.Controllers.Web
{
    public class TrackAboutController : Controller
    {
        private readonly AMVAC_IdentityContext _trackAboutIdentityContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public TrackAboutController(AMVAC_IdentityContext trackAboutIdentityContext,
            UserManager<ApplicationUser> userManager)
        {
            _trackAboutIdentityContext = trackAboutIdentityContext;
            _userManager = userManager;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult About()
        {
            // It means if you try to login without authentication then return t login screen
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login","TrackAbout");
            }
        }

        [Authorize(Roles ="Admin")] // direct used for authentication and authorization
        public IActionResult Member()
        {
            // It means if you try to login without authentication then return t login screen
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult UserRolesList()
        {
            //  In context class we have all table because of identitydbcontext
            // Get all list of users from aspnetusers table
            var users = _trackAboutIdentityContext.Users.OrderBy(u => u.Email).ToList();

            // step 2: calling model for binding the list of users in model
            var vm = new UserRoleManagementVM
            {
                Users = users
            };
            // var claims = User.Claims;

           // var user = await _userManager.FindByNameAsync(User.Identity.Name);
           //var claims =await  _userManager.GetClaimsAsync(User)
            return View(vm);
        }

        [HttpGet]
        public IActionResult AddNewRoleToUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddNewRoleToUser(AddNewRoleVM model)
        {
            _userManager.AddToRolesAsync(model.User,null);
            return RedirectToAction("UserRolesList");
        }
    }
}
