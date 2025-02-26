using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASS1.Models;
using Microsoft.AspNetCore.Authorization;

namespace ASS1.Controllers
{
    public class SystemAccountsController : Controller
    {
        private readonly FunewsManagementContext _context;

        public SystemAccountsController(FunewsManagementContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Admin,Staff")]
        // GET: SystemAccounts
        public async Task<IActionResult> Index()
        {
            return View(await _context.SystemAccounts.ToListAsync());
        }
        [Authorize(Roles = "Admin,Staff")]
        [HttpGet]
        [Route("SystemAccounts/ChangeStatus/{accountID}")]
        public async Task<IActionResult> ChangeStatus(short accountID)
        {
            var systemAccount = await _context.SystemAccounts.FindAsync(accountID);

            if (systemAccount == null)
            {
                return NotFound();
            }

            systemAccount.IsActive = !systemAccount.IsActive;
            _context.Update(systemAccount);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpGet]
        [Route("SystemAccounts/ChangeRole/{accountID}")]
        public async Task<IActionResult> ChangeRole(short accountID)
        {
            var systemAccount = await _context.SystemAccounts.FindAsync(accountID);

            if (systemAccount == null)
            {
                return NotFound();
            }

            if(systemAccount.AccountRole == 1)
            {
                systemAccount.AccountRole = 2;
            }
            else if (systemAccount.AccountRole == 2)
            {
                systemAccount.AccountRole = 1;
            }
            _context.Update(systemAccount);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin,Staff")]
        // GET: SystemAccounts/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemAccount = await _context.SystemAccounts
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (systemAccount == null)
            {
                return NotFound();
            }

            return View(systemAccount);
        }

        // GET: SystemAccounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SystemAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountId,AccountName,AccountEmail,AccountRole,AccountPassword")] SystemAccount systemAccount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(systemAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(systemAccount);
        }
        [Authorize(Roles = "Admin,Staff")]

        // GET: SystemAccounts/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemAccount = await _context.SystemAccounts.FindAsync(id);
            if (systemAccount == null)
            {
                return NotFound();
            }
            return View(systemAccount);
        }
        [Authorize(Roles = "Admin,Staff")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("AccountId,AccountName,AccountEmail,AccountPassword")] SystemAccount systemAccount)
        {
            if (id != systemAccount.AccountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Lấy dữ liệu cũ từ database
                    var existingAccount = await _context.SystemAccounts.FindAsync(id);
                    if (existingAccount == null)
                    {
                        return NotFound();
                    }

                    // Giữ nguyên Role cũ, chỉ cập nhật các trường được phép
                    existingAccount.AccountName = systemAccount.AccountName;
                    existingAccount.AccountEmail = systemAccount.AccountEmail;
                    existingAccount.AccountPassword = systemAccount.AccountPassword;

                    _context.Update(existingAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SystemAccountExists(systemAccount.AccountId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                // Chuyển hướng đến action "Profile" của controller "Staff"
                return RedirectToAction("Profile", "Staff", new { id = systemAccount.AccountId });
            }

            return View(systemAccount);
        }



        // GET: SystemAccounts/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemAccount = await _context.SystemAccounts
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (systemAccount == null)
            {
                return NotFound();
            }

            return View(systemAccount);
        }

        // POST: SystemAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var systemAccount = await _context.SystemAccounts.FindAsync(id);
            if (systemAccount != null)
            {
                _context.SystemAccounts.Remove(systemAccount);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SystemAccountExists(short id)
        {
            return _context.SystemAccounts.Any(e => e.AccountId == id);
        }
    }
}
