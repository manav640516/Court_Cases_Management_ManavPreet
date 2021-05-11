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
    public class LawyersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LawyersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Lawyers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Lawyers.ToListAsync());
        }

        // GET: Lawyers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lawyer = await _context.Lawyers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (lawyer == null)
            {
                return NotFound();
            }

            return View(lawyer);
        }

        // GET: Lawyers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lawyers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Speciality,Age,MobileNumber")] Lawyer lawyer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lawyer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lawyer);
        }

        // GET: Lawyers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lawyer = await _context.Lawyers.FindAsync(id);
            if (lawyer == null)
            {
                return NotFound();
            }
            return View(lawyer);
        }

        // POST: Lawyers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Speciality,Age,MobileNumber")] Lawyer lawyer)
        {
            if (id != lawyer.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lawyer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LawyerExists(lawyer.ID))
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
            return View(lawyer);
        }

        // GET: Lawyers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lawyer = await _context.Lawyers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (lawyer == null)
            {
                return NotFound();
            }

            return View(lawyer);
        }

        // POST: Lawyers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lawyer = await _context.Lawyers.FindAsync(id);
            _context.Lawyers.Remove(lawyer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LawyerExists(int id)
        {
            return _context.Lawyers.Any(e => e.ID == id);
        }
    }
}
