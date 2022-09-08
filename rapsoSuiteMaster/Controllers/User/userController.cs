using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rapsoSuiteMaster.interfaces;
using rapsoSuiteMaster.Models;
using rapsoSuiteMaster.Services;

namespace rapsoSuiteMaster.Controllers.User
{
    public class userController : Controller
    {
        private siteServices _siteServices;
        private readonly rapsoServer_DBContext _context;
        private string idLogin;

        public userController(rapsoServer_DBContext _context)
        {
            this._context = _context;
        }

        public IActionResult Index()
        {
            idLogin = HttpContext.Session.GetString("idLogin");
            _siteServices = new(_context, idLogin, User);

            return View(_siteServices.site);
        }

        public IActionResult updateSite(tbl_site tbl_Site)
        {
            idLogin = HttpContext.Session.GetString("idLogin");
            _siteServices = new(_context, tbl_Site);

            return RedirectToAction("index");
        }


    }
}
