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
    public class JudgesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JudgesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Judges
        public async Task<IActionResult> Index()
        {
            return View(await _context.Judges.ToListAsync());
        }

        // GET: Judges/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var judge = await _context.Judges
                .FirstOrDefaultAsync(m => m.ID == id);
            if (judge == null)
            {
                return NotFound();
            }

            return View(judge);
        }

        // GET: Judges/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Judges/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Speciality,Age")] Judge judge)
        {
            if (ModelState.IsValid)
            {
                _context.Add(judge);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(judge);
        }

        // GET: Judges/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var judge = await _context.Judges.FindAsync(id);
            if (judge == null)
            {
                return NotFound();
            }
            return View(judge);
        }

        // POST: Judges/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Speciality,Age")] Judge judge)
        {
            if (id != judge.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(judge);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JudgeExists(judge.ID))
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
            return View(judge);
        }

        // GET: Judges/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var judge = await _context.Judges
                .FirstOrDefaultAsync(m => m.ID == id);
            if (judge == null)
            {
                return NotFound();
            }

            return View(judge);
        }

        // POST: Judges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var judge = await _context.Judges.FindAsync(id);
            _context.Judges.Remove(judge);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JudgeExists(int id)
        {
            return _context.Judges.Any(e => e.ID == id);
        }
    }
}
