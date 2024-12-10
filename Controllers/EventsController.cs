using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proiect_MPA_EB_Cantor_Andrei.Data;
using Proiect_MPA_EB_Cantor_Andrei.Models;

namespace Proiect_MPA_EB_Cantor_Andrei.Controllers
{
    public class EventsController : Controller
    {
        private readonly Proiect_MPA_EB_Cantor_AndreiContext _context;

        public EventsController(Proiect_MPA_EB_Cantor_AndreiContext context)
        {
            _context = context;
        }

        // GET: Events
        public async Task<IActionResult> Index(string searchQuery, string sortOrder, int pageNumber=1, int pageSize=5)
        {
            var sortOrderList = new List<SelectListItem>

    {
        new SelectListItem { Value = "", Text = "Name (A-Z)" },
        new SelectListItem { Value = "name_desc", Text = "Name (Z-A)" },
        new SelectListItem { Value = "Date", Text = "Date (Ascending)" },
        new SelectListItem { Value = "date_desc", Text = "Date (Descending)" },
        new SelectListItem { Value = "Capacity", Text = "Capacity (Ascending)" },
        new SelectListItem { Value = "capacity_desc", Text = "Capacity (Descending)" }
    };

            var eventsQuery = from e in _context.Event.Include(e => e.Category)
                              select e;

            if (!string.IsNullOrEmpty(searchQuery))
            {
                eventsQuery = eventsQuery.Where(e => e.Name.Contains(searchQuery));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    eventsQuery = eventsQuery.OrderByDescending(e => e.Name);
                    break;
                case "Date":
                    eventsQuery = eventsQuery.OrderBy(e => e.Date);
                    break;
                case "date_desc":
                    eventsQuery = eventsQuery.OrderByDescending(e => e.Date);
                    break;
                case "Capacity":
                    eventsQuery = eventsQuery.OrderBy(e => e.Capacity);
                    break;
                case "capacity_desc":
                    eventsQuery = eventsQuery.OrderByDescending(e => e.Capacity);
                    break;
                default:
                    eventsQuery = eventsQuery.OrderBy(e => e.Name);
                    break;
            }

            var totalCount = await eventsQuery.CountAsync();
            var events = await eventsQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var viewModel = new EventIndexViewModel
            {
                SearchQuery = searchQuery,
                SortOrder = sortOrder,
                SortOrderList = sortOrderList,
                Events = events,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                PageSize = pageSize
            };

            return View(viewModel);
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event
                .Include(e => e.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Events/Create

        [Authorize(Policy = "AdminOnly")]
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Name");
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Date,Capacity,AvailableSeats,CategoryId")] Event @event)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Name", @event.CategoryId);
            return View(@event);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Name", @event.CategoryId);
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Date,Capacity,AvailableSeats,CategoryId")] Event @event)
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
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Name", @event.CategoryId);
            return View(@event);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event
                .Include(e => e.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Event.FindAsync(id);
            if (@event != null)
            {
                _context.Event.Remove(@event);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Event.Any(e => e.Id == id);
        }
    }
}
