using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASS1.Models;

namespace ASS1.Controllers
{
    public class SystemAccountsController : Controller
    {
        private readonly FunewsManagementContext _context;

        public SystemAccountsController(FunewsManagementContext context)
        {
            _context = context;
        }

        // GET: SystemAccounts
        public async Task<IActionResult> Index()
        {
            return View(await _context.SystemAccounts.ToListAsync());
        }

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

        // POST: SystemAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    _context.Update(systemAccount);
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
                // Chuyển hướng đến action "Profile" của controller "Staff" sau khi cập nhật thành công
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
