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
    public class EventController : Controller
    {
        private readonly AlumniManagementContext _context;

        public EventController(AlumniManagementContext context)
        {
            _context = context;
        }

        // GET: Event
        public async Task<IActionResult> Index()
        {
              return _context.Events != null ? 
                          View(await _context.Events.ToListAsync()) :
                          Problem("Entity set 'AlumniManagementContext.Events'  is null.");
        }

        // GET: Event/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Event/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Event/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EventName,EventDescription,EventDate,EventTime,AttendeesList")] Event @event, IFormFile eventImage)
        {
            if (ModelState.IsValid)
            {
                if (eventImage != null && eventImage.Length > 0)
                {
                    // Save the image to a folder and store the file path
                    var filePath = Path.Combine("wwwroot", "images", "events", eventImage.FileName);

                    // Ensure the directory exists
                    var directoryPath = Path.GetDirectoryName(filePath);

                    if (directoryPath != null && !Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    // Save the file to the specified path
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await eventImage.CopyToAsync(fileStream);
                    }

                    // Store the file path in the Event model
                    @event.EventImagePath = "/images/events/" + eventImage.FileName; // Store the relative path
                }

                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }

        // GET: Event/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            return View(@event);
        }

        // POST: Event/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EventName,EventDescription,EventDate,EventTime,AttendeesList")] Event @event)
        {
            if (id != @event.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.Id))
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
            return View(@event);
        }

        // GET: Event/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Events == null)
            {
                return Problem("Entity set 'AlumniManagementContext.Events'  is null.");
            }
            var @event = await _context.Events.FindAsync(id);
            if (@event != null)
            {
                if (!string.IsNullOrEmpty(@event.EventImagePath))
                {
                    // Construct the full path to the image
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", @event.EventImagePath);

                    // Check if the file exists and delete it
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }

                _context.Events.Remove(@event);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
          return (_context.Events?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
