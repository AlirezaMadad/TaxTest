using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.Models;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxPeriodsApiController : ControllerBase
    {
        private readonly TaxTestDbContext _context;

        public TaxPeriodsApiController(TaxTestDbContext context)
        {
            _context = context;
        }

        // GET: api/TaxPeriodsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaxPeriod>>> GetTaxPeriods()
        {
            return await _context.TaxPeriods.ToListAsync();
        }

        // GET: api/TaxPeriodsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaxPeriod>> GetTaxPeriod(int id)
        {
            var taxPeriod = await _context.TaxPeriods.FindAsync(id);

            if (taxPeriod == null)
            {
                return NotFound();
            }

            return taxPeriod;
        }

        // PUT: api/TaxPeriodsApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaxPeriod(int id, TaxPeriod taxPeriod)
        {
            if (id != taxPeriod.Id)
            {
                return BadRequest();
            }

            _context.Entry(taxPeriod).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaxPeriodExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TaxPeriodsApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaxPeriod>> PostTaxPeriod(TaxPeriod taxPeriod)
        {
            _context.TaxPeriods.Add(taxPeriod);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTaxPeriod", new { id = taxPeriod.Id }, taxPeriod);
        }

        // DELETE: api/TaxPeriodsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaxPeriod(int id)
        {
            var taxPeriod = await _context.TaxPeriods.FindAsync(id);
            if (taxPeriod == null)
            {
                return NotFound();
            }

            _context.TaxPeriods.Remove(taxPeriod);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaxPeriodExists(int id)
        {
            return _context.TaxPeriods.Any(e => e.Id == id);
        }
    }
}
