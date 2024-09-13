using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Presentation.Models;

namespace Presentation.Controllers
{
    public class TaxPeriodsController : Controller
    {
        private readonly TaxTestDbContext _context;

        public TaxPeriodsController(TaxTestDbContext context)
        {
            _context = context;
        }

        // GET: TaxPeriods
        public async Task<IActionResult> Index()
        {
            return View(await _context.TaxPeriods.ToListAsync());
        }

        // GET: TaxPeriods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taxPeriod = await _context.TaxPeriods
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taxPeriod == null)
            {
                return NotFound();
            }

            return View(taxPeriod);
        }

        // GET: TaxPeriods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TaxPeriods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartTime,EndTime,TaxAmmount")] TaxPeriod taxPeriod)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taxPeriod);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(taxPeriod);
        }

        // GET: TaxPeriods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taxPeriod = await _context.TaxPeriods.FindAsync(id);
            if (taxPeriod == null)
            {
                return NotFound();
            }
            return View(taxPeriod);
        }

        // POST: TaxPeriods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartTime,EndTime,TaxAmmount")] TaxPeriod taxPeriod)
        {
            if (id != taxPeriod.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taxPeriod);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaxPeriodExists(taxPeriod.Id))
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
            return View(taxPeriod);
        }

        // GET: TaxPeriods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taxPeriod = await _context.TaxPeriods
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taxPeriod == null)
            {
                return NotFound();
            }

            return View(taxPeriod);
        }

        // POST: TaxPeriods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taxPeriod = await _context.TaxPeriods.FindAsync(id);
            if (taxPeriod != null)
            {
                _context.TaxPeriods.Remove(taxPeriod);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaxPeriodExists(int id)
        {
            return _context.TaxPeriods.Any(e => e.Id == id);
        }
    }
}
