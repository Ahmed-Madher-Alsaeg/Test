using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AlphaBeratung.Data
{
    public class AlphaContextInitialiser
    {
        private readonly ILogger<AlphaContextInitialiser> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AlphaContextInitialiser(ILogger<AlphaContextInitialiser> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                if (_context.Database.IsSqlServer())
                {
                    await _context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {

                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }

        }

        public async Task TrySeedAsync()
        {
            IdentityRole administratorRole = new IdentityRole("Administrator");
            IdentityRole VisitorRole = new IdentityRole("Visitor");
            if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await _roleManager.CreateAsync(administratorRole);
                await _roleManager.CreateAsync(VisitorRole);
            }
            // Default roles

            // Default users
            IdentityUser administrator = new IdentityUser
            {
                UserName = "al-hassan@alpha-beratung.com",
                Email = "al-hassan@alpha-beratung.com",
                PhoneNumber = ""

            };

            IdentityUser administrator1 = new IdentityUser
            {
                UserName = "sam@alpha-beratung.com",
                Email = "sam@alpha-beratung.com",
                PhoneNumber = ""

            };

            IdentityUser Admin = new IdentityUser
            {
                UserName = "admin@alpha-beratung.com",
                Email = "admin@alpha-beratung.com",
                PhoneNumber = "00967773987199"

            };

            if (_userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await _userManager.CreateAsync(administrator, "Al-Hassan-Alpha-Beratung1!");
                await _context.SaveChangesAsync();
                await _userManager.AddToRoleAsync(administrator, "Administrator");

            }

            await _context.SaveChangesAsync();

            if (_userManager.Users.All(u => u.UserName != administrator1.UserName))
            {
                await _userManager.CreateAsync(administrator1, "Sam-Alpha-Beratung1!");
                await _context.SaveChangesAsync();
                await _userManager.AddToRoleAsync(administrator1, "Administrator");
            }

            await _context.SaveChangesAsync();

            if (_userManager.Users.All(u => u.UserName != Admin.UserName))
            {
                await _userManager.CreateAsync(Admin, "Als-ahmedmadher123");
                await _context.SaveChangesAsync();
                await _userManager.AddToRoleAsync(Admin, "Administrator");
            }

            await _context.SaveChangesAsync();

        }
    }
}


//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;

//namespace AlphaBeratung.Data
//{
//    public class AlphaContextInitialiser
//    {
//        private readonly ILogger<AlphaContextInitialiser> _logger;
//        private readonly ApplicationDbContext _context;
//        private readonly UserManager<IdentityUser> _userManager;
//        public AlphaContextInitialiser(ILogger<AlphaContextInitialiser> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager)
//        {
//            _logger = logger;
//            _context = context;
//            _userManager = userManager;
//        }

//        public async Task InitialiseAsync()
//        {
//            try
//            {
//                if (_context.Database.IsSqlServer())
//                {
//                    await _context.Database.MigrateAsync();
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "An error occurred while initialising the database.");
//                throw;
//            }
//        }

//        public async Task SeedAsync()
//        {
//            try
//            {

//                await TrySeedAsync();
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "An error occurred while seeding the database.");
//                throw;
//            }
//        }

//        public async Task TrySeedAsync()
//        {
//            // Default roles

//            // Default users
//            IdentityUser administrator = new IdentityUser
//            {
//                UserName = "al-hassan@alpha-beratung.com",
//                Email = "al-hassan@alpha-beratung.com",
//                PhoneNumber = ""

//            };

//            IdentityUser administrator1 = new IdentityUser
//            {
//                UserName = "sam@alpha-beratung.com",
//                Email = "sam@alpha-beratung.com",
//                PhoneNumber = ""

//            };

//            IdentityUser Admin = new IdentityUser
//            {
//                UserName = "admin@alpha-beratung.com",
//                Email = "admin@alpha-beratung.com",
//                PhoneNumber = "00967773987199"

//            };

//            if (_userManager.Users.All(u => u.UserName != administrator.UserName))
//            {
//                await _userManager.CreateAsync(administrator, "Al-Hassan-Alpha-Beratung1!");
//            }

//            await _context.SaveChangesAsync();

//            if (_userManager.Users.All(u => u.UserName != administrator1.UserName))
//            {
//                await _userManager.CreateAsync(administrator1, "Sam-Alpha-Beratung1!");
//            }

//            await _context.SaveChangesAsync();

//            if (_userManager.Users.All(u => u.UserName != Admin.UserName))
//            {
//                await _userManager.CreateAsync(Admin, "Als-ahmedmadher123");
//            }

//            await _context.SaveChangesAsync();

//        }
//    }
//}
