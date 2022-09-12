using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using rapsoSuiteMaster.Data;
using rapsoSuiteMaster.interfaces;
using rapsoSuiteMaster.localServices;
using rapsoSuiteMaster.Models;


namespace rapsoSuiteMaster.Controllers.Admin
{

    [Authorize(Roles = "AdministratorRapsoSuites")]
    public class NetUsersController : Controller
    {
        private readonly rapsoServer_DBContext _context;
        private readonly IConfiguration _conf;

        public NetUsersController(rapsoServer_DBContext context, IConfiguration conf)
        {
            _context = context;
            _conf = conf;
        }

        public async Task<IActionResult> Index()
        {
            return _context.AspNetUsers != null ?
            View(await _context.AspNetUsers.Include(X => X.AspNetUserRoles).ThenInclude(y => y.Role).ToListAsync()) :


            Problem("Entity set 'rapsoServer_DBContext.AspNetUsers'  is null.");
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.AspNetUsers == null)
            {
                return NotFound();
            }

            var aspNetUser = await _context.AspNetUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aspNetUser == null)
            {
                return NotFound();
            }

            return View(aspNetUser);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("email,phone,password, userName")] basicNetUserInfo _basicNetUserInfo)
        {
            taskResponse resultContent;
            if (ModelState.IsValid)
            {
                comunicationServices _comunicationServices = new(_conf);

                userRapsoSuites _usuario = new("administrator@rapsosuites.com", "Pn.14024284.Fm");
                StringContent _stringContentLogin = new StringContent(_usuario.jsonResponse());

                resultContent = _comunicationServices.callApiJson("getToken", _stringContentLogin, "/userApi/V1/getToken2", "", "").Result;

                if (resultContent.result == "Ok")
                {
                    StringContent _stringContent = new StringContent(_basicNetUserInfo.jsonResponse());

                    var resp = _comunicationServices.callApiJson("createUser", _stringContent, "/userApi/V1/createUser", "", resultContent.response).Result;
                    return Ok(resp);
                }
                else
                {
                    return Ok(resultContent);
                }
            }
            else
            {
                return Ok(new taskResponse("Create User / Model State", "Bad", ModelState.ToString()!).ToString()!);
            }
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.AspNetUsers == null)
            {
                return NotFound();
            }

            var aspNetUser = await _context.AspNetUsers.FindAsync(id);
            if (aspNetUser == null)
            {
                return NotFound();
            }
            return View(aspNetUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] AspNetUser aspNetUser)
        {
            if (id != aspNetUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aspNetUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AspNetUserExists(aspNetUser.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(aspNetUser);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.AspNetUsers == null)
            {
                return NotFound();
            }

            var aspNetUser = await _context.AspNetUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aspNetUser == null)
            {
                return NotFound();
            }

            return View(aspNetUser);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.AspNetUsers == null)
            {
                return Problem("Entity set 'rapsoServer_DBContext.AspNetUsers'  is null.");
            }
            var aspNetUser = await _context.AspNetUsers.FindAsync(id);
            if (aspNetUser != null)
            {
                _context.AspNetUsers.Remove(aspNetUser);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool AspNetUserExists(string id)
        {
            return (_context.AspNetUsers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
