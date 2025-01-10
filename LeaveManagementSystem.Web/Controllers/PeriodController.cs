using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeaveManagementSystem.Common.Static;

namespace LeaveManagementSystem.Application.Controllers
{
    [Authorize(Roles = Roles.Administrator)]
    public class PeriodController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PeriodController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Period
        public async Task<IActionResult> Index()
        {
            return View(await _context.Periods.ToListAsync());
        }

        // GET: Period/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var period = await _context.Periods
                .FirstOrDefaultAsync(m => m.Id == id);
            if (period == null)
            {
                return NotFound();
            }

            return View(period);
        }

        // GET: Period/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Period/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,StartDate,EndDate,Id")] Period period)
        {
            if (ModelState.IsValid)
            {
                period.Id = Guid.NewGuid();
                _context.Add(period);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(period);
        }

        // GET: Period/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var period = await _context.Periods.FindAsync(id);
            if (period == null)
            {
                return NotFound();
            }
            return View(period);
        }

        // POST: Period/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,StartDate,EndDate,Id")] Period period)
        {
            if (id != period.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(period);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeriodExists(period.Id))
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
            return View(period);
        }

        // GET: Period/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var period = await _context.Periods
                .FirstOrDefaultAsync(m => m.Id == id);
            if (period == null)
            {
                return NotFound();
            }

            return View(period);
        }

        // POST: Period/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var period = await _context.Periods.FindAsync(id);
            if (period != null)
            {
                _context.Periods.Remove(period);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeriodExists(Guid id)
        {
            return _context.Periods.Any(e => e.Id == id);
        }
    }
}
