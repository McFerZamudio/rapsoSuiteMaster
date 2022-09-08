using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using rapsoSuiteMaster.interfaces;
using rapsoSuiteMaster.Models;
using rapsoSuiteMaster.Services;
using System.Diagnostics;
using System.Security.Claims;

namespace rapsoSuiteMaster.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;

        private userServices _userServices;


        public HomeController(ILogger<HomeController> logger, userServices userServices, IConfiguration config, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _userServices = userServices;
            _config = config;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated == true)
            {
                var _user = await _userManager.FindByNameAsync(User.Identity.Name);
                HttpContext.Session.SetString("idLogin", _user.Id);

                if (User.IsInRole("AdministratorRapsoSuites") == true) return RedirectToAction("index", "admin");
                if (User.IsInRole("userRapsoSuites") == true) return RedirectToAction("index", "user");

            }








            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}