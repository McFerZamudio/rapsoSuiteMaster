using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace rapsoSuiteMaster.Controllers.client
{
    [Authorize(Roles = "userRapsoSuites,AdministratorRapsoSuites")]
    public class clientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
