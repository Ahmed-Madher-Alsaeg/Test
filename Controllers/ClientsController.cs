using AlphaBeratung.Data;
using AlphaBeratung.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlphaBeratung.Controllers
{
    [Authorize]
    public class ClientsController : Controller
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ApplicationDbContext context;

        public ClientsController(SignInManager<IdentityUser> signInManager, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            this.signInManager = signInManager;
            this.context = context;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            var clients = context.clients.ToList();
            return View(clients);
        }
        public IActionResult Details(int id)
        {
            var client = context.clients.Find(id);
            if(client == null)
            {
                TempData["Message"] = "Client was not found, please try again later";
                return RedirectToAction("Index");
            }
            else
            {
                return View(client);
            }
        }
        
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Employees()
        {
            var usersWithRoles = new List<UserWithRoles>();
            var emp = await userManager.Users.ToListAsync();
            foreach(var user in emp)
            {
                var roles = await userManager.GetRolesAsync(user);

                var userWithRoles = new UserWithRoles
                {
                    Id = user.Id,
                    Email = user.Email,
                    Username = user.UserName,
                    RoleName = roles.FirstOrDefault()
                };
                usersWithRoles.Add(userWithRoles);
            }
            return View(usersWithRoles);
        }
    }
}
