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
    public class CourtRoomsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CourtRoomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CourtRooms
        public async Task<IActionResult> Index()
        {
            return View(await _context.CourtRooms.ToListAsync());
        }

        // GET: CourtRooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courtRoom = await _context.CourtRooms
                .FirstOrDefaultAsync(m => m.ID == id);
            if (courtRoom == null)
            {
                return NotFound();
            }

            return View(courtRoom);
        }

        // GET: CourtRooms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CourtRooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,RoomNumber")] CourtRoom courtRoom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courtRoom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(courtRoom);
        }

        // GET: CourtRooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courtRoom = await _context.CourtRooms.FindAsync(id);
            if (courtRoom == null)
            {
                return NotFound();
            }
            return View(courtRoom);
        }

        // POST: CourtRooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,RoomNumber")] CourtRoom courtRoom)
        {
            if (id != courtRoom.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courtRoom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourtRoomExists(courtRoom.ID))
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
            return View(courtRoom);
        }

        // GET: CourtRooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courtRoom = await _context.CourtRooms
                .FirstOrDefaultAsync(m => m.ID == id);
            if (courtRoom == null)
            {
                return NotFound();
            }

            return View(courtRoom);
        }

        // POST: CourtRooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courtRoom = await _context.CourtRooms.FindAsync(id);
            _context.CourtRooms.Remove(courtRoom);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourtRoomExists(int id)
        {
            return _context.CourtRooms.Any(e => e.ID == id);
        }
    }
}
