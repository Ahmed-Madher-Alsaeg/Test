using AlphaBeratung.Data;
using AlphaBeratung.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AlphaBeratung.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact()
        { 
            return View(); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(Clients clients)
        {
            try {
                if (!ModelState.IsValid)
                {
                    TempData["Message"] = "Please fill in all fields in order to send a message ";
                    foreach (var error in ModelState.Values )
                    {
                        ModelState.AddModelError(string.Empty,error.ToString());
                    }
                    return View(clients);
                }
                else
                {
                    clients.Sent_Time=DateTime.Now;
                   await  context.clients.AddAsync(clients);
                   await  context.SaveChangesAsync();
                    TempData["Message"] = "Your message has been sent successfully, you will hear from us soon.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                TempData["Message"] = "Something wrong happened, please try later ";
                return View(clients);
            }
        }
        public IActionResult TermsOfService()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}