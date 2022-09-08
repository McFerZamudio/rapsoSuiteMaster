using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using rapsoSuiteMaster.interfaces;
using rapsoSuiteMaster.Models;

namespace rapsoSuiteMaster.Controllers.Admin
{
    [Authorize(Roles = "AdministratorRapsoSuites")]
    public class NetUserRolesController : Controller
    {
        private readonly rapsoServer_DBContext _context;
        private readonly IrolesUser _IrolesUser;

        public NetUserRolesController(rapsoServer_DBContext context, IrolesUser IrolesUser)
        {
            _context = context;
            _IrolesUser = IrolesUser;
        }

        // GET: NetUserRoles
        public async Task<IActionResult> Index()
        {
            var rapsoServer_DBContext = _context.AspNetUserRoles.Include(a => a.Role).Include(a => a.User);
            return View(await rapsoServer_DBContext.ToListAsync());
        }

        // GET: NetUserRoles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.AspNetUserRoles == null)
            {
                return NotFound();
            }

            var aspNetUserRole = await _context.AspNetUserRoles
                .Include(a => a.Role)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (aspNetUserRole == null)
            {
                return NotFound();
            }

            return View(aspNetUserRole);
        }

        // GET: NetUserRoles/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.AspNetRoles, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "UserName");
            return View();
        }

        // POST: NetUserRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,RoleId")] AspNetUserRole aspNetUserRole)
        {
            if (ModelState.IsValid)
            {
                var _UserRole = _IrolesUser.setRegister(aspNetUserRole);
                var _res = await _IrolesUser.Create(_UserRole);

                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.AspNetRoles, "Id", "Id", aspNetUserRole.RoleId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", aspNetUserRole.UserId);
            return View(aspNetUserRole);
        }

        // GET: NetUserRoles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.AspNetUserRoles == null)
            {
                return NotFound();
            }

            var aspNetUserRole = await _context.AspNetUserRoles.FindAsync(id);
            if (aspNetUserRole == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.AspNetRoles, "Id", "Id", aspNetUserRole.RoleId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", aspNetUserRole.UserId);
            return View(aspNetUserRole);
        }

        // POST: NetUserRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserId,RoleId")] AspNetUserRole aspNetUserRole)
        {
            if (id != aspNetUserRole.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aspNetUserRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AspNetUserRoleExists(aspNetUserRole.UserId))
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
            ViewData["RoleId"] = new SelectList(_context.AspNetRoles, "Id", "Id", aspNetUserRole.RoleId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", aspNetUserRole.UserId);
            return View(aspNetUserRole);
        }

        // GET: NetUserRoles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.AspNetUserRoles == null)
            {
                return NotFound();
            }

            var aspNetUserRole = await _context.AspNetUserRoles
                .Include(a => a.Role)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (aspNetUserRole == null)
            {
                return NotFound();
            }

            return View(aspNetUserRole);
        }

        // POST: NetUserRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.AspNetUserRoles == null)
            {
                return Problem("Entity set 'rapsoServer_DBContext.AspNetUserRoles'  is null.");
            }
            var aspNetUserRole = await _context.AspNetUserRoles.FindAsync(id);
            if (aspNetUserRole != null)
            {
                _context.AspNetUserRoles.Remove(aspNetUserRole);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AspNetUserRoleExists(string id)
        {
            return (_context.AspNetUserRoles?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
