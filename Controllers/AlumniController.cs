using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AlumniManagement.Entities;

namespace AlumniManagement.Controllers
{
    public class AlumniController : Controller
    {
        private readonly AlumniManagementContext _context;

        public AlumniController(AlumniManagementContext context)
        {
            _context = context;
        }

        // GET: Alumni
        public async Task<IActionResult> Index()
        {
              return _context.Alumni != null ? 
                          View(await _context.Alumni.ToListAsync()) :
                          Problem("Entity set 'AlumniManagementContext.Alumni'  is null.");
        }

        // GET: Alumni/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Alumni == null)
            {
                return NotFound();
            }

            var alumnus = await _context.Alumni
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alumnus == null)
            {
                return NotFound();
            }

            return View(alumnus);
        }

        // GET: Alumni/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Alumni/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Username,Password,CourseGraduated,YearGraduated,WorkingStatus,CurrentWork")] Alumnus alumnus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(alumnus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(alumnus);
        }

        // GET: Alumni/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Alumni == null)
            {
                return NotFound();
            }

            var alumnus = await _context.Alumni.FindAsync(id);
            if (alumnus == null)
            {
                return NotFound();
            }
            return View(alumnus);
        }

        // POST: Alumni/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Username,Password,CourseGraduated,YearGraduated,WorkingStatus,CurrentWork")] Alumnus alumnus)
        {
            if (id != alumnus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alumnus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlumnusExists(alumnus.Id))
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
            return View(alumnus);
        }

        // GET: Alumni/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Alumni == null)
            {
                return NotFound();
            }

            var alumnus = await _context.Alumni
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alumnus == null)
            {
                return NotFound();
            }

            return View(alumnus);
        }

        // POST: Alumni/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Alumni == null)
            {
                return Problem("Entity set 'AlumniManagementContext.Alumni'  is null.");
            }
            var alumnus = await _context.Alumni.FindAsync(id);
            if (alumnus != null)
            {
                _context.Alumni.Remove(alumnus);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlumnusExists(int id)
        {
          return (_context.Alumni?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
