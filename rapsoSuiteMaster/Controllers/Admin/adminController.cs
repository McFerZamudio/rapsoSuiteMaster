using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace rapsoSuiteMaster.Controllers.Admin
{
    [Authorize(Roles = "AdministratorRapsoSuites")]
    public class adminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
