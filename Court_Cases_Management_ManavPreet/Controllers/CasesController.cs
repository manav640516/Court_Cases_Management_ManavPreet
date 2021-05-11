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
    public class CasesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CasesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cases
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Cases.Include(x => x.Judge).Include(x => x.Lawyer).Include(x => x.Party);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Cases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @case = await _context.Cases
                .Include(x => x.Judge)
                .Include(x => x.Lawyer)
                .Include(x => x.Party)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (@case == null)
            {
                return NotFound();
            }

            return View(@case);
        }

        // GET: Cases/Create
        public IActionResult Create()
        {
            ViewData["JudgeID"] = new SelectList(_context.Judges, "ID", "ID");
            ViewData["LawyerID"] = new SelectList(_context.Lawyers, "ID", "ID");
            ViewData["PartyID"] = new SelectList(_context.Parties, "ID", "ID");
            return View();
        }

        // POST: Cases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,JudgeID,LawyerID,PartyID,Name,Type")] Case @case)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@case);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["JudgeID"] = new SelectList(_context.Judges, "ID", "ID", @case.JudgeID);
            ViewData["LawyerID"] = new SelectList(_context.Lawyers, "ID", "ID", @case.LawyerID);
            ViewData["PartyID"] = new SelectList(_context.Parties, "ID", "ID", @case.PartyID);
            return View(@case);
        }

        // GET: Cases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @case = await _context.Cases.FindAsync(id);
            if (@case == null)
            {
                return NotFound();
            }
            ViewData["JudgeID"] = new SelectList(_context.Judges, "ID", "ID", @case.JudgeID);
            ViewData["LawyerID"] = new SelectList(_context.Lawyers, "ID", "ID", @case.LawyerID);
            ViewData["PartyID"] = new SelectList(_context.Parties, "ID", "ID", @case.PartyID);
            return View(@case);
        }

        // POST: Cases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,JudgeID,LawyerID,PartyID,Name,Type")] Case @case)
        {
            if (id != @case.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@case);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CaseExists(@case.ID))
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
            ViewData["JudgeID"] = new SelectList(_context.Judges, "ID", "ID", @case.JudgeID);
            ViewData["LawyerID"] = new SelectList(_context.Lawyers, "ID", "ID", @case.LawyerID);
            ViewData["PartyID"] = new SelectList(_context.Parties, "ID", "ID", @case.PartyID);
            return View(@case);
        }

        // GET: Cases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @case = await _context.Cases
                .Include(x => x.Judge)
                .Include(x => x.Lawyer)
                .Include(x => x.Party)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (@case == null)
            {
                return NotFound();
            }

            return View(@case);
        }

        // POST: Cases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @case = await _context.Cases.FindAsync(id);
            _context.Cases.Remove(@case);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CaseExists(int id)
        {
            return _context.Cases.Any(e => e.ID == id);
        }
    }
}
