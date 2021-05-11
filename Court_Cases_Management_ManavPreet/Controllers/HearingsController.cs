using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Court_Cases_Management_ManavPreet.Data;
using Court_Cases_Management_ManavPreet.Models;
using Microsoft.AspNetCore.Authorization;

namespace Court_Cases_Management_ManavPreet.Controllers
{
    [Authorize]
    public class HearingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HearingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Hearings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Hearings.Include(h => h.Case);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Hearings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hearing = await _context.Hearings
                .Include(h => h.Case)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (hearing == null)
            {
                return NotFound();
            }

            return View(hearing);
        }

        // GET: Hearings/Create
        public IActionResult Create()
        {
            ViewData["CaseID"] = new SelectList(_context.Cases, "ID", "ID");
            return View();
        }

        // POST: Hearings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CaseID,CurrentDate,NextDate")] Hearing hearing)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hearing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CaseID"] = new SelectList(_context.Cases, "ID", "ID", hearing.CaseID);
            return View(hearing);
        }

        // GET: Hearings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hearing = await _context.Hearings.FindAsync(id);
            if (hearing == null)
            {
                return NotFound();
            }
            ViewData["CaseID"] = new SelectList(_context.Cases, "ID", "ID", hearing.CaseID);
            return View(hearing);
        }

        // POST: Hearings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CaseID,CurrentDate,NextDate")] Hearing hearing)
        {
            if (id != hearing.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hearing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HearingExists(hearing.ID))
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
            ViewData["CaseID"] = new SelectList(_context.Cases, "ID", "ID", hearing.CaseID);
            return View(hearing);
        }

        // GET: Hearings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hearing = await _context.Hearings
                .Include(h => h.Case)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (hearing == null)
            {
                return NotFound();
            }

            return View(hearing);
        }

        // POST: Hearings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hearing = await _context.Hearings.FindAsync(id);
            _context.Hearings.Remove(hearing);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HearingExists(int id)
        {
            return _context.Hearings.Any(e => e.ID == id);
        }
    }
}
