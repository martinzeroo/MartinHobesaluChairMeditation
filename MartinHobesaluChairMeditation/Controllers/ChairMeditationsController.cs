using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MartinHobesaluChairMeditation.Data;
using MartinHobesaluChairMeditation.Models;

namespace MartinHobesaluChairMeditation.Controllers
{
    public class ChairMeditationsController : Controller
    {

        private readonly ApplicationDbContext _context;

        public ChairMeditationsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> AddPrice([Bind("Id,Tone,OrderAmount,CompleteAmount,Price")] ChairMeditation chairMeditation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chairMeditation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chairMeditation);
        }

        // GET: ChairMeditations
        public async Task<IActionResult> Index()
        {
              return View(await _context.ChairMeditation.ToListAsync());
        }

        // GET: ChairMeditations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ChairMeditation == null)
            {
                return NotFound();
            }

            var chairMeditation = await _context.ChairMeditation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chairMeditation == null)
            {
                return NotFound();
            }

            return View(chairMeditation);
        }

        // GET: ChairMeditations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ChairMeditations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tone,OrderAmount,CompleteAmount,Price")] ChairMeditation chairMeditation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chairMeditation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chairMeditation);
        }

        // GET: ChairMeditations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ChairMeditation == null)
            {
                return NotFound();
            }

            var chairMeditation = await _context.ChairMeditation.FindAsync(id);
            if (chairMeditation == null)
            {
                return NotFound();
            }
            return View(chairMeditation);
        }

        // POST: ChairMeditations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tone,OrderAmount,CompleteAmount,Price")] ChairMeditation chairMeditation)
        {
            if (id != chairMeditation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chairMeditation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChairMeditationExists(chairMeditation.Id))
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
            return View(chairMeditation);
        }

        // GET: ChairMeditations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ChairMeditation == null)
            {
                return NotFound();
            }

            var chairMeditation = await _context.ChairMeditation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chairMeditation == null)
            {
                return NotFound();
            }

            return View(chairMeditation);
        }

        // POST: ChairMeditations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ChairMeditation == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ChairMeditation'  is null.");
            }
            var chairMeditation = await _context.ChairMeditation.FindAsync(id);
            if (chairMeditation != null)
            {
                _context.ChairMeditation.Remove(chairMeditation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChairMeditationExists(int id)
        {
          return _context.ChairMeditation.Any(e => e.Id == id);
        }
    }
}
